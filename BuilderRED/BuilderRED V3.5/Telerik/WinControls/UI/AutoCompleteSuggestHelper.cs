// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.AutoCompleteSuggestHelper
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
  public class AutoCompleteSuggestHelper : BaseAutoComplete
  {
    private StringComparison stringComparison = StringComparison.InvariantCultureIgnoreCase;
    private string filter;
    private RadDropDownListElement dropDownList;
    private bool isItemsDirty;
    private bool isUpdating;
    private SuggestMode suggestMode;

    public virtual bool IsItemsDirty
    {
      get
      {
        if (this.isItemsDirty || this.dropDownList.ListElement.Items.Count == 0)
          return this.dropDownList.DataSource == null;
        return false;
      }
      internal set
      {
        this.isItemsDirty = value;
      }
    }

    public AutoCompleteSuggestHelper(RadDropDownListElement owner)
      : base(owner)
    {
      this.filter = "";
      this.dropDownList = this.CreateDropDownElement();
      owner.Children.Add((RadElement) this.dropDownList);
      this.dropDownList.isSuggestMode = true;
      this.dropDownList.Visibility = ElementVisibility.Hidden;
      this.dropDownList.MaxSize = new Size(0, this.Owner.Size.Height);
      this.dropDownList.BorderThickness = new Padding(1, 0, 1, 0);
      this.dropDownList.DropDownSizingMode = SizingMode.RightBottom;
      this.AutoCompleteDataSource = owner.AutoCompleteDataSource;
      this.AutoCompleteDisplayMember = owner.AutoCompleteDisplayMember;
      this.AutoCompleteValueMember = owner.AutoCompleteValueMember;
      this.dropDownList.ListElement.SelectedIndexChanged += new Telerik.WinControls.UI.Data.PositionChangedEventHandler(this.SelectedIndexChanged);
      this.Owner.ListElement.ItemsChanged += new NotifyCollectionChangedEventHandler(this.CollectionChanged);
      this.dropDownList.PopupClosed += new RadPopupClosedEventHandler(this.PopupClosed);
    }

    protected virtual RadDropDownListElement CreateDropDownElement()
    {
      return new RadDropDownListElement();
    }

    protected virtual string Filter
    {
      get
      {
        return this.filter;
      }
    }

    public SuggestMode SuggestMode
    {
      get
      {
        return this.suggestMode;
      }
      set
      {
        this.suggestMode = value;
      }
    }

    protected virtual StringComparison StringComparison
    {
      get
      {
        return this.stringComparison;
      }
    }

    public virtual object AutoCompleteDataSource
    {
      get
      {
        return this.dropDownList.DataSource;
      }
      set
      {
        this.isUpdating = true;
        this.dropDownList.DataSource = value;
        this.isUpdating = false;
      }
    }

    public virtual string AutoCompleteValueMember
    {
      get
      {
        return this.dropDownList.ValueMember;
      }
      set
      {
        this.isUpdating = true;
        this.dropDownList.ValueMember = value;
        this.isUpdating = false;
      }
    }

    public virtual string AutoCompleteDisplayMember
    {
      get
      {
        return this.dropDownList.DisplayMember;
      }
      set
      {
        this.isUpdating = true;
        this.dropDownList.DisplayMember = value;
        this.isUpdating = false;
      }
    }

    public virtual RadDropDownListElement DropDownList
    {
      get
      {
        return this.dropDownList;
      }
      set
      {
        this.dropDownList = value;
      }
    }

    public virtual void PopupClosed(object sender, RadPopupClosedEventArgs args)
    {
      if (args.CloseReason != RadPopupCloseReason.Keyboard)
        return;
      this.Owner.SelectAllText();
    }

    public virtual void CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
    {
      this.isItemsDirty = this.dropDownList.DataSource == null;
    }

    protected virtual int ClampSelectedIndex(bool down, int selectedIndex, int itemsCount)
    {
      bool flag = false;
      int num = selectedIndex;
      if (down)
      {
        while (selectedIndex < itemsCount - 1)
        {
          ++selectedIndex;
          if (this.dropDownList.Items.Count > selectedIndex && this.dropDownList.Items[selectedIndex].Enabled)
          {
            flag = true;
            break;
          }
        }
        if (!flag)
          selectedIndex = num;
      }
      else
      {
        while (selectedIndex > 0)
        {
          --selectedIndex;
          if (this.dropDownList.Items.Count > selectedIndex && this.dropDownList.Items[selectedIndex].Enabled)
          {
            flag = true;
            break;
          }
        }
        if (!flag)
          selectedIndex = num;
      }
      return selectedIndex;
    }

    public virtual void HandleSelectNextOrPrev(bool next)
    {
      this.dropDownList.ListElement.SelectedIndex = this.ClampSelectedIndex(next, this.dropDownList.ListElement.SelectedIndex, this.dropDownList.ListElement.Items.Count);
    }

    public override void AutoComplete(KeyPressEventArgs e)
    {
      if (e.KeyChar == '\r')
      {
        this.Owner.Focus();
        this.Owner.ClosePopup(RadPopupCloseReason.Keyboard);
      }
      else
        this.HandleAutoSuggest(e.KeyChar != '\b' || this.Owner.EditableElementText.Length < 1 ? this.SetFindString(e) : this.SetFindStringFromBack());
    }

    protected virtual bool DefaultFilter(RadListDataItem item)
    {
      switch (this.suggestMode)
      {
        case SuggestMode.StartWiths:
          return item.Text.StartsWith(this.Filter, this.StringComparison);
        case SuggestMode.Contains:
          if ((this.StringComparison & StringComparison.InvariantCultureIgnoreCase) == StringComparison.InvariantCultureIgnoreCase || (this.StringComparison & StringComparison.InvariantCultureIgnoreCase) == StringComparison.CurrentCultureIgnoreCase)
            return item.Text.ToLower().Contains(this.Filter.ToLower());
          return item.Text.Contains(this.Filter);
        default:
          return item.Text.StartsWith(this.Filter, this.StringComparison);
      }
    }

    protected virtual void SyncOwnerElementWithSelectedIndex()
    {
      string text = this.dropDownList.ListElement.SelectedItem.Text;
      if (this.Owner.SelectItemFromText(text, true, this.dropDownList.ListElement.SelectedItem.Value) != -1)
        return;
      this.Owner.Text = text;
    }

    protected virtual void SyncItems()
    {
      if (!this.IsItemsDirty)
        return;
      this.isItemsDirty = false;
      this.SyncItemsCore();
    }

    protected virtual void SyncItemsCore()
    {
      this.dropDownList.ListElement.Items.Clear();
      this.dropDownList.ListElement.BeginUpdate();
      foreach (RadListDataItem radListDataItem in this.Owner.Items)
        this.dropDownList.ListElement.Items.Add(new RadListDataItem(radListDataItem.Text)
        {
          Tag = (object) radListDataItem,
          Value = radListDataItem.Value,
          TextWrap = radListDataItem.TextWrap,
          Enabled = radListDataItem.Enabled
        });
      this.dropDownList.ListElement.EndUpdate();
    }

    protected virtual void SelectedIndexChanged(object sender, Telerik.WinControls.UI.Data.PositionChangedEventArgs e)
    {
      if (this.isUpdating || this.dropDownList.ListElement.SelectedIndex == -1)
        return;
      this.SyncOwnerElementWithSelectedIndex();
    }

    public virtual string SetFindStringFromBack()
    {
      return this.Owner.SelectionLength != 0 ? this.Owner.EditableElementText.Substring(0, this.Owner.SelectionStart) : this.Owner.EditableElementText.Substring(0, this.Owner.EditableElementText.Length - 1);
    }

    public virtual string SetFindString(KeyPressEventArgs e)
    {
      return this.Owner.SelectionLength != 0 ? this.Owner.EditableElementText.Substring(0, this.Owner.SelectionStart) + (object) e.KeyChar : this.Owner.EditableElementText + (object) e.KeyChar;
    }

    public virtual void HandleAutoSuggest(string filter)
    {
      bool suspendSelectionEvents = this.dropDownList.SuspendSelectionEvents;
      this.dropDownList.SuspendSelectionEvents = true;
      this.SyncItems();
      this.dropDownList.SuspendSelectionEvents = suspendSelectionEvents;
      this.stringComparison = this.Owner.CaseSensitive ? StringComparison.InvariantCulture : StringComparison.InvariantCultureIgnoreCase;
      this.ApplyFilterToDropDown(filter);
      if (this.dropDownList.ListElement.Items.Count == 0 || string.IsNullOrEmpty(filter) || this.dropDownList.ListElement.Items.Count == 1 && string.Equals(filter, this.dropDownList.ListElement.Items[0].CachedText))
        this.dropDownList.ClosePopup();
      else
        this.ShowDropDownList();
    }

    public virtual void ShowDropDownList()
    {
      this.dropDownList.ListElement.SuspendSelectionEvents = true;
      this.dropDownList.Popup.Size = this.dropDownList.GetDesiredPopupSize();
      this.dropDownList.ListElement.SelectionMode = SelectionMode.One;
      this.dropDownList.AutoSizeItems = this.Owner.AutoSizeItems;
      this.dropDownList.FitToSizeMode = this.Owner.FitToSizeMode;
      int selectionStart = this.Owner.SelectionStart;
      int selectionLength = this.Owner.SelectionLength;
      this.dropDownList.BeginUpdate();
      this.dropDownList.ShowPopup();
      this.dropDownList.Popup.UpdateLocation();
      if (this.dropDownList.AutoSizeItems)
        this.dropDownList.Popup.Size = this.dropDownList.GetDesiredPopupSize();
      this.dropDownList.EndUpdate();
      this.dropDownList.ListElement.SelectedIndex = -1;
      this.Owner.SelectionStart = selectionStart;
      this.Owner.SelectionLength = selectionLength;
      this.dropDownList.ListElement.SuspendSelectionEvents = false;
    }

    public virtual void ApplyFilterToDropDown(string filter)
    {
      this.filter = filter;
      this.dropDownList.ListElement.SelectionMode = SelectionMode.None;
      this.dropDownList.ListElement.BeginUpdate();
      this.dropDownList.ListElement.Filter = (Predicate<RadListDataItem>) null;
      this.dropDownList.ListElement.Filter = new Predicate<RadListDataItem>(this.DefaultFilter);
      this.dropDownList.ListElement.SortStyle = SortStyle.Ascending;
      this.dropDownList.ListElement.EndUpdate();
    }

    public override void Dispose()
    {
      this.dropDownList.ListElement.SelectedIndexChanged -= new Telerik.WinControls.UI.Data.PositionChangedEventHandler(this.SelectedIndexChanged);
      this.Owner.ListElement.ItemsChanged -= new NotifyCollectionChangedEventHandler(this.CollectionChanged);
      this.dropDownList.PopupClosed -= new RadPopupClosedEventHandler(this.PopupClosed);
      if (!this.Owner.Children.Contains((RadElement) this.dropDownList))
        return;
      this.Owner.Children.Remove((RadElement) this.dropDownList);
    }
  }
}
