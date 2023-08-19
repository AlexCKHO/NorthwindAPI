# NorthwindAPI

## **Overview**

The **Northwind API** is an ASP.NET Core Web API project designed to provide CRUD operations for the Northwind database, specifically focusing on products and suppliers.

## **Setting up the Northwind Database**

### Download the Northwind Database Script

1. Visit to the SQL [Server Samples GitHub repository](https://github.com/microsoft/sql-server-samples/tree/master/samples/databases/northwind-pubs).

2. Locate and access the `instnwnd.sql` link, which will display a lengthy SQL script for setting up the Northwind Database.

3. Highlight the entire script and then copy it to your clipboard.

4. Initiate a new Notepad document, paste the copied script into this document, and subsequently save it as `InstallNorthwind.sql`. The saved location isn't restricted to any specific directory.

### Initializing the Northwind Database in Visual Studio

1. Start Visual Studio and opt to "Continue without Code" found at the interface's base.

2. Head to `View` and then select `SQL Server Object Explorer`.

3. Within the explorer, right-click on `SQLServer -> (localdb)\MSSQLLocalDB` and then opt for 'New Query'.
``
4. Navigate to `File`, then `Open`, followed by `File`, and browse to find and select your previously saved `InstallNorthwind.sql` script.

5. Initiate the script by pressing the 'Execute' button. Upon completion, the Northwind database will be set up.


## **Features**

- CRUD operations for products and suppliers.
- Utilizes Entity Framework for data operations.
- Structured with Repository and Service patterns for maintainability and scalability.

## Project Structure

### **Controllers**

- **Products Controller**: Handles CRUD operations related to products.
- **Suppliers Controller**: Handles CRUD operations related to suppliers.

### **Data Access Layer**

- **NorthwindContext**: The main Entity Framework database context.

- **Repositories**: Includes generic and specialized repositories.

  * **NorthwindRepository<T>**: Generic repository for CRUD operations.

  * **ProductsRepository**: Specialized repository for product operations.

  * **SuppliersRepository**: Specialized repository for supplier operations.

### **Services**

-  **NorthwindService<T>**: Generic service class offering CRUD operations for entities of type T.

-  **SupplierService**: Specialized service for supplier-related operations.

### **Utilities**

1. Utiles: Contains utility methods for transforming entities into DTOs.

### **Models**

 - **DTOs**: Data Transfer Objects used for API responses.
   * `ProductDTO`
   * `SupplierDTO`

- **Entities**: Database entities.

   * `Product`
   * `Supplier`
   * `Category`

## Setup & Configuration

* Dependencies:

   * Microsoft.AspNetCore.Mvc.NewtonsoftJson
   * Microsoft.AspNetCore.OpenApi
   * Microsoft.EntityFrameworkCore
   * Microsoft.EntityFrameworkCore.Design
   * Microsoft.EntityFrameworkCore.SqlServer
   * Microsoft.EntityFrameworkCore.Tools

## Usage

1. Ensure you have the .NET 7.0 SDK installed.
2. Navigate to the project directory.
3. Run dotnet restore to install dependencies.
4. Run dotnet run to start the API.
5. Use tools like Postman or Swagger (if configured) to interact with the available endpoints.

## Testing Overview 

The **Northwind API** testing suite is designed to validate the application's core functionalities. Through rigorous unit tests, the project validates crucial components of the application, from the data access layer to the controllers, ensuring that they operate consistently and helping to identify potential issues.

### Dependencies

- **Microsoft.EntityFrameworkCore.InMemory**: For simulating database operations in-memory.
- **Moq**: Utilized for creating mock objects in unit tests.
- **NUnit**: The primary framework for crafting unit tests.


### Test Classes & Scenarios

1. **RepositoryTests**: Concentrates on the repository layer, validating the behavior of data CRUD operations.
   
2. **SuppliersControllerShould**: Evaluates the `SuppliersController`, ensuring that its endpoints return the expected outcomes.

3. **SupplierServiceShould**: Ensures the service layer's operations, focusing on data retrieval and manipulation.


## Running the Tests

1. Navigate to the directory containing the test project.
2. Execute the command `dotnet test` to initiate all tests.
3. Examine the console output to identify any failing tests and their associated error messages.


## Future Enhancements
* Implement authentication and authorization.
* Extend the API to cover more entities from the Northwind database.
