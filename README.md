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
   git clone https://github.com/yourusername/your-repo-name.git

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

## Contributing

Contributions are welcome! Please fork the repository and submit pull requests for improvements or bug fixes. Make sure to follow coding standards and include necessary tests.

## License

This project is licensed under the MIT License. See the [LICENSE](LICENSE) file for details.

---
