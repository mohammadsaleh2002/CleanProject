# MyStore: A Clean Architecture E-commerce Project

This is an ASP.NET Core 8 e-commerce web application built as a university project. The primary focus is the practical implementation of **Clean Architecture** to create a maintainable, scalable, and testable application.

This solution separates concerns into four distinct layers (Domain, Application, Infrastructure, Presentation) and provides both a user-facing **MVC Website** and a **Web API** for backend services.

---

## 🛠️ Architecture & Technologies

This project is built using a modern .NET stack and industry-standard design patterns.

* **Framework:** ASP.NET Core 8
* **Database:** Entity Framework Core 8 (Code-First) & SQL Server
* **Core Architecture:**
    * **Clean Architecture:** (Domain, Application, Infrastructure, Presentation layers)
    * **Repository Pattern (Generic):** `IBaseRepository<T>`
    * **Unit of Work Pattern:** `IUnitOfWork` for transactional database operations.
* **Security (Custom Built):**
    * **Manual Cookie Authentication** (not using ASP.NET Core Identity)
    * **BCrypt.Net-Next:** For secure password hashing and verification.
    * `IUserService` for handling all authentication logic.
* **Logging (Completed Item #2):**
    * **Serilog:** Configured for structured logging to a daily rolling file (`/Logs/log-.txt`).
* **Interfaces:**
    * **ASP.NET Core MVC:** User-facing website built with Razor Views.
    * **ASP.NET Core Web API:** RESTful API for backend operations.
    * **Swagger/Swashbuckle:** For API documentation and testing.

---

## ✅ Implemented Features

As of now, the complete project infrastructure and the following core features are fully functional:

### 1. Architectural Foundation
* **4-Layer Structure:** All four projects are fully configured with correct dependencies.
* **Repository & Unit of Work:** Complete implementation for managing all 5 database entities (`Product`, `Category`, `User`, `Order`, `OrderItem`).
* **Dependency Injection (DI):** All services (`IUnitOfWork`, `IProductService`, `IUserService`) are correctly registered in `Program.cs`.

### 2. Web API (Service Layer)
* **Full CRUD APIs:** Endpoints for `Products`, `Categories`, `Users`, and `Orders`.
* **Swagger UI:** Integrated and functional for testing all API endpoints.

### 3. MVC Website (UI Layer)
* **Product Management:** Users can view (`Index.cshtml`) and create (`Create.cshtml`) products.
* **Modern Delete:** The product list uses **JavaScript (Fetch)** to call the `DELETE /api/Products` endpoint directly, providing an AJAX-based delete (no page refresh).
* **Dynamic Layout:** The main site layout (`_Layout.cshtml`) is complete.

### 4. Custom Authentication System
* **Secure Registration:** `POST /Account/Register` uses `IUserService` to securely hash passwords with **BCrypt** and save new users.
* **Secure Login:** `POST /Account/Login` uses `IUserService` to verify credentials with `BCrypt.Verify` and issues a secure **Authentication Cookie**.
* **Logout:** A secure `POST /Account/Logout` endpoint clears the cookie.
* **Dynamic UI:** The main layout header dynamically changes to show "Login/Register" (for guests) or "Hello [Username] / Logout" (for logged-in users).

### 5. Structured Logging
* **Serilog** is fully configured (as per professor's list item #2). It logs all application events (Info, Warning, Error) to a text file, with settings managed in `appsettings.json`.

---

## 🚀 Future Roadmap (Professor's List)

The following items are planned to complete the project requirements:

1.  **Global Exception Handling (Item #1):**
    * **To-Do:** Implement a custom middleware to catch all unhandled exceptions. This will provide standardized JSON error responses for the API and a user-friendly error page for the MVC site.

2.  **API Security (JWT) (Item #3):**
    * **To-Do:** Implement **JWT (JSON Web Token)** authentication for the Web API.
    * **Strategy:** Configure the application to accept *both* Cookie (for MVC) and JWT Bearer Token (for API) authentication schemes.

3.  **Refine Clean Architecture (DTOs) (Item #4):**
    * **To-Do:** This is the most critical refactoring task. We will stop passing Domain Entities directly to the Presentation layer.
    * **Plan:**
        1.  Create **DTOs (Data Transfer Objects)** (e.g., `ProductDto`, `CreateProductDto`) in the `Application` layer.
        2.  Install and configure **AutoMapper** to map Entities to DTOs.
        3.  Refactor all Services (`IProductService`) and Controllers (API & MVC) to consume and return DTOs.

4.  **Upscale Business Logic (Item #5):**
    * **To-Do:** Implement more complex, real-world business scenarios.
    * **Example:** Create a new `IOrderService` to manage the "Place Order" process, which will involve checking product inventory, calculating the final price from the database (not user input), and saving the `Order` and `OrderItems` in a single transaction using `IUnitOfWork`.

---

