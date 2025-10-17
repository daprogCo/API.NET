-- Step 1: Create the 'Cars' database
CREATE DATABASE Cars_Temp;
GO

-- Step 2: Switch context to the new 'Cars' database
USE Cars_Temp;
GO

-- Step 3: Create the 'Car' table
CREATE TABLE Car (
    Id INT PRIMARY KEY IDENTITY(1,1),
    name NVARCHAR(255) NOT NULL,
    mpg DECIMAL(3,1) NOT NULL,
    cylinders INT NOT NULL,
    displacement INT NOT NULL,
    horsepower INT NOT NULL,
    weight INT NOT NULL,
    acceleration DECIMAL(3,1) NOT NULL,
    model_year INT NOT NULL,
    origin NVARCHAR(255) NOT NULL,
    is_deleted INT NULL
);
GO
