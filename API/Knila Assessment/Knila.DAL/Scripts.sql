CREATE TABLE Users (Id INT PRIMARY KEY IDENTITY(1,1), FirstName NVARCHAR(100), LastName NVARCHAR(100), Email NVARCHAR(150), PhoneNumber NVARCHAR(20), Address NVARCHAR(200), City NVARCHAR(100), State NVARCHAR(100), Country NVARCHAR(100), PostalCode NVARCHAR(20));


--"Scaffold-DbContext "Server=localhost\SQLEXPRESS;Database=Knila;Trusted_Connection=True;TrustServerCertificate=True;" Microsoft.EntityFrameworkCore.SqlServer -OutputDir Models -Context KnilaDbContext -Force"