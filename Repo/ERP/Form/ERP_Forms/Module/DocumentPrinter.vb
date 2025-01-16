Public NotInheritable Class DocumentPrinter

    Shared Sub New()

    End Sub

    Public Shared Function PrintFile(ByVal fileName As String, printerSetting As System.Drawing.Printing.PrinterSettings) As Boolean

        Dim printProcess As System.Diagnostics.Process = Nothing
        Dim printed As Boolean = False
        Dim defaultPrinterSetting As System.Drawing.Printing.PrinterSettings = Nothing

        Try

            If printerSetting IsNot Nothing Then


                Dim startInfo As New ProcessStartInfo()

                startInfo.Verb = "Print"
                startInfo.Arguments = printerSetting.PrinterName     '' defaultPrinterSetting.PrinterName     ' <----printer to use---- 
                startInfo.FileName = fileName
                startInfo.UseShellExecute = True
                startInfo.CreateNoWindow = True
                startInfo.WindowStyle = ProcessWindowStyle.Hidden

                Using print As System.Diagnostics.Process = Process.Start(startInfo)

                    'Close the application after X milliseconds with WaitForExit(X)   

                    print.WaitForExit(10000)

                    If print.HasExited = False Then

                        If print.CloseMainWindow() Then
                            printed = True
                        Else
                            printed = True
                        End If

                    Else
                        printed = True

                    End If

                    print.Close()

                End Using


            Else
                Throw New Exception("Printers not found in the system...")
            End If


        Catch ex As Exception
            Throw
        End Try

        Return printed

    End Function


    ''' <summary>
    ''' Change the default printer using a print dialog Box
    ''' </summary>
    ''' <param name="defaultPrinterSetting"></param>
    ''' <remarks></remarks>
    Public Shared Sub ChangePrinterSettings(ByRef defaultPrinterSetting As System.Drawing.Printing.PrinterSettings)

        Dim printDialogBox As New PrintDialog

        If printDialogBox.ShowDialog = Windows.Forms.DialogResult.OK Then

            If printDialogBox.PrinterSettings.IsValid Then
                defaultPrinterSetting = printDialogBox.PrinterSettings
            End If

        End If

    End Sub



    ''' <summary>
    ''' Get the default printer settings in the system
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function GetDefaultPrinterSetting() As System.Drawing.Printing.PrinterSettings

        Dim defaultPrinterSetting As System.Drawing.Printing.PrinterSettings = Nothing

        For Each printer As String In System.Drawing.Printing.PrinterSettings.InstalledPrinters


            defaultPrinterSetting = New System.Drawing.Printing.PrinterSettings
            defaultPrinterSetting.PrinterName = printer

            If defaultPrinterSetting.IsDefaultPrinter Then
                Return defaultPrinterSetting
            End If

        Next

        Return defaultPrinterSetting

    End Function

End Class