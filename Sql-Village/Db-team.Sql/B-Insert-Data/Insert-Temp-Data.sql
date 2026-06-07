select * from temp_partyData


insert into PartyData(PartyName,PartyDate,BeginTime,EndTime,Marry
,BoyAmt,BoySchoolLevel,BoyAge1,BoyAge2,BoyPersons
,GirlAmt,GirlSchoolLevel,GirlAge1,GirlAge2,GirlPersons
,EarlyDate,EarlyBoyAmt,EarlyGirlAmt,TwoGirlsAmt
,MaxVote,PictureUrl,Restaurant,AddressNote,BusNote,IsOnOff,Notes)
select PartyName,PartyDate,BeginTime,EndTime,Marry
,BoyAmt,BoySchoolLevel,BoyAge1,BoyAge2,BoyPersons
,GirlAmt,GirlSchoolLevel,GirlAge1,GirlAge2,GirlPersons
,EarlyDate,EarlyBoyAmt,EarlyGirlAmt,TwoGirlsAmt
,MaxVote,PictureUrl,Restaurant,AddressNote,BusNote,IsOnOff,Notes
From temp_partyData

select * from AppUser
go
select * from au1User
go
insert into au1user (userName,phone,Email,Birthday,isonoff,userType,passwordHash,passwordSalt)
select userName,phone,Email,Birthday,isonoff,userType,passwordHash,passwordSalt from AppUser
go


