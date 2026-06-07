/*
select * from Tm2QuizSubject;
select * from Tm2QuizQuestion;
select * from Tm2QuizOption;
select * from Tm2QuizSurvey;
select * from Tm2QuizAnswer;
select * from Tm2QuizDiscount;
go


-- drop table Tm2QuizDiscount;
-- go
drop table Tm2QuizAnswer;
go
drop table Tm2QuizSurvey;
go
drop table Tm2QuizOption;
go
drop table Tm2QuizQuestion;
go
drop table Tm2QuizSubject;
go


*/

----------------------------------------------------------
-- 問卷調查主題檔
-- Drop TABLE Tm2QuizSubject;
----------------------------------------------------------
CREATE TABLE Tm2QuizSubject(
	-- SubjectId   INT NOT NULL IDENTITY(1,1),
	SubjectId   UniqueIdentifier NOT NULL DEFAULT NewSequentialId(),
	TeamId      nvarchar(100) NOT NULL,
	Subject     nvarchar(200) NOT NULL,
	IsOnOff     bit NOT NULL,
	Notes       nvarchar(200) NULL,
	--以下每檔資料表都會有這些欄位
	WriteInfo  NVARCHAR(100),
	CONSTRAINT Tm2QuizSubject_PrimaryKey PRIMARY KEY CLUSTERED (SubjectId),
	CONSTRAINT Tm2QuizSubject_ref_Au1Team FOREIGN KEY (TeamId) REFERENCES Au1Team (TeamId) ON UPDATE CASCADE ON DELETE NO ACTION,
);
go



----------------------------------------------------------
-- 問卷調查題目檔
-- Drop TABLE Tm2QuizQuestion;
----------------------------------------------------------
CREATE TABLE Tm2QuizQuestion(
	-- QuestionId   INT NOT NULL IDENTITY(1,1),
	QuestionId   UniqueIdentifier NOT NULL DEFAULT NewSequentialId(),
	SubjectId    UniqueIdentifier NOT NULL,
	QuestionDesc nvarchar(200) NOT NULL,
	SortOrder    decimal(6,2) NOT NULL,
	IsOnOff      bit NOT NULL,
	Notes        nvarchar(200) NULL,
	--以下每檔資料表都會有這些欄位
	WriteInfo  NVARCHAR(100),
	CONSTRAINT Question_PrimaryKey PRIMARY KEY CLUSTERED (QuestionId),
	CONSTRAINT Question_Subject FOREIGN KEY (SubjectId) REFERENCES Tm2QuizSubject(SubjectId),
);
go

-- CREATE UNIQUE INDEX Question_QuestionDesc ON Question (QuestionDesc asc);
-- go


--------------------------------------------
-- 問卷調查題目回答選項明細檔
-- Drop TABLE Tm2QuizOption;
--------------------------------------------
go
CREATE TABLE Tm2QuizOption(
	-- OptionId   INT NOT NULL IDENTITY(1,1),
	OptionId   UniqueIdentifier NOT NULL DEFAULT NewSequentialId(),
	QuestionId UniqueIdentifier NOT NULL,
	OptionDesc nvarchar(200) NOT NULL,
	SortOrder  decimal(6,2) NOT NULL,
	IsOnOff    bit NOT NULL,
	--以下每檔資料表都會有這些欄位
	WriteInfo  NVARCHAR(100),
    CONSTRAINT QuestionOption_Question FOREIGN KEY (QuestionId) REFERENCES Tm2QuizQuestion(QuestionId),
    CONSTRAINT QuestionOption_PrimaryKey PRIMARY KEY CLUSTERED (OptionId),
);
go

CREATE UNIQUE INDEX QuestionOption_OptionDesc ON Tm2QuizOption (QuestionId asc, OptionDesc asc);
go


