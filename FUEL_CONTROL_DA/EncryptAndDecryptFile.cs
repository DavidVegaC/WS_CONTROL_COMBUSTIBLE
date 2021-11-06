using System;
using System.Security.Cryptography;
using System.Text;
using System.IO;
using System.Configuration;


    public  class EncryptAndDecryptFile
    {
        /// <summary>
        /// Retorna un string encriptado en base a un parametro del tipo string
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public string EncryptToString(String text)
        {
            // Obtiene la llave de encriptacion en el appConfig
            AppSettingsReader settingsReader = new AppSettingsReader();
            String keyEncrit = (String)settingsReader.GetValue("SecurityKey", typeof(String));

            // Definir el tamaño de la clave y el vector de inicio a utilizarse
            int keySize = 32;
            int ivSize = 16;

            // Convertir la llave y el vector de inicio a su representación en bytes
            byte[] Key = UTF8Encoding.UTF8.GetBytes(keyEncrit);
            byte[] IV = UTF8Encoding.UTF8.GetBytes(keyEncrit);

            // Garantizar el tamaño correcto de la clave y el vector de inicio
            // mediante substring o padding
            Array.Resize<byte>(ref Key, keySize);
            Array.Resize<byte>(ref IV, ivSize);

            // Crear una instancia del algoritmo de Rijndael
            Rijndael RijndaelAlg = Rijndael.Create();

            // Establecer un flujo en memoria para el cifrado
            MemoryStream memoryStream = new MemoryStream();

            // Crear un flujo de cifrado basado en el flujo de los datos
            CryptoStream cryptoStream = new CryptoStream(memoryStream, RijndaelAlg.CreateEncryptor(Key, IV), CryptoStreamMode.Write);

            // Obtener la representación en bytes de la información a cifrar
            byte[] plainMessageBytes = UTF8Encoding.UTF8.GetBytes(text);

            // Cifrar los datos enviándolos al flujo de cifrado
            cryptoStream.Write(plainMessageBytes, 0, plainMessageBytes.Length);
            cryptoStream.FlushFinalBlock();

            // Obtener los datos datos cifrados como un arreglo de bytes
            byte[] cipherMessageBytes = memoryStream.ToArray();

            // Cerrar los flujos utilizados
            memoryStream.Close();
            cryptoStream.Close();

            // Retornar la representación de texto de los datos cifrados
            return Convert.ToBase64String(cipherMessageBytes);
        }

        /// <summary>
        /// Retorna un string en base a un parametro encriptado del tipo string
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public string DecryptFromString(String text)
        {

            try
            {

                // Obtiene la llave de encriptacion en el appConfig
                AppSettingsReader settingsReader = new AppSettingsReader();
                String keyEncrit = (String)settingsReader.GetValue("SecurityKey", typeof(String));

                // Definir el tamaño de la clave y el vector de inicio a utilizarse
                int keySize = 32;
                int ivSize = 16;

                // Convertir la llave y el vector de inicio a su representación en bytes
                byte[] Key = UTF8Encoding.UTF8.GetBytes(keyEncrit);
                byte[] IV = UTF8Encoding.UTF8.GetBytes(keyEncrit);

                // Garantizar el tamaño correcto de la clave y el vector de inicio
                // mediante substring o padding
                Array.Resize<byte>(ref Key, keySize);
                Array.Resize<byte>(ref IV, ivSize);

                // Obtener la representación en bytes del texto cifrado
                byte[] cipherTextBytes = Convert.FromBase64String(text);

                // Crear un arreglo de bytes para almacenar los datos descifrados
                byte[] plainTextBytes = new byte[cipherTextBytes.Length];

                // Crear una instancia del algoritmo de Rijndael			
                Rijndael RijndaelAlg = Rijndael.Create();

                // Crear un flujo en memoria con la representación de bytes de la información cifrada
                MemoryStream memoryStream = new MemoryStream(cipherTextBytes);

                // Crear un flujo de descifrado basado en el flujo de los datos
                CryptoStream cryptoStream = new CryptoStream(memoryStream, RijndaelAlg.CreateDecryptor(Key, IV), CryptoStreamMode.Read);

                // Obtener los datos descifrados obteniéndolos del flujo de descifrado
                int decryptedByteCount = cryptoStream.Read(plainTextBytes, 0, plainTextBytes.Length);

                // Cerrar los flujos utilizados
                memoryStream.Close();
                cryptoStream.Close();

                // Retornar la representación de texto de los datos descifrados
                return Encoding.UTF8.GetString(plainTextBytes, 0, decryptedByteCount);


            }
            catch (Exception ex)
            {

                return "Horror"+ex.Message;
            }


        }

        /// <summary>
        /// Encripta un archivo
        /// </summary>
        /// <param name="filename"></param>
        public static void EncryptToFile(String filename)
        {
            // Obtiene la llave de encriptacion en el appConfig
            AppSettingsReader settingsReader = new AppSettingsReader();
            String keyEncrit = (String)settingsReader.GetValue("SecurityKey", typeof(String));

            // Definir el tamaño de la clave y el vector de inicio a utilizarse
            int keySize = 32;
            int ivSize = 16;

            // Convertir la llave y el vector de inicio a su representación en bytes
            byte[] Key = UTF8Encoding.UTF8.GetBytes(keyEncrit);
            byte[] IV = UTF8Encoding.UTF8.GetBytes(keyEncrit);

            // Garantizar el tamaño correcto de la clave y el vector de inicio
            // mediante substring o padding
            Array.Resize<byte>(ref Key, keySize);
            Array.Resize<byte>(ref IV, ivSize);

            // Crear un flujo para leer el archivo
            FileStream fileStream = File.Open(filename, FileMode.Open, FileAccess.Read);
            StreamReader sr = new System.IO.StreamReader(fileStream, Encoding.Default);
            String plainMessage = sr.ReadToEnd();
            fileStream.Close();

            // Crear un flujo para el archivo a generarse
            fileStream = File.Open(filename, FileMode.Create, FileAccess.Write);

            // Crear una instancia del algoritmo Rijndael
            Rijndael RijndaelAlg = Rijndael.Create();

            // Crear un flujo de cifrado basado en el flujo de los datos
            CryptoStream cryptoStream = new CryptoStream(fileStream, RijndaelAlg.CreateEncryptor(Key, IV), CryptoStreamMode.Write);

            // Crear un flujo de escritura basado en el flujo de cifrado
            StreamWriter streamWriter = new StreamWriter(cryptoStream);

            // Cifrar el mensaje a través del flujo de escritura
            streamWriter.WriteLine(plainMessage);

            // Cerrar los flujos utilizados
            streamWriter.Close();
            cryptoStream.Close();
            fileStream.Close();
        }

        public static void EncryptToFile(String filename, String filename2)
        {
            // Obtiene la llave de encriptacion en el appConfig
            AppSettingsReader settingsReader = new AppSettingsReader();
            String keyEncrit = (String)settingsReader.GetValue("SecurityKey", typeof(String));

            // Definir el tamaño de la clave y el vector de inicio a utilizarse
            int keySize = 32;
            int ivSize = 16;

            // Convertir la llave y el vector de inicio a su representación en bytes
            byte[] Key = UTF8Encoding.UTF8.GetBytes(keyEncrit);
            byte[] IV = UTF8Encoding.UTF8.GetBytes(keyEncrit);

            // Garantizar el tamaño correcto de la clave y el vector de inicio
            // mediante substring o padding
            Array.Resize<byte>(ref Key, keySize);
            Array.Resize<byte>(ref IV, ivSize);

            // Crear un flujo para leer el archivo
            FileStream fileStream = File.Open(filename, FileMode.Open, FileAccess.Read);
            StreamReader sr = new System.IO.StreamReader(fileStream, Encoding.Default);
            String plainMessage = sr.ReadToEnd();
            fileStream.Close();

            // Crear un flujo para el archivo a generarse
            fileStream = File.Open(filename2, FileMode.Create, FileAccess.Write);

            // Crear una instancia del algoritmo Rijndael
            Rijndael RijndaelAlg = Rijndael.Create();

            // Crear un flujo de cifrado basado en el flujo de los datos
            CryptoStream cryptoStream = new CryptoStream(fileStream, RijndaelAlg.CreateEncryptor(Key, IV), CryptoStreamMode.Write);

            // Crear un flujo de escritura basado en el flujo de cifrado
            StreamWriter streamWriter = new StreamWriter(cryptoStream);

            // Cifrar el mensaje a través del flujo de escritura
            streamWriter.WriteLine(plainMessage);

            // Cerrar los flujos utilizados
            streamWriter.Close();
            cryptoStream.Close();
            fileStream.Close();
        }

        /// <summary>
        /// Desencrita un archivo
        /// </summary>
        /// <param name="filename"></param>
        public static void DecryptFromFile(String filename)
        {
            // Obtiene la llave de encriptacion en el appConfig
            AppSettingsReader settingsReader = new AppSettingsReader();
            String keyEncrit = (String)settingsReader.GetValue("SecurityKey", typeof(String));

            // Definir el tamaño de la clave y el vector de inicio a utilizarse
            int keySize = 32;
            int ivSize = 16;

            // Convertir la llave y el vector de inicio a su representación en bytes
            byte[] Key = UTF8Encoding.UTF8.GetBytes(keyEncrit);
            byte[] IV = UTF8Encoding.UTF8.GetBytes(keyEncrit);

            // Garantizar el tamaño correcto de la clave y el vector de inicio
            // mediante substring o padding
            Array.Resize<byte>(ref Key, keySize);
            Array.Resize<byte>(ref IV, ivSize);

            // Crear un flujo para el archivo a generarse
            FileStream fileStream = File.Open(filename, FileMode.Open, FileAccess.Read);

            // Crear una instancia del algoritmo Rijndael
            Rijndael RijndaelAlg = Rijndael.Create();

            // Crear un flujo de cifrado basado en el flujo de los datos
            CryptoStream cryptoStream = new CryptoStream(fileStream, RijndaelAlg.CreateDecryptor(Key, IV), CryptoStreamMode.Read);

            // Crear un flujo de lectura basado en el flujo de cifrado
            StreamReader streamReader = new StreamReader(cryptoStream);

            // Descifrar el mensaje a través del flujo de lectura
            string plainMessage = streamReader.ReadToEnd();
            fileStream.Close();

            // Crear un flujo de escritura basado en el flujo de cifrado
            StreamWriter streamWriter = new StreamWriter(filename);

            // Cifrar el mensaje a través del flujo de escritura
            byte[] byteArray = new byte[plainMessage.Length];
            streamWriter.WriteLine(plainMessage);

            // Cerrar los flujos utilizados
            cryptoStream.Close();
            streamReader.Close();
            streamWriter.Close();
            fileStream.Close();
        }

        public static void DecryptFromFile(String filename, String filename2)
        {
            // Obtiene la llave de encriptacion en el appConfig
            AppSettingsReader settingsReader = new AppSettingsReader();
            String keyEncrit = (String)settingsReader.GetValue("SecurityKey", typeof(String));

            // Definir el tamaño de la clave y el vector de inicio a utilizarse
            int keySize = 32;
            int ivSize = 16;

            // Convertir la llave y el vector de inicio a su representación en bytes
            byte[] Key = UTF8Encoding.UTF8.GetBytes(keyEncrit);
            byte[] IV = UTF8Encoding.UTF8.GetBytes(keyEncrit);

            // Garantizar el tamaño correcto de la clave y el vector de inicio
            // mediante substring o padding
            Array.Resize<byte>(ref Key, keySize);
            Array.Resize<byte>(ref IV, ivSize);

            // Crear un flujo para el archivo a generarse
            FileStream fileStream = File.Open(filename, FileMode.Open, FileAccess.Read);

            // Crear una instancia del algoritmo Rijndael
            Rijndael RijndaelAlg = Rijndael.Create();

            // Crear un flujo de cifrado basado en el flujo de los datos
            CryptoStream cryptoStream = new CryptoStream(fileStream, RijndaelAlg.CreateDecryptor(Key, IV), CryptoStreamMode.Read);

            // Crear un flujo de lectura basado en el flujo de cifrado
            StreamReader streamReader = new StreamReader(cryptoStream);

            // Descifrar el mensaje a través del flujo de lectura
            string plainMessage = streamReader.ReadToEnd();
            fileStream.Close();

            // Crear un flujo de escritura basado en el flujo de cifrado
            StreamWriter streamWriter = new StreamWriter(filename2);

            // Cifrar el mensaje a través del flujo de escritura
            byte[] byteArray = new byte[plainMessage.Length];
            streamWriter.WriteLine(plainMessage);

            // Cerrar los flujos utilizados
            cryptoStream.Close();
            streamReader.Close();
            streamWriter.Close();
            fileStream.Close();
        }
    }
