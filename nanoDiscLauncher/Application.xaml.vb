Imports System.IO
Imports System.Net
Class Application
    ReadOnly clientName = "nanoclient"
    ReadOnly clientExtension = ".exe"
    ReadOnly apiURL = "https://www.notlyze.de/api.php?call="

    Private clientPath As String
    Private Sub OnStart()
        'Logo mit progress_bar bei Download.
        Init()

        Process.Start(clientPath)
        Shutdown()
    End Sub

    Private Sub Init()
        Dim dataFolder = Path.GetTempPath

        clientPath = dataFolder & clientName & clientExtension

        If Directory.Exists(dataFolder) Then
            If File.Exists(clientPath) Then
                CheckClient(clientPath)
            Else
                'Not yet downloaded.
                DownloadClient()
            End If
        Else
            Directory.CreateDirectory(dataFolder)
        End If
    End Sub

    Private Sub DownloadClient()
        Try
            Using client As New WebClient
                client.DownloadFile(apiURL & "download", clientPath)
            End Using
        Catch
            MessageBox.Show("Something went wrong at downloading files.", "Launcher", MessageBoxButton.OK, MessageBoxImage.Error)
            Environment.Exit(1)
            Return
        End Try
    End Sub

    Private Sub CheckClient(clientPath As String)
        Dim clientHash = ConvertToHex(FileHash(clientPath))

        Dim latestHash
        Using client As New WebClient
            Try
                latestHash = client.DownloadString(apiURL & "hash")
            Catch
                MessageBox.Show("Something went wrong at fetching data.", "Launcher", MessageBoxButton.OK, MessageBoxImage.Error)
                Environment.Exit(1)
                Return
            End Try
        End Using

        If Not clientHash = latestHash Then
            'Update!
            DownloadClient()
        End If
    End Sub
End Class
