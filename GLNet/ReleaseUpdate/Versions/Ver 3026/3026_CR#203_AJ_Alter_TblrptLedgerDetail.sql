If Exists(SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TblrptLedgerDetail]') AND type in (N'U') )
Begin
	if Exists(select * from sys.columns where Name = N'VDescription'and Object_ID = Object_ID(N'[dbo].[TblrptLedgerDetail]'))
	Begin
			alter table TblrptLedgerDetail
			alter column VDescription varchar (500) null
	End
End