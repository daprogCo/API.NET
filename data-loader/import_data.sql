USE Cars;
GO

BULK INSERT Car
FROM '/tmp/Automobile.csv'
WITH (
    FIRSTROW = 2,
    FIELDTERMINATOR = ',',
    ROWTERMINATOR = '0x0A'
);
GO