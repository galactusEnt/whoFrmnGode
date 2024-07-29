using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using UnityEngine;

public static class SaveSystem
{
    private static readonly string playerFilePath = Path.Combine(Application.persistentDataPath, "playerData.json");
    private static readonly string gameFilePath = Path.Combine(Application.persistentDataPath, "gameData.json");

    public static void SavePlayerData(PlayerData data)
    {
        string json = JsonUtility.ToJson(data);

        File.WriteAllText(playerFilePath, json); //to be replaced with cloud saving
    }

    public static PlayerData LoadPlayerData()
    {
        if (File.Exists(playerFilePath))
        {
            string json = File.ReadAllText(playerFilePath);

            return JsonUtility.FromJson<PlayerData>(json); //to be replaced with cloud loading
        }

        return new PlayerData(); // Return a new PlayerData object if no save file exists
    }

    public static GameData LoadGameData()
    {
        if (File.Exists(gameFilePath))
        {
            string json = File.ReadAllText(gameFilePath);

            return JsonUtility.FromJson<GameData>(json); //to be replaced with cloud loading
        }

        return new GameData(); // Return a new GameData object if no save file exists
    }

    /* Encryption?
    private static string Encrypt(string plainText, string key)
    {
        using (Aes aes = Aes.Create())
        {
            byte[] keyBytes = Encoding.UTF8.GetBytes(key);
            Array.Resize(ref keyBytes, aes.KeySize / 8);
            aes.Key = keyBytes;

            using (MemoryStream ms = new MemoryStream())
            {
                byte[] iv = aes.IV;
                ms.Write(iv, 0, iv.Length);

                using (CryptoStream cs = new CryptoStream(ms, aes.CreateEncryptor(), CryptoStreamMode.Write))
                {
                    using (StreamWriter sw = new StreamWriter(cs))
                    {
                        sw.Write(plainText);
                    }
                }

                return Convert.ToBase64String(ms.ToArray());
            }
        }
    }

    private static string Decrypt(string cipherText, string key)
    {
        byte[] buffer = Convert.FromBase64String(cipherText);

        using (Aes aes = Aes.Create())
        {
            byte[] keyBytes = Encoding.UTF8.GetBytes(key);
            Array.Resize(ref keyBytes, aes.KeySize / 8);
            aes.Key = keyBytes;

            using (MemoryStream ms = new MemoryStream(buffer))
            {
                byte[] iv = new byte[aes.BlockSize / 8];
                ms.Read(iv, 0, iv.Length);
                aes.IV = iv;

                using (CryptoStream cs = new CryptoStream(ms, aes.CreateDecryptor(), CryptoStreamMode.Read))
                {
                    using (StreamReader sr = new StreamReader(cs))
                    {
                        return sr.ReadToEnd();
                    }
                }
            }
        }
    }
    */
}
