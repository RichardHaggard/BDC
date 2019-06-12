// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.RadPageViewItemPage
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.ComponentModel;

namespace Telerik.WinControls.UI
{
  public class RadPageViewItemPage : RadPageViewPage
  {
    private PageViewItemType itemType;

    public PageViewItemType ItemType
    {
      get
      {
        return this.itemType;
      }
      set
      {
        this.itemType = value;
      }
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public new bool Visible
    {
      get
      {
        return false;
      }
      set
      {
        base.Visible = value;
      }
    }
  }
}
