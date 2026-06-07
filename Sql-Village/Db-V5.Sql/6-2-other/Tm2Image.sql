-----------------------------------------------------------------------------------------------------------------------
-- 圖片資料檔
/*
DROP TABLE Pt2Image;
go
*/
-----------------------------------------------------------------------------------------------------------------------
CREATE TABLE Pt2Image
(
	Id         int IDENTITY(1,1) NOT NULL,
	FromId     UniqueIdentifier NOT NULL,
	FromTable  NVARCHAR(100) ,
	ImageUrl   NVARCHAR(200) ,
	SortOrder INT NOT NULL DEFAULT 0,
	Notes      nvarchar(200),
	-- ImageBase64 VARCHAR(max),  --must use varchar(max), not NVARCHAR(4000)
	-- ImageImage image,

	-- 以下每檔資料表都會有這些欄位
	WriteInfo NVARCHAR(100),
	CONSTRAINT Pt2Image_PrimaryKey PRIMARY KEY CLUSTERED (Id),
);
GO

-- CREATE UNIQUE CLUSTERED INDEX Pt2Image_AutoId ON Pt2Image(AutoId);
-- go
 CREATE INDEX Pt2Image_FromId ON Pt2Image(FromId);
 GO


-----------------------------------------------------------------------------------------------------------------------
-- Like資料檔
/*
DROP TABLE Pt2Like;
go
*/
-----------------------------------------------------------------------------------------------------------------------
CREATE TABLE Pt2Like
(
	Id         int IDENTITY(1,1) NOT NULL,
	UserId     UniqueIdentifier NOT NULL,
	FromId     UniqueIdentifier NOT NULL,
	-- FromTable  NVARCHAR(100) ,
	-- LikeTimes INT NOT NULL DEFAULT 0,

	-- 以下每檔資料表都會有這些欄位
	WriteInfo NVARCHAR(100),
	CONSTRAINT Pt2Like_PrimaryKey PRIMARY KEY CLUSTERED (Id),
);
GO

-- CREATE UNIQUE CLUSTERED INDEX Pt2Like_AutoId ON Pt2Like(AutoId);
-- go
CREATE UNIQUE INDEX Pt2Like_FromId ON Pt2Like(FromId,UserId);
GO




-----------------------------------------------------------------------------------------------------------------------
CREATE TABLE Pt2UnLike
(
	Id         int IDENTITY(1,1) NOT NULL,
	UserId     UniqueIdentifier NOT NULL,
	FromId     UniqueIdentifier NOT NULL,
	-- FromTable  NVARCHAR(100) ,
	-- LikeTimes INT NOT NULL DEFAULT 0,

	-- 以下每檔資料表都會有這些欄位
	WriteInfo NVARCHAR(100),
	CONSTRAINT Pt2UnLike_PrimaryKey PRIMARY KEY CLUSTERED (Id),
);
GO

-- CREATE UNIQUE CLUSTERED INDEX Pt2Like_AutoId ON Pt2Like(AutoId);
-- go
CREATE UNIQUE INDEX Pt2Like_FromId ON Pt2UnLike(FromId,UserId);
GO


