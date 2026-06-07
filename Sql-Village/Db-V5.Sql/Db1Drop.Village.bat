rem https://learn.microsoft.com/zh-tw/sql/tools/sqlcmd/sqlcmd-utility?view=sql-server-ver16
rem https://learn.microsoft.com/zh-tw/sql/tools/sqlcmd/sqlcmd-utility?view=sql-server-ver16&tabs=odbc%2Cwindows&pivots=cs1-cmd

sqlcmd -S .\SqlExpress01 -d VillageModel -f 65001 -E -i \MyCode\Village-v4\Db-Sql\Db-V4.sql\6-1-info\_Drop.info.SQL

sqlcmd -S .\SqlExpress01 -d VillageModel -f 65001 -E -i \MyCode\Village-v4\Db-Sql\Db-V4.sql\4-1-public\_Drop.public.SQL

sqlcmd -S .\SqlExpress01 -d VillageModel -f 65001 -E -i \MyCode\Village-v4\Db-Sql\Db-V4.sql\3-2-Citizen\_Drop.Citizen.SQL

sqlcmd -S .\SqlExpress01 -d VillageModel -f 65001 -E -i \MyCode\Village-v4\Db-Sql\Db-V4.sql\3-1-team\_Drop.team.SQL

sqlcmd -S .\SqlExpress01 -d VillageModel -f 65001 -E -i \MyCode\Village-v4\Db-Sql\Db-V4.sql\2-1-Master\_Drop.Master.SQL

sqlcmd -S .\SqlExpress01 -d VillageModel -f 65001 -E -i \MyCode\Village-v4\Db-Sql\Db-V4.sql\1-3-Key\_Drop.Key.SQL
sqlcmd -S .\SqlExpress01 -d VillageModel -f 65001 -E -i \MyCode\Village-v4\Db-Sql\Db-V4.sql\1-2-Auth\_Drop.auth.SQL
sqlcmd -S .\SqlExpress01 -d VillageModel -f 65001 -E -i \MyCode\Village-v4\Db-Sql\Db-V4.sql\1-1-App\_Drop.app.SQL

pause
