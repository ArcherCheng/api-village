DECLARE @databaseName varchar(100),
		@backupName varchar(100),
		@backupLocation varchar(100),
		@datetime varchar(8)
set @databaseName='Hr2024Ampoc2'
set @backupLocation='E:\db-backup\'
set @datetime=CONVERT(varchar(20),GETDATE(),112)
set @backupName=@backupLocation+@databaseName+'_'+@datetime+'.bak'
backup database @databaseName To Disk = @backupName


