# Order-Inventory System

## Table of Contents

1. [Project Overview](#project-overview)
2. [Architecture](#architecture)
    - [Clean Architecture](#clean-architecture)
    - [Layered Structure](#layered-structure)
3. [Project Structure](#project-structure)
    - [Order Service](#order-service)
    - [Inventory Service (Product Service)](#inventory-service-product-service)
4. [Configuration](#configuration)
    - [gRPC Configuration](#grpc-configuration)
    - [RabbitMQ Configuration](#rabbitmq-configuration)
    - [Database Configuration](#database-configuration)
5. [API Documentation](#api-documentation)
    - [Order Service Endpoints](#order-service-endpoints)
    - [Inventory Service (Product Service) Endpoints](#inventory-service-product-service-endpoints)
    - [gRPC Endpoints (Product Service)](#grpc-endpoints-product-service)
6. [gRPC Communication](#grpc-communication)
    - [ProductService Client](#productservice-client)
7. [Message Queue Communication](#message-queue-communication)
8. [Best Practices and Preferences](#best-practices-and-preferences)

## Project Overview

The **Order Inventory System** is a microservices-based application that manages orders and inventory within a retail system. The project is built using .NET 8, following the principles of Clean Architecture to ensure separation of concerns, testability, and maintainability.

## Architecture

### Clean Architecture

This project adheres to Clean Architecture principles, organizing code into distinct layers that enforce clear boundaries and ensure that the core business logic is independent of external concerns like UI, databases, and frameworks.

### Layered Structure

The solution is divided into the following layers:

- **Domain Layer**: Contains the core business logic and domain entities.
- **Application Layer**: Implements use cases and interacts with the Domain Layer.
- **Infrastructure Layer**: Provides implementations for external concerns like databases, messaging, and gRPC.
- **Presentation Layer**: Includes API controllers and gRPC service implementations.

## Project Structure

### Order Service

The Order Service is responsible for managing customer orders. It communicates with the Product Service via gRPC to retrieve product information and uses RabbitMQ for message-based communication.

**Key Projects:**
- `Order.Host`: The entry point for the Order service.
- `Order.Application`: Contains business logic and use cases.
- `Order.Data`: Manages data access and persistence.
- `Order.Grpc.Contracts`: Defines gRPC contracts for communication with the Product Service.
- `Order.Grpc.AspNetCore`: Implements the gRPC client and server-side logic.
- `Order.Message.Contracts`: Defines message contracts for RabbitMQ communication.
- `Order.Message.RabbitMQ`: Implements RabbitMQ messaging.

### Inventory Service (Product Service)

The Inventory Service manages the product catalog and stock levels. It exposes a gRPC service that the Order Service can call to fetch product details.

**Key Projects:**
- `Inventory.Host`: The entry point for the Inventory service.
- `Inventory.Application`: Contains business logic and use cases.
- `Inventory.Data`: Manages data access and persistence.
- `Inventory.Grpc.Contracts`: Defines gRPC contracts for the Product Service.
- `Inventory.Grpc.AspNetCore`: Implements the gRPC service for product information.
- `Inventory.Message.Contracts`: Defines message contracts for RabbitMQ communication.
- `Inventory.Message.RabbitMQ`: Implements RabbitMQ messaging.

## Configuration

### gRPC Configuration

Both services use gRPC for inter-service communication. The `GrpcModule` classes in each service configure the gRPC clients and services.

- **Order Service** runs on port 46737.
- **Inventory Service** runs on port 32180.

The gRPC service and client are configured in the respective `Startup` classes and `GrpcModule` classes.

### RabbitMQ Configuration

RabbitMQ is used for message-based communication between services. The message contracts are defined in the `Order.Message.Contracts` and `Inventory.Message.Contracts` projects.

Configure RabbitMQ settings in the `appsettings.json` file of each service.

### Database Configuration

The services use SQL databases, with configuration handled in the `SqlModule` classes. Update the connection strings in the `appsettings.json` files.

## API Documentation

Swagger is integrated into the project for API documentation. You can access it at:

- **Order Service**: `http://localhost:5284/swagger`
- **Inventory Service**: `http://localhost:32180/swagger`

### Order Service Endpoints
```
- **`GET /getPurchase/{id}`**
  - **Description**: Retrieves an order by its unique identifier.
  - **Response**: Returns the order details including customer information, product details, and order status.

- **`POST /createPurchase`**
  - **Description**: Creates a new order. The request body must include customer and product details.
  - **Response**: Returns the unique identifier of the newly created order.

- **`GET /getPurchases`**
  - **Description**: Retrieves a list of purchases made by customers. Optionally, it can filter by date range or customer ID.
  - **Response**: Returns a list of purchase details, including purchase date, product details, and total amount.

- **`DELETE /deletePurchase/{id}`**
  - **Description**: Deletes an order by its unique identifier.
  - **Response**: Returns a `204 No Content` status if the deletion is successful.
```
---
```
- **`GET /getUser/{id}`**
  - **Description**: Retrieves an user by its unique identifier.
  - **Response**: Returns the user details including their purchase history.

- **`POST /createUser`**
  - **Description**: Creates a new user.
  - **Response**: Returns the unique identifier of the newly created user.

- **`GET /getUsers`**
  - **Description**: Retrieves a list of users.
  - **Response**: Returns a list of user details including their purchase history.

- **`DELETE /deleteUser/{id}`**
  - **Description**: Deletes an user by its unique identifier.
  - **Response**: Returns a `204 No Content` status if the deletion is successful.
```
***

### Inventory Service (Product Service) Endpoints
```
- **`GET /getItem/{id}`**
  - **Description**: Retrieves product details by its unique identifier.
  - **Response**: Returns the product details including name, price, stock availability, and additional details.

- **`POST /createItem`**
  - **Description**: Adds a new product to the inventory. The request body must include product details.
  - **Response**: Returns the unique identifier of the newly created product.

- **`GET /getItems`**
  - **Description**: Retrieves a list of all available products. Can be filtered by category or stock availability.
  - **Response**: Returns a list of products, including their identifiers, names, prices, and stock levels.

- **`PUT /updateItem/{id}`**
  - **Description**: Updates an existing product's details. The request body must include the updated product information.
  - **Response**: Returns a `204 No Content` status if the update is successful.

- **`DELETE /deleteItem/{id}`**
  - **Description**: Deletes a product from the inventory by its unique identifier.
  - **Response**: Returns a `204 No Content` status if the deletion is successful.
```
### gRPC Endpoints (Product Service)

In addition to the REST API endpoints, the Inventory Service also exposes gRPC endpoints for inter-service communication.

- **`rpc GetProduct(GetProductRequest) returns (GetProductResponse)`**
  - **Description**: Retrieves product details via a gRPC call. This endpoint is used internally by the Order Service to fetch product information.
  - **Request**: A `GetProductRequest` containing the product ID.
  - **Response**: A `GetProductResponse` containing the product details.

## gRPC Communication

### ProductService Client

The Order Service uses a gRPC client to communicate with the Product Service. This client is configured in the `GrpcModule` class of the `Order.Grpc.AspNetCore` project.

## Message Queue Communication

RabbitMQ is used for message-based communication between the services. Messages related to stock updates are published to RabbitMQ, and the Product Service listens to these messages to update stock levels.

## Best Practices and Preferences

- **Nullability**: Nullable types are used throughout the project.
- **Swagger**: Integrated for API documentation.
- **HTTP Status Codes**: A `204 No Content` status code is returned when a body is not needed.
- **Port Configurations**: Custom ports are configured for gRPC services.
- **No Copy of .proto Files**: `.proto` files are not copied to the output directory.
