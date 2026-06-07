rem https://learn.microsoft.com/zh-tw/sql/tools/sqlcmd/sqlcmd-utility?view=sql-server-ver16
rem https://learn.microsoft.com/zh-tw/sql/tools/sqlcmd/sqlcmd-utility?view=sql-server-ver16&tabs=odbc%2Cwindows&pivots=cs1-cmd


sqlcmd -E -S .\SqlExpress01 -d VillageModel -i \MyCode\Village\Db.Sql\2-1-Village\_Drop.Village.SQL
sqlcmd -E -S .\SqlExpress01 -d VillageModel -i \MyCode\Village\Db.Sql\1-3-Auth\_Drop.Auth.SQL
sqlcmd -E -S .\SqlExpress01 -d VillageModel -i \MyCode\Village\Db.Sql\1-2-key\_Drop.Key.SQL
sqlcmd -E -S .\SqlExpress01 -d VillageModel -i \MyCode\Village\Db.Sql\1-1-App\_Drop.App.SQL

pause
