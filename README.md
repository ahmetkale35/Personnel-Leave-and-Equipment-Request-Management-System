# Personnel Leave and Equipment Request Management System

## Overview
This is a role-based web application developed with **ASP.NET MVC** and **Entity Framework**, designed to streamline the management of employee leave and equipment requests within an organization. It facilitates employees in submitting requests for leaves and equipment, while allowing administrators and IT staff to review, approve, reject, or cancel these requests efficiently.

## Features

- **User Authentication & Authorization:**  
  Secure login system with role-based access control (Admin, Employee, IT Staff). Roles determine the functionalities and views accessible to each user.

- **Leave Requests Management:**  
  Employees can create, view, and cancel leave requests. Admins and authorized roles can approve or reject leave requests.

- **Equipment Requests Management:**  
  Similar to leave requests, users can submit equipment demands. IT staff and admins can review, approve, or reject these requests.

- **Dynamic DataTables Integration:**  
  Interactive tables with search, sort, and pagination features to improve user experience when managing lists of requests.

- **Security Measures:**  
  - Passwords are securely stored using hashing algorithms.  
  - Protection against common web vulnerabilities including CSRF (Cross-Site Request Forgery), SQL Injection, and parameter tampering.  
  - Anti-forgery tokens are implemented for form submissions to prevent CSRF attacks.  
  - Role-based authorization attributes ensure only authorized users access sensitive actions.

- **Session Management:**  
  User sessions are handled securely with cookie-based authentication and proper session timeouts.

## Technologies Used

- ASP.NET MVC  
- Entity Framework Core  
- Microsoft SQL Server (or SQLite depending on setup)  
- Bootstrap 5 for responsive UI  
- jQuery and DataTables for dynamic table functionalities  
- Cookie Authentication for security  

## Installation and Setup

1. **Clone the repository:**

   ```bash
   git clone https://github.com/ahmetkale35/Personnel-Leave-and-Equipment-Request-Management-System.git


2. **Configure the database connection string in appsettings.json or Context class.**

 
3. **Apply migrations and update the database:**
   ```bash
   dotnet ef database update

4. **Run the project:**

   ```bash
   dotnet run

5. **Open the browser and navigate to the login page.**


 ## Usage

- **Admin Users:**  
  Can view all leave and equipment requests, approve or reject them, and manage user roles.

- **Employees:**  
  Can submit new leave or equipment requests, view the status of their requests, cancel pending requests, and delete all their requests.

- **IT Staff:**  
  Similar to Admin but focused primarily on equipment request approvals and management.

## Security Considerations

- All forms include anti-forgery tokens to prevent CSRF attacks.  
- Parameter tampering is mitigated through server-side validation and role-based access control.  
- User passwords are hashed before storage to protect sensitive data.  
- Database operations use Entity Framework ORM to avoid SQL injection vulnerabilities.  
- Caching for sensitive pages is disabled to prevent sensitive data from being stored in browser caches, especially after logout.

## Seed Users Creation (CreateSampleUser Action)

The project includes an action named `CreateSampleUser` in the `AccountController` to easily seed initial users into the database.

### What Does It Do?

- Automatically adds sample users (Admin, IT staff, employees) if they don't already exist in the database.
- Helps quickly set up test users when running the project for the first time.
- Creates the following users:

| Full Name    | Email              | Password | RoleId (Role)      |
|--------------|--------------------|----------|--------------------|
| Ahmet Yılmaz | ahmet@personel.com | 123      | 2 (Employee)       |
| Ayse Tek     | ayse@personel.com  | 1234     | 2 (Employee)       |
| Burak Aslan  | burak@bt.com       | 123      | 3 (IT Staff)       |
| Ali Yıldız   | ali@admin.com      | 321      | 1 (Admin)          |

### How to Use

After running the project, open your browser and navigate to:

https://localhost:5001/Account/CreateSampleUser


- If the users already exist, the action will skip adding duplicates and return `"User already exists."`
- If the users are successfully added, you will see `"Admin user successfully added."`

### Security Notice

- This action should only be used in development and testing environments.
- Access to this endpoint should be disabled or removed in production.
- Otherwise, unauthorized users could create new users and compromise the system.

  ## Sample User Accounts

| Role     | Email                | Password     |
| -------- | -------------------- | ------------ |
| Admin    | ali@admin.com        | 321          |
| Employee | ayse@personel.com    | 1234         |
| IT Staff | burak@bt.com         | 123          |

> **Note:** Passwords are stored securely as hashed values in the database.  
> Use these credentials only for testing purposes in a development environment.


## Contributing

Contributions are welcome! Please fork the repository and submit pull requests for improvements or bug fixes. Make sure to follow coding standards and include necessary tests.

## License

This project is licensed under the MIT License. See the [LICENSE](LICENSE) file for details.

---
