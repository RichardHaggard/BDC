// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.VirtualGridCellElement
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Telerik.WinControls.Paint;
using Telerik.WinControls.Styles;
using Telerik.WinControls.UI.StateManagers;

namespace Telerik.WinControls.UI
{
  public class VirtualGridCellElement : LightVisualElement, IVirtualizedElement<int>
  {
    private string formatString = "{0}";
    private string fieldName = string.Empty;
    private int columnIndex = int.MinValue;
    private int rowIndex = int.MinValue;
    public static RadProperty IsCurrentProperty = RadProperty.Register(nameof (IsCurrent), typeof (bool), typeof (VirtualGridCellElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) false, ElementPropertyOptions.AffectsDisplay));
    public static RadProperty IsCurrentColumnProperty = RadProperty.Register(nameof (IsCurrentColumn), typeof (bool), typeof (VirtualGridCellElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) false, ElementPropertyOptions.AffectsDisplay));
    public static RadProperty IsCurrentRowProperty = RadProperty.Register(nameof (IsCurrentRow), typeof (bool), typeof (VirtualGridCellElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) false, ElementPropertyOptions.AffectsDisplay));
    public static RadProperty IsSortedProperty = RadProperty.Register(nameof (IsSorted), typeof (bool), typeof (VirtualGridCellElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) false, ElementPropertyOptions.AffectsDisplay));
    public static RadProperty IsPinnedProperty = RadProperty.Register(nameof (IsPinned), typeof (bool), typeof (VirtualGridCellElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) false, ElementPropertyOptions.AffectsDisplay));
    public static RadProperty IsSelectedProperty = RadProperty.Register(nameof (IsSelected), typeof (bool), typeof (VirtualGridCellElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) false, ElementPropertyOptions.AffectsDisplay));
    public static RadProperty IsOddRowProperty = RadProperty.Register(nameof (IsOddRow), typeof (bool), typeof (VirtualGridCellElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) false, ElementPropertyOptions.AffectsDisplay));
    public const int ResizePointerOffset = 2;
    private object value;
    private VirtualGridTableElement tableElement;
    private VirtualGridRowElement rowElement;
    private IInputEditor editor;
    internal bool BestFitMeasure;

    static VirtualGridCellElement()
    {
      ItemStateManagerFactoryRegistry.AddStateManagerFactory((ItemStateManagerFactoryBase) new VirtualGridCellElementStateManagerFactory(), typeof (VirtualGridCellElement));
    }

    public VirtualGridCellElement()
    {
      this.StretchHorizontally = false;
      this.TextWrap = false;
      this.AutoEllipsis = true;
      this.CaptureOnMouseDown = true;
    }

    public int Data
    {
      get
      {
        return this.columnIndex;
      }
    }

    public VirtualGridViewInfo ViewInfo
    {
      get
      {
        return this.tableElement.ViewInfo;
      }
    }

    public string FormatString
    {
      get
      {
        return this.formatString;
      }
      protected set
      {
        this.formatString = value;
      }
    }

    public string FieldName
    {
      get
      {
        if (!string.IsNullOrEmpty(this.fieldName))
          return this.fieldName;
        return this.Text;
      }
      protected set
      {
        this.fieldName = value;
      }
    }

    public VirtualGridRowElement RowElement
    {
      get
      {
        return this.rowElement;
      }
    }

    public object Value
    {
      get
      {
        return this.value;
      }
      protected set
      {
        this.value = value;
      }
    }

    public int ColumnIndex
    {
      get
      {
        return this.columnIndex;
      }
    }

    public int RowIndex
    {
      get
      {
        return this.rowIndex;
      }
    }

    public VirtualGridTableElement TableElement
    {
      get
      {
        return this.tableElement;
      }
    }

    public virtual bool CanEdit
    {
      get
      {
        return true;
      }
    }

    public virtual bool IsPinned
    {
      get
      {
        return (bool) this.GetValue(VirtualGridCellElement.IsPinnedProperty);
      }
      set
      {
        int num = (int) this.SetValue(VirtualGridCellElement.IsPinnedProperty, (object) value);
      }
    }

    public virtual bool IsSelected
    {
      get
      {
        return (bool) this.GetValue(VirtualGridCellElement.IsSelectedProperty);
      }
      set
      {
        int num = (int) this.SetValue(VirtualGridCellElement.IsSelectedProperty, (object) value);
      }
    }

    public virtual bool IsSorted
    {
      get
      {
        return (bool) this.GetValue(VirtualGridCellElement.IsSortedProperty);
      }
      set
      {
        int num = (int) this.SetValue(VirtualGridCellElement.IsSortedProperty, (object) value);
      }
    }

    public virtual bool IsOddRow
    {
      get
      {
        return (bool) this.GetValue(VirtualGridCellElement.IsOddRowProperty);
      }
      set
      {
        int num = (int) this.SetValue(VirtualGridCellElement.IsOddRowProperty, (object) value);
      }
    }

    public virtual bool IsCurrent
    {
      get
      {
        return (bool) this.GetValue(VirtualGridCellElement.IsCurrentProperty);
      }
      set
      {
        int num = (int) this.SetValue(VirtualGridCellElement.IsCurrentProperty, (object) value);
      }
    }

    public virtual bool IsCurrentColumn
    {
      get
      {
        return (bool) this.GetValue(VirtualGridCellElement.IsCurrentColumnProperty);
      }
      set
      {
        int num = (int) this.SetValue(VirtualGridCellElement.IsCurrentColumnProperty, (object) value);
      }
    }

    public virtual bool IsCurrentRow
    {
      get
      {
        return (bool) this.GetValue(VirtualGridCellElement.IsCurrentRowProperty);
      }
      set
      {
        int num = (int) this.SetValue(VirtualGridCellElement.IsCurrentRowProperty, (object) value);
      }
    }

    public virtual IInputEditor Editor
    {
      get
      {
        return this.editor;
      }
    }

    public virtual void Initialize(VirtualGridTableElement owner)
    {
      this.tableElement = owner;
    }

    public virtual void Attach(int data, object context)
    {
      this.Attach(data, context, true);
    }

    protected internal virtual void Attach(int data, object context, bool synchronize)
    {
      this.columnIndex = data;
      this.rowIndex = (context as VirtualGridRowElement).RowIndex;
      this.rowElement = context as VirtualGridRowElement;
      if (synchronize)
        this.Synchronize();
      this.rowElement.PropertyChanged += new PropertyChangedEventHandler(this.rowElement_PropertyChanged);
    }

    public virtual void Detach()
    {
      this.columnIndex = int.MinValue;
      this.rowElement.PropertyChanged -= new PropertyChangedEventHandler(this.rowElement_PropertyChanged);
      this.rowElement = (VirtualGridRowElement) null;
    }

    public virtual void Synchronize()
    {
      this.Synchronize(true);
    }

    public virtual void Synchronize(bool updateContent)
    {
      this.IsPinned = this.tableElement.IsRowPinned(this.rowIndex) || this.tableElement.IsColumnPinned(this.Data);
      this.IsSelected = this.tableElement.GridElement.Selection.IsSelected(this.RowIndex, this.ColumnIndex, this.ViewInfo);
      if (this.tableElement.GridElement.CurrentCell != null && this.tableElement.GridElement.CurrentCell.ViewInfo == this.ViewInfo)
      {
        this.IsCurrentRow = this.tableElement.GridElement.CurrentCell.RowIndex == this.rowIndex;
        this.IsCurrentColumn = this.tableElement.GridElement.CurrentCell.ColumnIndex == this.columnIndex;
        this.IsCurrent = this.IsCurrentRow && this.IsCurrentColumn;
      }
      else
        this.IsCurrentRow = this.IsCurrentColumn = this.IsCurrent = false;
      if (updateContent)
      {
        VirtualGridCellValueNeededEventArgs args = new VirtualGridCellValueNeededEventArgs(this.rowIndex, this.columnIndex, this.ViewInfo);
        this.tableElement.GridElement.OnCellValueNeeded(args);
        this.UpdateInfo(args);
      }
      this.tableElement.GridElement.OnCellFormatting(new VirtualGridCellElementEventArgs(this, this.ViewInfo));
    }

    protected virtual void UpdateInfo(VirtualGridCellValueNeededEventArgs args)
    {
      this.value = args.Value;
      this.fieldName = args.FieldName;
      this.formatString = args.FormatString;
      this.Text = string.Format(this.formatString, this.value);
    }

    public virtual void Synchronize(VirtualGridRowElement context)
    {
      this.rowIndex = context.RowIndex;
      this.rowElement = context;
      this.Synchronize();
    }

    public virtual bool IsCompatible(int data, object context)
    {
      VirtualGridRowElement virtualGridRowElement = context as VirtualGridRowElement;
      if (data < 0 || this.ViewInfo.IsCustomColumn(data))
        return false;
      if (virtualGridRowElement != null)
        return virtualGridRowElement.RowIndex >= 0;
      return true;
    }

    public virtual void AddEditor(IInputEditor editor)
    {
      if (editor == null || this.editor == editor)
        return;
      this.editor = editor;
      RadItem editorElement = this.GetEditorElement(this.editor);
      if (editorElement == null || this.Children.Contains((RadElement) editorElement))
        return;
      int num = (int) editorElement.SetDefaultValueOverride(VisualElement.ForeColorProperty, (object) Color.FromKnownColor(KnownColor.ControlText));
      this.Children.Add((RadElement) editorElement);
      BaseVirtualGridEditor virtualGridEditor = editor as BaseVirtualGridEditor;
      if (virtualGridEditor == null || !virtualGridEditor.ClearCellText)
        return;
      this.Text = string.Empty;
    }

    public virtual void RemoveEditor(IInputEditor editor)
    {
      if (editor == null)
        return;
      RadItem editorElement = this.GetEditorElement(editor);
      if (editorElement != null && this.Children.Contains((RadElement) editorElement))
        this.Children.Remove((RadElement) editorElement);
      this.editor = (IInputEditor) null;
    }

    public RadItem GetEditorElement(IInputEditor editor)
    {
      RadItem radItem = editor as RadItem;
      if (radItem == null)
      {
        BaseGridEditor baseGridEditor = editor as BaseGridEditor;
        if (baseGridEditor != null)
        {
          radItem = baseGridEditor.EditorElement as RadItem;
        }
        else
        {
          BaseInputEditor baseInputEditor = editor as BaseInputEditor;
          if (baseInputEditor != null)
            radItem = baseInputEditor.EditorElement as RadItem;
        }
      }
      return radItem;
    }

    public virtual bool IsInResizeLocation(Point point)
    {
      return this.ViewInfo.AllowRowResize && point.Y >= this.ControlBoundingRectangle.Bottom - 2 && point.Y <= this.ControlBoundingRectangle.Bottom;
    }

    protected override void OnMouseMove(MouseEventArgs e)
    {
      base.OnMouseMove(e);
      this.TableElement.GridElement.OnCellMouseMove(new VirtualGridCellElementMouseEventArgs(this, this.ViewInfo, e));
    }

    private void rowElement_PropertyChanged(object sender, PropertyChangedEventArgs e)
    {
      this.OnRowElementPropertyChanged(e);
    }

    protected virtual void OnRowElementPropertyChanged(PropertyChangedEventArgs e)
    {
    }

    protected override void PaintElement(IGraphics graphics, float angle, SizeF scale)
    {
      base.PaintElement(graphics, angle, scale);
      this.TableElement.GridElement.OnCellPaint(this, graphics);
    }

    protected override SizeF MeasureOverride(SizeF availableSize)
    {
      SizeF sizeF = base.MeasureOverride(availableSize);
      return new SizeF(!this.BestFitMeasure ? (float) this.TableElement.GetColumnWidth(this.ColumnIndex) : sizeF.Width, sizeF.Height);
    }

    protected override SizeF ArrangeOverride(SizeF finalSize)
    {
      SizeF sizeF = base.ArrangeOverride(finalSize);
      if (this.editor != null)
      {
        RectangleF clientRectangle = this.GetClientRectangle(finalSize);
        this.ArrangeEditorElement(finalSize, clientRectangle);
      }
      return sizeF;
    }

    protected virtual void ArrangeEditorElement(SizeF finalSize, RectangleF clientRect)
    {
      RadElement editorElement = (RadElement) this.GetEditorElement(this.editor);
      float height1 = editorElement.DesiredSize.Height;
      if (editorElement.StretchVertically)
        height1 = clientRect.Height;
      float height2 = Math.Min(height1, finalSize.Height);
      RectangleF finalRect = new RectangleF(clientRect.X, clientRect.Y + (float) (((double) clientRect.Height - (double) height2) / 2.0), clientRect.Width, height2);
      editorElement.Arrange(finalRect);
    }
  }
}
