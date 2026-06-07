use Party2025
go
select * from MemberData
go
select * from PartyData
go
select * from PartyChatGroup
go
insert into PartyChatGroup(PartyId,SenderId,SendContents,SendDateTime)
select a.PartyId, b.SenderId,b.SendContents+CONVERT(nvarchar(20),ROW_NUMBER() over (order by a.partyId)) ,GETDATE() From PartyData a, PartyChatGroup b
where  a.PartyId not in (select PartyId from PartyChatGroup)