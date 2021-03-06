    /// <summary>
    /// Simple encryption/decryption using a random initialization vector
    /// and prepending it to the crypto text.
    /// </summary>
    /// <remarks>Based on multiple answers in http://stackoverflow.com/questions/165808/simple-two-way-encryption-for-c-sharp </remarks>
    public class SimpleAes : IDisposable
    {
        /// <summary>
        ///     Initialization vector length in bytes.
        /// </summary>
        private const int IvBytes = 16;
    
        /// <summary>
        ///     Must be exactly 16, 24 or 32 bytes long.
        /// </summary>
        private static readonly byte[] Key = Convert.FromBase64String("FILL ME WITH 24 (2 pad chars), 32 OR 44 (1 pad char) RANDOM CHARS"); // Base64 has a blowup of four-thirds (33%)
    
        private readonly UTF8Encoding _encoder;
        private readonly ICryptoTransform _encryptor;
        private readonly RijndaelManaged _rijndael;
    
        public SimpleAes()
        {
            _rijndael = new RijndaelManaged {Key = Key};
            _rijndael.GenerateIV();
            _encryptor = _rijndael.CreateEncryptor();
            _encoder = new UTF8Encoding();
        }
    
        public string Decrypt(string encrypted)
        {
            return _encoder.GetString(Decrypt(Convert.FromBase64String(encrypted)));
        }
    
        public void Dispose()
        {
            _rijndael.Dispose();
            _encryptor.Dispose();
        }
    
        public string Encrypt(string unencrypted)
        {
            return Convert.ToBase64String(Encrypt(_encoder.GetBytes(unencrypted)));
        }
    
        private byte[] Decrypt(byte[] buffer)
        {
            // IV is prepended to cryptotext
            byte[] iv = buffer.Take(IvBytes).ToArray();
            using (ICryptoTransform decryptor = _rijndael.CreateDecryptor(_rijndael.Key, iv))
            {
                return decryptor.TransformFinalBlock(buffer, IvBytes, buffer.Length - IvBytes);
            }
        }
    
        private byte[] Encrypt(byte[] buffer)
        {
            // Prepend cryptotext with IV
            byte [] inputBuffer = _encryptor.TransformFinalBlock(buffer, 0, buffer.Length); 
            return _rijndael.IV.Concat(inputBuffer).ToArray();
        }
    }
