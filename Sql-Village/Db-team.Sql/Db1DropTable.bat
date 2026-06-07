rem https://learn.microsoft.com/zh-tw/sql/tools/sqlcmd/sqlcmd-utility?view=sql-server-ver16
rem https://learn.microsoft.com/zh-tw/sql/tools/sqlcmd/sqlcmd-utility?view=sql-server-ver16&tabs=odbc%2Cwindows&pivots=cs1-cmd
 

@REM sqlcmd -E -S .\SqlExpress01 -d TeamModel -i \MyCode\Team\Sql\7-Questionnaire\_Drop.SQL
@REM sqlcmd -E -S .\SqlExpress01 -d TeamModel -i \MyCode\Team\Sql\6-Chat\_Drop.SQL
@REM sqlcmd -E -S .\SqlExpress01 -d TeamModel -i \MyCode\Team\Sql\5-Party\_Drop.SQL
@REM sqlcmd -E -S .\SqlExpress01 -d TeamModel -i \MyCode\Team\Sql\4-Member\_Drop.SQL
sqlcmd -E -S .\SqlExpress01 -d TeamModel -i \MyCode\Team\Sql\3-Master\_Drop.SQL
sqlcmd -E -S .\SqlExpress01 -d TeamModel -i \MyCode\Team\Sql\2-Auth\_Drop.SQL
sqlcmd -E -S .\SqlExpress01 -d TeamModel -i \MyCode\Team\Sql\1-App\_Drop.SQL

pause
