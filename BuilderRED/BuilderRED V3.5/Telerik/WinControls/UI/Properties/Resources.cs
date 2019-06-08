// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.Properties.Resources
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Resources;
using System.Runtime.CompilerServices;

namespace Telerik.WinControls.UI.Properties
{
  [CompilerGenerated]
  [GeneratedCode("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
  [DebuggerNonUserCode]
  internal class Resources
  {
    private static ResourceManager resourceMan;
    private static CultureInfo resourceCulture;

    internal Resources()
    {
    }

    [EditorBrowsable(EditorBrowsableState.Advanced)]
    internal static ResourceManager ResourceManager
    {
      get
      {
        if (object.ReferenceEquals((object) Telerik.WinControls.UI.Properties.Resources.resourceMan, (object) null))
          Telerik.WinControls.UI.Properties.Resources.resourceMan = new ResourceManager("Telerik.WinControls.UI.Properties.Resources", typeof (Telerik.WinControls.UI.Properties.Resources).Assembly);
        return Telerik.WinControls.UI.Properties.Resources.resourceMan;
      }
    }

    [EditorBrowsable(EditorBrowsableState.Advanced)]
    internal static CultureInfo Culture
    {
      get
      {
        return Telerik.WinControls.UI.Properties.Resources.resourceCulture;
      }
      set
      {
        Telerik.WinControls.UI.Properties.Resources.resourceCulture = value;
      }
    }

    internal static Bitmap ClearFilter
    {
      get
      {
        return (Bitmap) Telerik.WinControls.UI.Properties.Resources.ResourceManager.GetObject(nameof (ClearFilter), Telerik.WinControls.UI.Properties.Resources.resourceCulture);
      }
    }

    internal static Bitmap Filter
    {
      get
      {
        return (Bitmap) Telerik.WinControls.UI.Properties.Resources.ResourceManager.GetObject(nameof (Filter), Telerik.WinControls.UI.Properties.Resources.resourceCulture);
      }
    }

    internal static Bitmap FilteringIcon
    {
      get
      {
        return (Bitmap) Telerik.WinControls.UI.Properties.Resources.ResourceManager.GetObject(nameof (FilteringIcon), Telerik.WinControls.UI.Properties.Resources.resourceCulture);
      }
    }

    internal static Bitmap preloader
    {
      get
      {
        return (Bitmap) Telerik.WinControls.UI.Properties.Resources.ResourceManager.GetObject(nameof (preloader), Telerik.WinControls.UI.Properties.Resources.resourceCulture);
      }
    }

    internal static Bitmap validation_icon
    {
      get
      {
        return (Bitmap) Telerik.WinControls.UI.Properties.Resources.ResourceManager.GetObject(nameof (validation_icon), Telerik.WinControls.UI.Properties.Resources.resourceCulture);
      }
    }
  }
}
