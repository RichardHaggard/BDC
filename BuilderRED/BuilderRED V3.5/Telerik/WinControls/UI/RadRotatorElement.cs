// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.RadRotatorElement
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.ComponentModel;
using System.Drawing;
using Telerik.WinControls.Primitives;

namespace Telerik.WinControls.UI
{
  public class RadRotatorElement : RadItem
  {
    private RadRotatorItem rotator;
    private FillPrimitive backFill;
    private BorderPrimitive border;

    protected override void InitializeFields()
    {
      base.InitializeFields();
      this.ShouldHandleMouseInput = true;
    }

    protected override void CreateChildElements()
    {
      this.rotator = new RadRotatorItem();
      this.border = new BorderPrimitive();
      this.backFill = new FillPrimitive();
      this.backFill.Visibility = ElementVisibility.Hidden;
      this.border.Visibility = ElementVisibility.Visible;
      this.border.ZIndex = int.MaxValue;
      this.backFill.ZIndex = int.MinValue;
      this.Children.Add((RadElement) this.backFill);
      this.Children.Add((RadElement) this.border);
      this.Children.Add((RadElement) this.rotator);
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public RadItemOwnerCollection Items
    {
      get
      {
        return this.rotator.Items;
      }
    }

    [DefaultValue(null)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public RadItem DefaultItem
    {
      get
      {
        return this.rotator.DefaultItem;
      }
      set
      {
        this.rotator.DefaultItem = value;
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    [DefaultValue(2000)]
    public int Interval
    {
      get
      {
        return this.rotator.Interval;
      }
      set
      {
        this.rotator.Interval = value;
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    [DefaultValue(10)]
    public int AnimationFrames
    {
      get
      {
        return this.rotator.AnimationFrames;
      }
      set
      {
        this.rotator.AnimationFrames = value;
      }
    }

    [DefaultValue(true)]
    public bool ShouldStopOnMouseOver
    {
      get
      {
        return this.rotator.ShouldStopOnMouseOver;
      }
      set
      {
        this.rotator.ShouldStopOnMouseOver = value;
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    [DefaultValue(true)]
    public bool OpacityAnimation
    {
      get
      {
        return this.rotator.OpacityAnimation;
      }
      set
      {
        this.rotator.OpacityAnimation = value;
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public SizeF LocationAnimation
    {
      get
      {
        return this.rotator.LocationAnimation;
      }
      set
      {
        this.rotator.LocationAnimation = value;
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    public int CurrentIndex
    {
      get
      {
        return this.rotator.CurrentIndex;
      }
      set
      {
        this.rotator.CurrentIndex = value;
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    public RadItem CurrentItem
    {
      get
      {
        return this.rotator.CurrentItem;
      }
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public bool Running
    {
      get
      {
        return this.rotator.Running;
      }
      set
      {
        this.rotator.Running = value;
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    public RadRotatorItem RotatorItem
    {
      get
      {
        return this.rotator;
      }
    }

    public bool Start(bool startImmediately)
    {
      return this.RotatorItem.Start(startImmediately);
    }

    public void Stop()
    {
      this.RotatorItem.Stop();
    }

    public bool Goto(int index)
    {
      return this.RotatorItem.Goto(index);
    }

    public bool GotoDefault()
    {
      return this.RotatorItem.GotoDefault();
    }

    public bool Next()
    {
      return this.RotatorItem.Next();
    }

    public bool Previous()
    {
      return this.RotatorItem.Previous();
    }

    public event EventHandler ItemClicked
    {
      add
      {
        this.RotatorItem.ItemClicked += value;
      }
      remove
      {
        this.RotatorItem.ItemClicked -= value;
      }
    }

    public event CancelEventHandler StartRotation
    {
      add
      {
        this.RotatorItem.StartRotation += value;
      }
      remove
      {
        this.RotatorItem.StartRotation -= value;
      }
    }

    public event EventHandler StopRotation
    {
      add
      {
        this.RotatorItem.StopRotation += value;
      }
      remove
      {
        this.RotatorItem.StopRotation -= value;
      }
    }

    public event BeginRotateEventHandler BeginRotate
    {
      add
      {
        this.RotatorItem.BeginRotate += value;
      }
      remove
      {
        this.RotatorItem.BeginRotate -= value;
      }
    }

    public event EventHandler EndRotate
    {
      add
      {
        this.RotatorItem.EndRotate += value;
      }
      remove
      {
        this.RotatorItem.EndRotate -= value;
      }
    }
  }
}
