# E-Commerce APIs (.NET)

E-Commerce backend APIs built with **ASP.NET Core (.NET 9)**, **EF Core**, **ASP.NET Identity**, and **JWT** authentication. The API also supports **image upload** and serves uploaded files under `/Files`.

## Solution structure

- `ECommerce/ECommerce.API`: ASP.NET Core Web API (controllers, auth, OpenAPI/Scalar)
- `ECommerce/ECommerce.BLL`: business logic layer
- `ECommerce/ECommerce.DAL`: data access layer (EF Core, repositories, DbContext)
- `ECommerce/ECommerce.Common`: results/helpers

## Prerequisites

- **.NET SDK 9.x**
- **SQL Server**

## Run the API

From the repo root:

```bash
cd ECommerce/ECommerce.API
dotnet restore
dotnet run
```

Default dev URLs (from `launchSettings.json`):

- HTTP: `http://localhost:5002`
- HTTPS: `https://localhost:7091`

## API documentation (OpenAPI / Scalar)

In **Development**, the app exposes OpenAPI + Scalar UI. If you run the HTTPS profile, it opens Scalar by default.

## Auth (JWT) + seeded admin

On startup, the app seeds roles and an admin user:

- **Admin email**: `admin@ecommerce.com`
- **Admin password**: `Admin123abc`

Use `POST /api/Auth/login` to obtain a JWT, then send it as:

- `Authorization: Bearer <token>`

## Endpoints (high level)

### Auth

- `POST /api/Auth/register`
- `POST /api/Auth/login`

### Products

- `GET /api/Products`
- `GET /api/Products/paged` (anonymous allowed)
- `GET /api/Products/{id}`
- `POST /api/Products` (**Admin**)
- `PUT /api/Products/{id}` (**Admin**)
- `PATCH /api/Products/{id}` (**Admin**)
- `DELETE /api/Products/{id}` (**Admin**)
- `POST /api/Products/{id}/image` (**Admin**, multipart/form-data)

### Categories

- `GET /api/Categories`
- `GET /api/Categories/{id}`
- `POST /api/Categories` (**Admin**)
- `PUT /api/Categories/{id}` (**Admin**)
- `PATCH /api/Categories/{id}` (**Admin**)
- `DELETE /api/Categories/{id}` (**Admin**)
- `POST /api/Categories/{id}/image` (**Admin**, multipart/form-data)

### Cart (JWT required)

- `GET /api/Carts`
- `POST /api/Carts/items`
- `PUT /api/Carts/items`
- `DELETE /api/Carts/items/{productId}`
- `DELETE /api/Carts/Clear`

### Orders (JWT required)

- `POST /api/Orders` (create order from cart)
- `GET /api/Orders/{orderId}`
- `GET /api/Orders/user/{userId}` (**Admin**)

### Images (JWT + Admin)

- `POST /api/Images/upload` (**Admin**, multipart/form-data)

## Uploaded files

Uploaded images are stored in `ECommerce/ECommerce.API/Files` and served publicly at:

- `GET /Files/<filename>`
