CREATE TABLE Types (
	Id int NOT NULL IDENTITY(1,1),
	Type varchar(16) NOT NULL
	Cost decimal NOT NULL
	PRIMARY KEY (Id)
)

CREATE TABLE Inventory (
	Id int NOT NULL IDENTITY(1,1),
	TypeId int NOT NULL,
	AddDate datetime DEFAULT CURRENT_TIMESTAMP,
	Sold bool DEFAULT FALSE,
	PRIMARY KEY (Id)
	FOREIGN KEY (TypeId) REFERENCES Types(Id)
);

CREATE PROCEDURE SeedTypes
AS
	IF (Select COUNT(*) From Types) = 0
		INSERT Types (Type, Cost)
		VALUES
			(Soda, 0.95), -- 1
			(Candy Bar, 0.60), -- 2
			(Chips, 0.99) -- 3
GO;

CREATE PROCEDURE SeedInventory
AS
	IF (SELECT COUNT(*) FROM Inventory) = 0
		DECLARE @Counter INT
		SET @Counter=0
		WHILE (@Counter < 20)
		BEGIN
			INSERT INTO Inventory (TypeId) 
			VALUE (1), -- Soda
				(2), -- Candy Bar
				(3)  -- Chips
		END
GO;

CREATE PROCEDURE InitialSeed
AS
	Exec SeedTypes;
	Exec SeedInventory
GO;