rem --https://learn.microsoft.com/zh-tw/sql/tools/sqlcmd/sqlcmd-utility?view=sql-server-ver16

sqlcmd -S .\SqlExpress01 -d VillageModel -f 65001 -E -i \MyCode\Village\Db.Sql\0-1-Func\CheckIsFun1.SQL
sqlcmd -S .\SqlExpress01 -d VillageModel -f 65001 -E -i \MyCode\Village\Db.Sql\0-1-Func\TransferToFun1.SQL

sqlcmd -S .\SqlExpress01 -d VillageModel -f 65001 -E -i \MyCode\Village\Db.Sql\1-1-App\App0Master.SQL
sqlcmd -S .\SqlExpress01 -d VillageModel -f 65001 -E -i \MyCode\Village\Db.Sql\1-1-App\App1Log.SQL
sqlcmd -S .\SqlExpress01 -d VillageModel -f 65001 -E -i \MyCode\Village\Db.Sql\1-1-App\App2Temp.SQL

sqlcmd -S .\SqlExpress01 -d VillageModel -f 65001 -E -i \MyCode\Village\Db.Sql\1-2-key\Ap1Key.SQL

sqlcmd -S .\SqlExpress01 -d VillageModel -f 65001 -E -i \MyCode\Village\Db.Sql\1-3-Auth\Au1User.SQL
sqlcmd -S .\SqlExpress01 -d VillageModel -f 65001 -E -i \MyCode\Village\Db.Sql\1-3-Auth\Au2RoleUser.SQL

sqlcmd -S .\SqlExpress01 -d VillageModel -f 65001 -E -i \MyCode\Village\Db.Sql\2-1-Village\Va1Village.SQL



pause
