
---------------------------------------------------------------------------------------------------------------
drop view ViewPartySummary
go
create view ViewPartySummary as
select a.PartyId,a.PartyName,a.PartyDate,a.BeginTime,a.EndTime,a.PartyCity,a.PartyMarry,
a.BoyAmt,a.BoySchoolLevel,a.BoyAge1,a.BoyAge2,a.BoyPersons,
a.GirlAmt,a.GirlSchoolLevel,a.GirlAge1,a.GirlAge2,a.GirlPersons,
a.EarlyDate,a.EarlyBoyAmt,a.EarlyGirlAmt,a.TwoGirlsAmt,
a.MaxVote,a.PictureUrl,a.Restaurant,a.AddressNote,a.BusNote,a.IsOnOff,a.Notes,
b1.TotalPersons,b1.AvgAges,b1.AvgSchoolLevel,b1.AvgSalary,b1.AvgHeights,b1.AvgWeights,
b2.TotalBoys,b2.BoyAvgAges,b2.BoyAvgSchoolLevel,b2.BoyAvgSalary,b2.BoyAvgHeights,b2.BoyAvgWeights,b2.BoyMaxSalary,b2.BoyMaxHeights,
b3.TotalGirls,b3.GirlAvgAges,b3.GirlAvgSchoolLevel,b3.GirlAvgSalary,b3.GirlAvgHeights,b3.GirlAvgWeights,b3.GirlMaxSalary,b3.GirlMaxHeights,
e1.TotalGroupChats,e2.TotalGroupBoyChats,e3.TotalGroupGirlChats,
e4.TotalOtherChats,e5.TotalOtherBoyChats,e6.TotalOtherGirlChats,
f1.TotalVotes,f2.TotalMatches,f3.TotalBoyVotes,f4.TotalGirlVotes,
g1.TotalPhotos
from PartyData a
left outer join ViewPartyMemberTotalPersons b1 ON a.PartyId = b1.PartyId
left outer join ViewPartyMemberTotalBoys b2 ON a.PartyId = b2.PartyId
left outer join ViewPartyMemberTotalGirls b3 ON a.PartyId = b3.PartyId
left outer join ViewPartyChatGroupTotalChats e1 ON a.PartyId = e1.PartyId
left outer join ViewPartyChatGroupTotalBoyChats e2 ON a.PartyId = e2.PartyId
left outer join ViewPartyChatGroupTotalGirlChats e3 ON a.PartyId = e3.PartyId
left outer join ViewPartyChatOtherTotalChats e4 ON a.PartyId = e4.PartyId
left outer join ViewPartyChatOtherTotalBoyChats e5 ON a.PartyId = e5.PartyId
left outer join ViewPartyChatOtherTotalGirlChats e6 ON a.PartyId = e6.PartyId
left outer join ViewPartyVoteTotalVotes f1 ON a.PartyId = f1.PartyId
left outer join ViewPartyVoteTotalMatches f2 ON a.PartyId = f2.PartyId
left outer join ViewPartyVoteTotalBoyVotes f3 ON a.PartyId = f3.PartyId
left outer join ViewPartyVoteTotalGirlVotes f4 ON a.PartyId = f4.PartyId
left outer join ViewPartyPhotoGroupTotalPhotos g1 ON a.PartyId = g1.PartyId
go

-- select * from ViewPartySummary
-- go



/*
drop function AF_PartyMemberSexCounts
go
create function AF_PartyMemberSexCounts(@as_partyId integer,@ai_sex integer)
returns integer
begin
  DECLARE @rc integer
  if (@ai_sex=1)
	select @rc=count(a.partyId) from PartyMember a ,memberData b where a.partyId=@as_partyId and a.MemberId=b.MemberId and b.Sex=1;
  else if (@ai_sex=2)
	select @rc=count(a.partyId) from PartyMember a ,memberData b where a.partyId=@as_partyId and a.MemberId=b.MemberId and b.Sex=2;
  else
	select @rc=count(a.partyId) from PartyMember a where a.partyId=@as_partyId;

  return @rc;
end
go

--Alter Table PartyData drop column TotalPersons
--Alter Table PartyData drop column BoyPersons
--Alter Table PartyData drop column GirlPersons
go
Alter Table PartyData add TotalPersons as dbo.AF_PartyMemberSexCounts(partyId,0)
go
Alter Table PartyData add BoyPersons as dbo.AF_PartyMemberSexCounts(partyId,1)
go
Alter Table PartyData add GirlPersons as dbo.AF_PartyMemberSexCounts(partyId,2)
go

*/

