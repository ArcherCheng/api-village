rem --https://learn.microsoft.com/zh-tw/sql/tools/sqlcmd/sqlcmd-utility?view=sql-server-ver16

sqlcmd -S .\SqlExpress01 -d TeamModel -f 65001 -E -i \MyCode\Team\Sql\0-Func\CheckIsFun1.SQL
sqlcmd -S .\SqlExpress01 -d TeamModel -f 65001 -E -i \MyCode\Team\Sql\0-Func\TransferToFun1.SQL

sqlcmd -S .\SqlExpress01 -d TeamModel -f 65001 -E -i \MyCode\Team\Sql\1-App\App1Log.SQL
sqlcmd -S .\SqlExpress01 -d TeamModel -f 65001 -E -i \MyCode\Team\Sql\1-App\App2Temp.SQL

sqlcmd -S .\SqlExpress01 -d TeamModel -f 65001 -E -i \MyCode\Team\Sql\2-Auth\Au1Team10.SQL
sqlcmd -S .\SqlExpress01 -d TeamModel -f 65001 -E -i \MyCode\Team\Sql\2-Auth\Ab1Key.SQL
sqlcmd -S .\SqlExpress01 -d TeamModel -f 65001 -E -i \MyCode\Team\Sql\2-Auth\Au1User.SQL
sqlcmd -S .\SqlExpress01 -d TeamModel -f 65001 -E -i \MyCode\Team\Sql\2-Auth\Au2RoleUser.SQL

sqlcmd -S .\SqlExpress01 -d TeamModel -f 65001 -E -i \MyCode\Team\Sql\3-Master\Pt2Bulletin.SQL
sqlcmd -S .\SqlExpress01 -d TeamModel -f 65001 -E -i \MyCode\Team\Sql\3-Master\Pt2Forum.SQL
sqlcmd -S .\SqlExpress01 -d TeamModel -f 65001 -E -i \MyCode\Team\Sql\3-Master\Pt2Image.SQL
sqlcmd -S .\SqlExpress01 -d TeamModel -f 65001 -E -i \MyCode\Team\Sql\3-Master\Pt2Repair.SQL

@REM sqlcmd -S .\SqlExpress01 -d TeamModel -f 65001 -E -i \MyCode\Team\Sql\3-Master\PartyData.SQL

@REM sqlcmd -S .\SqlExpress01 -d TeamModel -f 65001 -E -i \MyCode\Team\Sql\4-Member\MemberChat.SQL
@REM sqlcmd -S .\SqlExpress01 -d TeamModel -f 65001 -E -i \MyCode\Team\Sql\4-Member\MemberCondition.SQL
@REM sqlcmd -S .\SqlExpress01 -d TeamModel -f 65001 -E -i \MyCode\Team\Sql\4-Member\MemberPhoto.SQL

@REM sqlcmd -S .\SqlExpress01 -d TeamModel -f 65001 -E -i \MyCode\Team\Sql\5-Party\PartyMember.SQL
@REM sqlcmd -S .\SqlExpress01 -d TeamModel -f 65001 -E -i \MyCode\Team\Sql\5-Party\PartyPhoto.SQL
@REM sqlcmd -S .\SqlExpress01 -d TeamModel -f 65001 -E -i \MyCode\Team\Sql\5-Party\PartySuggest.SQL
@REM sqlcmd -S .\SqlExpress01 -d TeamModel -f 65001 -E -i \MyCode\Team\Sql\5-Party\PartyVote.SQL
@REM sqlcmd -S .\SqlExpress01 -d TeamModel -f 65001 -E -i \MyCode\Team\Sql\5-Party\PartyChatGroup.SQL
@REM sqlcmd -S .\SqlExpress01 -d TeamModel -f 65001 -E -i \MyCode\Team\Sql\5-Party\PartyChatOther.SQL

@REM sqlcmd -S .\SqlExpress01 -d TeamModel -f 65001 -E -i \MyCode\Team\Sql\6-Chat\ChatGroup.SQL

@REM sqlcmd -S .\SqlExpress01 -d TeamModel -f 65001 -E -i \MyCode\Team\Sql\7-Questionnaire\Questionnaire.SQL

@REM sqlcmd -S .\SqlExpress01 -d TeamModel -f 65001 -E -i \MyCode\Team\Sql\6-Questionnaire\Questionnaire.SQL


pause
