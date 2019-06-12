// Decompiled with JetBrains decompiler
// Type: ADOX.Procedures
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.Collections;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.CustomMarshalers;

namespace ADOX
{
  [CompilerGenerated]
  [Guid("00000626-0000-0010-8000-00AA006D2EA4")]
  [TypeIdentifier]
  [ComImport]
  public interface Procedures : _Collection
  {
    [SpecialName]
    [MethodImpl(MethodCodeType = MethodCodeType.Runtime)]
    extern void _VtblGap1_1();

    [DispId(-4)]
    [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
    [return: MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof (EnumeratorToEnumVariantMarshaler))]
    new IEnumerator GetEnumerator();

    [SpecialName]
    [MethodImpl(MethodCodeType = MethodCodeType.Runtime)]
    extern void _VtblGap2_1();

    [DispId(0)]
    Procedure this[[MarshalAs(UnmanagedType.Struct), In] object Item] { [DispId(0), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] [return: MarshalAs(UnmanagedType.Interface)] get; }
  }
}
