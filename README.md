# Colleagues

## Overview
Colleagues is an ASP.NET Core MVC web application designed to analyze employee collaboration. It processes CSV files containing employee project data and calculates the total number of days pairs of employees have worked together on common projects. The app highlights the top pairs with the longest collaboration time, helping organizations understand team dynamics.

## Features
- Upload CSV files with employee project assignments.
- Calculate overlapping working days for employee pairs per project.
- Display the pair of employees who worked together the longest across all projects.
- Bootstrap-based responsive UI for a clean and user-friendly experience.

## Getting Started

### Prerequisites
- [.NET 6 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/6.0)
- Visual Studio 2022 or Visual Studio Code
- Git (for cloning the repository)

### Setup Instructions
1. Clone the repository:
   ```bash
   git clone https://github.com/GeorgiMrchv/Colleagues.git

Project Structure
Controllers/: MVC controllers to handle HTTP requests.

Models/: Data models representing employees and project info.

Services/: Business logic, including collaboration calculation.

Views/: Razor views for UI.

wwwroot/: Static files including Bootstrap and custom CSS.

Technologies Used
ASP.NET Core MVC (.NET 6)

Bootstrap 5 for styling

C# for backend logic

Razor Pages for the frontend

Notes
Bootstrap styles are included via local lib folder.

If Bootstrap does not load correctly, verify static files are served and paths are correct.

The application currently supports uploading and analyzing one CSV file at a time.
