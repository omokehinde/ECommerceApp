# E-Commerce Store Setup Guide

Follow these steps to set up and run the project on your local machine.

## Prerequisites
- [.NET 6.0 SDK](https://dotnet.microsoft.com/download)
- Code editor (e.g., [Visual Studio Code](https://code.visualstudio.com/))
- Database:
  - **Windows**: SQL Server
  - **Linux/macOS**: SQLite (included) or [SQL Server for Linux](https://docs.microsoft.com/en-us/sql/linux/sql-server-linux-setup)
- Git (for version control)

## Setup Instructions

### 1. Clone the Repository
```bash
git clone https://github.com/yourusername/ecommerce-app.git
cd ecommerce-app
```

## 2. Database Setup
For SQL Server (All Platforms):
### 1. Update connection string in appsettings.json:
```json
"ConnectionStrings": {
  "DefaultConnection": "Server=localhost;Database=ECommerceDB;User=sa;Password=your_password;"
}
```
### 2. Run migrations:
```bash
dotnet ef database update --project ECommerce.API
```

For SQLite (Linux/macOS):
No setup required - uses file-based database by default.

## 3. Restore Dependencies
```bash
dotnet restore
```
## 4. Build and Run
```bash
dotnet run --project ECommerce.API
```
## 5. Access the Application
Frontend: http://localhost::{PORT}

API Endpoints: http://localhost:{PORT}/swagger (requires Swagger setup)

Platform-Specific Notes
Windows
Install Visual Studio 2022 for full IDE experience

Ensure SQL Server is running

Linux
```bash
# Install .NET SDK
wget https://packages.microsoft.com/config/ubuntu/20.04/packages-microsoft-prod.deb -O packages-microsoft-prod.deb
sudo dpkg -i packages-microsoft-prod.deb
sudo apt-get update
sudo apt-get install -y dotnet-sdk-6.0
```
macOS
```bash
# Install Homebrew (if not installed)
/bin/bash -c "$(curl -fsSL https://raw.githubusercontent.com/Homebrew/install/HEAD/install.sh)"

# Install .NET SDK
brew install --cask dotnet-sdk
```

# Troubleshooting
Common Issues
Build Errors:

Run dotnet clean && dotnet restore

Verify .NET 6.0 SDK is installed (dotnet --version)

Database Connection Issues:

Check connection strings in appsettings.json

For SQL Server: Ensure service is running

Port Conflicts:

Change port in Properties/launchSettings.json

View Logs
```bash
cat ECommerce.API/Logs/*.log
```

