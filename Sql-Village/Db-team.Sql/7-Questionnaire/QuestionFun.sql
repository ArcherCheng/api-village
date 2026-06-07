
-------------------------------------------------------------------
--問卷調查主檔--Questionnaire-新增資料
select * from PartyMember
select * from Questionnaire
insert into Questionnaire(PartyId,MemberId,DiscountDate,DiscountType,DiscountAmt,IsUsed)
select partyId,MemberId,ApplyDate,'$',60,0 from PartyMember
--------------------------------------------------------------------
--問卷調查回答檔--QuestionnaireAnswer--建立隨機
insert into QuestionnaireAnswer(QuestionnaireId,QuestionId,AnswerId)
select a.QuestionnaireId,b.questionId,dbo.udfRandQa(b.QuestionId)
From Questionnaire a, Question b
go
------------------------------------------------------------------
select * from PartyMember
select * from Question
select * from QuestionAnswer
select * from Questionnaire
select * from QuestionnaireAnswer
------------------------------------------------------------------------------
drop View Temp_Rand_AnswerId
go
Create View Temp_Rand_AnswerId as
select a.QuestionId,a.QuestionDesc,
(select top 1 AnswerId from QuestionAnswer c where c.QuestionId = a.QuestionId order by NEWID()) as AnswerId
from Question a
GO

select * from Temp_Rand_AnswerId
go
select * from Temp_Rand_AnswerId
go
select * from Temp_Rand_AnswerId
go


drop function dbo.UdfRandQA
go
CREATE FUNCTION dbo.UdfRandQA (@Id uniqueidentifier)
RETURNS uniqueidentifier
AS
BEGIN
	DECLARE @AnswerId uniqueidentifier;
	SELECT TOP 1 @AnswerId = AnswerId FROM Temp_Rand_AnswerId where QuestionId = @Id ;
	Return @AnswerId;
END;
GO


----------------------------------------------------------------
-- Drop PROCEDURE  dbo.RandQA
-- go

-- CREATE PROCEDURE  dbo.RandQA (@Id uniqueidentifier)
-- AS
-- BEGIN
-- 	IF EXISTS(SELECT * FROM sys.tables WHERE SCHEMA_NAME(schema_id) LIKE 'dbo' AND name like 'Temp_Rand_Qa')
-- 	   DROP TABLE [dbo].[Temp_Rand_Qa];

-- 	DECLARE @AnswerId uniqueidentifier;
-- 	SELECT TOP 1 * Into Temp_Rand_Qa FROM QuestionAnswer where QuestionId = @Id ORDER BY NEWID();
-- END;
-- GO

EXEC dbo.RandQA '3D186245-1B84-ED11-8944-18C04D10AECF';
select * from Temp_Rand_Qa;

EXEC dbo.RandQA '3D186245-1B84-ED11-8944-18C04D10AECF';
select * from Temp_Rand_Qa;

EXEC dbo.RandQA '3D186245-1B84-ED11-8944-18C04D10AECF';
select * from Temp_Rand_Qa;

--------------------------------------------------------------------
select NEWID();
select NEWID();
select NEWID();

-------------------------------------------------------------------=
DECLARE @id uniqueidentifier;
Set @Id = '3D186245-1B84-ED11-8944-18C04D10AECF';
DECLARE @AnswerId uniqueidentifier;
SELECT  TOP 1 @AnswerId = AnswerId FROM QuestionAnswer where QuestionId = @id ORDER BY NEWID();
select @AnswerId;
SELECT  TOP 1 @AnswerId = AnswerId FROM QuestionAnswer where QuestionId = @id ORDER BY NEWID();
select @AnswerId;
SELECT  TOP 1 @AnswerId = AnswerId FROM QuestionAnswer where QuestionId = @id ORDER BY NEWID();
select @AnswerId;
SELECT  TOP 1 @AnswerId = AnswerId FROM QuestionAnswer where QuestionId = @id ORDER BY NEWID();
select @AnswerId;

-------------------------------------------------------------------------------------------
select a.QuestionDesc,
(select top 1 AnswerDesc from QuestionAnswer c where c.QuestionId = a.QuestionId order by NEWID()) as AnswerDesc
from Question a

select a.QuestionDesc,
(select top 1 AnswerDesc from QuestionAnswer c where c.QuestionId = a.QuestionId order by NEWID()) as AnswerDesc
from Question a

select a.QuestionDesc,
(select top 1 AnswerDesc from QuestionAnswer c where c.QuestionId = a.QuestionId order by NEWID()) as AnswerDesc
from Question a

select a.QuestionDesc,
(select top 1 AnswerDesc from QuestionAnswer c where c.QuestionId = a.QuestionId order by NEWID()) as AnswerDesc
from Question a

---------------------------------------------------------
