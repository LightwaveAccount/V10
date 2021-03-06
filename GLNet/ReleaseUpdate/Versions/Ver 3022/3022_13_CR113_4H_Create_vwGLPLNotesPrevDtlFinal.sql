CREATE VIEW [dbo].[vwGLPLNotesPrevDtlFinal]
AS
SELECT     TOP (100) PERCENT PL_Note_Title, sub_sub_code, sub_sub_title, detail_code, detail_title, ISNULL(SUM(debit_amount), 0) AS debit_amount, 
                      ISNULL(SUM(credit_amount), 0) AS credit_amount, ISNULL(SUM(ClosingAmount), 0) AS ClosingAmount, year_code
FROM         dbo.vwGLPLNotesPrevDtl
GROUP BY PL_Note_Title, sub_sub_code, sub_sub_title, detail_code, detail_title, year_code