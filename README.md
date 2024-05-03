# Chess++ Application

![GitHub last commit](https://img.shields.io/github/last-commit/CSC-3380-Spring-2024/Team-12)
![GitHub issues](https://img.shields.io/github/issues/CSC-3380-Spring-2024/Team-12)
![GitHub pull requests](https://img.shields.io/github/issues-pr/CSC-3380-Spring-2024/Team-12)
![GitHub](https://img.shields.io/github/license/CSC-3380-Spring-2024/Team-12)
![.NET](https://img.shields.io/badge/.NET-5.0-blue.svg)
![Blazor](https://img.shields.io/badge/Blazor-WebAssembly-blue)

Welcome to the Chess++ repository, a Chess application built using C# with Blazor for an interactive and modern web user interface. This guide will help you set up and run the project on your local machine.

## Prerequisites

Before you begin, ensure you have the .NET SDK installed on your machine. If you don't have it installed, you can download it from [here](https://dotnet.microsoft.com/download). Additionally, make sure you have the Blazor WebAssembly tooling installed. If not, you can install it using the following command:

dotnet new -i Microsoft.AspNetCore.Blazor.Templates

## Setup and Running the Application

1. **Clone the Repository**

If you haven't already, clone the repository to your local machine:

git clone https://github.com/CSC-3380-Spring-2024/Team-12

2. **Navigate to the Project Directory**

Change into the project directory:

    $ cd chess

3. **Install Dependencies**

To install all necessary dependencies, use the following command:

    $ dotnet restore

This command will download and install all the required NuGet packages for the project.

4. **Build the Project**

Build the project to ensure all dependencies are correctly set up:

    $ dotnet build

5. **Run the Application**

Finally, run the application:

    $ dotnet run