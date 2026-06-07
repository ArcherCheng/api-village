
IF EXISTS(SELECT 1 FROM sys.views WHERE name = 'ViewCity') DROP VIEW ViewCity
go


create view ViewCity as
select NationId,CityId,City,CityOrder
from Au1Team
group by NationId,CityId,City,CityOrder
go

select * from ViewCity order by CityOrder
go

IF EXISTS(SELECT 1 FROM sys.views WHERE name = 'ViewTown') Drop view ViewTown
go

create view ViewTown as
select NationId,CityId,City,TownId,Town,TownOrder
from Au1Team
group by NationId,CityId,City,TownId,Town,TownOrder
go


select * from ViewTown order by city,TownOrder
go