
select * from PartyData
select * from PartyMember
select * from MemberData order by Sex
select * from AppUser order by MobileTel

select * from PartyChatGroup

select * from PartyChat
select * from PartyChatOther
GO



insert into partyChatGroup(PartyId,SenderId,SendContents)
values ('773c24e6-1059-ed11-893e-18c04d10aecf','FC4E96CE-0E59-ED11-893E-18C04D10AECF','test message1')
go
insert into partyChatGroup(PartyId,SenderId,SendContents)
values ('773c24e6-1059-ed11-893e-18c04d10aecf','FD4E96CE-0E59-ED11-893E-18C04D10AECF','test message2')
go
insert into partyChatGroup(PartyId,SenderId,SendContents)
values ('773c24e6-1059-ed11-893e-18c04d10aecf','FE4E96CE-0E59-ED11-893E-18C04D10AECF','test message3')
go

insert into partyChatGroup(PartyId,SenderId,SendContents)
values ('773c24e6-1059-ed11-893e-18c04d10aecf','C44F96CE-0E59-ED11-893E-18C04D10AECF','test message4')
go
insert into partyChatGroup(PartyId,SenderId,SendContents)
values ('773c24e6-1059-ed11-893e-18c04d10aecf','C54F96CE-0E59-ED11-893E-18C04D10AECF','test message5')
go
insert into partyChatGroup(PartyId,SenderId,SendContents)
values ('773c24e6-1059-ed11-893e-18c04d10aecf','C64F96CE-0E59-ED11-893E-18C04D10AECF','test message6')
go