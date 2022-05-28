using System;
using System.Linq;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;

namespace Test.App.Shop.Domain.SeedWork;

public static class PasswordHasher
{
    internal static (string Hash, byte[] Salt) Hash(string password)
    {
        var salt = GenerateSalt();

        var key = KeyDerivation.Pbkdf2(
            password,
            salt,
            KeyDerivationPrf.HMACSHA1,
            10000,
            256 / 8
        );

        return (Convert.ToBase64String(key), salt);
    }

    public static bool Check(string hash, byte[] salt, string password)
    {
        if (string.IsNullOrEmpty(password)) return false;

        var key = KeyDerivation.Pbkdf2(
            password,
            salt,
            KeyDerivationPrf.HMACSHA1,
            10000,
            256 / 8
        );

        return Convert.ToBase64String(key).SequenceEqual(hash);
    }

    private static byte[] GenerateSalt()
    {
        var salt = new byte[128 / 8];
        using var rng = RandomNumberGenerator.Create();
        rng.GetNonZeroBytes(salt);
        return salt;
    }
}
