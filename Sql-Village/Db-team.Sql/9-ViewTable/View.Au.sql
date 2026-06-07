------------------------------------------------------------------------------------------
drop view ViewAu1ActionGroupCtrllerId
go

CREATE VIEW ViewAu1ActionGroupCtrllerId as
select DISTINCT	CtrllerId,CtrllerDesc from Au1Action where ISNULL(CtrllerDesc,'')!=''
GO
------------------------------------------------------------------------------------------
drop view ViewAu2RoleActionX1
go

CREATE VIEW ViewAu2RoleActionX1 as
select 	a.RoleId,a.CtrlActnId,a.IsOnOff
    ,b.CtrllerId,b.ActionId,b.ActionDesc,b.HttpMethod,b.HttpRoute,b.IsRbacAuthorize
    ,b.SpaSystem,b.CtrllerDesc
    from Au2RoleAction a
    left join Au1Action b ON a.CtrlActnId = b.CtrlActnId

GO

------------------------------------------------------------------------------------------
drop view ViewAu1ComponentGroupSystemId
go

CREATE VIEW ViewAu1ComponentGroupSystemId as
select DISTINCT	SystemId,SystemDesc from Au1Component

GO
------------------------------------------------------------------------------------------
drop view ViewAu2RoleComponentX1
go

CREATE VIEW ViewAu2RoleComponentX1 as
select 	a.RoleId,a.ComponentId,a.IsOnOff
    ,b.SystemId,b.SubGroup,b.ComponentDesc,b.SortOrder
    from Au2RoleComponent a
    left join Au1Component b ON a.ComponentId = b.ComponentId
GO
