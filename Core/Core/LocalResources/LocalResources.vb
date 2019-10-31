Namespace LeandroSoftware.Resources
    Public Class Local
        Public Shared Function ObtenerSerialEquipo() As String
            Dim fileSystemObject, drive As Object
            fileSystemObject = CreateObject("Scripting.FileSystemObject")
            Dim driveName As String = "C:\"
            drive = fileSystemObject.GetDrive(fileSystemObject.GetDriveName(fileSystemObject.GetAbsolutePathName(driveName)))
            Return drive.SerialNumber.ToString()
        End Function
    End Class
End Namespace