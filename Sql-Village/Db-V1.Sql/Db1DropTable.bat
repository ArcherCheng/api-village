rem https://learn.microsoft.com/zh-tw/sql/tools/sqlcmd/sqlcmd-utility?view=sql-server-ver16
rem https://learn.microsoft.com/zh-tw/sql/tools/sqlcmd/sqlcmd-utility?view=sql-server-ver16&tabs=odbc%2Cwindows&pivots=cs1-cmd


@REM sqlcmd -E -S .\SqlExpress01 -d VillageModel -i \MyCode\Village-V2\Sql\7-Questionnaire\_Drop.SQL
@REM sqlcmd -E -S .\SqlExpress01 -d VillageModel -i \MyCode\Village-V2\Sql\6-Chat\_Drop.SQL
@REM sqlcmd -E -S .\SqlExpress01 -d VillageModel -i \MyCode\Village-V2\Sql\5-Party\_Drop.SQL

sqlcmd -E -S .\SqlExpress01 -d VillageModel -i \MyCode\Village-V2\Sql\5-DayData\_Drop.SQL
sqlcmd -E -S .\SqlExpress01 -d VillageModel -i \MyCode\Village-V2\Sql\3-Key\_Drop.SQL
sqlcmd -E -S .\SqlExpress01 -d VillageModel -i \MyCode\Village-V2\Sql\2-Auth\_Drop.SQL
sqlcmd -E -S .\SqlExpress01 -d VillageModel -i \MyCode\Village-V2\Sql\1-App\_Drop.SQL

pause