--------------------------------------------
-- 客戶回答問卷調查總檔
-- Drop TABLE Tm2QuizSurvey;
--------------------------------------------
go
CREATE TABLE Tm2QuizSurvey(
	-- SurveyId        INT NOT NULL IDENTITY(1,1),
	SurveyId        UniqueIdentifier NOT NULL DEFAULT NewSequentialId(),
	TeamId          nvarchar(100) NOT NULL,
	UserId          UniqueIdentifier NOT NULL,
	SubjectId       UniqueIdentifier NOT NULL,
	DiscountType    NVARCHAR(1) NOT NULL,  -- 1=$,2=%
	DiscountValue   int NOT NULL,
 	DiscountDate    DateTime NOT NULL, --折扣有效日期
    IsUsed          bit NOT NULL,  --是否使用
    UseDate         DateTime null,  --是否使用
	Suggestions     NVARCHAR(1000), --客戶建議意見
	-- 以下每檔資料表都會有這些欄位
	WriteInfo  NVARCHAR(100),
    CONSTRAINT Tm2QuizSurvey_Au1Team FOREIGN KEY (TeamId) REFERENCES Au1Team(TeamId),
    CONSTRAINT Tm2QuizSurvey_Au1User FOREIGN KEY (UserId) REFERENCES Au1User(UserId),
    CONSTRAINT Tm2QuizSurvey_Tm2QuizSubject FOREIGN KEY (SubjectId) REFERENCES Tm2QuizSubject(SubjectId),
    CONSTRAINT Tm2QuizSurvey_PrimaryKey PRIMARY KEY CLUSTERED (SurveyId),
);
go
-- -- 確定每一張訂單只能問卷調查一次
--CREATE UNIQUE INDEX Tm2QuizSurvey_PartyMember ON Tm2QuizSurvey (TeamId asc, UserId asc) --WHERE Name is NOT NULL ;
--go


--------------------------------------------
-- 客戶回答問卷調查明細檔
-- QuestionAnswer;
--------------------------------------------
go
CREATE TABLE Tm2QuizAnswer(
	AnswerId    Int NOT NULL IDENTITY(1,1),
	-- AnswerId    UniqueIdentifier NOT NULL DEFAULT NewSequentialId(),
	OptionId    UniqueIdentifier NOT NULL,
	SurveyId    UniqueIdentifier NOT NULL,

	-- 以下每檔資料表都會有這些欄位
	WriteInfo  NVARCHAR(100),
    CONSTRAINT Tm2QuizAnswer_Tm2QuizOption FOREIGN KEY (OptionId) REFERENCES Tm2QuizOption(OptionId),
    CONSTRAINT Tm2QuizAnswer_Tm2QuizSurvey FOREIGN KEY (SurveyId) REFERENCES Tm2QuizSurvey(SurveyId),
    CONSTRAINT Tm2QuizAnswer_PrimaryKey PRIMARY KEY CLUSTERED (AnswerId),
);
go
CREATE INDEX Tm2QuizAnswer_OptionId ON Tm2QuizAnswer (OptionId asc, SurveyId asc) --WHERE Name is NOT NULL ;
go


-- ----------------------------------------------------------
-- -- 問卷調查折扣檔 Coupon
-- -- Drop TABLE Tm2QuizDiscount;
-- ----------------------------------------------------------
-- go
-- CREATE TABLE Tm2QuizDiscount(
-- 	Id            INT PRIMARY KEY IDENTITY(1,1),
-- 	-- Id            UniqueIdentifier NOT NULL DEFAULT NewSequentialId(),
-- 	MinAmount     int NOT NULL,
-- 	MaxAmount     int NOT NULL,
-- 	DiscountType  tinyint  NOT NULL,  -- 1=$,2=%
-- 	DiscountValue int NOT NULL, --amount or percent
-- 	IsOnOff       bit NOT NULL,
-- 	Notes         nvarchar(120) NULL,
-- 	--以下每檔資料表都會有這些欄位
-- 	WriteInfo  NVARCHAR(100),
--     -- CONSTRAINT Tm2QuizDiscount_PrimaryKey PRIMARY KEY CLUSTERED (Id),
-- 	CONSTRAINT Tm2QuizDiscount_Check_Amount Check (MaxAmount > MinAmount),
-- );
-- go

