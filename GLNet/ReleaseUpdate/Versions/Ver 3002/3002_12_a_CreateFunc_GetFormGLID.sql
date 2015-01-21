create FUNCTION [dbo].[GetFormGLID] 
(
	@FormName varchar(100)
)
RETURNS int
AS

BEGIN
	-- Declare the return variable here
	DECLARE  @FromID int

	-- Add the T-SQL statements to compute the return value here
	SELECT @FromID = Form_ID from tblGLSecurityForm where Form_Name = @FormName

	-- Return the result of the function
	RETURN @FromID

END
