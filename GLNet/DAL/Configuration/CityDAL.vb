Imports Model
Imports System.Data.SqlClient
Imports Utility.Utility




Public Class CityDAL

#Region "Local Functions and Procedures"
    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="objCity"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function IsValidateForSave(ByVal objCity As City) As Boolean

        Try
            Dim strSQL As String
            strSQL = "SELECT      field_name, field_code" _
            & " FROM         tblDefCities " _
            & " WHERE field_name = '" & objCity.CityName.Replace("'", "''") & "' AND city_id <> '" & objCity.CityID & "'"


            Using objNameDR As SqlDataReader = SQLHelper.ExecuteReader(SQLHelper.CON_STR, CommandType.Text, strSQL, Nothing)

                If Not objNameDR.HasRows Then

                    strSQL = "SELECT     field_name, field_code" _
                    & " FROM         tblDefCities " _
                    & " WHERE field_code = '" & objCity.CityCode.Replace("'", "''") & "'  AND city_id <> '" & objCity.CityID & "'"

                    Using objCodeDR As SqlDataReader = SQLHelper.ExecuteReader(SQLHelper.CON_STR, CommandType.Text, strSQL, Nothing)

                        If objCodeDR.HasRows Then

                            Throw New Exception(gstrMsgDuplicateCode)

                        End If

                    End Using

                ElseIf objNameDR.HasRows Then

                    Throw New Exception(gstrMsgDuplicateName)

                End If

                Return True

            End Using


        Catch ex As Exception
            Throw ex
        End Try
    End Function


    ''' <summary>
    ''' 
    ''' 
    ''' </summary>
    ''' <param name="objCity"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function IsValidateForDelete(ByVal objCity As City) As Boolean

        Try
            Dim strSQL As String

            ''Check in Areas
            strSQL = "SELECT     *  " _
            & " FROM         tblDefCityAreas " _
            & " WHERE city_id = '" & objCity.CityID & "'"

            Using objNameDR As SqlDataReader = SQLHelper.ExecuteReader(SQLHelper.CON_STR, CommandType.Text, strSQL, Nothing)

                If objNameDR.HasRows Then
                    Throw New Exception(gstrMsgDependentRecordExist & " (tblDefCityAreas)")
                End If

            End Using

            ''========
            ''Check in Shops
            strSQL = "SELECT     *  " _
            & " FROM         tblDefShops " _
            & " WHERE city_id = '" & objCity.CityID & "'"

            Using objNameDR As SqlDataReader = SQLHelper.ExecuteReader(SQLHelper.CON_STR, CommandType.Text, strSQL, Nothing)

                If objNameDR.HasRows Then
                    Throw New Exception(gstrMsgDependentRecordExist & " (tblDefShops)")
                End If

            End Using

            ''========
            ''Check in Member
            strSQL = "SELECT     *  " _
            & " FROM         tblMemberInfo " _
            & " WHERE city_id = '" & objCity.CityID & "'"

            Using objNameDR As SqlDataReader = SQLHelper.ExecuteReader(SQLHelper.CON_STR, CommandType.Text, strSQL, Nothing)

                If objNameDR.HasRows Then
                    Throw New Exception(gstrMsgDuplicateCode & " (tblMemberInfo)")
                End If

            End Using


            Return True

        Catch ex As Exception
            Throw ex
        End Try

    End Function

#End Region



#Region "Public Functions and Procedures"

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="objCity"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function Add(ByVal objCity As City) As Boolean
        'Try
        '    'Check DB Validation, If Not validate then the function returns an exception
        '    Me.IsValidateForSave(objCity)

        'Catch ex As Exception
        '    'Thorw exception if db validation false
        '    Throw ex

        'End Try

        Dim conn As New SqlConnection(SQLHelper.CON_STR)
        conn.Open()
        Dim trans As SqlTransaction = conn.BeginTransaction

        Try

            Dim strSQL As String

            strSQL = "INSERT INTO TblDefCities ( field_name, field_code, city_preference, sort_order, comments, readonly) " _
            & " VALUES ( '" & objCity.CityName.Trim.Replace("'", "''") & "', '" & objCity.CityCode.Trim.Replace("'", "''") & "', '" & objCity.CityPerference & "', '" & objCity.SortOrder & "', '" & objCity.Comments.Trim.Replace("'", "''") & "', '" & IIf(objCity.IsReadOnly, "ReadOnly", "") & "') " _
            & " Select Ident_Current('TblDefCities')"

            ''Execute SQL 
            objCity.CityID = Convert.ToInt32(SQLHelper.ExecuteScaler(trans, CommandType.Text, strSQL, Nothing))

            ''SQL Statement Log
            objCity.ActivityLog.SQLType = "INSERT"
            objCity.ActivityLog.TableName = "TblDefCities"
            objCity.ActivityLog.SQL = strSQL
            UtilityDAL.BuildSQLLog(objCity.ActivityLog, trans)

            ''Activity Log
            objCity.ActivityLog.FormAction = "Save"
            UtilityDAL.BuildActivityLog(objCity.ActivityLog, trans)

            ''Commit Traction
            trans.Commit()

            ''Return
            Return True


        Catch ex As SqlException
            trans.Rollback()
            Throw ex
        Catch ex As Exception
            trans.Rollback()
            Throw ex
        Finally
            conn.Close()
        End Try

    End Function

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="objCity"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function Update(ByVal objCity As City) As Boolean

        'Try
        '    'Check DB Validation, If Not validate then the function returns an exception
        '    Me.IsValidateForSave(objCity)


        'Catch ex As Exception
        '    'Thorw exception if db validation false
        '    Throw ex

        'End Try

        Dim conn As New SqlConnection(SQLHelper.CON_STR)
        conn.Open()
        Dim trans As SqlTransaction = conn.BeginTransaction

        Try

            Dim strSQL As String

            strSQL = "UPDATE TblDefCities SET " _
            & " field_name = '" & objCity.CityName.Trim.Replace("'", "''") & "', " _
            & " field_code = '" & objCity.CityCode.Trim.Replace("'", "''") & "', " _
            & " city_preference = '" & objCity.CityPerference & "',  " _
            & " sort_order = '" & objCity.SortOrder & "',  " _
            & " comments = '" & objCity.Comments.Trim.Replace("'", "''") & "',  " _
            & " readonly = '" & IIf(objCity.IsReadOnly, "ReadOnly", "") & "' " _
            & " WHERE City_id = " & objCity.CityID

            ''Execute SQL 
            Convert.ToInt32(SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL, Nothing))


            ''SQL Statement Log
            objCity.ActivityLog.SQLType = "UPDATE"
            objCity.ActivityLog.TableName = "TblDefCities"
            objCity.ActivityLog.SQL = strSQL
            UtilityDAL.BuildSQLLog(objCity.ActivityLog, trans)

            ''Activity Log
            objCity.ActivityLog.FormAction = "Update"
            UtilityDAL.BuildActivityLog(objCity.ActivityLog, trans)


            ''Commit Traction
            trans.Commit()

            ''Return
            Return True


        Catch ex As SqlException
            trans.Rollback()
            Throw ex
        Catch ex As Exception
            trans.Rollback()
            Throw ex
        Finally
            conn.Close()
        End Try
    End Function

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="objCity"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function Deleted(ByVal objCity As City) As Boolean

        'Try
        '    'Check DB Validation, If Not validate then the function returns an exception
        '    Me.IsValidateForDelete(objCity)

        'Catch ex As Exception
        '    'Thorw exception if db validation false
        '    Throw ex

        'End Try

        Dim conn As New SqlConnection(SQLHelper.CON_STR)
        conn.Open()
        Dim trans As SqlTransaction = conn.BeginTransaction

        Try

            Dim strSQL As String

            strSQL = "DELETE FROM TblDefCities " _
            & " WHERE City_id = " & objCity.CityID

            ''Execute SQL 
            Convert.ToInt32(SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL, Nothing))

            ''SQL Statement Log
            objCity.ActivityLog.SQLType = "DELETE"
            objCity.ActivityLog.TableName = "TblDefCities"
            objCity.ActivityLog.SQL = strSQL
            UtilityDAL.BuildSQLLog(objCity.ActivityLog, trans)

            ''Activity Log
            objCity.ActivityLog.FormAction = "Delete"
            UtilityDAL.BuildActivityLog(objCity.ActivityLog, trans)

            ''Commit Traction
            trans.Commit()

            ''Return
            Return True


        Catch ex As SqlException
            trans.Rollback()
            Throw ex
        Catch ex As Exception
            trans.Rollback()
            Throw ex
        Finally
            conn.Close()

        End Try
    End Function

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetAll(Optional ByVal strCondition As String = "") As DataTable
        Dim objDA As SqlClient.SqlDataAdapter
        Try

            Dim strSQL As String
            strSQL = "SELECT     city_id [City ID], field_name [City Name], field_code [City Code], city_preference [City Preference], sort_order [Sort Order], comments [Comments], readonly [Read Only] " _
            & " FROM         tblDefCities " _
            & " ORDER BY sort_order, Field_name "

            objDA = New SqlClient.SqlDataAdapter(strSQL, SQLHelper.CON_STR)

            Dim MyCollectionList As New DataTable(EnumHashTableKeyConstants.GetCityList.ToString)
            objDA.Fill(MyCollectionList)

            Return MyCollectionList

        Catch ex As SqlException
            Throw ex
        Catch ex As Exception
            Throw ex
        Finally
            objDA = Nothing
        End Try

    End Function

#End Region


End Class


