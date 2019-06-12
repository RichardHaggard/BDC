// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.RadGalleryMenuLayoutPanel
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using Telerik.WinControls.Layouts;

namespace Telerik.WinControls.UI
{
  public class RadGalleryMenuLayoutPanel : LayoutPanel
  {
    internal static readonly int minImageWidth = 20;
    private bool defaultBehavior = true;
    private const int shortcutTextInternalMargin = 4;

    protected override void InitializeFields()
    {
      base.InitializeFields();
    }

    public bool DefaultBehavior
    {
      get
      {
        return this.defaultBehavior;
      }
      set
      {
        this.defaultBehavior = value;
      }
    }

    protected override void CreateChildElements()
    {
    }
  }
}
