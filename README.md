# Order Management System

## Overview

This project demonstrates a microservices-based **Order Management System** implemented using .NET, gRPC, RabbitMQ, and Clean Architecture principles. The system consists of two microservices:

- **Order Service**: Manages order creation, retrieval, and updates.
- **Inventory Service**: Manages inventory levels and provides stock information.

## Architecture

The system follows the Clean Architecture pattern, which ensures a maintainable and scalable codebase. The architecture includes the following layers:

- **Presentation Layer**: Defines gRPC services and handles incoming requests.
- **Application Layer**: Implements use cases and business logic.
- **Domain Layer**: Contains core business entities and rules.
- **Infrastructure Layer**: Manages data persistence and RabbitMQ communication.

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
   - The **Order Service** receives a `CreateOrder` request.
   - After creating the order, it sends a message to the **Inventory Service** via RabbitMQ to update stock levels based on the order items.
   - The **Inventory Service** processes the message, updates stock levels, and optionally sends a confirmation back to the **Order Service**.
   - The **Order Service** completes the order creation process and may notify the user of the successful order.

2. **Stock Check Flow**:
   - Before creating an order, the **Order Service** calls the **Inventory Service**'s `GetStockInfo` endpoint to verify stock levels.

## Installation and Setup

1. **Clone the repository**:
   ```bash
   git clone https://github.com/butrint24/thesis-order-inventory-system.git
