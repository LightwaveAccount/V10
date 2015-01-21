if not exists ( select * from [tblChequeTemplates] where [Cheque_name] = 'Allied Bank' )
begin
insert into [tblChequeTemplates] ( [Cheque_name], [Cheque_template] , [Cheque_Report]) values (	'Allied Bank',	'ABL',	'crptChequeBankAllied.rpt')
end 
if not exists ( select * from [tblChequeTemplates] where [Cheque_name] = 'Faysal Bank' )
begin
insert into [tblChequeTemplates] ( [Cheque_name], [Cheque_template] , [Cheque_Report]) values(	'Faysal Bank',	'FBL',	'crptChequeBankFaysal.rpt')
end 
if not exists ( select * from [tblChequeTemplates] where [Cheque_name] = 'Alfalah Bank' )
begin
insert into [tblChequeTemplates] ( [Cheque_name], [Cheque_template] , [Cheque_Report]) values(	'Alfalah Bank',	'BAL',	'crptChequeBankAlfalah.rpt')
end
if not exists ( select * from [tblChequeTemplates] where [Cheque_name] = 'Askari Bank' )
begin
insert into [tblChequeTemplates] ( [Cheque_name], [Cheque_template] , [Cheque_Report]) values(	'Askari Bank',	'AKBL',	'crptChequeBankAskari.rpt')
end 
if not exists ( select * from [tblChequeTemplates] where [Cheque_name] = 'MCB bank' )
begin
insert into [tblChequeTemplates] ( [Cheque_name], [Cheque_template] , [Cheque_Report]) values(	'MCB bank' ,	'MCB' ,	'crptChequeBankMCB.rpt')
end 

if not exists ( select * from [tblChequeTemplates] where [Cheque_name] = 'HBL Bank' )
begin
insert into [tblChequeTemplates] ( [Cheque_name], [Cheque_template] , [Cheque_Report]) values(	'HBL Bank' ,	'HBL' ,	'crptChequeBankHBL.rpt')
end 


if not exists ( select * from [tblChequeTemplates] where [Cheque_name] = 'Standard Chartered Bank' )
begin
insert into [tblChequeTemplates] ( [Cheque_name], [Cheque_template] , [Cheque_Report]) values(	'Standard Chartered Bank' ,	'SCB' ,	'crptChequeBankSCB.rpt')
end 


if not exists ( select * from [tblChequeTemplates] where [Cheque_name] = 'Alfalah Islamic Bank' )
begin
insert into [tblChequeTemplates] ( [Cheque_name], [Cheque_template] , [Cheque_Report]) values(	'Alfalah Islamic Bank' ,	'AIB' ,	'crptChequeBankAIB.rpt')
end 