CREATE TABLE dbo.Flight
	(
	Number int NOT NULL,
	Terminal smallint NOT NULL,
	City nvarchar(250) NOT NULL,
	Airline nvarchar(250) NOT NULL,
	DateTimeOfArrival datetime NOT NULL,
	Status smallint NOT NULL
	)  ON [PRIMARY]
GO
ALTER TABLE dbo.Flight ADD CONSTRAINT
	PK_Flight PRIMARY KEY CLUSTERED 
	(
	Number
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
