select * from Ak1KeyRule
-- 設定系統參數
Insert Into Ak1KeyRule(TeamId,RuleGroup,RuleId,RuleLabel,RuleValue)
values('0970922888','Password','PW1X0010',N'是否有強制密碼變更作業(1=是/0=否)','0');
Insert Into Ak1KeyRule(TeamId,RuleGroup,RuleId,RuleLabel,RuleValue)
values('0970922888','Password','PW1X0011',N'強制幾天須密碼變更一次(30)','30');
Insert Into Ak1KeyRule(TeamId,RuleGroup,RuleId,RuleLabel,RuleValue)
values('0970922888','Password','PW1X0012',N'密碼變更最少字元長度(6)','6');
Insert Into Ak1KeyRule(TeamId,RuleGroup,RuleId,RuleLabel,RuleValue)
values('0970922888','Password','PW1X0013',N'密碼是否強制大寫英文字元(1=是/0=否)','0');
Insert Into Ak1KeyRule(TeamId,RuleGroup,RuleId,RuleLabel,RuleValue)
values('0970922888','Password','PW1X0014',N'密碼是否強制小寫英文字元(1=是/0=否)','0');
Insert Into Ak1KeyRule(TeamId,RuleGroup,RuleId,RuleLabel,RuleValue)
values('0970922888','Password','PW1X0015',N'密碼是否強制數字字元(1=是/0=否)','0');
Insert Into Ak1KeyRule(TeamId,RuleGroup,RuleId,RuleLabel,RuleValue)
values('0970922888','Password','PW1X0016',N'密碼是否強制特殊符號字元(1=是/0=否)','0');
Insert Into Ak1KeyRule(TeamId,RuleGroup,RuleId,RuleLabel,RuleValue)
values('0970922888','Password','PW1X0017',N'密碼是否強制英數字組合字元(1=是/0=否)','0');
Insert Into Ak1KeyRule(TeamId,RuleGroup,RuleId,RuleLabel,RuleValue)
values('0970922888','Password','PW1X0018',N'密碼是否允許重復使用(1=是/0=否)','0');
Insert Into Ak1KeyRule(TeamId,RuleGroup,RuleId,RuleLabel,RuleValue)
values('0970922888','Password','PW1X0019',N'是否啟用郵件通知用戶登入訊息(1=是/0=否)','1');
Insert Into Ak1KeyRule(TeamId,RuleGroup,RuleId,RuleLabel,RuleValue)
values('0970922888','Password','PW1X0020',N'是否啟用使用者登入不同機器檢查驗證碼(1=是/0=否)','1');
go