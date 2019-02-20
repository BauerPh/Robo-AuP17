Imports Microsoft.VisualBasic.ApplicationServices
Imports System.IO

Namespace My
    ' Für MyApplication sind folgende Ereignisse verfügbar:
    ' Startup: Wird beim Starten der Anwendung noch vor dem Erstellen des Startformulars ausgelöst.
    ' Shutdown: Wird nach dem Schließen aller Anwendungsformulare ausgelöst.  Dieses Ereignis wird nicht ausgelöst, wenn die Anwendung mit einem Fehler beendet wird.
    ' UnhandledException: Wird bei einem Ausnahmefehler ausgelöst.
    ' StartupNextInstance: Wird beim Starten einer Einzelinstanzanwendung ausgelöst, wenn die Anwendung bereits aktiv ist. 
    ' NetworkAvailabilityChanged: Wird beim Herstellen oder Trennen der Netzwerkverbindung ausgelöst.
    Partial Friend Class MyApplication
        Private Sub MyApplication_UnhandledException(sender As Object, e As UnhandledExceptionEventArgs) Handles Me.UnhandledException
            Dim filePath As String = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop),
                                            "RoboAUP17Error.log")

            Using sw As New StreamWriter(File.Open(filePath, FileMode.Append))
                sw.WriteLine("--------------New Exception--------------")
                sw.WriteLine(DateTime.Now)
                sw.WriteLine(e.Exception.Message)
                sw.WriteLine("---Stack Trace:")
                sw.WriteLine(e.Exception.StackTrace)
                If e.Exception.InnerException IsNot Nothing Then
                    sw.WriteLine("------Inner Exception:")
                    sw.WriteLine(e.Exception.InnerException.Message)
                    sw.WriteLine("---Stack Trace:")
                    sw.WriteLine(e.Exception.InnerException.StackTrace)
                End If
                sw.WriteLine()
            End Using
            MessageBox.Show($"Es ist ein Fehler aufgetreten. Die Anwendung wird beendet.{vbCrLf}Auf Ihrem Desktop befindet sich eine Logdatei mit Informationen zu dem Fehler.", "Exception", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End
        End Sub
    End Class
End Namespace
