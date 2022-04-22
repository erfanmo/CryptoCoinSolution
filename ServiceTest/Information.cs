using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Utilities;
using CommonObjects;

namespace ServiceTest
{
    public static class Information
    {

        public static string publicKey = "";

        public static void SecureCredential(UserCredential uCredential, string refCode)
        {
            string encKey = Utility.GetComplicatedPassword(20);
            uCredential.Password = SecurityManagement.EncryptRSA("Password=" + uCredential.Password + ";" +
                "EncKey=" + encKey + ";" +
                "Date=" + Utility.PersianDate() + ";" +
                "RefrenceCode=" + refCode + ";",
                publicKey,
                1024);
            uCredential.ExtendedPassword = SecurityManagement.EncryptRSA("Password=" + uCredential.ExtendedPassword + ";" +
                "EncKey=" + encKey + ";" +
                "Date=" + Utility.PersianDate() + ";" +
                "RefrenceCode=" + refCode + ";",
                publicKey,
                1024);
            uCredential.UserName = SecurityManagement.Encrypt3DES(uCredential.UserName, encKey);
            uCredential.CellNo = SecurityManagement.Encrypt3DES(uCredential.CellNo, encKey);
            uCredential.Data = SecurityManagement.Encrypt3DES(uCredential.Data, encKey);
            uCredential.FingerPrint = SecurityManagement.Encrypt3DES(uCredential.FingerPrint, encKey);
            uCredential.Ip = SecurityManagement.Encrypt3DES(uCredential.Ip, encKey);
            uCredential.Mac = SecurityManagement.Encrypt3DES(uCredential.Mac, encKey);
            uCredential.Otp = SecurityManagement.Encrypt3DES(uCredential.Otp, encKey);
            uCredential.Serial = SecurityManagement.Encrypt3DES(uCredential.Serial, encKey);
        }

    }
}
