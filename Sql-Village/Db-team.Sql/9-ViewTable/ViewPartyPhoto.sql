-----------------------------------------------------------------------------------------------------------------
drop view ViewPartyPhoto
go

create view ViewPartyPhoto as
SELECT a.Id,a.PartyId,a.Descriptions,a.PhotoUrl,a.IsMain,a.IsShow,
	x1.PartyName,x1.PartyDate,x1.PictureUrl
FROM PartyPhoto a
INNER JOIN PartyData x1 ON a.PartyId = x1.PartyId
go

-- select * from ViewPartyPhoto
-- go

-----------------------------------------------------------------------------------------------------------------
drop view ViewPartyPhotoGroupTotalPhotos
go

create view ViewPartyPhotoGroupTotalPhotos as
SELECT a.PartyId,Count(a.PartyId) as TotalPhotos
FROM PartyPhoto a
group by a.PartyId
go