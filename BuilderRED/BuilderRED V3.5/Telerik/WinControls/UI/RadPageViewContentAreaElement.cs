// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.RadPageViewContentAreaElement
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Telerik.WinControls.UI
{
  public class RadPageViewContentAreaElement : RadPageViewElementBase
  {
    private RadPageViewElement owner;

    protected override void InitializeFields()
    {
      base.InitializeFields();
      this.MinSize = new Size(24, 24);
      this.Padding = new Padding(4);
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    public RadPageViewElement Owner
    {
      get
      {
        return this.owner;
      }
      internal set
      {
        this.owner = value;
        this.NotifyPagesBackColor();
      }
    }

    protected override void OnPropertyChanged(RadPropertyChangedEventArgs e)
    {
      base.OnPropertyChanged(e);
      if (this.owner == null || this.ElementState != ElementState.Loaded)
        return;
      if (e.Property == RadElement.PaddingProperty || e.Property == RadElement.BoundsProperty)
        this.owner.OnContentBoundsChanged();
      if (e.Property != VisualElement.BackColorProperty)
        return;
      this.NotifyPagesBackColor();
    }

    protected override void OnLoaded()
    {
      base.OnLoaded();
      this.NotifyPagesBackColor();
    }

    private void NotifyPagesBackColor()
    {
      if (this.owner == null)
        return;
      foreach (RadPageViewItem radPageViewItem in (IEnumerable<RadPageViewItem>) this.owner.Items)
      {
        if (radPageViewItem.Page != null)
          radPageViewItem.Page.CallBackColorChanged();
      }
    }
  }
}
