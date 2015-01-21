If not Exists(SELECT * FROM tblGLConfiguration WHERE config_name='gl_ac_Services_Tax')
Begin
	INSERT INTO [dbo].tblGLConfiguration([config_no],[config_name],[config_value])
	VALUES (42,'gl_ac_Services_Tax','')
end


