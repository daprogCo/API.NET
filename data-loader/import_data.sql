USE Cars;
GO

BULK INSERT Car 
FROM '/tmp/Automobile.csv' 
WITH ( FIRSTROW = 2); 

GO