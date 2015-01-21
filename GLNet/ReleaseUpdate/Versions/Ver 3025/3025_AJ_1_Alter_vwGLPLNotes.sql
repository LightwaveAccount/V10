Alter view vwGLPLNotes   as SELECT     TOP (100) PERCENT dbo.tblGlDefGLNotes.note_no, dbo.tblGlDefGLNotes.note_title, dbo.tblGlCOAMainSubSub.sub_sub_code, 
                      dbo.tblGlCOAMainSubSub.sub_sub_title, dbo.vwGLPLNotesCurrent.debit_amount, dbo.vwGLPLNotesCurrent.credit_amount, 
                      dbo.vwGLPLNotesCurrent.ClosingAmount, ISNULL(dbo.vwGLPLNotesPrev.debit_amount, 0) AS debit_amount_Prev, 
                      ISNULL(dbo.vwGLPLNotesPrev.credit_amount, 0) AS credit_amount_Prev, ISNULL(dbo.vwGLPLNotesPrev.ClosingAmount, 0) 
                      AS ClosingAmount_Prev,
		 Note_Group=CASE note_no
         WHEN '1' THEN 'A'
         WHEN '2' THEN 'A'
         WHEN '3' THEN 'B'
         WHEN '4' THEN 'C'
		 WHEN '5' THEN 'C'
		 WHEN '6' THEN 'C'
		 WHEN '7' THEN 'D'
		 WHEN '8' THEN 'D' END
FROM         dbo.tblGlDefGLNotes INNER JOIN
                      dbo.tblGlCOAMainSubSub ON dbo.tblGlDefGLNotes.gl_note_id = dbo.tblGlCOAMainSubSub.PL_note_id LEFT OUTER JOIN
                      dbo.vwGLPLNotesCurrent ON dbo.tblGlCOAMainSubSub.sub_sub_code = dbo.vwGLPLNotesCurrent.sub_sub_code LEFT OUTER JOIN
                      dbo.vwGLPLNotesPrev ON dbo.tblGlCOAMainSubSub.sub_sub_code = dbo.vwGLPLNotesPrev.sub_sub_code
ORDER BY dbo.tblGlDefGLNotes.note_no
