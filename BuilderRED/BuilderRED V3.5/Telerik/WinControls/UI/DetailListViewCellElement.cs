// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.DetailListViewCellElement
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Telerik.WinControls.Styles;

namespace Telerik.WinControls.UI
{
  public abstract class DetailListViewCellElement : LightVisualElement, IVirtualizedElement<ListViewDetailColumn>
  {
    public static RadProperty CurrentProperty = RadProperty.Register("Current", typeof (bool), typeof (DetailListViewCellElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) false));
    public static RadProperty IsSortedProperty = RadProperty.Register("IsSorted", typeof (bool), typeof (DetailListViewCellElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) false));
    protected ListViewDetailColumn column;

    static DetailListViewCellElement()
    {
      ItemStateManagerFactoryRegistry.AddStateManagerFactory((ItemStateManagerFactoryBase) new ListViewCellStateManagerFactory(), typeof (DetailListViewCellElement));
    }

    public DetailListViewCellElement(ListViewDetailColumn column)
    {
      this.column = column;
    }

    public virtual ListViewDetailColumn Data
    {
      get
      {
        return this.column;
      }
    }

    public virtual void Attach(ListViewDetailColumn data, object context)
    {
      if (data == null)
        return;
      this.column = data;
      this.Synchronize();
      this.column.PropertyChanged += new PropertyChangedEventHandler(this.OnColumnPropertyChanged);
    }

    public virtual void Detach()
    {
      if (this.column != null)
        this.column.PropertyChanged -= new PropertyChangedEventHandler(this.OnColumnPropertyChanged);
      this.column = (ListViewDetailColumn) null;
      int num = (int) this.ResetValue(DetailListViewCellElement.CurrentProperty, ValueResetFlags.Local);
    }

    public virtual void Synchronize()
    {
      this.Text = this.column.HeaderText;
      int num1 = (int) this.SetValue(DetailListViewCellElement.CurrentProperty, (object) this.column.Current);
      if (this.column.Owner.SortDescriptors.IndexOf(this.column.Name) >= 0)
      {
        int num2 = (int) this.SetValue(DetailListViewCellElement.IsSortedProperty, (object) true);
      }
      else
      {
        int num3 = (int) this.SetValue(DetailListViewCellElement.IsSortedProperty, (object) false);
      }
      this.column.Owner.OnCellFormatting(new ListViewCellFormattingEventArgs(this));
    }

    public virtual bool IsCompatible(ListViewDetailColumn data, object context)
    {
      return true;
    }

    protected override SizeF MeasureOverride(SizeF availableSize)
    {
      SizeF sizeF = base.MeasureOverride(availableSize);
      if (this.IgnoreColumnWidth)
        return sizeF;
      return new SizeF(this.Data.Width, sizeF.Height);
    }

    protected override void CreateChildElements()
    {
      base.CreateChildElements();
      this.AllowDrag = true;
      this.AllowDrop = true;
      this.NotifyParentOnMouseInput = true;
      this.ImageLayout = ImageLayout.None;
    }

    protected override void DisposeManagedResources()
    {
      this.Detach();
      base.DisposeManagedResources();
    }

    [DefaultValue(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    internal bool IgnoreColumnWidth { get; set; }

    protected virtual void OnColumnPropertyChanged(object sender, PropertyChangedEventArgs e)
    {
      this.Synchronize();
    }
  }
}
