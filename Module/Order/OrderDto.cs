using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Stackbuld_API.Module.Order
{
    public record OrderItemDto(
        int ProductId,
        int Quantity
    );

    public record OrderDto(
        List<OrderItemDto> Items
    );
    public record OrderItemResponseDto(
    int ProductId,
    string ProductName,
    int Quantity,
    decimal UnitPrice
);

    public record OrderResponseDto(
        int Id,
        DateTime CreatedAt,
        List<OrderItemResponseDto> Items,
        decimal TotalAmount
    );
}
