// Decompiled with JetBrains decompiler
// Type: ADOX.Property
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace ADOX
{
  [CompilerGenerated]
  [Guid("00000503-0000-0010-8000-00AA006D2EA4")]
  [TypeIdentifier]
  [ComImport]
  public interface Property
  {
    [DispId(0)]
    [IndexerName("Value")]
    object this[] { [DispId(0), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] [return: MarshalAs(UnmanagedType.Struct)] get; [DispId(0), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] [param: MarshalAs(UnmanagedType.Struct), In] set; }
  }
}
