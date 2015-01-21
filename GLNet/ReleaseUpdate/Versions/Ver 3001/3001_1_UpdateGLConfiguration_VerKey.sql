If Exists(SELECT * FROM tblGLConfiguration WHERE config_name='Schema_Version' )
Begin
	Update tblGLConfiguration Set config_value ='3000' Where config_name='Schema_Version'
end