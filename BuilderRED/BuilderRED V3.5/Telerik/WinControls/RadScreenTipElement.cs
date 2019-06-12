// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.RadScreenTipElement
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.ComponentModel;
using System.Drawing;
using Telerik.WinControls.Elements;

namespace Telerik.WinControls
{
  public abstract class RadScreenTipElement : RadItem, IScreenTipContent
  {
    private string desc = "Override this property and provide custom screentip template description in DesignTime.";
    private Size tipSize = new Size(210, 50);
    private RadItemOwnerCollection items;
    private bool enableCustomSize;
    private Type parentType;

    protected override void InitializeFields()
    {
      base.InitializeFields();
      this.items = new RadItemOwnerCollection();
      this.items.ItemTypes = new Type[1]{ typeof (RadItem) };
      this.items.DefaultType = typeof (RadItem);
    }

    protected override void CreateChildElements()
    {
      this.items.Owner = (RadElement) this;
    }

    [DefaultValue(false)]
    public bool EnableCustomSize
    {
      get
      {
        return this.enableCustomSize;
      }
      set
      {
        this.enableCustomSize = value;
      }
    }

    [Browsable(true)]
    [Category("Data")]
    [RadNewItem("Type here", true)]
    public RadItemOwnerCollection Items
    {
      get
      {
        return this.items;
      }
    }

    public RadItemReadOnlyCollection TipItems
    {
      get
      {
        return new RadItemReadOnlyCollection(this.Items);
      }
    }

    [EditorBrowsable(EditorBrowsableState.Advanced)]
    public virtual string Description
    {
      get
      {
        return this.desc;
      }
      set
      {
        this.desc = value;
      }
    }

    [EditorBrowsable(EditorBrowsableState.Advanced)]
    public Type TemplateType
    {
      get
      {
        if (this.ElementTree != null)
          return (this.ElementTree.Control as RadScreenTip).TemplateType;
        return this.parentType;
      }
      set
      {
        this.parentType = value;
      }
    }

    public virtual Size TipSize
    {
      get
      {
        if (this.EnableCustomSize)
          return this.tipSize;
        return Size.Empty;
      }
      set
      {
        this.tipSize = value;
      }
    }
  }
}
