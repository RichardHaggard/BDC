// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.RadCommandBarMultilineToolstripHolderPanel
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Telerik.WinControls.Design;
using Telerik.WinControls.Elements;
using Telerik.WinControls.Layouts;

namespace Telerik.WinControls.UI
{
  public class RadCommandBarMultilineToolstripHolderPanel : RadCommandBarVisualElement, IItemsOwner
  {
    protected List<CommandBarRowElement> lines;
    private RadItemOwnerCollection items;
    protected StackLayoutPanel stackLayout;

    protected override void CreateChildElements()
    {
      base.CreateChildElements();
      this.stackLayout = new StackLayoutPanel();
      this.stackLayout.Orientation = Orientation.Vertical;
      this.items = new RadItemOwnerCollection();
      this.Items.ItemTypes = new System.Type[1]
      {
        typeof (CommandBarStripElement)
      };
      this.lines = new List<CommandBarRowElement>(1);
      this.AddLine();
      this.items = new RadItemOwnerCollection((RadElement) this.stackLayout);
      this.Items.ItemTypes = new System.Type[1]
      {
        typeof (CommandBarStripElement)
      };
      this.SyncStackLayoutWithLines();
      this.Children.Add((RadElement) this.stackLayout);
    }

    protected override SizeF MeasureOverride(SizeF constraint)
    {
      return base.MeasureOverride(constraint);
    }

    protected override SizeF ArrangeOverride(SizeF arrangeSize)
    {
      return base.ArrangeOverride(arrangeSize);
    }

    [RadPropertyDefaultValue("Orientation", typeof (StackLayoutPanel))]
    [Category("Behavior")]
    public override Orientation Orientation
    {
      get
      {
        return this.stackLayout.Orientation;
      }
      set
      {
        this.stackLayout.Orientation = value;
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public RadItemOwnerCollection Items
    {
      get
      {
        return this.items;
      }
    }

    protected virtual void AddLine()
    {
    }

    protected virtual void SyncStackLayoutWithLines()
    {
      this.stackLayout.Orientation = Orientation.Vertical;
      this.stackLayout.Children.Clear();
      foreach (RadElement line in this.lines)
        this.stackLayout.Children.Add(line);
      this.InvalidateMeasure(true);
      this.UpdateLayout();
    }

    public virtual void MoveToUpperLine(
      CommandBarStripElement element,
      CommandBarRowElement currentHolder)
    {
      int index = this.lines.IndexOf(currentHolder) - 1;
      if (index < 0)
        index = 0;
      currentHolder.Children.Remove((RadElement) element);
      if (currentHolder.Children.Count == 0)
        this.lines.Remove(currentHolder);
      this.lines[index].Children.Add((RadElement) element);
      this.SyncStackLayoutWithLines();
    }
  }
}
