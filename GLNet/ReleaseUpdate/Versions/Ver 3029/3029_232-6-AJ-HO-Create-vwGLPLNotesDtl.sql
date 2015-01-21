Create VIEW vwGLPLNotesDetail AS 
SELECT     TOP (100) PERCENT tblGlDefGLNotes.note_no, tblGlDefGLNotes.note_title, tblGlCOAMainSubSub.sub_sub_code, tblGlCOAMainSubSub.sub_sub_title, 
                      tblGlCOAMainSubSubDetail.detail_code as detail_code, tblGlCOAMainSubSubDetail.detail_Title as detail_Title, isnull(vwGLPLNotesCurrentDtl.debit_amount,0) as debit_amount, 
                      isnull(vwGLPLNotesCurrentDtl.credit_amount,0) as credit_amount, isnull(vwGLPLNotesCurrentDtl.ClosingAmount,0) as ClosingAmount, ISNULL(vwGLPLNotesPrevDtl.debit_amount, 0) 
                      AS debit_amount_Prev, ISNULL(vwGLPLNotesPrevDtl.credit_amount, 0) AS credit_amount_Prev, ISNULL(vwGLPLNotesPrevDtl.ClosingAmount, 0) 
                      AS ClosingAmount_Prev
FROM         tblGlDefGLNotes INNER JOIN
                      tblGlCOAMainSubSub ON tblGlDefGLNotes.gl_note_id = tblGlCOAMainSubSub.PL_note_id INNER JOIN
                      tblGlCOAMainSubSubDetail ON tblGlCOAMainSubSub.main_sub_sub_id = tblGlCOAMainSubSubDetail.main_sub_sub_id LEFT OUTER JOIN
                      vwGLPLNotesCurrentDtl ON tblGlCOAMainSubSubDetail.detail_code = vwGLPLNotesCurrentDtl.Detail_Code AND 
                      tblGlCOAMainSubSub.sub_sub_code = vwGLPLNotesCurrentDtl.sub_sub_code LEFT OUTER JOIN
                      vwGLPLNotesPrevDtl ON tblGlCOAMainSubSubDetail.detail_code = vwGLPLNotesPrevDtl.Detail_Code AND 
                      tblGlCOAMainSubSub.sub_sub_code = vwGLPLNotesPrevDtl.sub_sub_code
where (isnull(vwGLPLNotesCurrentDtl.ClosingAmount,0)<>0 or ISNULL(vwGLPLNotesPrevDtl.ClosingAmount, 0) <>0)
ORDER BY tblGlDefGLNotes.note_no

