If not Exists(SELECT * FROM tblGLConfiguration WHERE config_name='Pop3Server' )
Begin
	INSERT INTO [dbo].tblGLConfiguration([config_no],[config_name],[config_value])
	VALUES (32,'Pop3Server','')
end

If not Exists(SELECT * FROM tblGLConfiguration WHERE config_name='EMailFromAddress' )
Begin
	INSERT INTO [dbo].tblGLConfiguration([config_no],[config_name],[config_value])
	VALUES (33,'EMailFromAddress','')
end

If not Exists(SELECT * FROM tblGLConfiguration WHERE config_name='MailServerUser' )
Begin
	INSERT INTO [dbo].tblGLConfiguration([config_no],[config_name],[config_value])
	VALUES (34,'MailServerUser','')
end

If not Exists(SELECT * FROM tblGLConfiguration WHERE config_name='MailServerPassword' )
Begin
	INSERT INTO [dbo].tblGLConfiguration([config_no],[config_name],[config_value])
	VALUES (35,'MailServerPassword','')
end

If not Exists(SELECT * FROM tblGLConfiguration WHERE config_name='MailServerSSL' )
Begin
	INSERT INTO [dbo].tblGLConfiguration([config_no],[config_name],[config_value])
	VALUES (36,'MailServerSSL','')
end