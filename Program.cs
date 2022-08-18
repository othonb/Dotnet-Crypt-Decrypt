using cipher.service;

byte[] key = {
  111,
  166,
  171,
  158,
  18,
  141,
  7,
  227,
  38,
  136,
  5,
  149,
  246,
  135,
  147,
  4,
  227,
  172,
  53,
  63,
  219,
  39,
  35,
  188,
  172,
  8,
  174,
  139,
  220,
  162,
  228,
  64
};
byte[] iV = {
   89,
   211,
   75,
   156,
   210,
   104,
   129,
   11,
   158,
   3,
   235,
   214,
   89,
   251,
   63,
   0
};

var cipherService = new CipherService(key, iV);

byte[] encrypted = cipherService.Encrypt("Texto com mensagem limpa");

string decrypted = cipherService.Decrypt(encrypted);

Console.WriteLine($"Encrypted text: {System.Text.Encoding.UTF8.GetString(encrypted)}, Key length: {cipherService.Key.Length}, IV: {cipherService.IV.Length}");

Console.WriteLine(decrypted);

// var cipher = new CipherService(cipherService.Key, cipherService.IV);

// decrypted = cipher.Decrypt(encrypted);

// Console.WriteLine(decrypted);