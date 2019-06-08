// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.AutoCompleteAppendHelper
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;

namespace Telerik.WinControls.UI
{
  public class AutoCompleteAppendHelper : BaseAutoComplete
  {
    private string findString = "";
    private StringComparison stringComparison;
    private bool limitToList;

    public AutoCompleteAppendHelper(RadDropDownListElement owner)
      : base(owner)
    {
    }

    public bool LimitToList
    {
      get
      {
        return this.limitToList;
      }
      set
      {
        this.limitToList = value;
      }
    }

    protected string FindString
    {
      get
      {
        return this.findString;
      }
    }

    protected StringComparison StringComparison
    {
      get
      {
        return this.stringComparison;
      }
    }

    public override void AutoComplete(KeyPressEventArgs e)
    {
      this.AutoComplete(e, this.limitToList);
    }

    public void AutoComplete(KeyPressEventArgs e, bool limitToList)
    {
      if (e.KeyChar == '\r')
      {
        this.Owner.SelectAllText();
        this.Owner.Focus();
      }
      else
        this.SearchForStringInList(this.CreateFindString(e), e, limitToList);
    }

    private string CreateFindString(KeyPressEventArgs e)
    {
      return this.Owner.SelectionLength != 0 ? this.Owner.EditableElementText.Substring(0, this.Owner.SelectionStart) + (object) e.KeyChar : this.Owner.EditableElementText + (object) e.KeyChar;
    }

    private void SearchForStringInList(string findString, KeyPressEventArgs e, bool limitToList)
    {
      int itemIndex = -1;
      if (this.Owner.Items.Count > 0)
        itemIndex = this.FindShortestString(findString);
      if (itemIndex == -1)
      {
        e.Handled = limitToList && e.KeyChar != '\b';
      }
      else
      {
        this.SetEditableElementText(itemIndex);
        this.Owner.SelectionStart = findString.Length;
        this.Owner.SelectionLength = this.Owner.EditableElementText.Length;
        e.Handled = true;
      }
    }

    public virtual void SearchForStringInList(string findString)
    {
      int itemIndex = -1;
      if (this.Owner.Items.Count > 0)
        itemIndex = this.FindShortestString(findString);
      if (itemIndex == -1)
        return;
      this.SetEditableElementText(itemIndex);
      this.Owner.SelectionStart = findString.Length;
      this.Owner.SelectionLength = this.Owner.EditableElementText.Length;
    }

    protected virtual void SetEditableElementText(int itemIndex)
    {
      this.Owner.EditableElementText = this.Owner.Items[itemIndex].CachedText;
    }

    [EditorBrowsable(EditorBrowsableState.Advanced)]
    public int FindShortestString(string findString)
    {
      int count = this.Owner.ListElement.Items.Count;
      int num1 = -1;
      int num2 = int.MaxValue;
      int length1 = findString.Length;
      this.stringComparison = this.Owner.CaseSensitive ? StringComparison.InvariantCulture : StringComparison.InvariantCultureIgnoreCase;
      this.findString = findString;
      SortedDictionary<string, RadListDataItem> sortedDictionary = new SortedDictionary<string, RadListDataItem>();
      for (int index = 0; index < count; ++index)
      {
        RadListDataItem radListDataItem = this.Owner.ListElement.Items[index];
        if (radListDataItem.Enabled)
        {
          int length2 = radListDataItem.CachedText.Length;
          if (this.DefaultCompare(radListDataItem))
          {
            if (length1 == length2)
              return index;
            if (length1 < length2)
            {
              if (length2 < num2)
              {
                num2 = radListDataItem.CachedText.Length;
                num1 = radListDataItem.RowIndex;
              }
              sortedDictionary[radListDataItem.CachedText] = radListDataItem;
            }
          }
        }
      }
      IEnumerator<string> enumerator = (IEnumerator<string>) sortedDictionary.Keys.GetEnumerator();
      if (!enumerator.MoveNext())
        return num1;
      return sortedDictionary[enumerator.Current].RowIndex;
    }

    protected virtual bool DefaultCompare(RadListDataItem item)
    {
      return item.CachedText.StartsWith(this.FindString, this.StringComparison);
    }
  }
}
