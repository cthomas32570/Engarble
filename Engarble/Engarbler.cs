using System;
using System.Collections;
using System.IO;
using System.Text;
using System.Security.Cryptography;

internal static class Engarbler
{

    public static byte[] Compute(string filePath, string password)
    {
        int hashByte = 0;

        byte[] fileBin = File.ReadAllBytes(filePath);
        byte[] hashBin = MakeHash(password);

        for (int i = 0; i < fileBin.Length; i++)
        {
            fileBin[i] = ComputeByte(fileBin[i], hashBin[hashByte]);

            if (hashByte == 63)
                hashByte++;
            else
                hashByte = 0;
        }

        return fileBin;
    }

    private static byte[] MakeHash(string input)
    {
        return SHA512.Create().ComputeHash(Encoding.UTF8.GetBytes(input));
    }

    private static byte ComputeByte(byte fileByte, byte hashByte)
    {
        BitArray fileBits = new BitArray(new byte[] { fileByte });
        BitArray hashBits = new BitArray(new byte[] { hashByte });

        for (int i = 0; i < 8; i++)
        {
            if (hashBits[i])
                fileBits.Set(i, !fileBits[i]);
        }

        return ConvertToByte(fileBits);
    }

    private static byte ConvertToByte(BitArray bits)
    {
        byte b = 0;

        for (int i = 7; i >= 0; i--)
        {
            b = (byte)((b << 1) | (bits[i] ? 1 : 0));
        }

        return b;
    }

}
