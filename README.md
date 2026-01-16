# 🏰 Holstentor Restaurant Management System

[![License](https://img.shields.io/badge/License-Apache_2.0-D22128?style=for-the-badge&logo=apache)](LICENSE.txt)
[![Language](https://img.shields.io/badge/Language-C%23-239120.svg?style=for-the-badge&logo=csharp)](https://docs.microsoft.com/en-us/dotnet/csharp/)
[![Platform](https://img.shields.io/badge/Platform-Windows-0078D6.svg?style=for-the-badge&logo=windows)](https://www.microsoft.com/en-us/windows)
[![Framework](https://img.shields.io/badge/.NET%20Framework-4.7.2-512BD4.svg?style=for-the-badge&logo=.net&logoColor=white)](https://dotnet.microsoft.com/apps/aspnet)
[![Database](https://img.shields.io/badge/Database-SQL_Server-005A9C?style=for-the-badge&logo=microsoft-sql-server&logoColor=white)](https://www.microsoft.com/en-us/sql-server)

A comprehensive restaurant management system built with ASP.NET MVC 5, providing a complete admin interface for managing menus, categories, products, galleries, and users.

---

## 📋 Table of Contents

- [Overview](#overview)
- [Features](#features)
- [Technology Stack](#technology-stack)
- [Project Structure](#project-structure)
- [Installation](#installation)
- [Configuration](#configuration)
- [Usage](#usage)
- [Database](#database)
- [Contributing](#-contributing)

---

## 🎯 Overview

Holstentor is a professional restaurant management system specifically designed for internal administration. The system provides an intuitive admin interface for managing all important aspects of a restaurant, including menus, product categories, image galleries, and user management.

### Main Goals
- Simplified management of restaurant content
- User-friendly admin interface
- Secure authentication and authorization
- Flexible product categorization
- Gallery management for visual content

---

## ✨ Features

### 🔐 Authentication & Authorization
- User registration and login
- Password recovery via email
- Email confirmation
- Role-based access control (Admin, User)
- Password change functionality

### 📊 Admin Panel
- **Dashboard**: Overview of all important metrics
- **Category Management**: Create, edit, and delete categories
- **Subcategory Management**: Hierarchical product organization
- **Product Management**: Full CRUD operations for products
- **Gallery Management**: Upload and manage images
- **User Management**: Manage user accounts and roles
- **Message Management**: Receive and respond to customer inquiries

### 🖼️ Content Management
- Dynamic homepage management
- Customizable headers and footers
- Video upload and management
- Image galleries with subcategories
- About page with image management

### 👤 User Profile
- Profile view and editing
- Message management
- Password change

### 🌐 Public Pages
- Homepage with dynamic content
- About Us
- Contact form
- Gallery
- Imprint
- Privacy Policy

---

## 🛠️ Technology Stack

### Backend
- **Framework**: ASP.NET MVC 5.2.7
- **Language**: C# (.NET Framework 4.7.2)
- **ORM**: Entity Framework 6.2.0
- **Database Access**: LINQ to Entities

### Frontend
- **HTML5**: Semantic markup
- **CSS3**: Modern styling
- **JavaScript**: jQuery 3.3.1
- **UI Framework**: Bootstrap
- **jQuery UI**: 1.12.1
- **Additional Libraries**:
  - AOS (Animate On Scroll)
  - Owl Carousel
  - Fancybox
  - Isotope Layout
  - Typed.js
  - Rellax (Parallax)

### Database
- **DBMS**: Microsoft SQL Server
- **Access**: Entity Framework Database-First approach

### Development Tools
- **IDE**: Visual Studio
- **Build**: MSBuild
- **Package Manager**: NuGet

---

## 📁 Project Structure

```
Holstentor_MVC4.7/
├── Restaurant/                      # Main project
│   ├── App_Start/                   # Application configuration
│   │   └── RouteConfig.cs          # Routing configuration
│   ├── Controllers/                 # MVC Controllers
│   │   ├── AccountController.cs    # Authentication
│   │   ├── AdminController.cs      # Admin functions
│   │   ├── HomeController.cs       # Public pages
│   │   └── ProfileController.cs    # User profile
│   ├── Models/                      # Data models
│   │   ├── Domin/                  # Entity Framework models
│   │   ├── Plugin/                 # Helper functions (Email, Data)
│   │   ├── Repository/             # Repository pattern
│   │   └── ViewModels/             # View-specific models
│   ├── Views/                       # Razor Views
│   │   ├── Account/                # Login, Registration
│   │   ├── Admin/                  # Admin interface
│   │   ├── Home/                   # Public pages
│   │   ├── Profile/                # User profile
│   │   └── Shared/                 # Shared layouts
│   ├── wwwroot/                     # Static files
│   │   ├── assets/                 # CSS, JS, Images
│   │   │   ├── img/                # Images and uploads
│   │   │   └── vendor/             # Third-party libraries
│   │   └── dist/                   # Compiled assets
│   ├── Content/                     # CSS and themes
│   ├── Scripts/                     # JavaScript files
│   └── Web.config                  # Application configuration
├── Restaurant.sln                   # Visual Studio Solution
├── LICENSE.txt                      # Apache-2.0 License
└── README.md                        # This file
```

---

## 🚀 Installation

### Prerequisites

- **Visual Studio 2017** or higher
- **.NET Framework 4.7.2** or higher
- **SQL Server 2014** or higher (Express Edition is sufficient)
- **IIS Express** (included with Visual Studio)

### Step-by-Step Guide

1. **Clone the repository**
   ```bash
   git clone https://github.com/YourUsername/Holstentor_MVC4.7.git
   cd Holstentor_MVC4.7
   ```

2. **Open the solution**
   - Open `Restaurant.sln` in Visual Studio

3. **Restore NuGet packages**
   - Visual Studio will automatically restore packages
   - Or manually: Right-click on Solution → "Restore NuGet Packages"

4. **Create the database**
   - Open SQL Server Management Studio
   - Create a new database named `DbHolstentor`
   - Execute the database script (if available)

5. **Configure the connection string**
   - Open `Web.config`
   - Adjust the connection string:
   ```xml
   <connectionStrings>
     <add name="DbHolstentorEntities" 
          connectionString="data source=YourServer;initial catalog=DbHolstentor;integrated security=True;..."
          providerName="System.Data.EntityClient" />
   </connectionStrings>
   ```

6. **Run the project**
   - Press `F5` or click "Start"
   - The application will launch at `http://localhost:49908/`

---

## ⚙️ Configuration

### Email Settings

For password recovery and email confirmation, you need to configure the email settings in the `Email.cs` file:

```csharp
// Models/Plugin/Email.cs
// Configure your SMTP settings
```

### File Upload Paths

By default, uploaded files are stored in the following directories:
- Product images: `wwwroot/assets/img/backgrounds/upload/`
- Gallery images: `wwwroot/assets/img/backgrounds/gallery/`
- Album images: `wwwroot/assets/img/backgrounds/album/`

---

## 💻 Usage

### Admin Access

1. Navigate to `/Account/Einloggen`
2. Log in with admin credentials
3. You will be redirected to the admin dashboard

### Managing Categories

1. Go to **Admin → Kategorie**
2. Click "Create New Category"
3. Fill out the form and save

### Managing Products

1. Go to **Admin → Produkte**
2. Select a category
3. Create, edit, or delete products

### Managing Gallery

1. Go to **Admin → Galerie**
2. Upload images
3. Organize images into categories

---

## 🗄️ Database

### Entity Framework Models

The project uses Entity Framework Database-First approach with the following main tables:

- **Tbl_Users**: User data
- **Tbl_Roles**: User roles
- **Tbl_Kategorie**: Product categories
- **Tbl_Unterkategorie**: Subcategories
- **Tbl_Produkte**: Product information
- **Tbl_Galerie**: Gallery images
- **Tbl_Album**: Image galleries
- **Tbl_Hochladen**: Upload management
- **Tbl_Nachricht**: Messages/Contact inquiries
- **Tbl_Index**: Homepage content
- **Tbl_EmailConfirmed**: Email confirmations
- **Tbl_RestPassword**: Password recovery

### Database Diagram

The Entity-Relationship diagram can be found at:
`Models/Domin/DbHolstentorModel.edmx`

---

## 🤝 Contributing

Contributions are welcome! Please follow these steps:

1. Fork the repository
2. Create a feature branch (`git checkout -b feature/NewFeature`)
3. Commit your changes (`git commit -m 'Add new feature'`)
4. Push to the branch (`git push origin feature/NewFeature`)
5. Open a Pull Request

### Code Conventions

- Use meaningful variable names
- Comment complex code
- Follow C# Coding Conventions
- Test your changes thoroughly

---

## 📌 Notes

- This project is designed for **internal administration**
- No online ordering system included (can be extended)
- Expandable with features such as:
  - Customer frontend
  - Online ordering system
  - Reservation system
  - Multi-language support
  - API for mobile apps

---

## 🙏 Acknowledgments

- Bootstrap for the UI framework
- jQuery and jQuery UI for JavaScript functionality
- Entity Framework for database access
- All open-source libraries used

---

*Developed by Amir Argani*
