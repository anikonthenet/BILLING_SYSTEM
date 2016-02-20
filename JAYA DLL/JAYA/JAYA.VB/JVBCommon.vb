
#Region " Programmer Information "

'_________________________________________________________________________________________________________
'Author			: Ripan Paul
'Class Name		: JCSharpCommon
'Version		: 1.1
'Start Date		: 
'End Date		: 
'Class Desc		: Implemented Global methods
'_________________________________________________________________________________________________________

#End Region

#Region " Refered Namespaces "

Imports System
Imports System.Windows.Forms

#End Region

Public Class JVBCommon

#Region " gTextBoxValidation "
    '==============================================================================
    '-- This function for textbox validation checking 
    '==============================================================================
    Public Function gTextBoxValidation(ByVal KeyAscii As Integer, _
                                       ByVal DataTypeMaxLengthDecimalPoint As String, _
                                       ByVal TXTBOX As TextBox, _
                                       Optional ByVal strMINUS_Y_N As String = "") As Boolean
        '-------------------------------------------------------
        '-- START VARIABLE DECLARATION BLOCK -------------------
        '-------------------------------------------------------
        Dim iPointPosition As Integer
        Dim Position1, Position2 As Integer
        Dim cString, strMid As String
        Dim gl_DataType, gl_Number, gl_Decimal As String
        '-------------------------------------------------------
        '-- END VARIABLE DECLARATION BLOCK ---------------------
        '-------------------------------------------------------
        '-- START VARIABLE INITIALISATION BLOCK ----------------
        '-------------------------------------------------------
        cString = DataTypeMaxLengthDecimalPoint
        '-------------------------------------------------------
        Position1 = InStr(1, cString, ",")
        strMid = Mid(cString, Position1 + 1, Len(cString) - Position1)
        Position2 = InStr(Position1, strMid, ",")
        Position2 = Position2 + Position1
        '-------------------------------------------------------
        gl_DataType = Mid(cString, 1, 1)
        gl_Number = Val(Mid(cString, Position1 + 1, Position2 - (Position1 + 1)))
        gl_Decimal = Val(Right(cString, Len(cString) - Position2))
        '-------------------------------------------------------
        '-- END VARIABLE INITIALISATION BLOCK ------------------
        '-------------------------------------------------------
        '=======================================================
        '== If VARCHAR type of field ===========================
        '=======================================================
        If UCase(gl_DataType) = "V" Then
            '-----------------------------------------------------------------------------
            '-- If a part of the entered text is selected and then tried to edit /delete
            '-----------------------------------------------------------------------------
            If Len(TXTBOX.Text) >= Len(TXTBOX.SelectedText) And Len(TXTBOX.SelectedText) > 0 Then
                gTextBoxValidation = True
                Exit Function
            Else
                '------------------------------------------------------- 
                '- If number of characters exceed maximum length allowed
                '-------------------------------------------------------
                If Len(TXTBOX.Text) >= gl_Number Then
                    '------------------------------------- 
                    '- If key pressed is Backspace
                    '------------------------------------- 
                    If KeyAscii = 8 Then
                        gTextBoxValidation = True
                    Else
                        gTextBoxValidation = False
                    End If
                Else
                    '-------------------------------------------------------- 
                    '- If number of characters within  maximum length allowed
                    '--------------------------------------------------------
                    gTextBoxValidation = True
                    '--------------------------------------------------------
                End If
            End If
        Else
            '********************************************************
            '-- IF NUMBER TYPE WITH NO DECIMAL
            '********************************************************
            If UCase(gl_DataType) = "N" And gl_Decimal = 0 Then
                '---------------------------------------------------------------
                '-- If negative values allowed
                '---------------------------------------------------------------
                If strMINUS_Y_N = "Y" Then
                    '------------------------------------------- 
                    '- If Minus is entered
                    '------------------------------------------- 
                    If KeyAscii = 45 Then
                        If TXTBOX.SelectionStart = 0 Then
                            gTextBoxValidation = True
                            Exit Function
                        End If
                    End If
                End If
                '-----------------------------------------------------------------------------
                '-- If a part of the entered number is selected and then tried to edit /delete
                '-----------------------------------------------------------------------------
                If Len(TXTBOX.Text) >= Len(TXTBOX.SelectedText) And Len(TXTBOX.SelectedText) > 0 Then
                    '-------------------------------------------------------------- 
                    '- If user enters 0 - 9 or backspace
                    '-------------------------------------------------------------- 
                    If (KeyAscii >= 48 And KeyAscii <= 57) Or (KeyAscii = 8) Then
                        '-------------------------------------------------------------- 
                        gTextBoxValidation = True
                        '-------------------------------------------------------------- 
                    Else
                        '--------------------------------------------------------------
                        '- If user enters any other character except 0 - 9 or backspace
                        '-------------------------------------------------------------- 
                        gTextBoxValidation = False
                        '-------------------------------------------------------------- 
                    End If
                    '-------------------------------------------------------------- 
                Else
                    '------------------------------------------------------- 
                    '- If number of characters exceed maximum length allowed
                    '-------------------------------------------------------
                    If Len(TXTBOX.Text) >= gl_Number Then
                        '------------------------------------- 
                        '- If key pressed is Backspace
                        '------------------------------------- 
                        If KeyAscii = 8 Then
                            gTextBoxValidation = True
                        Else
                            gTextBoxValidation = False
                        End If
                        '------------------------------------- 
                    Else
                        '-------------------------------------------------------------- 
                        '- If user enters 0 - 9 or backspace
                        '-------------------------------------------------------------- 
                        If (KeyAscii >= 48 And KeyAscii <= 57) Or (KeyAscii = 8) Then
                            gTextBoxValidation = True
                        Else
                            gTextBoxValidation = False
                        End If
                        '-------------------------------------------------------------- 
                    End If
                End If
            Else
                '********************************************************
                '-- IF NUMBER WITH DECIMAL
                '********************************************************
                If UCase(gl_DataType) = UCase("N") Then
                    '---------------------------------------------------
                    '-- IF MINUS
                    '---------------------------------------------------
                    If strMINUS_Y_N = "Y" Then
                        If KeyAscii = 45 Then
                            If TXTBOX.SelectionStart = 0 Then
                                gTextBoxValidation = True
                                Exit Function
                            End If
                        End If
                    End If
                    '-----------------------------------------------------------------------------
                    '-- If a part of the entered number is selected and then tried to edit /delete
                    '-----------------------------------------------------------------------------
                    If Len(TXTBOX.Text) >= Len(TXTBOX.SelectedText) And Len(TXTBOX.SelectedText) > 0 Then
                        '-------------------------------------------------------------- 
                        '- If user enters 0 - 9 
                        '-------------------------------------------------------------- 
                        If (KeyAscii >= 48 And KeyAscii <= 57) Then
                            iPointPosition = InStr(Trim(TXTBOX.Text), ".")

                            If iPointPosition > 0 Then
                                If TXTBOX.SelectionStart >= iPointPosition Then
                                    gTextBoxValidation = True
                                Else
                                    If iPointPosition - 1 < ((gl_Number + 1) - gl_Decimal) Then
                                        gTextBoxValidation = True
                                    Else
                                        gTextBoxValidation = False
                                    End If
                                End If
                            Else
                                '------------------------------------------------------- 
                                '- If number of characters exceed maximum length allowed
                                '-------------------------------------------------------
                                If Len(TXTBOX.Text) < ((gl_Number + 1) - gl_Decimal) Then
                                    gTextBoxValidation = True
                                Else
                                    gTextBoxValidation = False
                                End If
                            End If
                        Else
                            '-------------------------------------------------------------- 
                            '- If press backspace
                            '-------------------------------------------------------------- 
                            If KeyAscii = 8 Then
                                gTextBoxValidation = True
                            Else
                                '-------------------------------------------------------------- 
                                '- If user press point key
                                '-------------------------------------------------------------- 
                                If KeyAscii = 46 Then
                                    If Len(TXTBOX.Text) >= Len(TXTBOX.SelectedText) And InStr(TXTBOX.SelectedText, ".") > 0 Then
                                        gTextBoxValidation = True
                                        Exit Function
                                    ElseIf InStr(Trim(TXTBOX.Text), ".") > 0 Then
                                        gTextBoxValidation = False
                                        Exit Function
                                    End If
                                Else
                                    gTextBoxValidation = False
                                End If
                            End If
                        End If
                    Else
                        '------------------------------------------------------- 
                        '- If number of characters exceed maximum length allowed
                        '-------------------------------------------------------
                        If Len(TXTBOX.Text) > gl_Number Then
                            If KeyAscii = 8 Then
                                gTextBoxValidation = True
                            Else
                                gTextBoxValidation = False
                            End If
                        Else
                            '-------------------------------------------------------------- 
                            '- If user enters 0 - 9
                            '-------------------------------------------------------------- 
                            If (KeyAscii >= 48 And KeyAscii <= 57) Then
                                iPointPosition = InStr(Trim(TXTBOX.Text), ".")

                                If iPointPosition > 0 Then
                                    If TXTBOX.SelectionStart >= iPointPosition Then
                                        If Len(TXTBOX.Text) < iPointPosition + gl_Decimal Then
                                            gTextBoxValidation = True
                                        Else
                                            gTextBoxValidation = False
                                        End If
                                    Else
                                        If iPointPosition - 1 < (gl_Number - gl_Decimal) Then
                                            gTextBoxValidation = True
                                        Else
                                            gTextBoxValidation = False
                                        End If
                                    End If
                                Else
                                    '------------------------------------------------------- 
                                    '- If number of characters exceed maximum length allowed
                                    '-------------------------------------------------------
                                    If Len(TXTBOX.Text) < (gl_Number - gl_Decimal) Then
                                        gTextBoxValidation = True
                                    Else
                                        gTextBoxValidation = False
                                    End If
                                End If
                            Else
                                '-------------------------------------------------------------- 
                                '- If press backspace
                                '-------------------------------------------------------------- 
                                If KeyAscii = 8 Then
                                    gTextBoxValidation = True
                                Else
                                    '-------------------------------------------------------------- 
                                    '- If user press point key
                                    '-------------------------------------------------------------- 
                                    If KeyAscii = 46 Then
                                        If InStr(Trim(TXTBOX.Text), ".") > 0 Then
                                            gTextBoxValidation = False
                                            Exit Function
                                        Else
                                            gTextBoxValidation = True
                                            Exit Function
                                        End If
                                        If Len(TXTBOX.Text) < InStr(Trim(TXTBOX.Text), ".") + gl_Decimal Then
                                            gTextBoxValidation = True
                                        Else
                                            gTextBoxValidation = False
                                        End If
                                    Else
                                        gTextBoxValidation = False
                                    End If
                                End If
                            End If
                        End If
                    End If
                End If
            End If
        End If
        '-------------------------------------------------------
        '-- END BLOCK ------------------------------------------
        '-------------------------------------------------------
    End Function
#End Region

End Class

