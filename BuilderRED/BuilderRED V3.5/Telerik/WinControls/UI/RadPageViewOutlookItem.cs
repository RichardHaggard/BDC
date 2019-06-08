// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.RadPageViewOutlookItem
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.ComponentModel;
using System.Drawing;
using Telerik.WinControls.Analytics;

namespace Telerik.WinControls.UI
{
  public class RadPageViewOutlookItem : RadPageViewStackItem
  {
    private RadPageViewOutlookAssociatedButton associatedOverflowButton;

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public RadPageViewOutlookAssociatedButton AssociatedOverflowButton
    {
      get
      {
        return this.associatedOverflowButton;
      }
      internal set
      {
        if (this.associatedOverflowButton != null && this.associatedOverflowButton != value)
          this.associatedOverflowButton.Dispose();
        this.associatedOverflowButton = value;
        if (value == null)
          return;
        this.SynchronizeSelectedStateWithAssociatedButton();
      }
    }

    private void SynchronizeSelectedStateWithAssociatedButton()
    {
      if (this.associatedOverflowButton == null)
        return;
      int num = (int) this.associatedOverflowButton.SetValue(OverflowItemsContainer.ItemSelectedProperty, (object) this.IsSelected);
    }

    protected override void OnPropertyChanged(RadPropertyChangedEventArgs e)
    {
      base.OnPropertyChanged(e);
      if (e.Property != RadPageViewItem.IsSelectedProperty)
        return;
      this.SynchronizeSelectedStateWithAssociatedButton();
      ControlTraceMonitor.TrackAtomicFeature((RadElement) this, "SelectionChanged", (object) this.Text);
    }

    protected override SizeF ArrangeOverride(SizeF finalSize)
    {
      SizeF sizeF = base.ArrangeOverride(finalSize);
      foreach (RadElement child in this.Children)
      {
        if (child is RadTextBoxControlElement)
        {
          float x = 5f;
          float y = (float) (((double) finalSize.Height - (double) child.DesiredSize.Height) / 2.0);
          child.Arrange(new RectangleF(x, y, child.DesiredSize.Width, child.DesiredSize.Height));
        }
      }
      return sizeF;
    }
  }
}
