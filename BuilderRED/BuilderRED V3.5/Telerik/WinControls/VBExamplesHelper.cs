// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.VBExamplesHelper
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

namespace Telerik.WinControls
{
  public class VBExamplesHelper
  {
    private static bool isVBContext;

    public static bool VBContext
    {
      set
      {
        VBExamplesHelper.isVBContext = value;
      }
    }

    public static string StripPath(string path)
    {
      if (VBExamplesHelper.isVBContext)
      {
        string[] strArray = path.Split('.');
        int length = strArray.Length;
        if (length > 1)
          path = strArray[length - 2] + "." + strArray[length - 1];
      }
      return path;
    }
  }
}
