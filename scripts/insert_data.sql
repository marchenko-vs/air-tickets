BULK INSERT [dbo].[Flights]
FROM '/flights.csv'
WITH (
  FIELDTERMINATOR = ',',
  ROWTERMINATOR = '\n',
  FIRSTROW = 2,
  datafiletype = 'widechar'
);

BULK INSERT [dbo].[Tickets]
FROM '/tickets.csv'
WITH (
  FIELDTERMINATOR = ',',
  ROWTERMINATOR = '\n',
  FIRSTROW = 2,
  datafiletype = 'widechar'
);

BULK INSERT [dbo].[Planes]
FROM '/planes.csv'
WITH (
  FIELDTERMINATOR = ',',
  ROWTERMINATOR = '\n',
  FIRSTROW = 2,
  datafiletype = 'widechar'
);

BULK INSERT [dbo].[Services]
FROM '/services.csv'
WITH (
  FIELDTERMINATOR = ',',
  ROWTERMINATOR = '\r\n',
  FIRSTROW = 2,
  datafiletype = 'widechar'
);
