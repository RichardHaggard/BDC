// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.RadTextBoxListElement
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Drawing;
using System.Windows.Forms;
using Telerik.WinControls.Data;
using Telerik.WinControls.Enumerations;

namespace Telerik.WinControls.UI
{
  public class RadTextBoxListElement : RadListElement
  {
    private string patternText;
    private string suggestedText;
    private int suggestionTextUpdateCount;
    private AutoCompleteMode autoCompleteMode;
    private TextPosition startPosition;
    private TextPosition endPosition;

    public RadTextBoxListElement()
    {
      this.Filter = new Predicate<RadListDataItem>(this.AutoCompleteFilter);
      this.SortStyle = SortStyle.Ascending;
      this.FormattingEnabled = false;
    }

    protected override System.Type ThemeEffectiveType
    {
      get
      {
        return typeof (RadListElement);
      }
    }

    public AutoCompleteMode AutoCompleteMode
    {
      get
      {
        return this.autoCompleteMode;
      }
      set
      {
        this.autoCompleteMode = value;
      }
    }

    public string SuggestedText
    {
      get
      {
        return this.suggestedText;
      }
    }

    public string PatternText
    {
      get
      {
        return this.patternText;
      }
    }

    public bool IsSuggestionMatched
    {
      get
      {
        return this.IsExactSuggestion(this.suggestedText, this.patternText);
      }
    }

    protected bool IsSuggestMode
    {
      get
      {
        if (this.autoCompleteMode != AutoCompleteMode.Suggest)
          return this.autoCompleteMode == AutoCompleteMode.SuggestAppend;
        return true;
      }
    }

    protected bool IsAppendMode
    {
      get
      {
        if (this.autoCompleteMode != AutoCompleteMode.Append)
          return this.autoCompleteMode == AutoCompleteMode.SuggestAppend;
        return true;
      }
    }

    public TextPosition StartPosition
    {
      get
      {
        return this.startPosition;
      }
      set
      {
        this.startPosition = value;
      }
    }

    public TextPosition EndPosition
    {
      get
      {
        return this.endPosition;
      }
      set
      {
        this.endPosition = value;
      }
    }

    public event SuggestedTextChangedEventHandler SuggestedTextChanged;

    protected void OnSuggestedTextChanged(AutoCompleteAction action)
    {
      this.OnSuggestedTextChanged(new SuggestedTextChangedEventArgs(this.patternText, this.suggestedText, this.startPosition, this.endPosition, action));
    }

    protected virtual void OnSuggestedTextChanged(SuggestedTextChangedEventArgs e)
    {
      SuggestedTextChangedEventHandler suggestedTextChanged = this.SuggestedTextChanged;
      if (suggestedTextChanged != null && this.suggestionTextUpdateCount == 0)
        suggestedTextChanged((object) this, e);
      this.startPosition = e.StartPosition;
      this.endPosition = e.EndPosition;
    }

    protected internal override void OnSelectedIndexChanged(int newIndex)
    {
      base.OnSelectedIndexChanged(newIndex);
      if (newIndex == -1 || this.startPosition == (TextPosition) null && this.endPosition == (TextPosition) null)
        return;
      bool performAppend = this.autoCompleteMode == AutoCompleteMode.Append;
      this.suggestedText = this.GetSuggestedText(this.Items[newIndex], performAppend);
      this.OnSuggestedTextChanged(performAppend ? AutoCompleteAction.Append : AutoCompleteAction.Replace);
    }

    protected bool IsMatching(string suggestion, string pattern)
    {
      return suggestion.ToUpperInvariant() == pattern.ToUpperInvariant();
    }

    protected bool IsExactSuggestion(string suggestion, string pattern)
    {
      if (this.IsMatching(this.suggestedText, pattern))
        return this.DataLayer.Items.Count == 1;
      return false;
    }

    public void SuspendSuggestNotifications()
    {
      ++this.suggestionTextUpdateCount;
    }

    public void ResumeSuggestNotifications()
    {
      if (this.suggestionTextUpdateCount <= 0)
        return;
      --this.suggestionTextUpdateCount;
    }

    public void Suggest(string pattern, TextPosition startPosition, TextPosition endPosition)
    {
      this.Suggest(pattern, startPosition, endPosition, true);
    }

    public void Suggest(
      string pattern,
      TextPosition startPosition,
      TextPosition endPosition,
      bool notify)
    {
      this.startPosition = startPosition;
      this.endPosition = endPosition;
      if (!notify)
        this.SuspendSuggestNotifications();
      this.SuggestOverride(pattern);
      if (notify)
        return;
      this.ResumeSuggestNotifications();
    }

    protected virtual void SuggestOverride(string pattern)
    {
      int selectedIndex = this.SelectedIndex;
      this.patternText = pattern;
      this.SuspendSuggestNotifications();
      if (selectedIndex >= 0)
      {
        RadListDataItem radListDataItem = this.Items[selectedIndex];
        radListDataItem.Selected = false;
        radListDataItem.Active = false;
      }
      this.DataLayer.Refresh();
      this.SelectedIndex = -1;
      this.ResumeSuggestNotifications();
      RadListDataItemCollection items = this.DataLayer.Items;
      int count = items.Count;
      if (count > 0)
      {
        this.UpdateLayout();
        this.ScrollToItem(items.First);
      }
      bool flag = this.IsAppendMode && count > 0;
      string str = string.Empty;
      if (flag)
        str = this.GetSuggestedText(items.First, true);
      else if (!this.IsSuggestMode && this.IsAppendMode)
        str = this.patternText;
      else if (count == 1)
        str = this.GetSuggestedText(items.First, false);
      AutoCompleteAction action = AutoCompleteAction.None;
      if (flag && !this.IsExactSuggestion(str, this.patternText))
        action = AutoCompleteAction.Append;
      this.SetSuggestedText(str, action);
    }

