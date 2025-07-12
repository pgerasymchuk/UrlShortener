# 🔗 ASP.NET Core URL Shortener API

This is a **clean architecture-based ASP.NET Core Web API** for shortening URLs.

## 📦 Features

- 🔐 **Authentication & Authorization** using ASP.NET Core Identity and JWT
- 👤 **Role-based access**: Admin, User
- ✂️ **URL shortening** (auto-generates short codes)
- 👥 **User management** (register, login)
- 🗂 **Own URL listing** (User/Admin)
- 🚀 **Short URL redirection** for guests and logged-in users
- 🧪 **Unit-tested repositories** using xUnit and EF Core InMemory

## 🧱 Tech Stack

- **Backend**: ASP.NET Core 8 Web API
- **Architecture**: Clean Architecture (Domain, Application, Infrastructure, WebAPI, Host)
- **Authentication**: ASP.NET Core Identity + JWT
- **Persistence**: EF Core with InMemoryDatabase (switchable to SQL Server)
- **MediatR**: CQRS for commands/queries
- **AutoMapper**: DTO to Entity mapping
- **Testing**: xUnit, EF Core InMemory

## ⚙️ How to Run

### 📦 Prerequisites
- .NET 8 SDK installed

### 🔧 Run

```bash
dotnet build
dotnet run --project Host
```

The backend will be available at: http://localhost:5087/

### 📌 TODO

- **Reorganize controller routes**
- **Add unit tests for services, integration tests**
- **Switch to real database**
- **Dockerize the application**