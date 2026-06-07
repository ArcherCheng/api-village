use MoonGodModel
delete QuestionOption
go
delete Question
go
delete QuestionSubject
go
-------------------------------------------------------------------------------
insert into QuestionSubject(SubjectDesc,IsOnOff)
select '室內交友聯誼活動問卷調查',1
go

-------------------------------------------------------------------------------
insert into Question(SubjectId,QuestionDesc,SortOrder,IsOnOff)
select SubjectId,'請問您從何處知道此活動',1,1 from QuestionSubject order by SubjectId Offset 0 Rows Fetch Next 1 Rows Only
go

insert into QuestionOption(QuestionId,OptionDesc,SortOrder,IsOnOff)
select top 1 QuestionId,'網路搜尋(如:Google、Yahoo...)',1,1 from Question
go
insert into QuestionOption(QuestionId,OptionDesc,SortOrder,IsOnOff)
select top 1 QuestionId,'網路廣告(如:FaceBook、YouTube...)',2,1 from Question
go
insert into QuestionOption(QuestionId,OptionDesc,SortOrder,IsOnOff)
select top 1 QuestionId,'親友告知',3,1 from Question
go
insert into QuestionOption(QuestionId,OptionDesc,SortOrder,IsOnOff)
select top 1 QuestionId,'廣告傳單',4,1 from Question
go
insert into QuestionOption(QuestionId,OptionDesc,SortOrder,IsOnOff)
select top 1 QuestionId,'其他',5,1 from Question
go

-------------------------------------------------------------------------------
insert into Question(SubjectId,QuestionDesc,SortOrder,IsOnOff)
select SubjectId,'請問您此活動有符合您的對象嗎?',1,1 from QuestionSubject order by SubjectId Offset 0 Rows Fetch Next 1 Rows Only
go

insert into QuestionOption(QuestionId,OptionDesc,SortOrder,IsOnOff)
select QuestionId,'有，3個以上',1,1 from Question order by QuestionId Offset 1 Rows Fetch Next 1 Rows Only
go
insert into QuestionOption(QuestionId,OptionDesc,SortOrder,IsOnOff)
select QuestionId,'有，1-3個',2,1 from Question order by QuestionId Offset 1 Rows Fetch Next 1 Rows Only
go
insert into QuestionOption(QuestionId,OptionDesc,SortOrder,IsOnOff)
select QuestionId,'沒有，但有幾個可以先做普通朋友開始',3,1 from Question order by QuestionId Offset 1 Rows Fetch Next 1 Rows Only
go
insert into QuestionOption(QuestionId,OptionDesc,SortOrder,IsOnOff)
select QuestionId,'沒有，連做普通朋友也不想要',4,1 from Question order by QuestionId Offset 1 Rows Fetch Next 1 Rows Only
go

-------------------------------------------------------------------------------
insert into Question(SubjectId,QuestionDesc,SortOrder,IsOnOff)
select SubjectId,'請問您會再來參加此活動',1,1 from QuestionSubject order by SubjectId Offset 0 Rows Fetch Next 1 Rows Only
go

insert into QuestionOption(QuestionId,OptionDesc,SortOrder,IsOnOff)
select QuestionId,'會再來，因為確實有機會認識其他異性',1,1 from Question order by QuestionId Offset 2 Rows Fetch Next 1 Rows Only
go
insert into QuestionOption(QuestionId,OptionDesc,SortOrder,IsOnOff)
select QuestionId,'不會再來，因為找不到符合的異性',2,1 from Question order by QuestionId Offset 2 Rows Fetch Next 1 Rows Only
go
insert into QuestionOption(QuestionId,OptionDesc,SortOrder,IsOnOff)
select QuestionId,'不一定，若心情或時間允許可能還會再來試試看',3,1 from Question order by QuestionId Offset 2 Rows Fetch Next 1 Rows Only
go

-------------------------------------------------------------------------------
insert into Question(SubjectId,QuestionDesc,SortOrder,IsOnOff)
select SubjectId,'請問您對此次活動滿意嗎?',1,1 from QuestionSubject order by SubjectId Offset 0 Rows Fetch Next 1 Rows Only
go

insert into QuestionOption(QuestionId,OptionDesc,SortOrder,IsOnOff)
select QuestionId,'非常滿意',1,1 from Question order by QuestionId Offset 3 Rows Fetch Next 1 Rows Only
go
insert into QuestionOption(QuestionId,OptionDesc,SortOrder,IsOnOff)
select QuestionId,'滿意',2,1 from Question order by QuestionId Offset 3 Rows Fetch Next 1 Rows Only
go
insert into QuestionOption(QuestionId,OptionDesc,SortOrder,IsOnOff)
select QuestionId,'普通',3,1 from Question order by QuestionId Offset 3 Rows Fetch Next 1 Rows Only
go
insert into QuestionOption(QuestionId,OptionDesc,SortOrder,IsOnOff)
select QuestionId,'不滿意',4,1 from Question order by QuestionId Offset 3 Rows Fetch Next 1 Rows Only
go
insert into QuestionOption(QuestionId,OptionDesc,SortOrder,IsOnOff)
select QuestionId,'非常不滿意',5,1 from Question order by QuestionId Offset 3 Rows Fetch Next 1 Rows Only
go


-------------------------------------------------------------------------------

select * from QuestionSubject
go
select * from Question
go
select * from QuestionOption
go

select * from QuestionAnswer
go