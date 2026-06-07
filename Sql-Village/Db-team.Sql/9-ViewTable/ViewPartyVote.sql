-----------------------------------------------------------------------------------------------------------------
drop view ViewPartyVote
go
create view ViewPartyVote as
SELECT a.Id,a.PartyId,a.VoteId,a.LikerId,a.VoteDate,
	x1.PartyName,x1.PartyDate,
	y1.MemberName LikeMemberName,y1.PhotoUrl LikePhotoUrl,
	y2.MemberName VoteMemberName,y2.PhotoUrl VotePhotoUrl
FROM PartyVote a
INNER JOIN PartyData x1 ON a.PartyId = x1.PartyId
INNER JOIN MemberData y1 ON a.LikerId = y1.MemberId
INNER JOIN MemberData y2 ON a.VoteId = y2.MemberId
go
-- select * from ViewPartyMember
-- go
-----------------------------------------------------------------------------------------------------------------


---------------------------------------------------------------------------------------------------------------
--統計每場總投票數
drop view ViewPartyVoteTotalVotes
go
create view ViewPartyVoteTotalVotes as
select PartyId,count(PartyId) TotalVotes
from PartyVote
group by PartyId
go

-- Select * from ViewPartyVoteTotalVotes
-- go

---------------------------------------------------------------------------------------------------------------
--統計每場男生總投票數
drop view ViewPartyVoteTotalBoyVotes
go
create view ViewPartyVoteTotalBoyVotes as
select a.PartyId,count(a.PartyId) TotalBoyVotes
from PartyVote a,MemberData b
where a.VoteId = b.MemberId and b.Sex=1
group by PartyId
go

-- Select * from ViewPartyVoteTotalBoyVotes
-- go
---------------------------------------------------------------------------------------------------------------
---------------------------------------------------------------------------------------------------------------
--統計每場男生總投票數
drop view ViewPartyVoteTotalGirlVotes
go
create view ViewPartyVoteTotalGirlVotes as
select a.PartyId,count(a.PartyId) TotalGirlVotes
from PartyVote a,MemberData b
where a.VoteId = b.MemberId and b.Sex=2
group by PartyId
go

-- Select * from ViewPartyVoteTotalGirlVotes
-- go
---------------------------------------------------------------------------------------------------------------


--統計每場成功配對人數
drop view ViewPartyVoteTotalMatches
go
create view ViewPartyVoteTotalMatches as
select a.PartyId,count(a.PartyId) TotalMatches
from PartyVote a ,PartyVote b
where a.LikerId = b.VoteId and a.VoteId = b.LikerId and a.PartyId = b.PartyId
group by a.PartyId
go

-- Select * from ViewPartyVoteTotalMatches
-- go
---------------------------------------------------------------------------------------------------------------
--配對成功人員名單
drop view ViewPartyVoteMatchMembers
go

create view ViewPartyVoteMatchMembers as
select a.PartyId,a.VoteId,a.LikerId,a1.MemberName VoteName, a2.MemberName LikerName
from PartyVote a ,PartyVote b, MemberData a1, MemberData a2
where a.LikerId = b.VoteId and a.VoteId = b.LikerId and b.PartyId = a.PartyId
and a.VoteId = a1.MemberId and a.LikerId=a2.MemberId
go

-- select * from ViewPartyVoteMatchMembers
-- go

---------------------------------------------------------------------------------------------------------------
--投票每人得票數名單
Drop view ViewPartyVoteLikerCounts
go

create view ViewPartyVoteLikerCounts as
select a.PartyId,a.LikerId,b.Sex,b.MemberName,b.PhotoUrl,count(a.id) Counts
from PartyVote a
INNER join MemberData b ON a.LikerId = b.MemberId
group by a.partyId,a.LikerId,b.sex,b.MemberName,b.PhotoUrl
go

-- select * from ViewPartyVoteLikerCounts
-- go
---------------------------------------------------------------------------------------------------------------