// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.PropertyGridCheckBoxItemElement
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.Windows.Forms;
using Telerik.WinControls.UI.PropertyGridData;

namespace Telerik.WinControls.UI
{
  public class PropertyGridCheckBoxItemElement : PropertyGridItemElement
  {
    private RadCheckBoxElement checkBoxElement;

    protected override void CreateChildElements()
    {
      base.CreateChildElements();
      this.checkBoxElement = new RadCheckBoxElement();
      this.checkBoxElement.Margin = new Padding(2, 0, 2, 0);
      this.checkBoxElement.ToggleStateChanged += new StateChangedEventHandler(this.checkBoxElement_ToggleStateChanged);
      this.checkBoxElement.ToggleStateChanging += new StateChangingEventHandler(this.checkBoxElement_ToggleStateChanging);
      this.checkBoxElement.MouseMove += new MouseEventHandler(this.checkBoxElement_MouseMove);
      this.ValueElement.Children.Add((RadElement) this.checkBoxElement);
      this.ValueElement.DrawText = false;
    }

    protected override void DisposeManagedResources()
    {
      base.DisposeManagedResources();
      this.checkBoxElement.ToggleStateChanged -= new StateChangedEventHandler(this.checkBoxElement_ToggleStateChanged);
      this.checkBoxElement.ToggleStateChanging -= new StateChangingEventHandler(this.checkBoxElement_ToggleStateChanging);
      this.checkBoxElement.MouseMove -= new MouseEventHandler(this.checkBoxElement_MouseMove);
    }

    public RadCheckBoxElement CheckBoxElement
    {
      get
      {
        return this.checkBoxElement;
      }
    }

    protected override System.Type ThemeEffectiveType
    {
      get
      {
        return typeof (PropertyGridItemElement);
      }
    }

    protected virtual void OnToggleStateChanging(StateChangingEventArgs args)
    {
      if (this.PropertyTableElement.ReadOnly)
      {
        args.Cancel = true;
      }
      else
      {
        PropertyGridItem data = this.Data as PropertyGridItem;
        if (!data.ReadOnly || data.Accessor is ImmutableItemAccessor)
          return;
        args.Cancel = true;
      }
    }

    protected virtual void OnToggleStateChanged(StateChangedEventArgs args)
    {
      if (this.PropertyTableElement.IsEditing)
        this.PropertyTableElement.EndEdit();
      PropertyGridItem data = (PropertyGridItem) this.Data;
      int hashCode = this.PropertyTableElement.SelectedObject.GetHashCode();
      if ((object) data.PropertyType == (object) typeof (bool))
        data.Value = (object) (args.ToggleState == Telerik.WinControls.Enumerations.ToggleState.On);
      else if ((object) data.PropertyType == (object) typeof (bool?))
      {
        switch (args.ToggleState)
        {
          case Telerik.WinControls.Enumerations.ToggleState.Off:
            data.Value = (object) false;
            break;
          case Telerik.WinControls.Enumerations.ToggleState.On:
            data.Value = (object) true;
            break;
          case Telerik.WinControls.Enumerations.ToggleState.Indeterminate:
            data.Value = (object) null;
            break;
        }
      }
      else if ((object) data.PropertyType == (object) typeof (Telerik.WinControls.Enumerations.ToggleState))
        data.Value = (object) this.CheckBoxElement.ToggleState;
      else if (data.TypeConverter != null && data.TypeConverter.CanConvertFrom(typeof (Telerik.WinControls.Enumerations.ToggleState)))
      {
        this.CheckBoxElement.ToggleState = (Telerik.WinControls.Enumerations.ToggleState) data.TypeConverter.ConvertTo(data.Value, typeof (Telerik.WinControls.Enumerations.ToggleState));
        data.Value = data.TypeConverter.ConvertFrom((object) args.ToggleState);
      }
      if (this.PropertyTableElement == null || hashCode != this.PropertyTableElement.SelectedObject.GetHashCode())
        return;
      if (!data.Selected)
        data.Selected = true;
      this.Synchronize();
    }

    protected virtual void OnCheckBoxMouseMove(MouseEventArgs e)
    {
      this.ElementTree.Control.Cursor = Cursors.Default;
    }

    private void checkBoxElement_ToggleStateChanging(object sender, StateChangingEventArgs args)
    {
      this.OnToggleStateChanging(args);
    }

    private void checkBoxElement_ToggleStateChanged(object sender, StateChangedEventArgs args)
    {
      this.OnToggleStateChanged(args);
    }

    private void checkBoxElement_MouseMove(object sender, MouseEventArgs e)
    {
      this.OnCheckBoxMouseMove(e);
    }

    public override void Synchronize()
    {
      base.Synchronize();
      PropertyGridItem data = (PropertyGridItem) this.Data;
      if (data == null)
        return;
      this.CheckBoxElement.ToggleStateChanging -= new StateChangingEventHandler(this.checkBoxElement_ToggleStateChanging);
      this.CheckBoxElement.ToggleStateChanged -= new StateChangedEventHandler(this.checkBoxElement_ToggleStateChanged);
      this.CheckBoxElement.IsThreeState = data.IsThreeState;
      if ((object) data.PropertyType == (object) typeof (bool))
        this.CheckBoxElement.ToggleState = (bool) data.Value ? Telerik.WinControls.Enumerations.ToggleState.On : Telerik.WinControls.Enumerations.ToggleState.Off;
      else if ((object) data.PropertyType == (object) typeof (bool?))
      {
        bool? nullable = data.Value as bool?;
        if (nullable.HasValue)
          this.CheckBoxElement.ToggleState = nullable.Value ? Telerik.WinControls.Enumerations.ToggleState.On : Telerik.WinControls.Enumerations.ToggleState.Off;
        else
          this.CheckBoxElement.ToggleState = Telerik.WinControls.Enumerations.ToggleState.Indeterminate;
      }
      else if ((object) data.PropertyType == (object) typeof (Telerik.WinControls.Enumerations.ToggleState))
        this.CheckBoxElement.ToggleState = (Telerik.WinControls.Enumerations.ToggleState) data.Value;
      else if (data.TypeConverter != null && data.TypeConverter.CanConvertTo(typeof (Telerik.WinControls.Enumerations.ToggleState)))
        this.CheckBoxElement.ToggleState = (Telerik.WinControls.Enumerations.ToggleState) data.TypeConverter.ConvertTo(data.Value, typeof (Telerik.WinControls.Enumerations.ToggleState));
      this.checkBoxElement.ToggleStateChanged += new StateChangedEventHandler(this.checkBoxElement_ToggleStateChanged);
      this.checkBoxElement.ToggleStateChanging += new StateChangingEventHandler(this.checkBoxElement_ToggleStateChanging);
    }

    public override bool IsCompatible(PropertyGridItemBase data, object context)
    {
      PropertyGridItem propertyGridItem = data as PropertyGridItem;
      if (propertyGridItem == null)
        return false;
      if ((object) propertyGridItem.PropertyType != (object) typeof (bool) && (object) propertyGridItem.PropertyType != (object) typeof (bool?))
        return (object) propertyGridItem.PropertyType == (object) typeof (Telerik.WinControls.Enumerations.ToggleState);
      return true;
    }

    public override void AddEditor(IInputEditor editor)
    {
    }

    public override void RemoveEditor(IInputEditor editor)
    {
    }
  }
}
