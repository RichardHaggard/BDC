// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.SafeLibraryHandle
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using Microsoft.Win32.SafeHandles;
using System.Security;
using System.Security.Permissions;

namespace Telerik.WinControls
{
  [SecurityCritical]
  [HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
  internal sealed class SafeLibraryHandle : SafeHandleZeroOrMinusOneIsInvalid
  {
    internal SafeLibraryHandle()
      : base(true)
    {
    }

    protected override bool ReleaseHandle()
    {
      return TimeZoneUnsafeNativeMethods.FreeLibrary(this.handle);
    }
  }
}
