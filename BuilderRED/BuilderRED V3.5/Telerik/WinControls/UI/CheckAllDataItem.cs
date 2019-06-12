// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.CheckAllDataItem
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;

namespace Telerik.WinControls.UI
{
  public class CheckAllDataItem : RadCheckedListDataItem
  {
    private bool isChecked;
    private RadCheckedDropDownListElement checkedElement;
    private bool setCheckState;

    public CheckAllDataItem(string text, RadCheckedDropDownListElement checkedElement)
      : base(text)
    {
      this.checkedElement = checkedElement;
    }

    public override bool Checked
    {
      get
      {
        return this.isChecked;
      }
      set
      {
        this.SetCheckState(value, false);
      }
    }

    public void SetCheckState(bool value, bool silent)
    {
      if (this.setCheckState || !this.checkedElement.ShowCheckAllItems || this.isChecked == value)
        return;
      this.setCheckState = true;
      if (this.CheckAllItemCheckedChanging != null)
      {
        RadCheckedListDataItemCancelEventArgs e = new RadCheckedListDataItemCancelEventArgs((RadCheckedListDataItem) this);
        this.CheckAllItemCheckedChanging((object) this.ownerElement, e);
        if (e.Cancel)
        {
          this.setCheckState = false;
          return;
        }
      }
      if (this.OnNotifyPropertyChanging("Checked"))
      {
        this.setCheckState = false;
      }
      else
      {
        this.isChecked = value;
        int num = (int) this.SetValue(RadCheckedListDataItem.CheckedProperty, (object) value);
        this.OnNotifyPropertyChanged("Checked");
        this.OnSelectedItemChanged(EventArgs.Empty);
        if (silent)
        {
          this.setCheckState = false;
          if (this.CheckAllItemCheckedChanged == null)
            return;
          this.CheckAllItemCheckedChanged((object) this.ownerElement, new RadCheckedListDataItemEventArgs((RadCheckedListDataItem) this));
        }
        else
        {
          this.checkedElement.BeginUpdate();
          this.checkedElement.AutoCompleteEditableAreaElement.AutoCompleteTextBox.ListElement.SuspendSelectionEvents = true;
          this.checkedElement.ListElement.DataLayer.ListSource.BeginUpdate();
          foreach (RadCheckedListDataItem checkedListDataItem in (RadListDataItemCollection) this.checkedElement.Items)
            checkedListDataItem.Checked = value;
          this.checkedElement.ListElement.DataLayer.ListSource.EndUpdate();
          this.checkedElement.EndUpdate();
          this.checkedElement.SyncEditorElementWithSelectedItem();
          this.checkedElement.AutoCompleteEditableAreaElement.AutoCompleteTextBox.ListElement.SuspendSelectionEvents = false;
          this.setCheckState = false;
          if (this.CheckAllItemCheckedChanged == null)
            return;
          this.CheckAllItemCheckedChanged((object) this.ownerElement, new RadCheckedListDataItemEventArgs((RadCheckedListDataItem) this));
        }
      }
    }

    public event RadCheckedListDataItemCancelEventHandler CheckAllItemCheckedChanging;

    public event RadCheckedListDataItemEventHandler CheckAllItemCheckedChanged;
  }
}
