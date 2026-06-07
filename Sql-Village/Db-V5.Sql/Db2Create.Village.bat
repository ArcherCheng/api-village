rem --https://learn.microsoft.com/zh-tw/sql/tools/sqlcmd/sqlcmd-utility?view=sql-server-ver16

sqlcmd -S .\SqlExpress01 -d VillageModel -f 65001 -E -i \MyCode\Village-V4\Db-Sql\Db-V4.Sql\0-1-Func\AppFun1.SQL
sqlcmd -S .\SqlExpress01 -d VillageModel -f 65001 -E -i \MyCode\Village-V4\Db-Sql\Db-V4.Sql\0-1-Func\DatetimeFun1.SQL
sqlcmd -S .\SqlExpress01 -d VillageModel -f 65001 -E -i \MyCode\Village-V4\Db-Sql\Db-V4.Sql\0-1-Func\OtherFun1.SQL

sqlcmd -S .\SqlExpress01 -d VillageModel -f 65001 -E -i \MyCode\Village-V4\Db-Sql\Db-V4.Sql\1-1-App\AppDataLog.SQL
sqlcmd -S .\SqlExpress01 -d VillageModel -f 65001 -E -i \MyCode\Village-V4\Db-Sql\Db-V4.Sql\1-1-App\AppTemp.SQL
sqlcmd -S .\SqlExpress01 -d VillageModel -f 65001 -E -i \MyCode\Village-V4\Db-Sql\Db-V4.Sql\1-1-App\AppUserLogin.SQL
sqlcmd -S .\SqlExpress01 -d VillageModel -f 65001 -E -i \MyCode\Village-V4\Db-Sql\Db-V4.Sql\1-1-App\AppUserMachine.SQL
sqlcmd -S .\SqlExpress01 -d VillageModel -f 65001 -E -i \MyCode\Village-V4\Db-Sql\Db-V4.Sql\1-1-App\AppUserMessage.SQL
sqlcmd -S .\SqlExpress01 -d VillageModel -f 65001 -E -i \MyCode\Village-V4\Db-Sql\Db-V4.Sql\1-1-App\AppUserRequest.SQL
sqlcmd -S .\SqlExpress01 -d VillageModel -f 65001 -E -i \MyCode\Village-V4\Db-Sql\Db-V4.Sql\1-1-App\AppUserStar.SQL

sqlcmd -S .\SqlExpress01 -d VillageModel -f 65001 -E -i \MyCode\Village-V4\Db-Sql\Db-V4.Sql\1-2-Auth\Au1Team.SQL
sqlcmd -S .\SqlExpress01 -d VillageModel -f 65001 -E -i \MyCode\Village-V4\Db-Sql\Db-V4.Sql\1-2-Auth\Au1User.SQL

sqlcmd -S .\SqlExpress01 -d VillageModel -f 65001 -E -i \MyCode\Village-V4\Db-Sql\Db-V4.Sql\1-3-Key\Ak0KeyCode.SQL
sqlcmd -S .\SqlExpress01 -d VillageModel -f 65001 -E -i \MyCode\Village-V4\Db-Sql\Db-V4.Sql\1-3-Key\Ak0KeyRule.SQL

sqlcmd -S .\SqlExpress01 -d VillageModel -f 65001 -E -i \MyCode\Village-V4\Db-Sql\Db-V4.Sql\2-1-Master\Ma1Master.SQL
sqlcmd -S .\SqlExpress01 -d VillageModel -f 65001 -E -i \MyCode\Village-V4\Db-Sql\Db-V4.Sql\2-1-Master\Ma2MasterEducation.SQL
sqlcmd -S .\SqlExpress01 -d VillageModel -f 65001 -E -i \MyCode\Village-V4\Db-Sql\Db-V4.Sql\2-1-Master\Ma2MasterExperience.SQL
sqlcmd -S .\SqlExpress01 -d VillageModel -f 65001 -E -i \MyCode\Village-V4\Db-Sql\Db-V4.Sql\2-1-Master\Ma2MasterPolicy.SQL
sqlcmd -S .\SqlExpress01 -d VillageModel -f 65001 -E -i \MyCode\Village-V4\Db-Sql\Db-V4.Sql\2-1-Master\Ma2MasterPhoto.SQL
sqlcmd -S .\SqlExpress01 -d VillageModel -f 65001 -E -i \MyCode\Village-V4\Db-Sql\Db-V4.Sql\2-1-Master\Ma2MasterPartner.SQL

sqlcmd -S .\SqlExpress01 -d VillageModel -f 65001 -E -i \MyCode\Village-V4\Db-Sql\Db-V4.Sql\3-1-Team\Tm2Announcement.SQL
sqlcmd -S .\SqlExpress01 -d VillageModel -f 65001 -E -i \MyCode\Village-V4\Db-Sql\Db-V4.Sql\3-1-Team\Tm2Activity.SQL
sqlcmd -S .\SqlExpress01 -d VillageModel -f 65001 -E -i \MyCode\Village-V4\Db-Sql\Db-V4.Sql\3-1-Team\Tm2Questionnaire.SQL

sqlcmd -S .\SqlExpress01 -d VillageModel -f 65001 -E -i \MyCode\Village-V4\Db-Sql\Db-V4.Sql\3-2-Citizen\Cz2Petition.SQL
sqlcmd -S .\SqlExpress01 -d VillageModel -f 65001 -E -i \MyCode\Village-V4\Db-Sql\Db-V4.Sql\3-2-Citizen\Cz2Repair.SQL
sqlcmd -S .\SqlExpress01 -d VillageModel -f 65001 -E -i \MyCode\Village-V4\Db-Sql\Db-V4.Sql\3-2-Citizen\Cz2Suggestion.SQL

sqlcmd -S .\SqlExpress01 -d VillageModel -f 65001 -E -i \MyCode\Village-V4\Db-Sql\Db-V4.Sql\4-1-public\Pb2Forum.SQL
sqlcmd -S .\SqlExpress01 -d VillageModel -f 65001 -E -i \MyCode\Village-V4\Db-Sql\Db-V4.Sql\4-1-public\Pb2Bulletin.SQL

sqlcmd -S .\SqlExpress01 -d VillageModel -f 65001 -E -i \MyCode\Village-V4\Db-Sql\Db-V4.Sql\6-1-info\Tm5Shop.SQL
sqlcmd -S .\SqlExpress01 -d VillageModel -f 65001 -E -i \MyCode\Village-V4\Db-Sql\Db-V4.Sql\6-1-info\Tm5Party.SQL
sqlcmd -S .\SqlExpress01 -d VillageModel -f 65001 -E -i \MyCode\Village-V4\Db-Sql\Db-V4.Sql\6-1-info\Tm5View.SQL



pause
