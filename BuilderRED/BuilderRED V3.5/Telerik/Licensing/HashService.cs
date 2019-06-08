// Decompiled with JetBrains decompiler
// Type: Telerik.Licensing.HashService
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Security.Cryptography;
using System.Text;

namespace Telerik.Licensing
{
  public class HashService : IHashingService
  {
    private static readonly object serviceLock = new object();
    private static IHashingService service;

    private HashService()
    {
    }

    public static IHashingService GetInstance()
    {
      if (HashService.service == null)
      {
        lock (HashService.serviceLock)
        {
          if (HashService.service == null)
            HashService.service = (IHashingService) new HashService();
        }
      }
      return HashService.service;
    }

    public string Sha256(string input)
    {
      return Convert.ToBase64String(new SHA1CryptoServiceProvider().ComputeHash(Encoding.UTF8.GetBytes(input)));
    }
  }
}
