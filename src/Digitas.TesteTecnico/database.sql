CREATE TABLE dbo.BtcAsk
	(
		BtcAskId INT NOT NULL IDENTITY (1, 1),
		Microtimestamp BIGINT NOT NULL,
		UsdValue INT NOT NULL,
		Amount DECIMAL(11,8) NOT NULL
	)  ON [PRIMARY]
	
	ALTER TABLE dbo.BtcAsk 
	ADD CONSTRAINT
		PK_BtcAskId PRIMARY KEY CLUSTERED 
		(
			BtcAskId
		) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

	ALTER TABLE dbo.BtcBid 
	SET (LOCK_ESCALATION = TABLE)

CREATE TABLE dbo.SimulationQuote
	(
		SimulationQuoteId INT NOT NULL IDENTITY (1, 1),
		SimulationQuoteHashIdentifier NVARCHAR(64) NOT NULL,
		OperationChoice INT NOT NULL,
		Coin INT NOT NULL,
		RequestAmount DECIMAL(11,8) NOT NULL,		
		FinalResult DECIMAL(11,8) NOT NULL
	)  ON [PRIMARY]
	
ALTER TABLE dbo.SimulationQuote 
ADD CONSTRAINT
		PK_SimulationQuote PRIMARY KEY CLUSTERED 
		(
			SimulationQuoteId
		) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

ALTER TABLE dbo.SimulationQuote 
SET (LOCK_ESCALATION = TABLE)


-- #############################

CREATE TABLE dbo.SimulationQuoteValue
(
	SimulationQuoteValueId INT NOT NULL IDENTITY (1, 1),
	SimulationQuoteId INT NOT NULL,
	Amount DECIMAL(11,8) NOT NULL,
	UsdValue INT NOT NULL
)ON [PRIMARY]

ALTER TABLE dbo.SimulationQuoteValue 
	ADD CONSTRAINT
		PK_SimulationQuoteValueId PRIMARY KEY CLUSTERED 
		(
			SimulationQuoteValueId
		) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

ALTER TABLE dbo.SimulationQuoteValue 
	ADD CONSTRAINT
		FK_SimulationQuote_SimulationQuoteValue FOREIGN KEY
		(
			SimulationQuoteId
		) REFERENCES dbo.SimulationQuote
		(
			SimulationQuoteId
		) ON UPDATE  NO ACTION 
		 ON DELETE  NO ACTION 

ALTER TABLE dbo.SimulationQuoteValue 
SET (LOCK_ESCALATION = TABLE)





