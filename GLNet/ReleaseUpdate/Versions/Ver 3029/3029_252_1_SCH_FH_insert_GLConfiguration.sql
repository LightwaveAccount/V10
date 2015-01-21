If not Exists(SELECT * FROM tblGLConfiguration WHERE config_name='SMSAPIUserName' )
Begin
	INSERT INTO [dbo].tblGLConfiguration([config_no],[config_name],[config_value])
	VALUES (38,'SMSAPIUserName','')
end

If not Exists(SELECT * FROM tblGLConfiguration WHERE config_name='SMSAPIPassword' )
Begin
	INSERT INTO [dbo].tblGLConfiguration([config_no],[config_name],[config_value])
	VALUES (39,'SMSAPIPassword','')
end

If not Exists(SELECT * FROM tblGLConfiguration WHERE config_name='BrandName' )
Begin
	INSERT INTO [dbo].tblGLConfiguration([config_no],[config_name],[config_value])
	VALUES (40,'BrandName','')
end

 
 