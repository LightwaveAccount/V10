IF EXISTS (select * from sys.tables  where name='TblGLSmsLog')
BEGIN
	DROP table  TblGLSmsLog
END