CREATE VIEW [dbo].[vwGLPLNotesDetail]
AS
SELECT     TOP (100) PERCENT dbo.tblGlDefGLNotes.note_no, dbo.tblGlDefGLNotes.note_title, dbo.tblGlCOAMainSubSub.sub_sub_code, 
                      dbo.tblGlCOAMainSubSub.sub_sub_title, dbo.vwGLPLNotesCurrentDtl.Detail_Code, dbo.vwGLPLNotesCurrentDtl.detail_Title, 
                      dbo.vwGLPLNotesCurrentDtl.debit_amount, dbo.vwGLPLNotesCurrentDtl.credit_amount, dbo.vwGLPLNotesCurrentDtl.ClosingAmount, 
                      ISNULL(dbo.vwGLPLNotesPrevDtl.debit_amount, 0) AS debit_amount_Prev, ISNULL(dbo.vwGLPLNotesPrevDtl.credit_amount, 0) 
                      AS credit_amount_Prev, ISNULL(dbo.vwGLPLNotesPrevDtl.ClosingAmount, 0) AS ClosingAmount_Prev
FROM         dbo.tblGlDefGLNotes INNER JOIN
                      dbo.tblGlCOAMainSubSub ON dbo.tblGlDefGLNotes.gl_note_id = dbo.tblGlCOAMainSubSub.PL_note_id INNER JOIN
                      dbo.tblGlCOAMainSubSubDetail ON dbo.tblGlCOAMainSubSub.main_sub_sub_id = dbo.tblGlCOAMainSubSubDetail.main_sub_sub_id INNER JOIN
                      dbo.vwGLPLNotesPrevDtl ON dbo.tblGlCOAMainSubSubDetail.detail_code = dbo.vwGLPLNotesPrevDtl.Detail_Code AND 
                      dbo.tblGlCOAMainSubSub.sub_sub_code = dbo.vwGLPLNotesPrevDtl.sub_sub_code INNER JOIN
                      dbo.vwGLPLNotesCurrentDtl ON dbo.tblGlCOAMainSubSubDetail.detail_code = dbo.vwGLPLNotesCurrentDtl.Detail_Code AND 
                      dbo.tblGlCOAMainSubSub.sub_sub_code = dbo.vwGLPLNotesCurrentDtl.sub_sub_code
ORDER BY dbo.tblGlDefGLNotes.note_no