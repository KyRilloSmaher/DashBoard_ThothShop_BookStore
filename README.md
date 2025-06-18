# ThothStore Dashboard

<div align="center">

![ThothStore Logo](https://img.shields.io/badge/ThothStore-Dashboard-FFD700?style=for-the-badge&logo=book&logoColor=000000)
![.NET](https://img.shields.io/badge/.NET-8.0-512BD4?style=for-the-badge&logo=.net&logoColor=white)
![ASP.NET Core](https://img.shields.io/badge/ASP.NET%20Core-8.0-512BD4?style=for-the-badge&logo=dotnet&logoColor=white)
![Bootstrap](https://img.shields.io/badge/Bootstrap-5.3-7952B3?style=for-the-badge&logo=bootstrap&logoColor=white)

*A modern, responsive dashboard for managing bookstore operations with an elegant black and gold theme inspired by the ancient Egyptian god Thoth.*

[Features](#features) • [Technology Stack](#technology-stack) • [Installation](#installation) • [Usage](#usage) • [Architecture](#architecture) • [Contributing](#contributing)

</div>

---

## 📋 Table of Contents

- [Overview](#overview)
- [Features](#features)
- [Technology Stack](#technology-stack)
- [Installation](#installation)
- [Usage](#usage)
- [Architecture](#architecture)
- [API Documentation](#api-documentation)
- [Contributing](#contributing)
- [License](#license)

## 🎯 Overview

ThothStore Dashboard is a comprehensive web application designed for bookstore management. Built with ASP.NET Core 8.0 and featuring a modern, responsive interface with a distinctive black and gold theme inspired by Thoth, the ancient Egyptian god of wisdom and knowledge.

The dashboard provides administrators with powerful tools to manage books, authors, categories, orders, and analytics in an intuitive and visually appealing interface.

## ✨ Features

### 📊 Dashboard Analytics
- **Real-time Statistics**: Sales, orders, books, and author counts
- **Performance Metrics**: Month-over-month growth indicators
- **Recent Activity Feed**: Live updates on system activities
- **Quick Actions**: Fast access to common administrative tasks

### 📚 Book Management
- **Comprehensive Catalog**: Full CRUD operations for books
- **Stock Management**: Real-time inventory tracking
- **Category Organization**: Hierarchical book categorization
- **Search & Filter**: Advanced search capabilities
- **Low Stock Alerts**: Automated notifications for inventory management

### 👥 Author Management
- **Author Profiles**: Complete author information management
- **Biographical Data**: Gender, nationality, and birth date tracking
- **Book Associations**: Link authors to their published works
- **Pagination**: Efficient handling of large author databases

### 🏷️ Category Management
- **Category Hierarchy**: Organized book categorization
- **Dynamic Categories**: Flexible category creation and modification
- **Search Functionality**: Quick category lookup

### 📦 Order Management
- **Order Tracking**: Complete order lifecycle management
- **Status Updates**: Real-time order status monitoring
- **Customer Information**: Detailed customer order history
- **Analytics**: Order performance insights

### 🎨 Modern UI/UX
- **Responsive Design**: Mobile-first approach
- **Thoth Theme**: Elegant black and gold color scheme
- **Interactive Elements**: Hover effects and smooth animations
- **Accessibility**: WCAG compliant design
- **Dark Mode**: Eye-friendly interface

## 🛠️ Technology Stack

### Backend
- **ASP.NET Core 8.0**: Modern web framework
- **C#**: Primary programming language
- **Entity Framework Core**: ORM for database operations
- **Dependency Injection**: Built-in IoC container

### Frontend
- **Bootstrap 5.3**: Responsive CSS framework
- **Font Awesome 6.4**: Icon library
- **Google Fonts (Inter)**: Typography
- **jQuery 3.6**: JavaScript library
- **Custom CSS**: Thoth-themed styling

### Architecture
- **MVC Pattern**: Model-View-Controller architecture
- **Repository Pattern**: Data access abstraction
- **Service Layer**: Business logic separation
- **View Models**: Data transfer objects

## 🚀 Installation

### Prerequisites
- [.NET 8.0 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [Visual Studio 2022](https://visualstudio.microsoft.com/) or [VS Code](https://code.visualstudio.com/)
- [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads) or [SQLite](https://www.sqlite.org/)

### Setup Instructions

1. **Clone the Repository**
   ```bash
   git clone https://github.com/yourusername/ThothStore-Dashboard.git
   cd ThothStore-Dashboard
   ```

2. **Restore Dependencies**
   ```bash
   dotnet restore
   ```

3. **Configure Database**
   - Update connection string in `appsettings.json`
   - Run database migrations (if applicable)

4. **Build the Application**
   ```bash
   dotnet build
   ```

5. **Run the Application**
   ```bash
   dotnet run
   ```

6. **Access the Dashboard**
   - Open your browser and navigate to `https://localhost:5001`
   - Default admin credentials: (configure as needed)

## 📖 Usage

### Dashboard Overview
The main dashboard provides a comprehensive overview of your bookstore operations:

- **Statistics Cards**: View key metrics at a glance
- **Recent Orders**: Monitor latest customer orders
- **Top Authors**: Track bestselling authors
- **Trending Books**: Identify popular inventory items
- **Quick Actions**: Perform common administrative tasks

### Book Management
1. Navigate to **Books** section
2. Use search functionality to find specific books
3. Add new books with complete details
4. Update inventory levels and pricing
5. Monitor stock levels with automated alerts

### Author Management
1. Access **Authors** section
2. Create comprehensive author profiles
3. Associate authors with their published works
4. Search and filter author database
5. Update biographical information

### Category Management
1. Go to **Categories** section
2. Create new book categories
3. Organize books by genre, subject, or theme
4. Maintain category hierarchy

### Order Management
1. View **Orders** section
2. Track order status and progress
3. Monitor customer information
4. Analyze order patterns and trends

## 🏗️ Architecture

### Project Structure
```
ThothStore_DashBoard/
├── Controllers/          # MVC Controllers
├── Models/              # Data Models and ViewModels
├── Services/            # Business Logic Services
├── Handlers/            # Request/Response Handlers
├── Views/               # Razor Views
├── wwwroot/             # Static Assets
│   ├── css/            # Stylesheets
│   ├── js/             # JavaScript Files
│   └── images/         # Image Assets
└── Properties/          # Project Properties
```

### Key Components

#### Controllers
- **HomeController**: Dashboard overview and statistics
- **BookController**: Book CRUD operations
- **AuthorController**: Author management
- **CategoryController**: Category operations
- **OrderController**: Order tracking and management
- **AdminController**: User administration
- **AnalyticController**: Analytics and reporting

#### Models
- **ViewModels**: Data transfer objects for views
- **Request/Response Models**: API communication models
- **Entity Models**: Database entity representations

#### Services
- **Business Logic**: Core application functionality
- **Data Access**: Repository pattern implementation
- **Validation**: Input validation and business rules

## 📚 API Documentation

### Book Endpoints
- `GET /Book/Home` - Retrieve book catalog
- `POST /Book/Create` - Add new book
- `PUT /Book/Update/{id}` - Update book information
- `DELETE /Book/Delete/{id}` - Remove book from catalog
- `GET /Book/Search` - Search books by criteria

### Author Endpoints
- `GET /Author/Home` - Retrieve author list
- `POST /Author/Create` - Add new author
- `PUT /Author/Update/{id}` - Update author profile
- `DELETE /Author/Delete/{id}` - Remove author
- `GET /Author/Search` - Search authors

### Category Endpoints
- `GET /Category/Home` - Retrieve categories
- `POST /Category/Create` - Add new category
- `PUT /Category/Update/{id}` - Update category
- `DELETE /Category/Delete/{id}` - Remove category

### Order Endpoints
- `GET /Order/Home` - Retrieve order list
- `GET /Order/{id}` - Get specific order details
- `PUT /Order/UpdateStatus/{id}` - Update order status

## 🤝 Contributing

We welcome contributions to improve ThothStore Dashboard! Please follow these guidelines:

### Development Setup
1. Fork the repository
2. Create a feature branch: `git checkout -b feature/amazing-feature`
3. Make your changes following the coding standards
4. Test thoroughly
5. Commit your changes: `git commit -m 'Add amazing feature'`
6. Push to the branch: `git push origin feature/amazing-feature`
7. Open a Pull Request

### Coding Standards
- Follow C# naming conventions
- Use meaningful variable and method names
- Add XML documentation for public APIs
- Write unit tests for new functionality
- Ensure responsive design for all UI changes

### Commit Message Format
```
type(scope): description

[optional body]

[optional footer]
```

Types: `feat`, `fix`, `docs`, `style`, `refactor`, `test`, `chore`

## 📄 License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

## 🙏 Acknowledgments

- **Thoth**: Ancient Egyptian god of wisdom, knowledge, and writing
- **Bootstrap Team**: For the excellent CSS framework
- **Font Awesome**: For the comprehensive icon library
- **ASP.NET Core Team**: For the powerful web framework

## 📞 Support

For support and questions:
- 📧 Email: support@thothstore.com
- 🐛 Issues: [GitHub Issues](https://github.com/KyRilloSmaher/DashBoard_ThothShop_BookStore/issues)
- 📖 Documentation: [Wiki](https://github.com/yourusername/KyRilloSmaher/DashBoard_ThothShop_BookStorewiki)

---

<div align="center">

**Built with ❤️ and ☕ by the ThothStore Team**

*May the wisdom of Thoth guide your bookstore operations*

</div> 