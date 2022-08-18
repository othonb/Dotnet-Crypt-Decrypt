using System.Security.Cryptography;

namespace cipher.service {

  class CipherService {

    public byte[] Key { get; set; }
    public byte[] IV { get; set; }

    public CipherService() {

      // Create a new instance of the Aes
      // class.  This generates a new key and initialization
      // vector (IV).
      using (Aes myAes = Aes.Create()) {

        Key = myAes.Key;
        IV = myAes.IV;

      }
    }

    public CipherService(byte[] Key, byte[] IV) {

      this.Key = Key;
      this.IV = IV;
    }

    public byte[] Encrypt(string plainText) {

      // Check arguments.
      if (plainText == null || plainText.Length <= 0)
        throw new ArgumentNullException("plainText");

      if (Key == null || Key.Length <= 0)
        throw new ArgumentNullException("Key");

      if (IV == null || IV.Length <= 0)
        throw new ArgumentNullException("IV");

      byte[] encrypted;

      // Create an Aes object
      // with the specified key and IV.
      using (Aes aesAlg = Aes.Create())
      {
        aesAlg.Key = Key;
        aesAlg.IV = IV;

        // Create an encryptor to perform the stream transform.
        ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

        // Create the streams used for encryption.
        using (MemoryStream msEncrypt = new MemoryStream()) {

          using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write)) {

            using (StreamWriter swEncrypt = new StreamWriter(csEncrypt)) {

              //Write all data to the stream.
              swEncrypt.Write(plainText);
            }

            encrypted = msEncrypt.ToArray();

          }
        }
      }

      // Return the encrypted bytes from the memory stream.
      return encrypted;

    }

    public string Decrypt(byte[] cipherText) {

      // Check arguments.
      if (cipherText == null || cipherText.Length <= 0)
        throw new ArgumentNullException("cipherText");

      if (Key == null || Key.Length <= 0)
        throw new ArgumentNullException("Key");

      if (IV == null || IV.Length <= 0)
        throw new ArgumentNullException("IV");

      // Declare the string used to hold
      // the decrypted text.
      string plaintext;

      // Create an Aes object
      // with the specified key and IV.
      using (Aes aesAlg = Aes.Create()) {

        aesAlg.Key = Key;
        aesAlg.IV = IV;

        // Create a decryptor to perform the stream transform.
        ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

        // Create the streams used for decryption.
        using (MemoryStream msDecrypt = new MemoryStream(cipherText)) {

          using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read)) {

            using (StreamReader srDecrypt = new StreamReader(csDecrypt)) {

              // Read the decrypted bytes from the decrypting stream
              // and place them in a string.
              plaintext = srDecrypt.ReadToEnd();
            }
          }
        }
      }

      return plaintext;
    }

  }

}