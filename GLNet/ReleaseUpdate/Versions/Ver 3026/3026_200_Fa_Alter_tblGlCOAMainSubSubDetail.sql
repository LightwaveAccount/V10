If Exists(SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tblGlCOAMainSubSubDetail]') AND type in (N'U') )
Begin
	if Exists(select * from sys.columns where Name = N'detail_title'and Object_ID = Object_ID(N'[dbo].[tblGlCOAMainSubSubDetail]'))
	Begin
			alter table tblGlCOAMainSubSubDetail
			alter column detail_title varchar (500) null
	End
End