// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.RadRotator
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Telerik.Licensing;
using Telerik.WinControls.Design;
using Telerik.WinControls.Primitives;

namespace Telerik.WinControls.UI
{
  [DefaultProperty("Items")]
  [LicenseProvider(typeof (TelerikLicenseProvider))]
  [Designer("Telerik.WinControls.UI.Design.RadRotatorDesigner, Telerik.WinControls.UI.Design, Version=2018.3.1016.20, Culture=neutral, PublicKeyToken=5bb2a467cbec794e")]
  [ToolboxItem(true)]
  [RadToolboxItem(false)]
  [Description("A multipurpose component for content rotation and customization")]
  [DefaultEvent("ItemClicked")]
  [Docking(DockingBehavior.Ask)]
  public class RadRotator : RadControl
  {
    private RadRotatorElement rotatorElement;

    public RadRotator()
    {
      Size defaultSize = this.DefaultSize;
      this.ElementTree.PerformInnerLayout(true, 0, 0, defaultSize.Width, defaultSize.Height);
    }

    protected override void CreateChildItems(RadElement parent)
    {
      this.rotatorElement = new RadRotatorElement();
      parent.Children.Add((RadElement) this.rotatorElement);
    }

    [Category("Layout")]
    [DefaultValue(false)]
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public override bool AutoSize
    {
      get
      {
        return base.AutoSize;
      }
      set
      {
        base.AutoSize = value;
      }
    }

    protected override Size DefaultSize
    {
      get
      {
        return RadControl.GetDpiScaledSize(new Size(200, 180));
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    public RadRotatorElement RotatorElement
    {
      get
      {
        return this.rotatorElement;
      }
    }

    [Description("Gets or sets whether RadRotator should stop rotating on MouseOver")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    [DefaultValue(true)]
    [Category("Behavior")]
    [Browsable(true)]
    public bool ShouldStopOnMouseOver
    {
      get
      {
        return this.RotatorElement.RotatorItem.ShouldStopOnMouseOver;
      }
      set
      {
        this.RotatorElement.RotatorItem.ShouldStopOnMouseOver = value;
      }
    }

    [Category("Data")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [Browsable(true)]
    public RadItemCollection Items
    {
      get
      {
        return (RadItemCollection) this.rotatorElement.RotatorItem.Items;
      }
    }

    [Browsable(false)]
    [DefaultValue(null)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Category("Data")]
    [RadNewItem("DefaultItem", false)]
    public RadItem DefaultItem
    {
      get
      {
        return this.rotatorElement.RotatorItem.DefaultItem;
      }
      set
      {
        this.rotatorElement.RotatorItem.DefaultItem = value;
      }
    }

    [DefaultValue(2000)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public int Interval
    {
      get
      {
        return this.rotatorElement.RotatorItem.Interval;
      }
      set
      {
        this.rotatorElement.RotatorItem.Interval = value;
      }
    }

    [DefaultValue(10)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public int AnimationFrames
    {
      get
      {
        return this.rotatorElement.RotatorItem.AnimationFrames;
      }
      set
      {
        this.rotatorElement.RotatorItem.AnimationFrames = value;
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    [DefaultValue(true)]
    public bool OpacityAnimation
    {
      get
      {
        return this.rotatorElement.RotatorItem.OpacityAnimation;
      }
      set
      {
        this.rotatorElement.RotatorItem.OpacityAnimation = value;
      }
    }

    [DefaultValue(typeof (SizeF), "0,-1")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    [ExtenderProvidedProperty]
    public SizeF LocationAnimation
    {
      get
      {
        return this.rotatorElement.RotatorItem.LocationAnimation;
      }
      set
      {
        this.rotatorElement.RotatorItem.LocationAnimation = value;
      }
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public int CurrentIndex
    {
      get
      {
        return this.rotatorElement.RotatorItem.CurrentIndex;
      }
      set
      {
        this.rotatorElement.RotatorItem.CurrentIndex = value;
      }
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public RadItem CurrentItem
    {
      get
      {
        return this.rotatorElement.RotatorItem.CurrentItem;
      }
    }

    [DefaultValue(false)]
    public bool Running
    {
      get
      {
        return this.rotatorElement.RotatorItem.Running;
      }
      set
      {
        this.rotatorElement.RotatorItem.Running = value;
      }
    }

    public bool Start(bool startImmediately)
    {
      return this.rotatorElement.RotatorItem.Start(startImmediately);
    }

    public bool Start()
    {
      return this.Start(false);
    }

    public void Stop()
    {
      this.rotatorElement.RotatorItem.Stop();
    }

    public bool Goto(int index)
    {
      return this.rotatorElement.RotatorItem.Goto(index);
    }

    public void GotoDefault()
    {
      this.rotatorElement.RotatorItem.GotoDefault();
    }

    public bool Previous()
    {
      return this.rotatorElement.RotatorItem.Previous();
    }

    public bool Next()
    {
      return this.rotatorElement.RotatorItem.Next();
    }

    protected override void OnThemeChanged()
    {
      base.OnThemeChanged();
      if (TelerikHelper.IsMaterialTheme(this.ThemeName))
      {
        this.RootElement.SetThemeValueOverride(RadItem.EnableElementShadowProperty, (object) true, "");
        this.RotatorElement.SetThemeValueOverride(VisualElement.ForeColorProperty, (object) Color.Transparent, "", typeof (BorderPrimitive));
      }
      else
      {
        this.RootElement.ResetThemeValueOverride(RadItem.EnableElementShadowProperty);
        int num = (int) this.RotatorElement.Children[1].ResetValue(VisualElement.ForeColorProperty, ValueResetFlags.Style);
      }
    }

    public event EventHandler ItemClicked
    {
      add
      {
        this.rotatorElement.RotatorItem.ItemClicked += value;
      }
      remove
      {
        this.rotatorElement.RotatorItem.ItemClicked -= value;
      }
    }

    public event CancelEventHandler StartRotation
    {
      add
      {
        this.rotatorElement.RotatorItem.StartRotation += value;
      }
      remove
      {
        this.rotatorElement.RotatorItem.StartRotation -= value;
      }
    }

    public event EventHandler StopRotation
    {
      add
      {
        this.rotatorElement.RotatorItem.StopRotation += value;
      }
      remove
      {
        this.rotatorElement.RotatorItem.StopRotation -= value;
      }
    }

    public event BeginRotateEventHandler BeginRotate
    {
      add
      {
        this.rotatorElement.RotatorItem.BeginRotate += value;
      }
      remove
      {
        this.rotatorElement.RotatorItem.BeginRotate -= value;
      }
    }

    public event EventHandler EndRotate
    {
      add
      {
        this.rotatorElement.RotatorItem.EndRotate += value;
      }
      remove
      {
        this.rotatorElement.RotatorItem.EndRotate -= value;
      }
    }
  }
}
