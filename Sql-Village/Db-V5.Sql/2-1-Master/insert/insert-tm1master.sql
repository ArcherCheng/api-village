select * from au1team
select * from tm1master
select * from Tm1MasterEducation
select * from Tm1MasterExperience
select * from Tm1MasterPolicy
select * from Tm1MasterPhoto
go



delete from Tm1MasterEducation
go
delete from Tm1MasterExperience
go
delete from Tm1MasterPolicy
go
delete from Tm1MasterPhoto
go
delete from Tm1Master
go



insert into Tm1Master(TeamId,MasterName,Description,Sex,Birthday,ElectYear,ElectDate,MobileTel,OfficeTel,Email,ServiceTime,Address,LineId,Facebook)
values('0970922888','鄭誠啟','認真負責','男','1964-12-1',2026,'2026-12-5','0970922888','03-3013566','a0970922888@gmail.com','AM:09:00 PM:22:00','桃園市桃園區中正路915號7F之四','0970922888','ArcherCheng')
go
insert into Tm1Master(TeamId,MasterName,Description,Sex,Birthday,ElectYear,ElectDate,MobileTel,OfficeTel,Email,ServiceTime,Address,LineId,Facebook)
values('0937452882','鄭誠啟','認真負責','男','1964-12-1',2026,'2026-12-5','0937452882','03-3013566','a0970922888@gmail.com','AM:09:00 PM:22:00','桃園市桃園區中正路915號7F之四','0970922888','ArcherCheng')
go
insert into Tm1Master(TeamId,MasterName,Description,Sex,Birthday,ElectYear,ElectDate,MobileTel,OfficeTel,Email,ServiceTime,Address,LineId,Facebook)
values('0931388546','鄭誠啟','認真負責','男','1964-12-1',2026,'2026-12-5','0970922888','03-3013566','a0970922888@gmail.com','AM:09:00 PM:22:00','桃園市桃園區中正路915號7F之四','0970922888','ArcherCheng')
go



insert into Tm1MasterEducation(TeamId,OrderNo,OrderTitle,Descriptions)
values('0970922888',1,'高中','武陵高中普通科');
go
insert into Tm1MasterEducation(TeamId,OrderNo,OrderTitle,Descriptions)
values('0970922888',2,'大學','中原大學-工業工程系');
go
insert into Tm1MasterEducation(TeamId,OrderNo,OrderTitle,Descriptions)
values('0970922888',3,'碩士','中興大學-企業管理系');
go
insert into Tm1MasterEducation(TeamId,OrderNo,OrderTitle,Descriptions)
values('0970922888',4,'博士','台灣大學-財務金融管理系');
go



insert into Tm1MasterExperience(TeamId,OrderNo,OrderTitle,Descriptions)
values('0970922888',1,'社區發展協會','2000 桃園市年蘆竹區山腳里社區發展協會理事長');
go
insert into Tm1MasterExperience(TeamId,OrderNo,OrderTitle,Descriptions)
values('0970922888',2,'里長','2000 桃園市年蘆竹區山腳里里長');
go
insert into Tm1MasterExperience(TeamId,OrderNo,OrderTitle,Descriptions)
values('0970922888',3,'氣功班','桃園市年蘆竹區山腳里氣功班');
go
insert into Tm1MasterExperience(TeamId,OrderNo,OrderTitle,Descriptions)
values('0970922888',4,'巡守隊隊長','桃園市年蘆竹區山腳里巡守隊隊長');
go


insert into Tm1MasterPolicy(TeamId,OrderNo,OrderTitle,Descriptions)
values('0970922888',1,'強化治安監控','強化治安監控');
go
insert into Tm1MasterPolicy(TeamId,OrderNo,OrderTitle,Descriptions)
values('0970922888',2,'推動長者照護','推動長者照護');
go
insert into Tm1MasterPolicy(TeamId,OrderNo,OrderTitle,Descriptions)
values('0970922888',3,'提升環境品質','提升環境品質');
go
insert into Tm1MasterPolicy(TeamId,OrderNo,OrderTitle,Descriptions)
values('0970922888',4,'爭取建設資源','爭取建設資源');
go


insert into Tm1MasterPhoto(TeamId,OrderNo,PhotoUrl,Descriptions)
values('0970922888',1,'https://picsum.photos/800/400?1','強化治安監控');
go
insert into Tm1MasterPhoto(TeamId,OrderNo,PhotoUrl,Descriptions)
values('0970922888',2,'https://picsum.photos/800/400?2','推動長者照護');
go
insert into Tm1MasterPhoto(TeamId,OrderNo,PhotoUrl,Descriptions)
values('0970922888',3,'https://picsum.photos/800/400?3','提升環境品質');
go
insert into Tm1MasterPhoto(TeamId,OrderNo,PhotoUrl,Descriptions)
values('0970922888',4,'https://picsum.photos/800/400?4','爭取建設資源');
go





