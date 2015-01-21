IF EXISTS (select * from sys.tables  where name='tblGLSMSConfiguration')
BEGIN
	DROP table  tblGLSMSConfiguration
END