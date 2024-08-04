# Order Management System

## Overview

This project demonstrates a microservices-based **Order Management System** implemented using .NET, gRPC, RabbitMQ, and Clean Architecture principles. The system consists of two microservices:

- **Order Service**: Manages order creation, retrieval, and updates.
- **Inventory Service**: Manages inventory levels and provides stock information.

## Architecture

The system follows the Clean Architecture pattern, which ensures a maintainable and scalable codebase. The architecture includes the following layers:

- **Presentation Layer**: Defines how the system interacts with external requests and responses.
  - **Order.Rest.AspNetCore**: This project defines the gRPC services and handles incoming requests via gRPC endpoints.

- **Application Layer**: Implements use cases and business logic.
  - **Order.Application**: Implements the use cases and business logic.
  - **Order.Application.Contracts**: Defines contracts or interfaces used by the Application Layer, providing a clear separation between the application logic and other layers.

- **Domain Layer**: Contains core business entities and rules.
  - **Order.Data.Contracts**: Contains domain-related contracts and core business rules.

- **Infrastructure Layer**: Manages data persistence and communication.
  - **Order.Data.Pgsql**: Manages data persistence, specifically for PostgreSQL.
  - **Order.Data.Contracts**: Defines the contracts related to data access.
  - **RabbitMQ Communication**: Handles messaging and integration with RabbitMQ.

## Solution Structure

The solution file `Order.sln` includes the following projects:

1. **Order.Host**
   - **Path**: `Order.Host\Order.Host.csproj`
   - **Description**: The entry point of the application, typically containing the `Startup` class and the configuration for the ASP.NET Core application.

2. **Order.Data.Contracts**
   - **Path**: `Order.Data.Contracts\Order.Data.Contracts.csproj`
   - **Description**: Contains data access interfaces and contracts that define the data layer's interactions with the rest of the application.

3. **Order.Data.Pgsql**
   - **Path**: `Order.Data.Pgsql\Order.Data.Pgsql.csproj`
   - **Description**: Implements the data access layer using PostgreSQL. It includes the concrete implementations of the data access interfaces.

4. **Order.Application**
   - **Path**: `Order.Application\Order.Application.csproj`
   - **Description**: Contains application services, business logic, and use cases. It interacts with the domain layer and provides functionality to the presentation layer.

5. **Order.Application.Contracts**
   - **Path**: `Order.Application.Contracts\Order.Application.Contracts.csproj`
   - **Description**: Defines contracts for application services and use cases, providing a layer of abstraction between the application and the domain.

6. **Order.Rest.AspNetCore**
   - **Path**: `Order.Rest.AspNetCore\Order.Rest.AspNetCore.csproj`
   - **Description**: The ASP.NET Core web API project that serves as the presentation layer for the application. It handles HTTP requests and responses.

7. **Order.Rest.Contracts**
   - **Path**: `Order.Rest.Contracts\Order.Rest.Contracts.csproj`
   - **Description**: Contains contracts and models used for communication between the API and clients.

## Microservices

### Order Service

- **Responsibilities**: 
  - Create orders.
  - Retrieve order details.

- **Endpoints**:
  1. **CreateOrder** (gRPC)
     - **Request**: Contains order details such as product ID, quantity, and customer ID.
     - **Response**: Returns order ID and status.
  2. **GetOrderDetails** (gRPC)
     - **Request**: Order ID.
     - **Response**: Returns order details including status, items, and customer information.

### Inventory Service

- **Responsibilities**:
  - Manage inventory levels.
  - Provide stock information.

- **Endpoints**:
  1. **UpdateStock** (gRPC)
     - **Request**: Contains product ID and new stock quantity.
     - **Response**: Returns success or failure status.
  2. **GetStockInfo** (gRPC)
     - **Request**: Product ID.
     - **Response**: Returns current stock level and product details.

## Communication Flow

1. **Order Creation Flow**:
   - The **Order Service** receives a `CreateOrder` request via gRPC.
   - After creating the order, it sends a message to the **Inventory Service** via RabbitMQ to update stock levels based on the order items.
   - The **Inventory Service** processes the message, updates stock levels, and optionally sends a confirmation back to the **Order Service**.
   - The **Order Service** completes the order creation process and may notify the user of the successful order.

2. **Stock Check Flow**:
   - Before creating an order, the **Order Service** calls the **Inventory Service**'s `GetStockInfo` endpoint via gRPC to verify stock levels.

## Installation and Setup

1. **Clone the repository**:
   ```bash
   git clone https://github.com/butrint24/thesis-order-inventory-system.git
