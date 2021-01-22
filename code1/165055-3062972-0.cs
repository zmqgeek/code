    Imports System
    Imports System.IO
    Imports System.Security.Cryptography
    
    Namespace Security
        Public Class Encryption
            Public Shared Function Encrypt(ByVal plainText As String, _
                                              ByVal passPhrase As String, _
                                              ByVal saltValue As String, _
                                              ByVal hashAlgorithm As String, _
                                              ByVal passwordIterations As Integer, _
                                              ByVal initVector As String, _
                                              ByVal keySize As Integer) _
                                      As String
    
                ' Convert strings into byte arrays.
                ' Let us assume that strings only contain ASCII codes.
                ' If strings include Unicode characters, use Unicode, UTF7, or UTF8 
                ' encoding.
                Dim initVectorBytes As Byte()
                initVectorBytes = Encoding.ASCII.GetBytes(initVector)
    
                Dim saltValueBytes As Byte()
                saltValueBytes = Encoding.ASCII.GetBytes(saltValue)
    
                ' Convert our plaintext into a byte array.
                ' Let us assume that plaintext contains UTF8-encoded characters.
                Dim plainTextBytes As Byte()
                plainTextBytes = Encoding.UTF8.GetBytes(plainText)
    
                ' First, we must create a password, from which the key will be derived.
                ' This password will be generated from the specified passphrase and 
                ' salt value. The password will be created using the specified hash 
                ' algorithm. Password creation can be done in several iterations.
                Dim password As PasswordDeriveBytes
                password = New PasswordDeriveBytes(passPhrase, _
                                                   saltValueBytes, _
                                                   hashAlgorithm, _
                                                   passwordIterations)
    
                ' Use the password to generate pseudo-random bytes for the encryption
                ' key. Specify the size of the key in bytes (instead of bits).
                Dim keyBytes As Byte()
                keyBytes = password.GetBytes(keySize / 8)
    
                ' Create uninitialized Rijndael encryption object.
                Dim symmetricKey As RijndaelManaged
                symmetricKey = New RijndaelManaged()
    
                ' It is reasonable to set encryption mode to Cipher Block Chaining
                ' (CBC). Use default options for other symmetric key parameters.
                symmetricKey.Mode = CipherMode.CBC
    
                ' Generate encryptor from the existing key bytes and initialization 
                ' vector. Key size will be defined based on the number of the key 
                ' bytes.
                Dim encryptor As ICryptoTransform
                encryptor = symmetricKey.CreateEncryptor(keyBytes, initVectorBytes)
    
                ' Define memory stream which will be used to hold encrypted data.
                Dim memoryStream As MemoryStream
                memoryStream = New MemoryStream()
    
                ' Define cryptographic stream (always use Write mode for encryption).
                Dim cryptoStream As CryptoStream
                cryptoStream = New CryptoStream(memoryStream, _
                                                encryptor, _
                                                CryptoStreamMode.Write)
                ' Start encrypting.
                cryptoStream.Write(plainTextBytes, 0, plainTextBytes.Length)
    
                ' Finish encrypting.
                cryptoStream.FlushFinalBlock()
    
                ' Convert our encrypted data from a memory stream into a byte array.
                Dim cipherTextBytes As Byte()
                cipherTextBytes = memoryStream.ToArray()
    
                ' Close both streams.
                memoryStream.Close()
                cryptoStream.Close()
    
                ' Convert encrypted data into a base64-encoded string.
                Dim cipherText As String
                cipherText = Convert.ToBase64String(cipherTextBytes)
    
                ' Return encrypted string.
                Encrypt = cipherText
            End Function
    
            ' <summary>
            ' Decrypts specified ciphertext using Rijndael symmetric key algorithm.
            ' </summary>
            ' <param name="cipherText">
            ' Base64-formatted ciphertext value.
            ' </param>
            ' <param name="passPhrase">
            ' Passphrase from which a pseudo-random password will be derived. The 
            ' derived password will be used to generate the encryption key. 
            ' Passphrase can be any string. In this example we assume that this 
            ' passphrase is an ASCII string.
            ' </param>
            ' <param name="saltValue">
            ' Salt value used along with passphrase to generate password. Salt can 
            ' be any string. In this example we assume that salt is an ASCII string.
            ' </param>
            ' <param name="hashAlgorithm">
            ' Hash algorithm used to generate password. Allowed values are: "MD5" and
            ' "SHA1". SHA1 hashes are a bit slower, but more secure than MD5 hashes.
            ' </param>
            ' <param name="passwordIterations">
            ' Number of iterations used to generate password. One or two iterations
            ' should be enough.
            ' </param>
            ' <param name="initVector">
            ' Initialization vector (or IV). This value is required to encrypt the 
            ' first block of plaintext data. For RijndaelManaged class IV must be 
            ' exactly 16 ASCII characters long.
            ' </param>
            ' <param name="keySize">
            ' Size of encryption key in bits. Allowed values are: 128, 192, and 256. 
            ' Longer keys are more secure than shorter keys.
            ' </param>
            ' <returns>
            ' Decrypted string value.
            ' </returns>
            ' <remarks>
            ' Most of the logic in this function is similar to the Encrypt 
            ' logic. In order for decryption to work, all parameters of this function
            ' - except cipherText value - must match the corresponding parameters of 
            ' the Encrypt function which was called to generate the 
            ' ciphertext.
            ' </remarks>
            Public Shared Function Decrypt(ByVal cipherText As String, _
                                           ByVal passPhrase As String, _
                                           ByVal saltValue As String, _
                                           ByVal hashAlgorithm As String, _
                                           ByVal passwordIterations As Integer, _
                                           ByVal initVector As String, _
                                           ByVal keySize As Integer) _
                                   As String
    
                ' Convert strings defining encryption key characteristics into byte
                ' arrays. Let us assume that strings only contain ASCII codes.
                ' If strings include Unicode characters, use Unicode, UTF7, or UTF8
                ' encoding.
                Dim initVectorBytes As Byte()
                initVectorBytes = Encoding.ASCII.GetBytes(initVector)
    
                Dim saltValueBytes As Byte()
                saltValueBytes = Encoding.ASCII.GetBytes(saltValue)
    
                ' Convert our ciphertext into a byte array.
                Dim cipherTextBytes As Byte()
                cipherTextBytes = Convert.FromBase64String(cipherText)
    
                ' First, we must create a password, from which the key will be 
                ' derived. This password will be generated from the specified 
                ' passphrase and salt value. The password will be created using
                ' the specified hash algorithm. Password creation can be done in
                ' several iterations.
                Dim password As PasswordDeriveBytes
                password = New PasswordDeriveBytes(passPhrase, _
                                                   saltValueBytes, _
                                                   hashAlgorithm, _
                                                   passwordIterations)
    
                ' Use the password to generate pseudo-random bytes for the encryption
                ' key. Specify the size of the key in bytes (instead of bits).
                Dim keyBytes As Byte()
                keyBytes = password.GetBytes(keySize / 8)
    
                ' Create uninitialized Rijndael encryption object.
                Dim symmetricKey As RijndaelManaged
                symmetricKey = New RijndaelManaged()
    
                ' It is reasonable to set encryption mode to Cipher Block Chaining
                ' (CBC). Use default options for other symmetric key parameters.
                symmetricKey.Mode = CipherMode.CBC
    
                ' Generate decryptor from the existing key bytes and initialization 
                ' vector. Key size will be defined based on the number of the key 
                ' bytes.
                Dim decryptor As ICryptoTransform
                decryptor = symmetricKey.CreateDecryptor(keyBytes, initVectorBytes)
    
                ' Define memory stream which will be used to hold encrypted data.
                Dim memoryStream As MemoryStream
                memoryStream = New MemoryStream(cipherTextBytes)
    
                ' Define memory stream which will be used to hold encrypted data.
                Dim cryptoStream As CryptoStream
                cryptoStream = New CryptoStream(memoryStream, _
                                                decryptor, _
                                                CryptoStreamMode.Read)
    
                ' Since at this point we don't know what the size of decrypted data
                ' will be, allocate the buffer long enough to hold ciphertext;
                ' plaintext is never longer than ciphertext.
                Dim plainTextBytes As Byte()
                ReDim plainTextBytes(cipherTextBytes.Length)
    
                ' Start decrypting.
                Dim decryptedByteCount As Integer
                decryptedByteCount = cryptoStream.Read(plainTextBytes, _
                                                       0, _
                                                       plainTextBytes.Length)
    
                ' Close both streams.
                memoryStream.Close()
                cryptoStream.Close()
    
                ' Convert decrypted data into a string. 
                ' Let us assume that the original plaintext string was UTF8-encoded.
                Dim plainText As String
                plainText = Encoding.UTF8.GetString(plainTextBytes, _
                                                    0, _
                                                    decryptedByteCount)
    
                ' Return decrypted string.
                Decrypt = plainText
            End Function
        End Class
    
    End Namespace
