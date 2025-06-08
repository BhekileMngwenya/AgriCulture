# AgriCulture Application

## Overview

AgriCulture is a web application designed to manage farmers, their profiles, and products. It supports two main roles: Farmers and Employees. Farmers can create and manage their products, while Employees can oversee farmers and products in the system.

## Features

- User authentication and role management (Farmer, Employee)
- Farmer profile creation, editing, and management
- Product creation, editing, deletion with image upload support
- Azure Blob Storage integration for storing product images
- Dashboards for Farmers and Employees with summaries and recent activity
- Search and filter functionality on dashboards
- Responsive UI with Bootstrap styling

## Technologies Used

- ASP.NET Core MVC
- Entity Framework Core with SQL Server
- Azure Blob Storage for image management
- Bootstrap for UI styling
- C#

## Setup Instructions

1. Clone the repository:
   ```
   git clone https://github.com/BhekileMngwenya/AgriCulture.git
   cd AgriCulture
   ```

2. Configure the database connection string in `appsettings.json`.

3. Configure Azure Blob Storage connection string and container name in `appsettings.json`.

4. Apply database migrations:
   ```
   dotnet ef database update
   ```

5. Run the application:
   ```
   dotnet run
   ```

6. Access the application in your browser at `http://localhost:5000` (or the configured port).

## Testing

- Test product creation, editing, and deletion flows for both Farmer and Employee roles.
- Verify image upload and display functionality.
- Test profile management features.
- Check dashboard UI and navigation.
- Validate error handling and edge cases.

## Notes

- Ensure Azure Blob Storage container has appropriate permissions and CORS settings for image accessibility.
- Default images are used when no image is uploaded.
- User roles control access to different parts of the application.

## Contact

For issues or contributions, please open an issue or pull request on the GitHub repository.

---
