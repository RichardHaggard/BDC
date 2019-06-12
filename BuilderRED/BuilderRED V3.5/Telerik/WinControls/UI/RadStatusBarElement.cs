// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.RadStatusBarElement
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Telerik.WinControls.Layout;
using Telerik.WinControls.Layouts;
using Telerik.WinControls.Primitives;
using Telerik.WinControls.Styles;
using Telerik.WinControls.UI.StateManagers;

namespace Telerik.WinControls.UI
{
  public class RadStatusBarElement : RadItem
  {
    public static RadProperty RotateGripOnRightToLeftProperty = RadProperty.Register(nameof (RotateGripOnRightToLeft), typeof (bool), typeof (RadStatusBarElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) true, ElementPropertyOptions.AffectsDisplay));
    private RadItemOwnerCollection items;
    private WrapLayoutPanel itemsWrapLayoutPanel;
    private StatusBarBoxLayout itemsBoxLayout;
    private BorderPrimitive borderPrimitive;
    private FillPrimitive fillPrimitive;
    private RadStatusBarLayoutStyle layoutStyle;
    private Orientation orientation;
    private RadGripElement grip;
    private ToolStripGripStyle gripStyle;

    public event ValueChangingEventHandler LayoutStyleChanging;

    public event EventHandler LayoutStyleChanged;

    static RadStatusBarElement()
    {
      ItemStateManagerFactoryRegistry.AddStateManagerFactory((ItemStateManagerFactoryBase) new RadStatusBarElementStateManager(), typeof (RadStatusBarElement));
    }

    protected override void InitializeFields()
    {
      base.InitializeFields();
      this.items = new RadItemOwnerCollection();
      this.items.ItemTypes = new System.Type[7]
      {
        typeof (RadLabelElement),
        typeof (CommandBarSeparator),
        typeof (RadButtonElement),
        typeof (RadProgressBarElement),
        typeof (RadStatusBarPanelElement),
        typeof (RadSplitButtonElement),
        typeof (RadTrackBarElement)
      };
      this.items.DefaultType = typeof (RadButtonElement);
      this.StretchHorizontally = true;
      this.StretchVertically = false;
    }

