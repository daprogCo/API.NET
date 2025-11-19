-----------------------------------------
-- Create the 'Cars' database
-----------------------------------------
CREATE DATABASE Cars;
GO

USE Cars;
GO

-----------------------------------------
-- Final table Car
-----------------------------------------
CREATE TABLE Car (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    name NVARCHAR(255) NOT NULL,
    mpg DECIMAL(5,2) NULL,
    cylinders INT NULL,
    displacement DECIMAL(5,2) NULL,
    horsepower INT NULL,
    weight INT NULL,
    acceleration DECIMAL(5,2) NULL,
    model_year INT NULL,
    origin NVARCHAR(255) NULL,
    is_deleted BIT NOT NULL DEFAULT 0
);
GO

-----------------------------------------
-- Staging table CarTemp (matches CSV)
-----------------------------------------
CREATE TABLE CarTemp (
    name NVARCHAR(255),
    mpg FLOAT,
    cylinders INT,
    displacement FLOAT,
    horsepower FLOAT NULL,
    weight INT,
    acceleration FLOAT,
    model_year INT,
    origin NVARCHAR(255)
);
GO
