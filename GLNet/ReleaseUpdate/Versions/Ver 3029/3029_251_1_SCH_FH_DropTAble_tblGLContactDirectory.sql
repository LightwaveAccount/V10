IF EXISTS (select * from sys.tables  where name='tblGLContactDirectory')
BEGIN
	DROP table  tblGLContactDirectory
END