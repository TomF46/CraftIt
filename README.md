# CraftIt Api

This is an example api project for a hypothetical social craft site, which allows users to upload and browse craft recipes with step by step instructions with images.

This is a .NET Core 2.1 api which also comes with a testing project which contains unit tests using NUnit to test the functionality of implemeted code in controllers and services.

This project is created using (but not limited to) the following technologies and philosophes.

* .NET Core 2.1
* Entity Framework 
* Swagger (Ui for testing and documenting api during development)
* JWT User authentication
* Coding against interfaces not implementation
* Dependency injection
* Nuget package manager for 3rd party packages
* NUnit testing framework


This can be ran locally by downloading the files, running Nuget restore, updating the connection string and secret (generate your own secret a placeholder will be in place in downloaded files) in appsettings.json , and then running the migrations to create the database.

Swagger documentation can be viewed when the project is running by going to https://localhost:{port}/swagger