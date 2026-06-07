-------------------------------------------------------------------------
--派對活動成員名單資料
-----------------------------------------------------------------------------------------------------------------
drop view ViewPartyMember
go
create view ViewPartyMember as
SELECT a.Id,a.PartyId,a.MemberId,a.ApplyDate,a.PartyAmt,a.IsWaiting
	,a.BankName,a.BankAccount,a.BankDate,a.BankAmt,a.BankNotes,a.FriendsName
	,a.IsBankCheck,a.CheckNotes
	,a.SeatNo,a.CheckOver,a.Notes
	,x1.PartyName,x1.PartyDate,x1.BeginTime,x1.EndTime,x1.PartyCity,x1.PartyMarry,x1.PictureUrl
	,x1.BoyAmt,x1.BoyAge1,x1.BoyAge2,x1.BoySchoolLevel,x1.BoyPersons
	,x1.GirlAmt,x1.GirlAge1,x1.GirlAge2,x1.GirlSchoolLevel,x1.GirlPersons
	,y1.FirstName,y1.LastName,y1.MemberName,y1.NickName
	,y1.Sex,y1.BirthYear,y1.Marry,y1.SchoolLevel
	,y1.Heights,y1.Weights,y1.BodyShape,y1.Salary
	,y1.Blood,y1.Star,y1.Country,y1.City
	,y1.SchoolYear,y1.SchoolName,y1.SchoolMajor
	,y1.WorkType,y1.WorkDep,y1.WorkPos,y1.WorkCity
	,y1.Religion,y1.ReligionStrong
	,y1.Personality,y1.Technique,y1.Interest
	,y1.Introduction,y1.LikeCondition
	,y1.IsMatchOnOff,y1.IsPhotoOnOff,y1.IsBlackMember,y1.IsJobCheck,y1.IsIdCheck,y1.IsSchoolCheck
	,y1.PhotoUrl,y1.IdPhoto11Url,y1.IdPhoto12Url,y1.JobPhotoUrl,y1.SchoolPhotoUrl
FROM PartyMember a
INNER JOIN PartyData x1 ON a.PartyId = x1.PartyId
INNER JOIN MemberData y1 ON a.MemberId = y1.MemberId
go
-- select * from ViewPartyMember
-- go

-------------------------------------------------------------------------
--活動統計資料
-------------------------------------------------------------------------
--統計每場活動報名人數及平均年齡(全部)
drop view ViewPartyMemberTotalPersons
go
create view ViewPartyMemberTotalPersons as
select a.PartyId,
  count(a.PartyId) TotalPersons,
  AVG(YEAR(c.PartyDate) - b.BirthYear) as AvgAges,
  AVG(b.SchoolLevel) as AvgSchoolLevel,
  AVG(b.Salary) as AvgSalary,
  AVG(b.Heights) as AvgHeights,
  AVG(b.Weights) as AvgWeights
from PartyMember a
INNER join MemberData b ON a.MemberId = b.MemberId
INNER join PartyData c ON a.PartyId = c.PartyId
group by a.PartyId
go
-- select * from ViewPartyMemberPersonSum
-- go

---------------------------------------------------------------------------------------------------------------
--統計每場活動報名人數及平均年齡(男生)
drop view ViewPartyMemberTotalBoys
go
create view ViewPartyMemberTotalBoys as
select a.PartyId,
  count(a.MemberId) TotalBoys,
  AVG(YEAR(c.PartyDate) - b.BirthYear) as BoyAvgAges,
  AVG(b.SchoolLevel) as BoyAvgSchoolLevel,
  AVG(b.Salary) as BoyAvgSalary,
  AVG(b.Heights) as BoyAvgHeights,
  AVG(b.Weights) as BoyAvgWeights,
  Max(b.Salary) as BoyMaxSalary,
  Max(b.Heights) as BoyMaxHeights
from PartyMember a
INNER join MemberData b ON a.MemberId = b.MemberId
INNER join PartyData c ON a.PartyId = c.PartyId
where b.sex = 1
group by a.PartyId
go
-- select * from ViewPartyMemberTotalBoys
-- go

---------------------------------------------------------------------------------------------------------------
--統計每場活動報名人數及平均年齡(女生)
drop view ViewPartyMemberTotalGirls
go
create view ViewPartyMemberTotalGirls as
select a.PartyId,
  count(a.MemberId) TotalGirls,
  AVG(YEAR(c.PartyDate) - b.BirthYear) as GirlAvgAges,
  AVG(b.SchoolLevel) as GirlAvgSchoolLevel,
  AVG(b.Salary) as GirlAvgSalary,
  AVG(b.Heights) as GirlAvgHeights,
  AVG(b.Weights) as GirlAvgWeights,
  Max(b.Salary) as GirlMaxSalary,
  Max(b.Heights) as GirlMaxHeights
from PartyMember a
INNER join MemberData b ON a.MemberId = b.MemberId
INNER join PartyData c ON a.PartyId = c.PartyId
where b.sex = 2
group by a.PartyId
go
-- select * from ViewPartyMemberTotalGirls
-- go

