// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.LayoutControlSplitterItem
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.ComponentModel;
using System.Windows.Forms;
using Telerik.WinControls.Design;
using Telerik.WinControls.Themes;

namespace Telerik.WinControls.UI
{
  public class LayoutControlSplitterItem : LayoutControlSeparatorItem
  {
    public static RadProperty HorizontalImageProperty = RadProperty.Register(nameof (HorizontalImage), typeof (RadImageShape), typeof (LayoutControlSeparatorItem), new RadPropertyMetadata((PropertyChangedCallback) null));
    public static RadProperty VerticalImageProperty = RadProperty.Register(nameof (VerticalImage), typeof (RadImageShape), typeof (LayoutControlSeparatorItem), new RadPropertyMetadata((PropertyChangedCallback) null));
    private SplitterElementLayout splitterElement;

    [Browsable(false)]
    [RadPropertyDefaultValue("SplitterThickness", typeof (LayoutControlSeparatorItem))]
    [VsbBrowsable(true)]
    public RadImageShape HorizontalImage
    {
      get
      {
        return this.GetValue(LayoutControlSplitterItem.HorizontalImageProperty) as RadImageShape;
      }
      set
      {
        int num = (int) this.SetValue(LayoutControlSplitterItem.HorizontalImageProperty, (object) value);
      }
    }

    [Browsable(false)]
    [VsbBrowsable(true)]
    [RadPropertyDefaultValue("SplitterThickness", typeof (LayoutControlSeparatorItem))]
    public RadImageShape VerticalImage
    {
      get
      {
        return this.GetValue(LayoutControlSplitterItem.VerticalImageProperty) as RadImageShape;
      }
      set
      {
        int num = (int) this.SetValue(LayoutControlSplitterItem.VerticalImageProperty, (object) value);
      }
    }

    protected override void CreateVisualItem()
    {
      this.splitterElement = new SplitterElementLayout((SplitterElement) null);
      this.splitterElement.ShouldHandleMouseInput = false;
      this.splitterElement.NotifyParentOnMouseInput = true;
      this.splitterElement.RadPropertyChanged += new RadPropertyChangedEventHandler(this.splitterElement_RadPropertyChanged);
      this.Children.Add((RadElement) this.splitterElement);
    }

    private void splitterElement_RadPropertyChanged(object sender, RadPropertyChangedEventArgs e)
    {
      if (e.Property != SplitterElementLayout.IsVerticalProperty)
        return;
      this.UpdateSplitterImage();
    }

    protected override void OnPropertyChanged(RadPropertyChangedEventArgs e)
    {
      base.OnPropertyChanged(e);
      if (e.Property != LayoutControlSplitterItem.HorizontalImageProperty && e.Property != LayoutControlSplitterItem.VerticalImageProperty)
        return;
      this.UpdateSplitterImage();
    }

    protected virtual void UpdateSplitterImage()
    {
      if ((bool) this.splitterElement.GetValue(SplitterElementLayout.IsVerticalProperty))
      {
        if (this.VerticalImage == null)
          return;
        this.splitterElement.BackgroundShape = this.VerticalImage;
      }
      else
      {
        if (this.HorizontalImage == null)
          return;
        this.splitterElement.BackgroundShape = this.HorizontalImage;
      }
    }

    protected override void UpdateVisualItemOrientation()
    {
      int num = (int) this.splitterElement.SetValue(SplitterElementLayout.IsVerticalProperty, (object) (this.Orientation == Orientation.Vertical));
    }
  }
}
