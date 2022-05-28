/*
Relationship diagram
+-------+            +-----------+            +-------------+            +----------------+
| Types | 1 ------ 1 | Inventory | (*) ---- 1 | Transaction | 1 ------ 1 | CCVerification |
+-------+            +-----------+            +-------------+            +----------------+
 - Id ---------+      - Id                 +---- Id                   +---- Id
 - Type        +------- TypeId             |   - CCVerificationId ----+   - Approved 
 - Cost               - InsertDate         |   - InsertDate               - OriginalTransactionAmount
                      - TransactionId -----+   - RefundRequested          - InsertDate
                      - InsertDate                                        
                      - SaleDate
                      - RefundDate
*/

CREATE TABLE Types (
	Id int NOT NULL IDENTITY(1,1),
	Type varchar(16) NOT NULL,
	Cost decimal NOT NULL,
	PRIMARY KEY (Id)
)

CREATE TABLE CCVerification (
    Id int NOT NULL IDENTITY(1,1),
    Approved boolean NOT NULL,
    OriginalTransactionAmount decimal NOT NULL,
    InsertDate datetime DEFAULT CURRENT_TIMESTAMP,
    PRIMARY KEY (Id)
)

CREATE TABLE Transactions (
    Id int NOT NULL IDENTITY(1,1),
    CCVerificationId int NOT NULL,
    InsertDate datetime NOT NULL CURRENT_TIMESTAMP,
    RefundRequested boolean NOT NULL DEFAULT 0,
    PRIMARY KEY (Id)
    FOREIGN KEY (CCVerificationId) REFERENCES CCVerification(Id)
)

CREATE TABLE Inventory (
	Id int NOT NULL IDENTITY(1,1),
	TypeId int NOT NULL,
    TransactionId int NULL,
    InsertDate datetime DEFAULT CURRENT_TIMESTAMP,
    SaleDate datetime NULL,
    RefundDate datetime NULL,
	PRIMARY KEY (Id)
	FOREIGN KEY (TypeId) REFERENCES Types(Id)
    FOREIGN KEY (TransactionId) REFERENCES Transactions(Id)
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