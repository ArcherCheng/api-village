rem --https://learn.microsoft.com/zh-tw/sql/tools/sqlcmd/sqlcmd-utility?view=sql-server-ver16

sqlcmd -S .\SqlExpress01 -d VillageModel -f 65001 -E -i \MyCode\Village-V2\Sql\0-Func\CheckIsFun1.SQL
sqlcmd -S .\SqlExpress01 -d VillageModel -f 65001 -E -i \MyCode\Village-V2\Sql\0-Func\TransferToFun1.SQL

sqlcmd -S .\SqlExpress01 -d VillageModel -f 65001 -E -i \MyCode\Village-V2\Sql\1-App\App1Log.SQL
sqlcmd -S .\SqlExpress01 -d VillageModel -f 65001 -E -i \MyCode\Village-V2\Sql\1-App\App2Temp.SQL
sqlcmd -S .\SqlExpress01 -d VillageModel -f 65001 -E -i \MyCode\Village-V2\Sql\1-App\Aa1Master.SQL

sqlcmd -S .\SqlExpress01 -d VillageModel -f 65001 -E -i \MyCode\Village-V2\Sql\2-Auth\Au1Component.SQL
sqlcmd -S .\SqlExpress01 -d VillageModel -f 65001 -E -i \MyCode\Village-V2\Sql\2-Auth\Au1User.SQL
sqlcmd -S .\SqlExpress01 -d VillageModel -f 65001 -E -i \MyCode\Village-V2\Sql\2-Auth\Au2MasterComponent.SQL

sqlcmd -S .\SqlExpress01 -d VillageModel -f 65001 -E -i \MyCode\Village-V2\Sql\3-Key\Ak1Key.SQL

sqlcmd -S .\SqlExpress01 -d VillageModel -f 65001 -E -i \MyCode\Village-V2\Sql\5-DayData\Va2Bulletin.SQL
sqlcmd -S .\SqlExpress01 -d VillageModel -f 65001 -E -i \MyCode\Village-V2\Sql\5-DayData\Va2Dementia.SQL
sqlcmd -S .\SqlExpress01 -d VillageModel -f 65001 -E -i \MyCode\Village-V2\Sql\5-DayData\Va2Forum.SQL
sqlcmd -S .\SqlExpress01 -d VillageModel -f 65001 -E -i \MyCode\Village-V2\Sql\5-DayData\Va2Repair.SQL

pause


