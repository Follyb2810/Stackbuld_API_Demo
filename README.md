```bash
dotenet new webapi -n Stackbuld_API
dotnet add package Microsoft.EntityFrameworkCore --version 8.0.22
dotnet add package Microsoft.EntityFrameworkCore.Tools --version 8.0.22
dotnet add package Microsoft.AspNetCore.Authentication.JwtBearer --version 8.0.22
dotnet add package Microsoft.IdentityModel.Tokens --version 8.15.0
dotnet add package Microsoft.EntityFrameworkCore.Sqlite --version 8.0.22

dotnet tool install --global dotnet-ef
dotnet ef migrations add InitialCreate
dotnet ef database update
dotnet run
```

```c#
var tasks = new List<Task<PlaceOrderResponse>>();
for (int i = 0; i < 50; i++) {
    tasks.Add(client.PostAsJsonAsync("/api/orders", new PlaceOrderRequest { Items = new List<PlaceOrderItemDto> { new(1,1) } }));
}
await Task.WhenAll(tasks);

```

# Stackbuld_API_Demo
