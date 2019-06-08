// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.Drawing.SurfaceFactory
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Drawing;
using System.IO;
using System.Reflection;

namespace Telerik.WinControls.Drawing
{
  public class SurfaceFactory
  {
    public static SurfaceFactory CreateFromFile(string assemblyPath)
    {
      if (File.Exists(assemblyPath))
      {
        Assembly assembly = Assembly.LoadFile(assemblyPath);
        if ((object) assembly == null)
          return (SurfaceFactory) null;
        foreach (Type exportedType in assembly.GetExportedTypes())
        {
          if (!exportedType.IsAbstract && exportedType.IsSubclassOf(typeof (SurfaceFactory)))
            return Activator.CreateInstance(exportedType) as SurfaceFactory;
        }
      }
      return (SurfaceFactory) null;
    }

    public Surface CreateSurface()
    {
      return this.CreateSurface((object[]) null);
    }

    public virtual Surface CreateSurface(params object[] arguments)
    {
      return (Surface) new GdiSurface((Graphics) null);
    }
  }
}
