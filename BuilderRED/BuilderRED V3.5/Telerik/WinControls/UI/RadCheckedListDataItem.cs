// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.RadCheckedListDataItem
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.ComponentModel;
using System.Reflection;
using Telerik.WinControls.Interfaces;

namespace Telerik.WinControls.UI
{
  public class RadCheckedListDataItem : RadListDataItem
  {
    public static readonly RadProperty CheckedProperty = RadProperty.Register(nameof (Checked), typeof (bool), typeof (RadCheckedListDataItem), (RadPropertyMetadata) new RadElementPropertyMetadata((object) false));
    private bool isChecked;

    private event EventHandler CheckedChanged;

    public RadCheckedListDataItem()
    {
    }

    public RadCheckedListDataItem(string text)
      : base(text)
    {
    }

    public RadCheckedListDataItem(string text, bool check)
      : base(text)
    {
      this.Checked = check;
    }

    public virtual bool Checked
    {
      get
      {
        if (this.ownerElement != null && !string.IsNullOrEmpty(this.ownerElement.CheckedMember) && this.DataBoundItem != null)
        {
          PropertyInfo property = this.DataBoundItem.GetType().GetProperty(this.ownerElement.CheckedMember, BindingFlags.Instance | BindingFlags.Public);
          if ((object) property != null)
            return (bool) property.GetValue(this.DataBoundItem, (object[]) null);
        }
        return this.isChecked;
      }
      set
      {
        bool flag = true;
        if (this.IsDataBound && !string.IsNullOrEmpty(this.ownerElement.CheckedMember) && this.Checked != value)
        {
          PropertyInfo property = this.DataBoundItem.GetType().GetProperty(this.ownerElement.CheckedMember, BindingFlags.Instance | BindingFlags.Public);
          if ((object) property != null && this.Checked != value)
          {
            if (this.OnNotifyPropertyChanging(nameof (Checked)))
              return;
            flag = false;
            property.SetValue(this.DataBoundItem, (object) value, (object[]) null);
          }
        }
        if (this.isChecked == value || flag && this.OnNotifyPropertyChanging(nameof (Checked)))
          return;
        this.isChecked = value;
        int num = (int) this.SetValue(RadCheckedListDataItem.CheckedProperty, (object) value);
        this.OnNotifyPropertyChanged(nameof (Checked));
        this.OnSelectedItemChanged(EventArgs.Empty);
      }
    }

    public override RadListElement Owner
    {
      get
      {
        return base.Owner;
      }
      internal set
      {
        base.Owner = value;
        if (value == null || this.Owner.ElementTree == null)
          return;
        (((DropDownPopupForm) this.Owner.ElementTree.Control).OwnerDropDownListElement as RadCheckedDropDownListElement).ProcessItem(this);
      }
    }

    [Description("Gets or sets a value that indicates if this item is selected. It is not applicable for RadCheckedListDataItem.")]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [DefaultValue(false)]
    [Browsable(false)]
    public override bool Selected
    {
      get
      {
        return base.Selected;
      }
      set
      {
        base.Selected = false;
      }
    }

    protected virtual void OnSelectedItemChanged(EventArgs e)
    {
      if (this.CheckedChanged == null)
        return;
      this.CheckedChanged((object) this, e);
    }

    protected internal override void SetDataBoundItem(bool dataBinding, object value)
    {
      base.SetDataBoundItem(dataBinding, value);
      string str = this.dataLayer.GetCheckedValue(this).ToString();
      if (string.IsNullOrEmpty(str))
        str = "False";
      this.Checked = Convert.ToBoolean(str);
    }

    public event PropertyChangingEventHandlerEx PropertyChanging;

    protected override void OnNotifyPropertyChanged(string propertyName)
    {
      if (propertyName == "Checked")
      {
        if (this.ownerElement == null || !this.ownerElement.IsInValidState(false) || this.ownerElement.ElementTree == null)
          return;
        RadCheckedDropDownListElement dropDownListElement = (RadCheckedDropDownListElement) ((DropDownPopupForm) this.ownerElement.ElementTree.Control).OwnerDropDownListElement;
        dropDownListElement.ProcessItem(this);
        dropDownListElement.OnItemCheckedChanged(new RadCheckedListDataItemEventArgs(this));
      }
      base.OnNotifyPropertyChanged(propertyName);
    }

    protected virtual bool OnNotifyPropertyChanging(string propertyName)
    {
      return this.OnNotifyPropertyChanging(new PropertyChangingEventArgsEx(propertyName));
    }

    protected virtual bool OnNotifyPropertyChanging(PropertyChangingEventArgsEx args)
    {
      if (args.PropertyName == "Checked")
      {
        if (this.ownerElement == null || !this.ownerElement.IsInValidState(false) || this.ownerElement.ElementTree == null)
          return false;
        RadCheckedDropDownListElement dropDownListElement = (RadCheckedDropDownListElement) ((DropDownPopupForm) this.ownerElement.ElementTree.Control).OwnerDropDownListElement;
        args.Cancel = dropDownListElement.OnItemCheckedChanging(new RadCheckedListDataItemCancelEventArgs(this));
      }
      if (this.PropertyChanging == null)
        return args.Cancel;
      this.PropertyChanging((object) this, args);
      return args.Cancel;
    }
  }
}
