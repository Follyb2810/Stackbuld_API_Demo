# **Stackbuld API Demo – Product Catalog & Order Processing**

A production-grade **ASP.NET Core Web API** designed to manage a product catalog, process orders, and ensure strong data integrity with concurrency protection to prevent overselling.

This project follows clean architecture principles and demonstrates how to build a scalable, maintainable API suitable for enterprise environments.

---

## **Features**

### **Product Catalog**

- Full CRUD operations
- Product fields:

  - `Id`
  - `Name`
  - `Description`
  - `Price`
  - `StockQuantity`

### **Order Processing**

- Users can place orders with one or many product items
- Validates stock availability before order creation
- Processes stock reduction atomically using database transactions
- Prevents overselling during concurrent requests

### **Data Integrity & Concurrency Handling**

- Uses EF Core transactions

---

## **Architecture**

This project follows **Modular Monolith** principles:

```
/Contract  ➢ repositories
/Infrastructure    → EF Core, database,
/Module
    /feature ⁃
      IfeatureRepository
      featureRepository
      featureService
      IfeatureService
      featureController
      featureDto
      featureModel
```

### **Key Practices**

- Repository pattern
- Dependency Injection
- DTO-based request/response models
- Separation of concerns
- Logging & error handling middleware
- Transaction boundary around order creation

---

## ⚙️ **Tech Stack**

| Component           | Choice                                      |
| ------------------- | ------------------------------------------- |
| **Language**        | C# (.NET 8)                                 |
| **Framework**       | ASP.NET Core Web API                        |
| **ORM**             | Entity Framework Core                       |
| **Database**        | SQLite (lightweight for demo, easy to run)  |
| **Auth (optional)** | JWT Bearer (added for production readiness) |
| **Migrations**      | dotnet-ef                                   |

---

## 📌 **Assumptions**

- Orders are simple and do not require customer accounts for this demo
- Stock is decreased only after successful order creation
- Concurrency issues are handled at the database level using transactions
- SQLite is used for convenience; in a real production system PostgreSQL/MSSQL is recommended
- All endpoints are synchronous for clarity, but async/await is used internally

---

## 🛠 **Setup Instructions**

Clone the repo:

```bash
git clone https://github.com/Follyb2810/Stackbuld_API_Demo.git
cd Stackbuld_API_Demo
```

### **Install Dependencies**

```bash
dotnet restore
```

### **Database Setup**

```bash
dotnet tool install --global dotnet-ef
dotnet ef migrations add InitialCreate
dotnet ef database update
```

### **Run the API**

```bash
dotnet run
```

API will be available at:

```
http://localhost:5211
```

### **Swagger Documentation**

```
/swagger/v1/swagger.json
/swagger/index.html
```

---

## 📦 **Packages Installed**

```bash
dotnet new webapi -n Stackbuld_API

dotnet add package Microsoft.EntityFrameworkCore --version 8.0.22
dotnet add package Microsoft.EntityFrameworkCore.Tools --version 8.0.22
dotnet add package Microsoft.AspNetCore.Authentication.JwtBearer --version 8.0.22
dotnet add package Microsoft.IdentityModel.Tokens --version 8.15.0
dotnet add package Microsoft.EntityFrameworkCore.Sqlite --version 8.0.22

dotnet tool install --global dotnet-ef
```

## 📁 **Project Goals**

- Demonstrate clean, maintainable architecture
- Provide strong handling of concurrency and transactions
- Show real-world patterns used in enterprise .NET development

---

## 🙌 **Author**

Babatunde Yusuf Folorunsho
Built for the **Stackbuld Technical Assessment**
