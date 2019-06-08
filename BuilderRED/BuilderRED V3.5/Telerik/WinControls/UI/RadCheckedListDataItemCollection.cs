// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.RadCheckedListDataItemCollection
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.Collections.Generic;
using System.ComponentModel;

namespace Telerik.WinControls.UI
{
  public class RadCheckedListDataItemCollection : RadListDataItemCollection
  {
    private RadCheckedDropDownListElement owner;

    public RadCheckedListDataItemCollection(
      RadCheckedDropDownListElement owner,
      ListDataLayer dataLayer,
      RadListElement ownerListElement)
      : base(dataLayer, ownerListElement)
    {
      this.owner = owner;
    }

    public void Add(RadCheckedListDataItem item)
    {
      base.Add((RadListDataItem) item);
    }

    public override void Add(string text)
    {
      base.Add((RadListDataItem) new RadCheckedListDataItem(text));
    }

    public void Add(string text, bool check)
    {
      base.Add((RadListDataItem) new RadCheckedListDataItem(text, check));
    }

    public override void AddRange(IEnumerable<string> textStrings)
    {
      foreach (string textString in textStrings)
        base.Add((RadListDataItem) new RadCheckedListDataItem(textString));
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    [Description("Use Add(RadCheckedListDataItem item) method instead.")]
    public override void Add(RadListDataItem item)
    {
      if (item is RadCheckedListDataItem)
        base.Add(item);
      else
        base.Add((RadListDataItem) this.CreateCheckedListDataItem(item));
    }

    protected virtual RadCheckedListDataItem CreateCheckedListDataItem(
      RadListDataItem item)
    {
      RadCheckedListDataItem checkedListDataItem = new RadCheckedListDataItem(item.Text, false);
      checkedListDataItem.Value = item.Value;
      checkedListDataItem.Tag = item.Tag;
      checkedListDataItem.Height = item.Height;
      checkedListDataItem.Image = item.Image;
      checkedListDataItem.ImageAlignment = item.ImageAlignment;
      checkedListDataItem.TextAlignment = item.TextAlignment;
      checkedListDataItem.TextImageRelation = item.TextImageRelation;
      checkedListDataItem.TextWrap = item.TextWrap;
      checkedListDataItem.TextOrientation = item.TextOrientation;
      return checkedListDataItem;
    }

    public RadCheckedListDataItem this[int index]
    {
      get
      {
        return (RadCheckedListDataItem) base[index];
      }
      set
      {
        this[index] = (RadListDataItem) value;
      }
    }
  }
}
