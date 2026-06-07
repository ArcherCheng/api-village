--------------------------------------------------------
-- Au1Role
--------------------------------------------------------
delete from Au1Role
go
insert into Au1Role(RoleId,RoleName,IsOnOff,SortOrder)
values('Users','一般用戶角色', 1, 10);
insert into Au1Role(RoleId,RoleName,IsOnOff,SortOrder)
values('Operators','系統操作員角色', 1, 30);
insert into Au1Role(RoleId,RoleName,IsOnOff,SortOrder)
values('Managers','系統管理員角色', 1, 40);
insert into Au1Role(RoleId,RoleName,IsOnOff,SortOrder)
values('Admins','系統管理員角色', 1, 40);
go

--------------------------------------------------------
-- AppUser
--------------------------------------------------------
-- insert into AppUser(UserId,UserName,Phone,Email,Birthday,IsOnOff,IsAdmin)
-- values('0001','0001','097092288','a0970922888@gmail.com','2000-1-1',1,1);
-- go






