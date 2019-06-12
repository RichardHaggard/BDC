// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.RadCalendarFastNavigationElement
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Telerik.WinControls.UI
{
  public class RadCalendarFastNavigationElement : LightVisualElement
  {
    private Padding childrenMargin = new Padding(1, 1, 1, 1);
    private RadItemOwnerCollection items;

    protected override void InitializeFields()
    {
      base.InitializeFields();
      this.items = new RadItemOwnerCollection();
      this.items.ItemTypes = new System.Type[1]
      {
        typeof (FastNavigationItem)
      };
      this.items.ItemsChanged += new ItemChangedDelegate(this.items_ItemsChanged);
      this.DrawFill = true;
      this.DrawBorder = true;
      this.Shape = (ElementShape) new RoundRectShape(5);
      this.Margin = new Padding(1, 1, 1, 1);
    }

    protected override void DisposeManagedResources()
    {
      this.items.ItemsChanged -= new ItemChangedDelegate(this.items_ItemsChanged);
      base.DisposeManagedResources();
    }

    public Padding ChildrenMargin
    {
      get
      {
        return this.childrenMargin;
      }
      set
      {
        this.childrenMargin = value;
      }
    }

    [DefaultValue(null)]
    [Description("Gets the items collection of the element")]
    [Category("Behavior")]
    public RadItemOwnerCollection Items
    {
      get
      {
        return this.items;
      }
    }

    private void items_ItemsChanged(
      RadItemCollection changed,
      RadItem target,
      ItemsChangeOperation operation)
    {
      switch (operation)
      {
        case ItemsChangeOperation.Inserted:
          this.Children.Add((RadElement) target);
          break;
        case ItemsChangeOperation.Cleared:
          for (int index = this.Children.Count - 1; index >= 0; --index)
          {
            FastNavigationItem child = this.Children[index] as FastNavigationItem;
            if (child != null)
              this.Children.Remove((RadElement) child);
          }
          break;
      }
    }

    protected override SizeF ArrangeOverride(SizeF finalSize)
    {
      if (this.ElementTree == null || this.ElementTree.Control.Parent == null)
        return base.ArrangeOverride(finalSize);
      float val1 = 0.0f;
      float num = 0.0f;
      for (int index = 0; index < this.Items.Count; ++index)
      {
        SizeF desiredSize = this.Items[index].DesiredSize;
        num = Math.Max(num, desiredSize.Height);
        val1 = Math.Max(val1, desiredSize.Width);
      }
      for (int index = 0; index < this.Items.Count; ++index)
        this.Items[index].Arrange(new RectangleF((float) this.ChildrenMargin.Left, (float) index * num + (float) this.ChildrenMargin.Top, finalSize.Width - (float) this.ChildrenMargin.Horizontal - (float) this.Margin.Horizontal, num));
      return finalSize;
    }
  }
}
