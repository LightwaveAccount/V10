If Exists(SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TblrptDailyActivity]') AND type in (N'U'))
Begin
	if Exists(select * from sys.columns where Name = N'VDescription'and Object_ID = Object_ID(N'[dbo].[TblrptDailyActivity]'))
	Begin
		alter table TblrptDailyActivity
		Alter Column VDescription varchar(4000)
	End
End