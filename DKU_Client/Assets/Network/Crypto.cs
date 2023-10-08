using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Security.Cryptography;
using System;
using System.Text;

public class Crypto
{
    public static string salt = "";
    public static string hashed_password = "";

    static SHA256 _sha256 = new SHA256Managed();

    private static void CreateSalt(int size)
    {
        //Generate a cryptographic random number.
        RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
        byte[] buff = new byte[size];
        rng.GetBytes(buff);
        salt = Convert.ToBase64String(buff);

        // Return a Base64 string representation of the random number.
    }

    public static void SHA256_Generate(string str)
    {
        CreateSalt(6);
        string salted_password = String.Concat(salt, str);

        Byte[] hash = _sha256.ComputeHash(Encoding.UTF8.GetBytes(salted_password));
        hashed_password = Convert.ToBase64String(hash);
    }
}
