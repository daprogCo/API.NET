USE Cars;
GO

-----------------------------------------
-- 1. BULK INSERT into CarTemp
-- (schema EXACTLY matches CSV)
-----------------------------------------
BULK INSERT CarTemp
FROM '/tmp/Automobile.csv'
WITH (
    FIRSTROW = 2,
    FIELDTERMINATOR = ',',
    ROWTERMINATOR = '0x0D0A',   -- CRLF
    TABLOCK,
    KEEPNULLS
);
GO

-----------------------------------------
-- 2. Insert validated/transformed rows 
--    from staging into final Car table
-----------------------------------------
INSERT INTO Car (
    name,
    mpg,
    cylinders,
    displacement,
    horsepower,
    weight,
    acceleration,
    model_year,
    origin
)
SELECT
    name,
    mpg,
    cylinders,
    displacement,
    horsepower,
    weight,
    acceleration,
    model_year,
    origin
FROM CarTemp;
GO

-----------------------------------------
-- 3. Delete staging table
-----------------------------------------
DROP TABLE CarTemp;
GO