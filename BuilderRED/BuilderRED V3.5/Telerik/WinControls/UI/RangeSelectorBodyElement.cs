// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.RangeSelectorBodyElement
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Telerik.WinControls.UI
{
  public class RangeSelectorBodyElement : StackLayoutElement
  {
    private bool showButtons = true;
    private RangeSelectorArrowButton leftArrow;
    private RangeSelectorArrowButton rightArrow;
    private RangeSelectorViewContainer viewContainer;

    protected override void InitializeFields()
    {
      base.InitializeFields();
      this.StretchHorizontally = true;
      this.StretchVertically = true;
    }

    protected override void CreateChildElements()
    {
      this.leftArrow = new RangeSelectorArrowButton();
      this.leftArrow.Class = "LeftArrowButton";
      this.leftArrow.Click += new EventHandler(this.leftArrow_Click);
      this.viewContainer = new RangeSelectorViewContainer();
      this.rightArrow = new RangeSelectorArrowButton();
      this.rightArrow.Class = "RightArrowButton";
      this.rightArrow.Click += new EventHandler(this.rightArrow_Click);
      this.SetElementsOrder();
      this.SetHandlesVisibilityAndOrientation();
    }

    protected override void DisposeManagedResources()
    {
      if (this.leftArrow != null)
        this.leftArrow.Click -= new EventHandler(this.leftArrow_Click);
      if (this.rightArrow != null)
        this.rightArrow.Click -= new EventHandler(this.rightArrow_Click);
      base.DisposeManagedResources();
    }

    public RangeSelectorViewContainer ViewContainer
    {
      get
      {
        return this.viewContainer;
      }
    }

    public RangeSelectorArrowButton LeftArrow
    {
      get
      {
        return this.leftArrow;
      }
    }

    public RangeSelectorArrowButton RightArrow
    {
      get
      {
        return this.rightArrow;
      }
    }

    [DefaultValue(true)]
    public bool ShowButtons
    {
      get
      {
        return this.showButtons;
      }
      set
      {
        if (this.showButtons == value)
          return;
        if (value)
        {
          this.LeftArrow.Visibility = ElementVisibility.Visible;
          this.RightArrow.Visibility = ElementVisibility.Visible;
        }
        else
        {
          this.LeftArrow.Visibility = ElementVisibility.Collapsed;
          this.RightArrow.Visibility = ElementVisibility.Collapsed;
        }
        this.showButtons = value;
        this.OnNotifyPropertyChanged(nameof (ShowButtons));
      }
    }

    private void rightArrow_Click(object sender, EventArgs e)
    {
      if ((double) this.ViewContainer.TrackingElement.EndRange == 100.0)
        return;
      RangeSelectorSelectionChangingEventArgs changingArgs = new RangeSelectorSelectionChangingEventArgs(this.ViewContainer.TrackingElement.StartRange + 1f, this.ViewContainer.TrackingElement.EndRange + 1f);
      this.OnSelectionChanging(changingArgs);
      if (changingArgs.Cancel)
        return;
      if ((double) this.ViewContainer.TrackingElement.EndRange + 1.0 > 100.0)
      {
        float num = 100f - this.ViewContainer.TrackingElement.EndRange;
        this.ViewContainer.TrackingElement.EndRange = 100f;
        this.ViewContainer.TrackingElement.StartRange += num;
        this.OnSelectionChanged(new EventArgs());
      }
      else
      {
        ++this.ViewContainer.TrackingElement.EndRange;
        ++this.ViewContainer.TrackingElement.StartRange;
        if (this.ViewContainer.AssociatedElement is IRangeSelectorElement)
          (this.ViewContainer.AssociatedElement as IRangeSelectorElement).UpdateAssociatedView();
        this.OnSelectionChanged(new EventArgs());
      }
    }

    private void leftArrow_Click(object sender, EventArgs e)
    {
      if ((double) this.ViewContainer.TrackingElement.StartRange == 0.0)
        return;
      RangeSelectorSelectionChangingEventArgs changingArgs = new RangeSelectorSelectionChangingEventArgs(this.ViewContainer.TrackingElement.StartRange - 1f, this.ViewContainer.TrackingElement.EndRange - 1f);
      this.OnSelectionChanging(changingArgs);
      if (changingArgs.Cancel)
        return;
      if ((double) this.ViewContainer.TrackingElement.StartRange - 1.0 < 0.0)
      {
        float num = 0.0f - this.ViewContainer.TrackingElement.StartRange;
        this.ViewContainer.TrackingElement.StartRange = 0.0f;
        this.ViewContainer.TrackingElement.EndRange += num;
        this.OnSelectionChanged(new EventArgs());
      }
      else
      {
        --this.ViewContainer.TrackingElement.StartRange;
        --this.ViewContainer.TrackingElement.EndRange;
        if (this.ViewContainer.AssociatedElement is IRangeSelectorElement)
          (this.ViewContainer.AssociatedElement as IRangeSelectorElement).UpdateAssociatedView();
        this.OnSelectionChanged(new EventArgs());
      }
    }

    private void OnSelectionChanging(
      RangeSelectorSelectionChangingEventArgs changingArgs)
    {
      (this.Parent as RadRangeSelectorElement).OnSelectionChanging(changingArgs);
    }

    private void OnSelectionChanged(EventArgs eventArgs)
    {
      (this.Parent as RadRangeSelectorElement).OnSelectionChanged(eventArgs);
    }

    protected override SizeF MeasureOverride(SizeF availableSize)
    {
      SizeF empty = SizeF.Empty;
      if (this.Orientation == Orientation.Horizontal)
      {
        empty.Height = availableSize.Height;
        if (this.ShowButtons)
        {
          this.leftArrow.Measure(availableSize);
          empty.Width += this.leftArrow.DesiredSize.Width;
          this.rightArrow.Measure(availableSize);
          empty.Width += this.rightArrow.DesiredSize.Width;
        }
        this.viewContainer.Measure(new SizeF(availableSize.Width - empty.Width, availableSize.Height));
        empty.Width += this.viewContainer.DesiredSize.Width;
      }
      else
      {
        empty.Width = availableSize.Width;
        if (this.ShowButtons)
        {
          this.rightArrow.Measure(availableSize);
          empty.Height += this.leftArrow.DesiredSize.Height;
          this.rightArrow.Measure(availableSize);
          empty.Height += this.rightArrow.DesiredSize.Height;
        }
        this.viewContainer.Measure(new SizeF(availableSize.Width, availableSize.Height - empty.Height));
        empty.Height += this.viewContainer.DesiredSize.Height;
      }
      return empty;
    }

    private void SetElementsOrder()
    {
      this.Children.Clear();
      if (this.Orientation == Orientation.Horizontal)
      {
        this.Children.Add((RadElement) this.leftArrow);
        this.Children.Add((RadElement) this.viewContainer);
        this.Children.Add((RadElement) this.rightArrow);
      }
      else
      {
        this.Children.Add((RadElement) this.rightArrow);
        this.Children.Add((RadElement) this.viewContainer);
        this.Children.Add((RadElement) this.leftArrow);
      }
    }

    private void SetHandlesVisibilityAndOrientation()
    {
      if (this.showButtons)
      {
        this.leftArrow.Visibility = ElementVisibility.Visible;
        this.rightArrow.Visibility = ElementVisibility.Visible;
      }
      else
      {
        this.leftArrow.Visibility = ElementVisibility.Collapsed;
        this.rightArrow.Visibility = ElementVisibility.Collapsed;
      }
      if (this.Orientation == Orientation.Horizontal)
      {
        this.leftArrow.StretchVertically = true;
        this.rightArrow.StretchVertically = true;
      }
      else
      {
        this.leftArrow.StretchHorizontally = true;
        this.rightArrow.StretchHorizontally = true;
      }
    }
  }
}
