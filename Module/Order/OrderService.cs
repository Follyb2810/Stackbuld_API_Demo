using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Stackbuld_API.Module.Product;
using Stackbuld_API.Infrastructure;

namespace Stackbuld_API.Module.Order;

public class OrderService : IOrderService
{
    private readonly IOrderRepository _orderRepository;
    private readonly IProductRepository _productRepository;
    private readonly AppDbContext _db;

    public OrderService(IOrderRepository orderRepository, IProductRepository productRepository, AppDbContext db)
    {
        _orderRepository = orderRepository;
        _productRepository = productRepository;
        _db = db;
    }

    public async Task<OrderResponseDto> CreateAsync(OrderDto dto)
    {
        await using var transaction = await _db.Database.BeginTransactionAsync();

        var order = new OrderModel();

        foreach (var item in dto.Items)
        {
            var product = await _productRepository.GetByIdAsync(item.ProductId);

            if (product == null)
                throw new Exception($"Product ID {item.ProductId} does not exist.");

            if (product.StockQuantity < item.Quantity)
                throw new Exception($"Product '{product.Name}' does not have enough stock.");

            product.StockQuantity -= item.Quantity;
            _productRepository.Update(product);

            order.Items.Add(new OrderItemModel
            {
                ProductId = product.Id,
                Quantity = item.Quantity,
                UnitPrice = product.Price
            });
        }

        order.TotalAmount = order.Items.Sum(i => i.UnitPrice * i.Quantity);

        await _orderRepository.CreateAsync(order);
        await _orderRepository.SaveChangesAsync();
        await transaction.CommitAsync();

        var responseItems = order.Items.Select(i =>
        {
            var product = dto.Items.First(p => p.ProductId == i.ProductId);
            return new OrderItemResponseDto(
                i.ProductId,
                product.ProductId.ToString(),
                i.Quantity,
                i.UnitPrice
            );
        }).ToList();

        return new OrderResponseDto(
            order.Id,
            order.CreatedAt,
            responseItems,
            order.TotalAmount
        );
    }

    public async Task<OrderResponseDto?> GetByIdAsync(int id)
    {
        var order = await _orderRepository.GetByIdAsync(id);
        if (order == null) return null;

        var items = order.Items.Select(i => new OrderItemResponseDto(
            i.ProductId,
            i.Product?.Name ?? "",
            i.Quantity,
            i.UnitPrice
        )).ToList();

        return new OrderResponseDto(
            order.Id,
            order.CreatedAt,
            items,
            order.TotalAmount
        );
    }

    public async Task<IEnumerable<OrderResponseDto>> GetAllAsync()
    {
        var orders = await _orderRepository.GetAllAsync();

        return orders.Select(o => new OrderResponseDto(
            o.Id,
            o.CreatedAt,
            o.Items.Select(i => new OrderItemResponseDto(
                i.ProductId,
                i.Product?.Name ?? "",
                i.Quantity,
                i.UnitPrice
            )).ToList(),
            o.TotalAmount
        ));
    }

    public async Task<OrderResponseDto?> UpdateAsync(int id, OrderDto dto)
    {
        // For simplicity, prevent updating stock in update
        var order = await _orderRepository.GetByIdAsync(id);
        if (order == null) return null;

        order.Items.Clear();
        foreach (var item in dto.Items)
        {
            var product = await _productRepository.GetByIdAsync(item.ProductId);
            if (product == null)
                throw new Exception($"Product ID {item.ProductId} does not exist.");

            order.Items.Add(new OrderItemModel
            {
                ProductId = product.Id,
                Quantity = item.Quantity,
                UnitPrice = product.Price
            });
        }

        order.TotalAmount = order.Items.Sum(i => i.UnitPrice * i.Quantity);

        _orderRepository.Update(order);
        await _orderRepository.SaveChangesAsync();

        var responseItems = order.Items.Select(i => new OrderItemResponseDto(
            i.ProductId,
            i.Product?.Name ?? "",
            i.Quantity,
            i.UnitPrice
        )).ToList();

        return new OrderResponseDto(
            order.Id,
            order.CreatedAt,
            responseItems,
            order.TotalAmount
        );
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var order = await _orderRepository.GetByIdAsync(id);
        if (order == null) return false;

        _orderRepository.Delete(order);
        await _orderRepository.SaveChangesAsync();
        return true;
    }
}
