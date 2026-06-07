/*
select * from QuestionSubject;
select * from Question;
select * from QuestionOption;
select * from QuestionSurvey;
select * from QuestionAnswer;
select * from QuestionDiscount;
go


drop table QuestionAnswer;
go
drop table QuestionSurvey;
go
drop table QuestionOption;
go
drop table Question;
go
drop table QuestionSubject;
go
drop table QuestionDiscount;
go
*/

----------------------------------------------------------
-- 問卷調查主題檔
-- Drop TABLE QuestionSubject;
----------------------------------------------------------
CREATE TABLE QuestionSubject(
	-- SubjectId   INT NOT NULL IDENTITY(1,1),
	SubjectId   UniqueIdentifier NOT NULL DEFAULT NewSequentialId(),
	SubjectDesc nvarchar(200) NOT NULL,
	IsOnOff     bit NOT NULL,
	Notes       nvarchar(200) NULL,
	--以下每檔資料表都會有這些欄位
	CreateUser NVARCHAR(100),
	UpdateUser NVARCHAR(100),
	BatchUser  NVARCHAR(100),
	CONSTRAINT QuestionSubject_PrimaryKey PRIMARY KEY CLUSTERED (SubjectId),
);
go



----------------------------------------------------------
-- 問卷調查題目檔
-- Drop TABLE Question;
----------------------------------------------------------
CREATE TABLE Question(
	-- QuestionId   INT NOT NULL IDENTITY(1,1),
	QuestionId   UniqueIdentifier NOT NULL DEFAULT NewSequentialId(),
	SubjectId    UniqueIdentifier NOT NULL,
	QuestionDesc nvarchar(200) NOT NULL,
	SortOrder    decimal(6,2) NOT NULL,
	IsOnOff      bit NOT NULL,
	Notes        nvarchar(200) NULL,
	--以下每檔資料表都會有這些欄位
	CreateUser NVARCHAR(100),
	UpdateUser NVARCHAR(100),
	BatchUser NVARCHAR(100),
	CONSTRAINT Question_PrimaryKey PRIMARY KEY CLUSTERED (QuestionId),
	CONSTRAINT Question_Subject FOREIGN KEY (SubjectId) REFERENCES QuestionSubject(SubjectId),
);
go

-- CREATE UNIQUE INDEX Question_QuestionDesc ON Question (QuestionDesc asc);
-- go


--------------------------------------------
-- 問卷調查題目回答選項明細檔
-- Drop TABLE QuestionOption;
--------------------------------------------
go
CREATE TABLE QuestionOption(
	-- OptionId   INT NOT NULL IDENTITY(1,1),
	OptionId   UniqueIdentifier NOT NULL DEFAULT NewSequentialId(),
	QuestionId UniqueIdentifier NOT NULL,
	OptionDesc nvarchar(200) NOT NULL,
	SortOrder  decimal(6,2) NOT NULL,
	IsOnOff    bit NOT NULL,
	--以下每檔資料表都會有這些欄位
	CreateUser NVARCHAR(100),
	UpdateUser NVARCHAR(100),
	BatchUser  NVARCHAR(100),
    CONSTRAINT QuestionOption_Question FOREIGN KEY (QuestionId) REFERENCES Question(QuestionId),
    CONSTRAINT QuestionOption_PrimaryKey PRIMARY KEY CLUSTERED (OptionId),
);
go

CREATE UNIQUE INDEX QuestionOption_OptionDesc ON QuestionOption (QuestionId asc, OptionDesc asc);
go


--------------------------------------------
-- 客戶回答問卷調查總檔
-- Drop TABLE QuestionSurvey;
--------------------------------------------
go
CREATE TABLE QuestionSurvey(
	-- SurveyId        INT NOT NULL IDENTITY(1,1),
	SurveyId        UniqueIdentifier NOT NULL DEFAULT NewSequentialId(),
	PartyId         UniqueIdentifier NOT NULL,
	MemberId        UniqueIdentifier NOT NULL,
	SubjectId       UniqueIdentifier NOT NULL,
	DiscountType    NVARCHAR(1) NOT NULL,  -- 1=$,2=%
	DiscountValue   int NOT NULL,
 	DiscountDate    DateTime NOT NULL, --折扣有效日期
    IsUsed          bit NOT NULL,  --是否使用
    UseDate         DateTime null,  --是否使用
	Suggestions     NVARCHAR(1000), --客戶建議意見
	-- 以下每檔資料表都會有這些欄位
	CreateUser NVARCHAR(100),
	UpdateUser NVARCHAR(100),
	BatchUser NVARCHAR(100),
    CONSTRAINT QuestionSurvey_PartyData FOREIGN KEY (PartyId) REFERENCES PartyData(PartyId),
    CONSTRAINT QuestionSurvey_MemberData FOREIGN KEY (MemberId) REFERENCES MemberData(MemberId),
    CONSTRAINT QuestionSurvey_QuestionSubject FOREIGN KEY (SubjectId) REFERENCES QuestionSubject(SubjectId),
    CONSTRAINT QuestionSurvey_PrimaryKey PRIMARY KEY CLUSTERED (SurveyId),
);
go
-- -- 確定每一張訂單只能問卷調查一次
CREATE UNIQUE INDEX QuestionSurvey_PartyMember ON QuestionSurvey (PartyId asc, MemberId asc) --WHERE Name is NOT NULL ;
go


--------------------------------------------
-- 客戶回答問卷調查明細檔
-- QuestionAnswer;
--------------------------------------------
go
CREATE TABLE QuestionAnswer(
	AnswerId    Int NOT NULL IDENTITY(1,1),
	-- AnswerId    UniqueIdentifier NOT NULL DEFAULT NewSequentialId(),
	OptionId    UniqueIdentifier NOT NULL,
	SurveyId    UniqueIdentifier NOT NULL,

	-- 以下每檔資料表都會有這些欄位
	CreateUser NVARCHAR(100),
	UpdateUser NVARCHAR(100),
	BatchUser  NVARCHAR(100),
    CONSTRAINT QuestionAnswer_QuestionOption FOREIGN KEY (OptionId) REFERENCES QuestionOption(OptionId),
    CONSTRAINT QuestionAnswer_QuestionSurvey FOREIGN KEY (SurveyId) REFERENCES QuestionSurvey(SurveyId),
    CONSTRAINT QuestionAnswer_PrimaryKey PRIMARY KEY CLUSTERED (AnswerId),
);
go
CREATE INDEX QuestionAnswer_OptionId ON QuestionAnswer (OptionId asc, SurveyId asc) --WHERE Name is NOT NULL ;
go


----------------------------------------------------------
-- 問卷調查折扣檔 Coupon
-- Drop TABLE QuestionDiscount;
----------------------------------------------------------
go
CREATE TABLE QuestionDiscount(
	Id            INT PRIMARY KEY IDENTITY(1,1),
	-- Id            UniqueIdentifier NOT NULL DEFAULT NewSequentialId(),
	MinAmount     int NOT NULL,
	MaxAmount     int NOT NULL,
	DiscountType  tinyint  NOT NULL,  -- 1=$,2=%
	DiscountValue int NOT NULL, --amount or percent
	IsOnOff       bit NOT NULL,
	Notes         nvarchar(120) NULL,
	--以下每檔資料表都會有這些欄位
	CreateUser NVARCHAR(100),
	UpdateUser NVARCHAR(100),
	BatchUser NVARCHAR(100),
    -- CONSTRAINT QuestionDiscount_PrimaryKey PRIMARY KEY CLUSTERED (Id),
	CONSTRAINT QuestionDiscount_Check_Amount Check (MaxAmount > MinAmount),
);
go