    protected override void CreateChildElements()
    {
      this.itemsWrapLayoutPanel = new WrapLayoutPanel();
      this.itemsBoxLayout = new StatusBarBoxLayout();
      this.itemsWrapLayoutPanel.Visibility = ElementVisibility.Collapsed;
      DockLayoutPanel.SetDock((RadElement) this.itemsWrapLayoutPanel, Dock.Right);
      this.items.Owner = (RadElement) this.itemsBoxLayout;
      this.items.ItemTypes = new System.Type[13]
      {
        typeof (RadLabelElement),
        typeof (CommandBarSeparator),
        typeof (RadButtonElement),
        typeof (RadCheckBoxElement),
        typeof (RadImageButtonElement),
        typeof (RadProgressBarElement),
        typeof (RadRadioButtonElement),
        typeof (RadRepeatButtonElement),
        typeof (RadSplitButtonElement),
        typeof (RadStatusBarPanelElement),
        typeof (RadToggleButtonElement),
        typeof (RadTrackBarElement),
        typeof (RadWaitingBarElement)
      };
      this.fillPrimitive = new FillPrimitive();
      this.borderPrimitive = new BorderPrimitive();
      this.borderPrimitive.BoxStyle = BorderBoxStyle.OuterInnerBorders;
      this.borderPrimitive.Class = "StatusBarBorder";
      this.borderPrimitive.Width = 2f;
      this.borderPrimitive.BackColor = Color.DarkBlue;
      this.borderPrimitive.InnerColor = Color.White;
      this.fillPrimitive = new FillPrimitive();
      this.fillPrimitive.ShouldUsePaintBufferState = false;
      this.fillPrimitive.Class = "StatusBarFill";
      this.Children.Add((RadElement) this.borderPrimitive);
      this.Children.Add((RadElement) this.fillPrimitive);
      this.Children.Add((RadElement) this.itemsWrapLayoutPanel);
      this.Children.Add((RadElement) this.itemsBoxLayout);
      DockLayoutPanel.SetDock((RadElement) this.itemsWrapLayoutPanel, Dock.Left);
      DockLayoutPanel.SetDock((RadElement) this.itemsBoxLayout, Dock.Left);
      this.grip = new RadGripElement();
      this.Children.Add((RadElement) this.grip);
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [RadEditItemsAction]
    [RadNewItem("Type here", true)]
    [Browsable(true)]
    [Category("Data")]
    public RadItemOwnerCollection Items
    {
      get
      {
        return this.items;
      }
    }

    public Orientation Orientation
    {
      get
      {
        return this.orientation;
      }
      set
      {
        if (this.orientation == value)
          return;
        this.orientation = value;
        this.itemsBoxLayout.Orientation = this.orientation;
        this.itemsWrapLayoutPanel.Orientation = this.orientation;
        this.SetStreching();
        this.UpdateGripStyle();
        this.UpdateSeparatorsItems();
      }
    }

    public ToolStripGripStyle GripStyle
    {
      get
      {
        return this.gripStyle;
      }
      set
      {
        this.gripStyle = value;
        switch (this.gripStyle)
        {
          case ToolStripGripStyle.Hidden:
            this.grip.Enabled = false;
            this.grip.Image.Visibility = ElementVisibility.Collapsed;
            this.itemsBoxLayout.Margin = new Padding(0, 0, 0, 0);
            this.itemsWrapLayoutPanel.Margin = new Padding(0, 0, 0, 0);
            break;
          case ToolStripGripStyle.Visible:
            this.grip.Enabled = true;
            this.grip.Image.Visibility = ElementVisibility.Visible;
            if (this.RotateGripOnRightToLeft)
              this.grip.Image.AngleTransform = this.RightToLeft ? 90f : 0.0f;
            this.itemsBoxLayout.Margin = this.RightToLeft ? new Padding(14, 0, 0, 0) : new Padding(0, 0, 14, 0);
            this.itemsWrapLayoutPanel.Margin = this.RightToLeft ? new Padding(14, 0, 0, 0) : new Padding(0, 0, 14, 0);
            break;
        }
      }
    }

    [Category("Layout")]
    [Description("ToolStripLayoutStyle")]
    [AmbientValue(0)]
    public RadStatusBarLayoutStyle LayoutStyle
    {
      get
      {
        return this.layoutStyle;
      }
      set
      {
        if (this.layoutStyle == value)
          return;
        ValueChangingEventArgs args = new ValueChangingEventArgs((object) this.layoutStyle, (object) value);
        this.OnLayoutStyleChanging((object) this, args);
        if (args.Cancel)
          return;
        this.layoutStyle = value;
        switch (this.layoutStyle)
        {
          case RadStatusBarLayoutStyle.Stack:
            this.itemsBoxLayout.Visibility = ElementVisibility.Visible;
            this.itemsWrapLayoutPanel.Visibility = ElementVisibility.Collapsed;
            this.items.Owner = (RadElement) this.itemsBoxLayout;
            break;
          case RadStatusBarLayoutStyle.Overflow:
            this.itemsBoxLayout.Visibility = ElementVisibility.Collapsed;
            this.itemsWrapLayoutPanel.Visibility = ElementVisibility.Visible;
            this.items.Owner = (RadElement) this.itemsWrapLayoutPanel;
            break;
        }
        this.SetStreching();
        this.OnLayoutStyleChanged((object) this, EventArgs.Empty);
      }
    }

    public bool RotateGripOnRightToLeft
    {
      get
      {
        return (bool) this.GetValue(RadStatusBarElement.RotateGripOnRightToLeftProperty);
      }
      set
      {
        int num = (int) this.SetValue(RadStatusBarElement.RotateGripOnRightToLeftProperty, (object) value);
      }
    }

    private void ItemsChanged(
      RadItemCollection changed,
      RadElement target,
      ItemsChangeOperation operation)
    {
      RadItem radItem = target as RadItem;
      if (radItem == null || operation != ItemsChangeOperation.Inserted && operation != ItemsChangeOperation.Set)
        return;
      radItem.Margin = new Padding(1, 1, 1, 1);
    }

    private void OnLayoutStyleChanged(object sender, EventArgs eventArgs)
    {
      if (this.LayoutStyleChanged == null)
        return;
      this.LayoutStyleChanged(sender, eventArgs);
    }

    private void OnLayoutStyleChanging(object sender, ValueChangingEventArgs args)
    {
      if (this.LayoutStyleChanging == null)
        return;
      this.LayoutStyleChanging(sender, args);
    }

    protected override void OnPropertyChanged(RadPropertyChangedEventArgs e)
    {
      base.OnPropertyChanged(e);
      if (e.Property == RadElement.RightToLeftProperty)
        this.UpdateGripStyle();
      if (e.Property != RadStatusBarElement.RotateGripOnRightToLeftProperty)
        return;
      this.UpdateGripStyle();
      int num = (int) this.grip.ResetValue(RadElement.AngleTransformProperty, ValueResetFlags.Local);
    }

    private void UpdateSeparatorsItems()
    {
      int num = 0;
      if (this.Orientation == Orientation.Vertical)
        num = 90;
      foreach (RadItem radItem in (RadItemCollection) this.Items)
      {
        if (radItem is CommandBarSeparator)
          radItem.AngleTransform = (float) num;
      }
    }

    private void UpdateGripStyle()
    {
      switch (this.gripStyle)
      {
        case ToolStripGripStyle.Hidden:
          this.grip.Image.Visibility = ElementVisibility.Collapsed;
          break;
        case ToolStripGripStyle.Visible:
          switch (this.ElementTree.Control.Dock)
          {
            case DockStyle.None:
            case DockStyle.Top:
            case DockStyle.Right:
              this.grip.Image.Visibility = ElementVisibility.Collapsed;
              this.itemsBoxLayout.Margin = new Padding(0, 0, 0, 0);
              this.itemsWrapLayoutPanel.Margin = new Padding(0, 0, 0, 0);
              return;
            case DockStyle.Bottom:
            case DockStyle.Left:
            case DockStyle.Fill:
              this.grip.Image.Visibility = ElementVisibility.Visible;
              if (this.RotateGripOnRightToLeft)
                this.grip.Image.AngleTransform = this.RightToLeft ? 90f : 0.0f;
              this.itemsBoxLayout.Margin = this.RightToLeft ? new Padding(14, 0, 0, 0) : new Padding(0, 0, 14, 0);
              this.itemsWrapLayoutPanel.Margin = this.RightToLeft ? new Padding(14, 0, 0, 0) : new Padding(0, 0, 14, 0);
              return;
            default:
              return;
          }
      }
    }

    private void SetStreching()
    {
      this.ElementTree.RootElement.StretchHorizontally = false;
      this.ElementTree.RootElement.StretchVertically = false;
      this.StretchHorizontally = false;
      this.itemsBoxLayout.StretchHorizontally = false;
      this.StretchVertically = false;
      this.itemsBoxLayout.StretchVertically = false;
      if (this.LayoutStyle == RadStatusBarLayoutStyle.Stack)
      {
        this.ElementTree.RootElement.StretchVertically = true;
        this.StretchVertically = true;
        this.itemsBoxLayout.StretchVertically = true;
        this.ElementTree.RootElement.StretchHorizontally = true;
        this.StretchHorizontally = true;
        this.itemsBoxLayout.StretchHorizontally = true;
        if (this.Orientation == Orientation.Vertical)
        {
          this.ElementTree.RootElement.StretchHorizontally = false;
          this.StretchHorizontally = false;
          this.itemsBoxLayout.StretchHorizontally = false;
        }
        else
        {
          this.ElementTree.RootElement.StretchVertically = false;
          this.StretchVertically = false;
          this.itemsBoxLayout.StretchVertically = false;
        }
      }
      else if (this.Orientation == Orientation.Vertical)
      {
        this.ElementTree.RootElement.StretchVertically = true;
        this.StretchVertically = true;
        this.itemsBoxLayout.StretchVertically = true;
        this.ElementTree.RootElement.StretchHorizontally = false;
        this.StretchHorizontally = false;
        this.itemsBoxLayout.StretchHorizontally = false;
      }
      else
      {
        this.ElementTree.RootElement.StretchVertically = false;
        this.StretchVertically = false;
        this.itemsBoxLayout.StretchVertically = false;
        this.ElementTree.RootElement.StretchHorizontally = true;
        this.StretchHorizontally = true;
        this.itemsBoxLayout.StretchHorizontally = true;
      }
      this.ElementTree.Control.PerformLayout();
    }
  }
}
