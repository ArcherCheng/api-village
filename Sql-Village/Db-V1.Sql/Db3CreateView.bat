sqlcmd -S .\SqlExpress01 -d PolicyTeamModel -f 65001 -E -i \MyCode\Policy\Sql\9-ViewTable\1-ViewAuth.SQL
sqlcmd -S .\SqlExpress01 -d PolicyTeamModel -f 65001 -E -i \MyCode\Policy\Sql\9-ViewTable\2-ViewAppLike.SQL
sqlcmd -S .\SqlExpress01 -d PolicyTeamModel -f 65001 -E -i \MyCode\Policy\Sql\9-ViewTable\3-ViewPt2Bulletin.SQL
sqlcmd -S .\SqlExpress01 -d PolicyTeamModel -f 65001 -E -i \MyCode\Policy\Sql\9-ViewTable\4-ViewPt2Forum.SQL

pause
