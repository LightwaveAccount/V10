If not Exists(SELECT * FROM tblGLConfiguration WHERE config_name='CustomerInfo' )
Begin
	INSERT INTO [dbo].[tblGLConfiguration]([config_no],[config_name],[config_value])
	VALUES (30,'CustomerInfo','False')
end