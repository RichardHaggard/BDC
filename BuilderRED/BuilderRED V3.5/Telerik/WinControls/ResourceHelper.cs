// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.ResourceHelper
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Windows.Forms;

namespace Telerik.WinControls
{
  public static class ResourceHelper
  {
    public static Bitmap ImageFromResource(System.Type type, string resourceName)
    {
      Assembly assembly = type.Assembly;
      Bitmap bitmap1 = (Bitmap) null;
      Stream stream = (Stream) null;
      try
      {
        stream = assembly.GetManifestResourceStream(resourceName);
        if (stream != null)
        {
          Bitmap bitmap2 = (Bitmap) Image.FromStream(stream);
          bitmap1 = new Bitmap((Image) bitmap2);
          bitmap2.Dispose();
        }
        return bitmap1;
      }
      catch (Exception ex)
      {
        return (Bitmap) null;
      }
      finally
      {
        stream?.Close();
      }
    }

    public static Cursor CursorFromResource(System.Type type, string resourceName)
    {
      Assembly assembly = type.Assembly;
      Cursor cursor = (Cursor) null;
      Stream stream = (Stream) null;
      try
      {
        stream = assembly.GetManifestResourceStream(resourceName);
        if (stream != null)
          cursor = new Cursor(stream);
        return cursor;
      }
      catch (Exception ex)
      {
        return (Cursor) null;
      }
      finally
      {
        stream?.Close();
      }
    }
  }
}
