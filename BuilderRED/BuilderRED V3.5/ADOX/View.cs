// Decompiled with JetBrains decompiler
// Type: ADOX.View
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace ADOX
{
  [CompilerGenerated]
  [Guid("00000613-0000-0010-8000-00AA006D2EA4")]
  [TypeIdentifier]
  [ComImport]
  public interface View
  {
    [DispId(0)]
    [IndexerName("Command")]
    object this[] { [DispId(0), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] [return: MarshalAs(UnmanagedType.Struct)] get; [DispId(0), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] [param: MarshalAs(UnmanagedType.IDispatch), In] set; }

    [SpecialName]
    [MethodImpl(MethodCodeType = MethodCodeType.Runtime)]
    extern void _VtblGap1_1();

    [DispId(1)]
    string Name { [DispId(1), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] [return: MarshalAs(UnmanagedType.BStr)] get; }
  }
}
