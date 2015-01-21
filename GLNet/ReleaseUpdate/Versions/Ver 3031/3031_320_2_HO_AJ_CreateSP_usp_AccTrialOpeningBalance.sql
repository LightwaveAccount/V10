 --//CR#320 New parameter and Criteria for Source added   
Create PROCEDURE [usp_AccTrialOpeningBalance]  
 @Acc_Detail_Code  VARCHAR(20) = ' ',  
 @From_Date  DATETIME,  
 @Location_ID     INT,  
 @UnPosted_Records  Bit ,  
 @Other_Voucher_Records BIT ,
 @Source VARCHAR(50) 
AS  
-- =========================================================================================================  
-- Variable Declaration  
 DECLARE @dtStartDate   DATETIME  
 DECLARE @iLocation_ID   INT  
 DECLARE @iFinancial_Year_ID  INT  
 DECLARE @iNext_Financial_Year_ID  INT  
 DECLARE @strYear_Code   VARCHAR(10)  
 DECLARE @strNext_Year_Code   VARCHAR(10)  
   
 DECLARE @strAcc_Detail_Criteria VARCHAR(100)   
 DECLARE @strPost_Criteria  VARCHAR(100)   
  
 DECLARE @strSQL   NVARCHAR(3999)  
  
-- =========================================================================================================  
-- Variable Initialization  
  
 SET @iFinancial_Year_ID = 0  
  
  
 -- Initializtion of Account Detail Criteria for selection query building  
  
 IF  @Acc_Detail_Code <>  '%  -   -   -     '   
  BEGIN  
   SET @strAcc_Detail_Criteria = '   AND (tblGlCOAMainSubSubDetail.detail_code LIKE   '''+ @Acc_Detail_Code + ''')   '  
  END  
 ELSE  
  BEGIN  
   SET @strAcc_Detail_Criteria = '  '  
  END  
 SET @strAcc_Detail_Criteria = '  '  
   
 -- Initializtion of Post Criteria for selection query building   
 IF @UnPosted_Records  = '1'  
 BEGIN  
  SET @strPost_Criteria = '  '  
 END  
 ELSE  
 BEGIN  
  SET @strPost_Criteria = ' AND (tblGlVoucher.Post = ''1'')  '  
 END  
  
-- =========================================================================================================  
 -- clear data from temporary vouchers table  
 Truncate Table tmpTblGLAccountsOpening  
  
 --1. check if @locationid = 0 then Get data for all locations  
 IF @Location_ID = 0   
  
  BEGIN  
   -- 1.1 Declare a cursor for locations  
   DECLARE Locations_Cursor CURSOR FOR  
    SELECT Location_ID FROM tblGLDefLocation   
   
   -- 1.2 Opening the Location Cursor  
   OPEN Locations_Cursor  
     
   -- 1.3 Fetching the Location  
   FETCH NEXT FROM Locations_Cursor  
    INTO @iLocation_ID  
     
   -- 1.4 Looping through the each record of the Location Cursor  
   WHILE @@FETCH_STATUS = 0  
   BEGIN  
   
    -- 1.4.1 Get Last Closing Date of the location   
    --  aginst the location_id and closing date less then date passed throught the procedure parameters  
    
    SELECT @iFinancial_Year_ID = ISNULL( Y.Financial_Year_ID,0)  , @dtStartDate = y.End_Date , @strYear_Code = Y.Year_Code  
    FROM tblGLDefFinancialYear Y INNER JOIN tblGLDefFinancialYearStatus S ON  Y.Financial_Year_ID = S.Financial_Year_ID   
    WHERE S.Status = 'Closed' AND S.Location_ID = @iLocation_id AND y.End_Date < CONVERT(VARCHAR,@From_Date ,102) ORDER BY Y.End_Date   
      
      
    IF @iFinancial_Year_ID > 0  
     BEGIN   
      -- If closed financial year is found  
      -- 1.4.1.1  Get Year Code of Next Financial Year ID      
      SET @StrNext_Year_Code = CONVERT( VARCHAR, LEFT(@strYear_Code,4) + 1 )  +  '-'   + CONVERT( VARCHAR, RIGHT(@strYear_Code,4) + 1)  
        
      -- 1.4.1.2  Get ID of Next Financial Year ID   
      SELECT @iNext_Financial_Year_ID = Financial_Year_ID , @dtStartDate = Start_Date  FROM tblGLDefFinancialYear WHERE Year_Code = @StrNext_Year_Code  
        
      -- 1.4.1.3   Building query for Last Closing Voucher against the Financial_Year_ID and Location_ID and insert them into the tmp table  
--CR#320 Criteria for Source added
--      SET @strSQL =    
--      'INSERT INTO tmpTblGLAccountsOpening  (coa_detail_id, Opening_Debit_Amount, Opening_Credit_Amount ,  OpeningBalance)    
--      SELECT     tblGlCOAMainSubSubDetail.coa_detail_id, SUM(ISNULL(tblGlVoucherDetail.debit_amount, 0)) AS Opening_Debit_Amount,    
--            SUM(ISNULL(tblGlVoucherDetail.credit_amount, 0)) AS Opening_Credit_Amount, SUM(ISNULL(tblGlVoucherDetail.debit_amount, 0))  - SUM(ISNULL(tblGlVoucherDetail.credit_amount, 0)) AS OpeningBalance   
--      FROM        tblGlVoucher INNER JOIN  tblGlVoucherDetail ON tblGlVoucher.voucher_id = tblGlVoucherDetail.voucher_id INNER JOIN    
--          tblGlCOAMainSubSubDetail ON tblGlVoucherDetail.coa_detail_id = tblGlCOAMainSubSubDetail.coa_detail_id INNER JOIN tblGlDefFinancialYear ON   
--            tblGlVoucher.finiancial_year_id = tblGlDefFinancialYear.financial_year_id   
--      WHERE      (tblGlVoucher.finiancial_year_id = ' + CONVERT(VARCHAR, @iNext_Financial_Year_ID) +'  ) AND (tblGlVoucher.location_id =' + CONVERT(VARCHAR,@ILocation_ID ) + '  ) ' +   @strPost_Criteria  +  @strAcc_Detail_Criteria  + 'AND (tblGlDefFinanc
--ialYear.year_code =''' +  @strNext_Year_Code + ''' ) AND (tblGlVoucher.voucher_date <   ''' + CONVERT(VARCHAR(10),   @dtStartdate  ,102) + '''  ) AND (tblGlVoucher.Voucher_No =  ''000000'' ) GROUP BY tblGlCOAMainSubSubDetail.coa_detail_id, tblGlCOAMainSub
--SubDetail.detail_code'      
      SET @strSQL =    
      'INSERT INTO tmpTblGLAccountsOpening  (coa_detail_id, Opening_Debit_Amount, Opening_Credit_Amount ,  OpeningBalance)    
      SELECT     tblGlCOAMainSubSubDetail.coa_detail_id, SUM(ISNULL(tblGlVoucherDetail.debit_amount, 0)) AS Opening_Debit_Amount,    
            SUM(ISNULL(tblGlVoucherDetail.credit_amount, 0)) AS Opening_Credit_Amount, SUM(ISNULL(tblGlVoucherDetail.debit_amount, 0))  - SUM(ISNULL(tblGlVoucherDetail.credit_amount, 0)) AS OpeningBalance   
      FROM        tblGlVoucher INNER JOIN  tblGlVoucherDetail ON tblGlVoucher.voucher_id = tblGlVoucherDetail.voucher_id INNER JOIN    
          tblGlCOAMainSubSubDetail ON tblGlVoucherDetail.coa_detail_id = tblGlCOAMainSubSubDetail.coa_detail_id INNER JOIN tblGlDefFinancialYear ON   
            tblGlVoucher.finiancial_year_id = tblGlDefFinancialYear.financial_year_id   
      WHERE      (tblGlVoucher.finiancial_year_id = ' + CONVERT(VARCHAR, @iNext_Financial_Year_ID) +'  ) AND (tblGlVoucher.location_id =' + CONVERT(VARCHAR,@ILocation_ID ) + '  ) ' +   @strPost_Criteria  +  @strAcc_Detail_Criteria  + 'AND (tblGlDefFinancialYear.year_code =''' +  @strNext_Year_Code + ''' ) AND (tblGlVoucher.voucher_date <   ''' + CONVERT(VARCHAR(10),   @dtStartdate  ,102) + '''  ) AND (tblGlVoucher.Voucher_No =  ''000000'' ) GROUP BY tblGlCOAMainSubSubDetail.coa_detail_id, tblGlCOAMainSubSubDetail.detail_code' 
    
      -- 1.4.1.4  Executing Query  
      EXECUTE sp_ExecuteSQL @strSQL      
    
     END  
    ELSE  
     BEGIN  
   
      -- if closed financial year is not found  
      -- 1.4.2.1  get the minimum date of the transaction from tblGLVoucher agains the selected location   
      --   and assign it as a start date  
   
      SELECT @dtStartDate  = DATEADD(day,-1,MIN(Voucher_Date)) FROM tblGLVoucher WHERE Location_ID  = @iLocation_ID  
   
     END  
   
     -- 1.4.2.2    Building query string for data insertion between start and from date against the  and insert them into the tmp table  
     SET @strSQL =    
                 ' INSERT INTO tmpTblGLAccountsOpening  (coa_detail_id, Opening_Debit_Amount, Opening_Credit_Amount ,  OpeningBalance)   
      SELECT     tblGlCOAMainSubSubDetail.coa_detail_id, SUM(ISNULL(tblGlVoucherDetail.debit_amount, 0)) AS Opening_Debit_Amount,   
                                 SUM(ISNULL(tblGlVoucherDetail.credit_amount, 0)) AS Opening_Credit_Amount, SUM(ISNULL(tblGlVoucherDetail.debit_amount, 0))  - SUM(ISNULL(tblGlVoucherDetail.credit_amount, 0)) AS OpeningBalance  
      FROM         tblGlVoucher INNER JOIN  
                                 tblGlVoucherDetail ON tblGlVoucher.voucher_id = tblGlVoucherDetail.voucher_id INNER JOIN  
                                  tblGlCOAMainSubSubDetail ON tblGlVoucherDetail.coa_detail_id = tblGlCOAMainSubSubDetail.coa_detail_id INNER JOIN  
                                  tblGlDefFinancialYear ON tblGlVoucher.finiancial_year_id = tblGlDefFinancialYear.financial_year_id  
     WHERE      (tblGlVoucher.location_id = ' + CONVERT(VARCHAR,@ILocation_ID) + ' )  ' +   @strPost_Criteria  +  @strAcc_Detail_Criteria  +  ' AND (tblGlVoucher.voucher_date BETWEEN '''+   CONVERT(VARCHAR(10), @dtStartdate   ,102) + '''  AND '''  +  CONVERT(VARCHAR,  DATEADD(day,-1,@From_Date ) ,102 ) + ''' ) AND (tblGlVoucher.Voucher_No <> ''000000'' )  
     GROUP BY tblGlCOAMainSubSubDetail.coa_detail_id, tblGlCOAMainSubSubDetail.detail_code '    
       
     -- 1.4.2.3  Executing Query string   
     EXECUTE sp_ExecuteSQL @strSQL      
      
    SET @iFinancial_Year_ID = 0  
    FETCH NEXT FROM Locations_Cursor  
    INTO @iLocation_ID  
     
      
   END  
   
   CLOSE  Locations_Cursor   
   DEALLOCATE Locations_Cursor   
   
     
  END  
 ELSE  
 BEGIN  
    
  SET @iLocation_ID = @Location_ID  
  
  -- 2.1 Get Last Closing Date of the location  
  --  aginst the location_id and closing date less then date passed throught the procedure parameters  
  
  SELECT @iFinancial_Year_ID =  ISNULL( Y.Financial_Year_ID,0)  , @dtStartDate = y.End_Date , @strYear_Code = Y.Year_Code  
  FROM tblGLDefFinancialYear Y INNER JOIN tblGLDefFinancialYearStatus S ON  Y.Financial_Year_ID = S.Financial_Year_ID   
  WHERE S.Status = 'Closed' AND S.Location_ID = @iLocation_id AND y.End_Date < CONVERT(VARCHAR,@From_Date ,102) ORDER BY Y.End_Date     
    
  IF @iFinancial_Year_ID > 0  
  BEGIN   
   -- 2.1.1  Get Year Code of Next Financial Year ID      
   SET @StrNext_Year_Code = CONVERT( VARCHAR, LEFT(@strYear_Code,4) + 1 )  +  '-'   + CONVERT( VARCHAR, RIGHT(@strYear_Code,4) + 1)  
     
   -- 2.1.2  Get ID of Next Financial Year ID   
   SELECT @iNext_Financial_Year_ID = Financial_Year_ID , @dtStartDate = Start_Date FROM tblGLDefFinancialYear WHERE Year_Code = @StrNext_Year_Code  
     
   -- 2.1.3    Building Query string to get the Last Closing Voucher against the Financial_Year_ID and Location_ID and insert them into the tmp table  
   SET @strSQL =   
   ' INSERT INTO tmpTblGLAccountsOpening  (coa_detail_id, Opening_Debit_Amount, Opening_Credit_Amount ,  OpeningBalance)   
    SELECT     tblGlCOAMainSubSubDetail.coa_detail_id, SUM(ISNULL(tblGlVoucherDetail.debit_amount, 0)) AS Opening_Debit_Amount,  SUM(ISNULL(tblGlVoucherDetail.credit_amount, 0)) AS Opening_Credit_Amount,   
           SUM(ISNULL(tblGlVoucherDetail.debit_amount, 0))  - SUM(ISNULL(tblGlVoucherDetail.credit_amount, 0)) AS OpeningBalance    
   FROM         tblGlVoucher INNER JOIN tblGlVoucherDetail ON tblGlVoucher.voucher_id = tblGlVoucherDetail.voucher_id INNER JOIN    
          tblGlCOAMainSubSubDetail ON tblGlVoucherDetail.coa_detail_id = tblGlCOAMainSubSubDetail.coa_detail_id INNER JOIN tblGlDefFinancialYear ON   
          tblGlVoucher.finiancial_year_id = tblGlDefFinancialYear.financial_year_id    
   WHERE      (tblGlVoucher.finiancial_year_id = ''' + CONVERT(VARCHAR, @iNext_Financial_Year_ID) +  ''' ) AND (tblGlVoucher.location_id = ' +  CONVERT(VARCHAR, @ILocation_ID)  + ' ) ' +    @strPost_Criteria  +  @strAcc_Detail_Criteria  +   ' AND (tblGlDefFinancialYear.year_code = '''  + @strNext_Year_Code + ''' ) AND (tblGlVoucher.voucher_date < ''' + CONVERT(VARCHAR(10),  @dtStartdate  ,102) + ''' ) AND (tblGlVoucher.Voucher_No = ''000000'' )    
   GROUP BY tblGlCOAMainSubSubDetail.coa_detail_id, tblGlCOAMainSubSubDetail.detail_code '   
     
   -- 2.1.4  Executing Query  
     
   EXECUTE sp_ExecuteSQL @strSQL  
  
  END  
  ELSE  
  BEGIN  
  
   -- if closed financial year is not found  
   -- 2.2.1  get the minimum date of the transaction from tblGLVoucher agains the selected location   
   --   and assign it as a start date  
   
   SELECT @dtStartDate  = DATEADD(day,-1,MIN(Voucher_Date)) FROM tblGLVoucher WHERE Location_ID  = @iLocation_ID  
  END  
  
   -- 2.2.2    Building Query string to get the Last Closing Voucher against the Financial_Year_ID and Location_ID and insert them into the tmp table  
--CR#320 A new Filter for source has been added
--   SET @strSQL =   
--   ' INSERT INTO tmpTblGLAccountsOpening  (coa_detail_id, Opening_Debit_Amount, Opening_Credit_Amount ,  OpeningBalance)   
--   SELECT     tblGlCOAMainSubSubDetail.coa_detail_id, SUM(ISNULL(tblGlVoucherDetail.debit_amount, 0)) AS Opening_Debit_Amount,   
--                               SUM(ISNULL(tblGlVoucherDetail.credit_amount, 0)) AS Opening_Credit_Amount, SUM(ISNULL(tblGlVoucherDetail.debit_amount, 0))  - SUM(ISNULL(tblGlVoucherDetail.credit_amount, 0)) AS OpeningBalance  
--   FROM         tblGlVoucher INNER JOIN  
--                               tblGlVoucherDetail ON tblGlVoucher.voucher_id = tblGlVoucherDetail.voucher_id INNER JOIN  
--                                tblGlCOAMainSubSubDetail ON tblGlVoucherDetail.coa_detail_id = tblGlCOAMainSubSubDetail.coa_detail_id INNER JOIN  
--                                tblGlDefFinancialYear ON tblGlVoucher.finiancial_year_id = tblGlDefFinancialYear.financial_year_id  
--   WHERE      (tblGlVoucher.location_id = ' +  CONVERT(VARCHAR, @ILocation_ID)  + ' ) ' +   @strPost_Criteria  +  @strAcc_Detail_Criteria  + ' AND (tblGlVoucher.voucher_date BETWEEN '''  + CONVERT(VARCHAR, @dtStartdate  ,102) + '''  AND '''+  CONVERT(VARCHAR, DATEADD(day,-1, @From_Date) , 102 ) + ''' ) AND (tblGlVoucher.Voucher_No <>''000000'' )  
--   GROUP BY tblGlCOAMainSubSubDetail.coa_detail_id, tblGlCOAMainSubSubDetail.detail_code'  
  SET @strSQL =   
   ' INSERT INTO tmpTblGLAccountsOpening  (coa_detail_id, Opening_Debit_Amount, Opening_Credit_Amount ,  OpeningBalance)   
   SELECT     tblGlCOAMainSubSubDetail.coa_detail_id, SUM(ISNULL(tblGlVoucherDetail.debit_amount, 0)) AS Opening_Debit_Amount,   
                               SUM(ISNULL(tblGlVoucherDetail.credit_amount, 0)) AS Opening_Credit_Amount, SUM(ISNULL(tblGlVoucherDetail.debit_amount, 0))  - SUM(ISNULL(tblGlVoucherDetail.credit_amount, 0)) AS OpeningBalance  
   FROM         tblGlVoucher INNER JOIN  
                               tblGlVoucherDetail ON tblGlVoucher.voucher_id = tblGlVoucherDetail.voucher_id INNER JOIN  
                                tblGlCOAMainSubSubDetail ON tblGlVoucherDetail.coa_detail_id = tblGlCOAMainSubSubDetail.coa_detail_id INNER JOIN  
                                tblGlDefFinancialYear ON tblGlVoucher.finiancial_year_id = tblGlDefFinancialYear.financial_year_id  
   WHERE      (tblGlVoucher.location_id = ' +  CONVERT(VARCHAR, @ILocation_ID)  + ' ) ' +   @strPost_Criteria  +  @strAcc_Detail_Criteria  + ' AND (tblGlVoucher.voucher_date BETWEEN '''  + CONVERT(VARCHAR, @dtStartdate  ,102) + '''  AND '''+  CONVERT(VARCHAR, DATEADD(day,-1, @From_Date) , 102 ) + ''' ) AND (tblGlVoucher.Voucher_No <>''000000'' )  AND tblGlVoucher.Source='''+@Source+'''
   GROUP BY tblGlCOAMainSubSubDetail.coa_detail_id, tblGlCOAMainSubSubDetail.detail_code'  
     
   -- 2.2.3  Executing Quer  
   EXECUTE sp_ExecuteSQL @strSQL  
 END  