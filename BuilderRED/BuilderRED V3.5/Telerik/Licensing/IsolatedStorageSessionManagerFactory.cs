// Decompiled with JetBrains decompiler
// Type: Telerik.Licensing.IsolatedStorageSessionManagerFactory
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

namespace Telerik.Licensing
{
  internal class IsolatedStorageSessionManagerFactory : ISessionManagerFactory
  {
    private static readonly object managerLock = new object();
    private static IsolatedStorageSessionManager manager;

    public ISessionManager TryCreateManager()
    {
      return (ISessionManager) this.GetInstance();
    }

    private IsolatedStorageSessionManager GetInstance()
    {
      if (IsolatedStorageSessionManagerFactory.manager == null)
      {
        lock (IsolatedStorageSessionManagerFactory.managerLock)
        {
          if (IsolatedStorageSessionManagerFactory.manager == null)
            IsolatedStorageSessionManagerFactory.manager = new IsolatedStorageSessionManager();
        }
      }
      return IsolatedStorageSessionManagerFactory.manager;
    }
  }
}