    protected virtual bool AutoCompleteFilter(RadListDataItem item)
    {
      RadTextBoxAutoCompleteDropDown completeDropDown = this.ElementTree != null ? this.ElementTree.Control as RadTextBoxAutoCompleteDropDown : (RadTextBoxAutoCompleteDropDown) null;
      if (completeDropDown != null && completeDropDown.OwnerElement.ElementTree != null && completeDropDown.OwnerElement.ElementTree.Control.Site != null)
        return true;
      if (string.IsNullOrEmpty(this.patternText))
        return false;
      return this.AutoCompleteFilterOverride(item);
    }

    protected virtual bool AutoCompleteFilterOverride(RadListDataItem item)
    {
      return (item.Text ?? string.Empty).ToUpperInvariant().StartsWith(this.patternText.ToUpperInvariant());
    }

    internal override bool ProcessKeyboardSelection(Keys keyCode)
    {
      if (keyCode != Keys.Up && keyCode != Keys.Down && (keyCode != Keys.Prior && keyCode != Keys.Next))
        return false;
      int count = this.Items.Count;
      if (count <= 0)
        return false;
      if (keyCode == Keys.Prior || keyCode == Keys.Next)
        keyCode = keyCode == Keys.Prior ? Keys.Up : Keys.Down;
      if (this.startPosition == (TextPosition) null && this.endPosition == (TextPosition) null)
        return false;
      if (!this.IsSuggestMode && this.SelectedItem == null && count > 1 && (keyCode == Keys.Up || keyCode == Keys.Down))
      {
        ++this.suggestionTextUpdateCount;
        this.SelectedItem = this.DataLayer.Items.First;
        --this.suggestionTextUpdateCount;
      }
      bool flag = base.ProcessKeyboardSelection(keyCode);
      if (!flag)
      {
        if (this.IsSuggestMode && this.suggestedText != this.patternText)
        {
          this.SetSuggestedText(this.patternText, AutoCompleteAction.Replace);
          ++this.suggestionTextUpdateCount;
          this.SelectedItem = (RadListDataItem) null;
          --this.suggestionTextUpdateCount;
        }
        else
        {
          switch (keyCode)
          {
            case Keys.Up:
              this.SelectedItem = this.DataLayer.Items.Last;
              break;
            case Keys.Down:
              this.SelectedItem = this.DataLayer.Items.First;
              break;
          }
        }
      }
      return flag;
    }

    protected void SetSuggestedText(string text, AutoCompleteAction action)
    {
      this.suggestedText = text;
      this.OnSuggestedTextChanged(action);
    }

    protected internal override void OnItemsChanged(NotifyCollectionChangedEventArgs args)
    {
      base.OnItemsChanged(args);
      if (args.Action != NotifyCollectionChangedAction.Reset)
        return;
      this.patternText = (string) null;
    }

    protected string GetSuggestedText(RadListDataItem item, bool performAppend)
    {
      string str = item.Text;
      if (performAppend)
        str = this.patternText + str.Substring(this.patternText.Length);
      return str;
    }

    public RadListDataItem GetFirstFullyVisibleItem()
    {
      return this.GetFullyVisibleItem(true);
    }

    public RadListDataItem GetLastFullyVisibleItem()
    {
      return this.GetFullyVisibleItem(false);
    }

    protected virtual RadListDataItem GetFullyVisibleItem(bool firstItem)
    {
      RadElementCollection children = this.ViewElement.Children;
      if (children.Count > 0)
      {
        int index = firstItem ? 0 : children.Count - 1;
        RadListVisualItem radListVisualItem;
        do
        {
          radListVisualItem = children[index] as RadListVisualItem;
          index = firstItem ? index + 1 : index - 1;
        }
        while (this.IsItemPartiallyVisible(radListVisualItem) && index < children.Count && index >= 0);
        if (!this.IsItemPartiallyVisible(radListVisualItem))
          return radListVisualItem.Data;
      }
      return (RadListDataItem) null;
    }

    public RadListVisualItem GetVisualItemAtPoint(Point location)
    {
      for (RadElement radElement = this.ElementTree.GetElementAtPoint(location); radElement != null; radElement = radElement.Parent)
      {
        RadListVisualItem radListVisualItem = radElement as RadListVisualItem;
        if (radListVisualItem != null)
          return radListVisualItem;
      }
      return (RadListVisualItem) null;
    }

    public virtual RadListDataItem Find(string text)
    {
      RadListDataItem selectedItem = this.SelectedItem;
      if (selectedItem != null && this.IsMatching(selectedItem.Text, text))
        return selectedItem;
      for (int index = 0; index < this.DataLayer.ListSource.Count; ++index)
      {
        RadListDataItem radListDataItem = this.DataLayer.ListSource[index];
        if (this.IsMatching(radListDataItem.Text, text))
          return radListDataItem;
      }
      return (RadListDataItem) null;
    }
  }
}
