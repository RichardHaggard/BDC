// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.DataFilterCheckBoxValueEditorElement
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Drawing;
using System.Windows.Forms;

namespace Telerik.WinControls.UI
{
  public class DataFilterCheckBoxValueEditorElement : DataFilterValueEditorElement
  {
    private RadCheckmark checkMark;
    private bool checkBoxClicked;

    public DataFilterCheckBoxValueEditorElement(BaseDataFilterNodeElement dataFilterNodeElement)
      : base(dataFilterNodeElement)
    {
    }

    protected internal override System.Type EditorType
    {
      get
      {
        return base.EditorType;
      }
      set
      {
        base.EditorType = value;
      }
    }

    protected override void InitializeFields()
    {
      base.InitializeFields();
      this.DrawText = false;
    }

    protected override void CreateChildElements()
    {
      base.CreateChildElements();
      this.checkMark = this.CreateCheckBox();
      this.checkMark.NotifyParentOnMouseInput = true;
      this.checkMark.ShouldHandleMouseInput = true;
      this.checkMark.EnableVisualStates = true;
      this.checkMark.Alignment = ContentAlignment.MiddleCenter;
      this.Children.Add((RadElement) this.checkMark);
    }

    protected virtual RadCheckmark CreateCheckBox()
    {
      return new RadCheckmark();
    }

    public override void Synchronize(DataFilterCriteriaNode criteriaNode)
    {
      base.Synchronize(criteriaNode);
      this.checkMark.CheckState = Convert.ToBoolean(criteriaNode.DescriptorValue) ? Telerik.WinControls.Enumerations.ToggleState.On : Telerik.WinControls.Enumerations.ToggleState.Off;
    }

    internal override void AddEditor(IInputEditor editor)
    {
    }

    internal override void RemoveEditor()
    {
      RadDataFilterElement dataFilterElement = this.FindAncestor<BaseDataFilterNodeElement>().DataFilterElement;
      if (dataFilterElement == null)
        return;
      dataFilterElement.currentNodeEditorType = (System.Type) null;
    }

    protected override void OnMouseDown(MouseEventArgs e)
    {
      base.OnMouseDown(e);
      this.checkBoxClicked = false;
      this.checkBoxClicked = this.CheckBoxClicked(e.Location);
    }

    protected override void OnMouseUp(MouseEventArgs e)
    {
      base.OnMouseUp(e);
      if (e.Button == MouseButtons.Left && this.checkBoxClicked)
      {
        this.ToggleCheckMark();
        DataFilterCriteriaElement ancestor = this.FindAncestor<DataFilterCriteriaElement>();
        if (ancestor != null)
          ancestor.CriteriaNode.DescriptorValue = (object) (this.checkMark.CheckState == Telerik.WinControls.Enumerations.ToggleState.On);
      }
      this.checkBoxClicked = false;
    }

    protected virtual void ToggleCheckMark()
    {
      Telerik.WinControls.Enumerations.ToggleState toggleState;
      switch (this.checkMark.CheckState)
      {
        case Telerik.WinControls.Enumerations.ToggleState.On:
          toggleState = Telerik.WinControls.Enumerations.ToggleState.Off;
          break;
        default:
          toggleState = Telerik.WinControls.Enumerations.ToggleState.On;
          break;
      }
      this.checkMark.CheckState = toggleState;
    }

    private bool CheckBoxClicked(Point point)
    {
      return this.ControlBoundingRectangle.Contains(point);
    }
  }
}
