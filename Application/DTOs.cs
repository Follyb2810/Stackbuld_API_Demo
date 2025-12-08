public record CreateProductDto(string Name, string? Description, decimal Price, int StockQuantity);
public record UpdateProductDto(string? Name, string? Description, decimal? Price, int? StockQuantity);
public record ProductDto(int Id, string Name, string? Description, decimal Price, int StockQuantity);

public record PlaceOrderItemDto(int ProductId, int Quantity);
public record PlaceOrderRequest(List<PlaceOrderItemDto> Items);
public record PlaceOrderResponse(bool Success, string Message, int? OrderId);
