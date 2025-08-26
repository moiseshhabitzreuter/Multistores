**Store Management API**
---------------------
About
---------------------
The Store API is hosted on Azure App Services and provides a robust set of endpoints for managing store data. It supports operations to create, read, update, and delete stores, making it easy to integrate into various applications and services.

The API uses a SQL Server database hosted on Azure, ensuring secure, scalable, and reliable storage of all store information. The solution is designed following best practices for .NET backend development, with a clean separation between API controllers, service layer, contracts layer, and data access layer.

Technologies Used
---------------------
* .NET 9.0
* EntityFramework Core
* Azure App Services
* Azure SQL Database
---------------------
API Methods
---------------------
Store
------------
* Get Stores
* URL: /api/store
* Method: GET
* Description: Retrieves a list of all stores that are not marked as deleted.
------------
* Get Store By Id
* URL: /api/store/{id}
* Method: GET
* Description: Retrieves a specific store by its unique identifier.
------------
* Delete Store
* URL: /api/store/{id}
* Method: DELETE
* Description: Performs a logical deletion of a store by setting the IsDeleted flag to true.
------------
* Create Store
* URL: /api/store
* Method: POST
* Description: Creates a new store using the request body fields.
------------
* Update store
* URL: /api/store/{id}
* Method: PUT
* Description: Updates an existing store using the request body fields.
---------------------
