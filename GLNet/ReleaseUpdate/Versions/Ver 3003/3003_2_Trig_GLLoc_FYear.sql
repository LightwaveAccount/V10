Create TRIGGER [dbo].[trg_Location_Fyears]
ON [dbo].[tblGlDefFinancialYear]
FOR INSERT
AS
	DECLARE @iLocation_ID INT
	DECLARE @iFinancial_year_id INT

	--	Getting the Max Financial Year ID
	-- SELECT @iFinancial_year_id = ISNULL(MAX(Financial_year_id),0) FROM tblGlDefFinancialYear
	SELECT @iFinancial_year_id = ISNULL(MAX(Financial_year_id),0) FROM INSERTED
	
	--	Cursor to get all locations
	DECLARE Location_Cursor CURSOR FOR
	SELECT Location_ID FROM tblGLDefLocation
	
	
	OPEN Location_Cursor
	
	FETCH NEXT FROM Location_Cursor INTO @iLocation_ID
	WHILE @@FETCH_STATUS = 0
	BEGIN
		
            --	Inserting data from cursor to financial year status table
	    INSERT INTO tblGlDefFinancialYearStatus
                        (financial_year_id, status, location_id)
			VALUES  ( @ifinancial_year_id ,'Open',@iLocation_id)

	    FETCH NEXT FROM Location_Cursor INTO @iLocation_ID
	END
	
	CLOSE Location_Cursor
	DEALLOCATE Location_Cursor


-- 6.	Creation of sp of Account Opening Balance
/*IF EXISTS (SELECT name FROM sysobjects 
         WHERE name = 'usp_AccOpeningBalance' AND type = 'P')
   DROP PROCEDURE usp_AccOpeningBalance*/



