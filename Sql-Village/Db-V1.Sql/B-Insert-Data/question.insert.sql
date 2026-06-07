
insert into QuestionDiscount(MinAmount,MaxAmount,DiscountType,DiscountAmt,IsOnOff)
values(500,700,'$',50,1);
insert into QuestionDiscount(MinAmount,MaxAmount,DiscountType,DiscountAmt,IsOnOff)
values(700,1000,'$',100,1);
go
select * from questionDiscount
go
-----------------------------------------------------------------

insert into question(QuestionDesc, IsOnOff, SortOrder)
values('聯誼次數', 1, 1)
go
select * from question
go

DECLARE @questionId UniqueIdentifier;
select @questionId = questionId from question where QuestionDesc='聯誼次數' ;

insert into QuestionAnswer(QuestionId, AnswerDesc, SortOrder)
values(@questionId,'1 次',1)
insert into QuestionAnswer(QuestionId, AnswerDesc, SortOrder)
values(@questionId,'2-4 次',2)
insert into QuestionAnswer(QuestionId, AnswerDesc, SortOrder)
values(@questionId,'5-9 次',3)
insert into QuestionAnswer(QuestionId, AnswerDesc, SortOrder)
values(@questionId,'10 次以上',4)
insert into QuestionAnswer(QuestionId, AnswerDesc, SortOrder)
values(@questionId,'來過，但忘記幾次了',5)
go
select * from question
select * from questionAnswer
go

insert into question(QuestionDesc, IsOnOff, SortOrder)
values('參加人數', 1, 2)
go
DECLARE @questionId UniqueIdentifier;
select @questionId = questionId from question where QuestionDesc='參加人數' ;

insert into QuestionAnswer(QuestionId, AnswerDesc, SortOrder)
values(@questionId,'自己一個人來',1)
insert into QuestionAnswer(QuestionId, AnswerDesc, SortOrder)
values(@questionId,'有找朋友並有一起來',2)
insert into QuestionAnswer(QuestionId, AnswerDesc, SortOrder)
values(@questionId,'有找朋友但朋友沒有來',2)
go
select * from question
select * from questionAnswer
go


insert into question(QuestionDesc, IsOnOff, SortOrder)
values('訊息管道', 1, 4)
go
DECLARE @questionId UniqueIdentifier;
select @questionId = questionId from question where QuestionDesc='訊息管道' ;
insert into QuestionAnswer(QuestionId, AnswerDesc, SortOrder)
values(@questionId,'Google網路搜尋',1)
insert into QuestionAnswer(QuestionId, AnswerDesc, SortOrder)
values(@questionId,'FaceBook社群看到',2)
insert into QuestionAnswer(QuestionId, AnswerDesc, SortOrder)
values(@questionId,'朋友介紹',3)
insert into QuestionAnswer(QuestionId, AnswerDesc, SortOrder)
values(@questionId,'宣傳單廣告',4)
insert into QuestionAnswer(QuestionId, AnswerDesc, SortOrder)
values(@questionId,'以前就知道',5)
insert into QuestionAnswer(QuestionId, AnswerDesc, SortOrder)
values(@questionId,'其他',6)
go
select * from question
select * from questionAnswer
go

insert into question(QuestionDesc, IsOnOff, SortOrder)
values('聯誼滿意度', 1, 3)
go
DECLARE @questionId UniqueIdentifier;
select @questionId = questionId from question where QuestionDesc='聯誼滿意度' ;
insert into QuestionAnswer(QuestionId, AnswerDesc, SortOrder)
values(@questionId,'非常滿意',1)
insert into QuestionAnswer(QuestionId, AnswerDesc, SortOrder)
values(@questionId,'滿意',2)
insert into QuestionAnswer(QuestionId, AnswerDesc, SortOrder)
values(@questionId,'一般',3)
insert into QuestionAnswer(QuestionId, AnswerDesc, SortOrder)
values(@questionId,'不滿意',4)
insert into QuestionAnswer(QuestionId, AnswerDesc, SortOrder)
values(@questionId,'非常不滿意',5)

select * from question
select * from questionAnswer
go




