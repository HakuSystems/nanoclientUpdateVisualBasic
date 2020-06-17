Imports System.Security.Cryptography
Imports System.IO

Module Helper
    Public Function FileHash(filePath As String)
        Dim fileBytes() As Byte = File.ReadAllBytes(filePath)
        Dim targetCrypto As New MD5CryptoServiceProvider()
        Dim byteHash() As Byte = targetCrypto.ComputeHash(fileBytes)

        Return Convert.ToBase64String(byteHash)
    End Function

    Public Function ConvertToHex(baseString As String) As String
        Dim b64bytes() As Byte = System.Convert.FromBase64String(baseString)

        Dim ret As String
        For Each b As Byte In b64bytes
            ret &= Hex(b).PadLeft(2, "0"c)
        Next

        Return ret.ToLower
    End Function
End Module
