declare @rightID int 
set @rightID = (SELECT ControlID FROM tblGLSecurityFormControl WHERE FormID= dbo.GetFormGLID('frmGLPostingVoucher') and ControlName = 'Post')
declare @GroupID int

declare CurPost cursor
for select tblGLSecurityGroup.GROUP_ID from tblGLSecurityGroup where tblGLSecurityGroup.Group_name='Administrator'
open CurPost
fetch next from CurPost into @GroupID
WHILE @@FETCH_STATUS = 0
BEGIN   
		If not Exists(SELECT * FROM dbo.tblGLSecurityControlRight WHERE GroupID= @GroupID and ControlID = @rightID)
		Begin
			insert into tblGLSecurityControlRight (GroupID, ControlID) values ( @GroupID , @rightID)
		End
   fetch next from CurPost into @GroupID
END
CLOSE CurPost
DEALLOCATE CurPost



