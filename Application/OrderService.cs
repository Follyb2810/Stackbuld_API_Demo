using Microsoft.EntityFrameworkCore;
public interface IOrderService
{
    Task<PlaceOrderResponse> PlaceOrderAsync(PlaceOrderRequest request);
}

public class OrderService : IOrderService
{
    private readonly AppDbContext _db;
    public OrderService(AppDbContext db) { _db = db; }

    public async Task<PlaceOrderResponse> PlaceOrderAsync(PlaceOrderRequest request)
    {
        if (request.Items == null || !request.Items.Any())
            return new PlaceOrderResponse(false, "Order must contain at least one item.", null);

        // Normalize quantities > 0
        if (request.Items.Any(i => i.Quantity <= 0))
            return new PlaceOrderResponse(false, "Quantities must be > 0.", null);

        using var tx = await _db.Database.BeginTransactionAsync(); // default isolation is fine when we use conditional update
        try
        {
            // Load product data for price calculation and existence
            var productIds = request.Items.Select(i => i.ProductId).Distinct().ToList();
            var products = await _db.Products.Where(p => productIds.Contains(p.Id)).ToListAsync();

            // Check all products exist
            var missing = productIds.Except(products.Select(p => p.Id)).ToList();
            if (missing.Any())
            {
                await tx.RollbackAsync();
                return new PlaceOrderResponse(false, $"Products not found: {string.Join(',', missing)}", null);
            }

            // For each item, attempt to decrement stock atomically.
            foreach (var item in request.Items)
            {
                // Use raw SQL to decrement where stock sufficient
                var rows = await _db.Database.ExecuteSqlInterpolatedAsync($@"
                    UPDATE Products
                    SET StockQuantity = StockQuantity - {item.Quantity}
                    WHERE Id = {item.ProductId} AND StockQuantity >= {item.Quantity};
                ");
                if (rows == 0)
                {
                    // Not enough stock for this product
                    await tx.RollbackAsync();
                    return new PlaceOrderResponse(false, $"Insufficient stock for product id {item.ProductId}", null);
                }
            }

            // Create order and items using current prices
            var order = new Order();
            foreach (var item in request.Items)
            {
                var product = products.Single(p => p.Id == item.ProductId);
                var oi = new OrderItem
                {
                    ProductId = product.Id,
                    Quantity = item.Quantity,
                    UnitPrice = product.Price
                };
                order.Items.Add(oi);
                order.TotalAmount += product.Price * item.Quantity;
            }

            _db.Orders.Add(order);
            await _db.SaveChangesAsync();

            await tx.CommitAsync();
            return new PlaceOrderResponse(true, "Order placed successfully", order.Id);
        }
        catch (Exception ex)
        {
            await tx.RollbackAsync();
            // log exception in real app
            return new PlaceOrderResponse(false, "An error occurred while placing the order: " + ex.Message, null);
        }
    }
}
