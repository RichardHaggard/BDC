// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.FilterMenuListItem
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.Collections;
using System.Drawing;
using System.Windows.Forms;

namespace Telerik.WinControls.UI
{
  public class FilterMenuListItem : RadMenuItemBase
  {
    private FilterMenuListElement list;
    private RadListFilterDistinctValuesTable distinctValues;

    public RadListControl ListControl
    {
      get
      {
        return this.list.ListControl;
      }
    }

    public RadListFilterDistinctValuesTable DistinctListValues
    {
      set
      {
        this.distinctValues = value;
        this.PopulateList();
      }
    }

    protected override void InitializeFields()
    {
      base.InitializeFields();
      this.Padding = new Padding(5, 5, 5, 0);
    }

    private void PopulateList()
    {
      foreach (DictionaryEntry dictionaryEntry in new SortedList((IDictionary) this.distinctValues, (IComparer) new ListFilterComparer((IDictionary) this.distinctValues)))
        this.list.ListControl.Items.Add(new RadListDataItem((string) dictionaryEntry.Key, dictionaryEntry.Value));
    }

    protected override void CreateChildElements()
    {
      base.CreateChildElements();
      this.list = new FilterMenuListElement();
      this.Children.Add((RadElement) this.list);
    }

    protected override SizeF MeasureOverride(SizeF availableSize)
    {
      base.MeasureOverride(availableSize);
      return new SizeF(0.0f, 40f);
    }

    protected override SizeF ArrangeOverride(SizeF finalSize)
    {
      RectangleF clientRectangle = this.GetClientRectangle(finalSize);
      RadDropDownMenuLayout ancestor = this.FindAncestor<RadDropDownMenuLayout>();
      if (ancestor != null)
      {
        clientRectangle.X += this.RightToLeft ? 0.0f : ancestor.LeftColumnWidth;
        clientRectangle.Width -= ancestor.LeftColumnWidth;
      }
      foreach (RadElement child in this.Children)
      {
        if (child == this.list)
          child.Arrange(new RectangleF(clientRectangle.X, 0.0f, clientRectangle.Width, clientRectangle.Height));
        else
          child.Arrange(clientRectangle);
      }
      return finalSize;
    }
  }
}
