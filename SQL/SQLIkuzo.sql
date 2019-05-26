CREATE TABLE Line (
    LineId varchar(30) NOT NULL,
    Description varchar(100) NOT NULL,
    LastUpdateDate [datetime] NOT NULL ,
    PRIMARY KEY (LineId)
); 

CREATE TABLE Modal (
    ModalId varchar(30) NOT NULL,
    LineId varchar(30) NOT NULL,
    LastUpdateDate [datetime] NOT NULL ,
    PRIMARY KEY (ModalId),
    FOREIGN KEY (LineId) REFERENCES Line(LineId)
); 

CREATE TABLE Gps (
    GpsGuid [uniqueidentifier] NOT NULL DEFAULT (newid()),
	[ModalId] [varchar](30) NOT NULL,
	[LineId] [varchar](30) NOT NULL,
	[Latitude] [decimal](12, 6) NOT NULL,
	[Longitude] [decimal](12, 6) NOT NULL,
	[Direction] [int] NOT NULL,
	[TimeStamp] [datetime] NOT NULL,
    LastUpdateDate [datetime] NOT NULL 
    PRIMARY KEY (GpsGuid)
); 

CREATE TABLE Itinerary (
    ItineraryGuid [uniqueidentifier] NOT NULL DEFAULT (newid()),    
	[Latitude] [decimal](12, 6) NOT NULL,
	[Longitude] [decimal](12, 6) NOT NULL,
    LineId varchar(30) NOT NULL,
    [Sequence] int NOT NULL,
    Returning bit NOT NULL,
    LastUpdateDate [datetime] NOT NULL ,
    PRIMARY KEY (ItineraryGuid),
    FOREIGN KEY (LineId) REFERENCES Line(LineId)
); 

CREATE TABLE Tag (
    TagId varchar(30) NOT NULL,
    LineId varchar(30) NOT NULL,
    TagName varchar(50) NULL,
    LastUpdateDate [datetime] NOT NULL ,
    PRIMARY KEY (TagId),
    FOREIGN KEY (LineId) REFERENCES Line(LineId)
); 

CREATE TABLE GpsHistory (
    GpsHistoryId [bigint] IDENTITY(1,1),
	[ModalId] [varchar](30) NOT NULL,
	[LineId] [varchar](30) NOT NULL,
	[Latitude] [decimal](12, 6) NOT NULL,
	[Longitude] [decimal](12, 6) NOT NULL,
	[Direction] [int] NOT NULL,
	[TimeStamp] [datetime] NOT NULL,
    LastUpdateDate [datetime] NOT NULL 
    PRIMARY KEY (GpsHistoryId)
); 
