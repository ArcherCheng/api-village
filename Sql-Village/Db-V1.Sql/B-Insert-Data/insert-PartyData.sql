use Party2022
select * from PartyData
go
select * from PartyMember
go
select * from MemberData
go
select * from AppUser
go

use Party2025
GO
select * from PartyData
go
select * from PartyMember
go
select * from MemberData
go
select * from AppUser
go

select * from ViewPartySummary
go

/*
insert into PartyData(PartyId,PartyName,PartyDate,BeginTime,EndTime,Marry
,BoyAmt,BoySchoolLevel,BoyAge1,BoyAge2,BoyPersons
,GirlAmt,GirlSchoolLevel,GirlAge1,GirlAge2,GirlPersons
,EarlyDate,EarlyBoyAmt,EarlyGirlAmt,TwoGirlsAmt,PictureUrl
,Restaurant,IsOnOff,MaxVote)
select PartyId,PartyName,PartyDate,BeginTime,EndTime,Marry
,BoyAmt,BoySchoolLevel,BoyAge1,BoyAge2,BoyPersons
,GirlAmt,GirlSchoolLevel,GirlAge1,GirlAge2,GirlPersons
,EarlyDate,EarlyBoyAmt,EarlyGirlAmt,TwoGirlsAmt,PictureUrl
,Restaurant,IsOnOff,MaxVote
from Party2022.dbo.PartyData
GO
*/
/*
insert into AppUser(UserId,UserName,MobileTel,Email,Birthday,UserRole,UserData,UserType,UserCode,PasswordHash,PasswordSalt,PasswordChangeDate,IsOnOff)
select UserId,UserName,Phone,Email,Birthday,'users',UserData,0,0,PasswordHash,PasswordSalt,GETDATE(),1
from Party2022.dbo.AppUser
go
insert into MemberData(MemberId,FirstName,LastName,NickName,BirthYear,Sex,Marry,SchoolLevel,Heights,Weights,Blood
,Star,City,WorkType,Religion,Salary,Introduction,LikeCondition)
select MemberId,FirstName,LastName,NickName,BirthYear,Sex,Marry,SchoolLevel,Heights,Weights,Blood
,Star,City,WorkType,Religion,Salary,Introduction,LikeCondition
from Party2022.dbo.MemberData
go
*/
/*
insert into PartyMember(PartyId,MemberId,ApplyDate,PartyAmt)
select PartyId,MemberId,ApplyDate,PartyAmt
From Party2022.dbo.PartyMember
*/