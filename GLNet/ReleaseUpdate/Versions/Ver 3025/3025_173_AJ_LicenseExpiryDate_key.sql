If not Exists(SELECT * FROM tblGlConfiguration WHERE config_name='GL_LED' )
Begin
	INSERT INTO [dbo].[tblGlConfiguration]([config_no],[config_name],[config_value])
	VALUES (37,'GL_LED','2012-03-31')
end