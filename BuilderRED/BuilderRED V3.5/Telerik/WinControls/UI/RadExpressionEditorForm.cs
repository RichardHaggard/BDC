// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.RadExpressionEditorForm
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.Layout;
using Telerik.Data.Expressions;
using Telerik.WinControls.Localization;
using Telerik.WinControls.UI.Localization;

namespace Telerik.WinControls.UI
{
  public class RadExpressionEditorForm : RadForm
  {
    private static Point location = Point.Empty;
    private RadTreeNode treeNodeFunctions = new RadTreeNode();
    private RadTreeNode treeNodeAggregate = new RadTreeNode();
    private RadTreeNode treeNodeText = new RadTreeNode();
    private RadTreeNode treeNodeDateTime = new RadTreeNode();
    private RadTreeNode treeNodeLogical = new RadTreeNode();
    private RadTreeNode treeNodeMath = new RadTreeNode();
    private RadTreeNode treeNodeOther = new RadTreeNode();
    private RadTreeNode treeNodeOperators = new RadTreeNode();
    private RadTreeNode treeNodeConstants = new RadTreeNode();
    private RadTreeNode treeNodeFields = new RadTreeNode();
    private static string themeName;
    private static ExpressionItemsList expressionList;
    private string expression;
    private ExpressionItemsList fieldList;
    private GridViewDataColumn referencedColumn;
    private ExpressionFormattingObject formattingObject;
    private GridViewTemplate template;
    private string resultPreviewStr;
    private string funcDescriptionStr;
    protected bool listSelectedItemClicked;
    private RadThemeComponentBase.ThemeContext context;
    private string theme;
    private IContainer components;
    private RadPanel radPanel2;
    protected RadButton radButtonCancel;
    protected RadButton radButtonOK;
    protected RadButton radButtonNot;
    protected RadButton radButtonOr;
    protected RadButton radButtonAnd;
    protected RadButton radButtonGreater;
    protected RadButton radButtonGreaterOrEqual;
    protected RadButton radButtonLessOrEqual;
    protected RadButton radButtonLess;
    protected RadButton radButtonNonequal;
    protected RadButton radButtonEqual;
    protected RadButton radButtonModulo;
    protected RadButton radButtonDivide;
    protected RadButton radButtonMultiply;
    protected RadButton radButtonMinus;
    protected RadButton radButtonPlus;
    private RadSeparator radSeparator1;
    private RadSeparator radSeparator2;
    private RadSeparator radSeparator3;
    protected RadTextBox radTextBoxExpression;
    protected RadLabel radLabelDescription;
    protected RadTreeView radTreeViewFunctions;
    protected RadTreeView radListControlFunctionsList;
    private RadLabel radLabelPreview;
    private RadPanel radPanel5;
    private RadPanel radPanel4;
    private RadSplitContainer radSplitContainer1;
    private SplitPanel splitPanel1;
    private SplitPanel splitPanel2;
    private RadSplitContainer radSplitContainer2;
    private SplitPanel splitPanel3;
    private SplitPanel splitPanel4;
    private RadPanel radPanel1;

    internal RadExpressionEditorForm()
    {
      this.InitializeComponent();
      this.LocalizeLabels();
      ExprEditorDragDropManager.Attach(this.radListControlFunctionsList, this.radTextBoxExpression);
    }

    public RadExpressionEditorForm(GridViewDataColumn referencedColumn)
      : this()
    {
      if (referencedColumn == null)
        return;
      this.referencedColumn = referencedColumn;
      this.LoadFieldList(referencedColumn.OwnerTemplate);
      this.expression = referencedColumn.Expression;
    }

    public RadExpressionEditorForm(
      ExpressionFormattingObject formattingObject,
      GridViewTemplate template)
      : this()
    {
      if (formattingObject == null)
        return;
      this.formattingObject = formattingObject;
      this.template = template;
      this.LoadFieldList(template);
      this.expression = formattingObject.Expression;
    }

    public string Expression
    {
      get
      {
        return this.expression;
      }
      set
      {
        this.expression = value;
      }
    }

    public new static string ThemeName
    {
      private get
      {
        return RadExpressionEditorForm.themeName;
      }
      set
      {
        RadExpressionEditorForm.themeName = value;
      }
    }

    public static ExpressionItemsList ExpressionItemsList
    {
      get
      {
        if (RadExpressionEditorForm.expressionList == null)
          RadExpressionEditorForm.expressionList = new ExpressionItemsList();
        return RadExpressionEditorForm.expressionList;
      }
    }

    public ExpressionItemsList FieldList
    {
      get
      {
        return this.fieldList;
      }
    }

    public RadTreeNode TreeNodeFunctions
    {
      get
      {
        return this.treeNodeFunctions;
      }
    }

    public RadTreeNode TreeNodeAggregate
    {
      get
      {
        return this.treeNodeAggregate;
      }
    }

    public RadTreeNode TreeNodeText
    {
      get
      {
        return this.treeNodeText;
      }
    }

    public RadTreeNode TreeNodeDateTime
    {
      get
      {
        return this.treeNodeDateTime;
      }
    }

    public RadTreeNode TreeNodeLogical
    {
      get
      {
        return this.treeNodeLogical;
      }
    }

    public RadTreeNode TreeNodeMath
    {
      get
      {
        return this.treeNodeMath;
      }
    }

    public RadTreeNode TreeNodeOther
    {
      get
      {
        return this.treeNodeOther;
      }
    }

    public RadTreeNode TreeNodeOperators
    {
      get
      {
        return this.treeNodeOperators;
      }
    }

    public RadTreeNode TreeNodeConstants
    {
      get
      {
        return this.treeNodeConstants;
      }
    }

    public RadTreeNode TreeNodeFields
    {
      get
      {
        return this.treeNodeFields;
      }
    }

    public GridViewDataColumn ReferencedColumn
    {
      get
      {
        return this.referencedColumn;
      }
    }

    public ExpressionFormattingObject FormattingObject
    {
      get
      {
        return this.formattingObject;
      }
    }

    public GridViewTemplate Template
    {
      get
      {
        return this.template;
      }
    }

    public virtual void LoadFieldList(GridViewTemplate viewTemplate)
    {
      this.fieldList = new ExpressionItemsList();
      foreach (GridViewColumn column in (Collection<GridViewDataColumn>) viewTemplate.Columns)
      {
        if (column.IsVisible)
        {
          ExpressionItem expressionItem = new ExpressionItem()
          {
            Type = ExpressionItemType.Field
          };
          expressionItem.Name = expressionItem.Syntax = expressionItem.Value = column.Name;
          if (!string.IsNullOrEmpty(expressionItem.Name))
            this.FieldList.Add(expressionItem);
        }
      }
    }

    public static void Show(RadGridView radGridView, GridViewDataColumn column)
    {
      if (column == null)
        throw new ArgumentException("Column argument cannot be null!");
      RadExpressionEditorForm.ShowForm(new RadExpressionEditorForm(column), radGridView, column.Expression, column.HeaderText);
    }

    public static void Show(
      RadGridView radGridView,
      GridViewDataColumn column,
      RadExpressionEditorForm form)
    {
      if (column == null)
        throw new ArgumentException("Column argument cannot be null!");
      RadExpressionEditorForm.ShowForm(form, radGridView, column.Expression, column.HeaderText);
    }

    internal static RadExpressionEditorForm Show(
      RadGridView radGridView,
      ExpressionFormattingObject formattingObject,
      RadExpressionEditorForm form)
    {
      if (formattingObject == null)
        throw new ArgumentException("Formatting object argument cannot be null!");
      RadExpressionEditorForm.ShowForm(form, radGridView, formattingObject.Expression, string.Empty);
      return form;
    }

    private void SetTheme()
    {
      this.SetTheme(string.Empty);
    }

    private void SetTheme(string themeName)
    {
      if (string.IsNullOrEmpty(themeName))
      {
        if (string.IsNullOrEmpty(RadExpressionEditorForm.ThemeName))
          return;
        themeName = RadExpressionEditorForm.ThemeName;
      }
      this.ThemeName = themeName;
      foreach (Control control in (ArrangedElementCollection) this.Controls)
        this.SetTheme(control, themeName);
      this.theme = themeName;
    }

    private void SetTheme(Control control, string themeName)
    {
      if (control is RadControl)
        ((RadControl) control).ThemeName = themeName;
      foreach (Control control1 in (ArrangedElementCollection) control.Controls)
        this.SetTheme(control1, themeName);
    }

    private void LocalizeLabels()
    {
      this.funcDescriptionStr = "<html><b>" + LocalizationProvider<RadGridLocalizationProvider>.CurrentProvider.GetLocalizedString("ExpressionFormDescription") + "</b> ";
      this.radLabelDescription.Text = this.funcDescriptionStr;
      this.resultPreviewStr = "<html>" + LocalizationProvider<RadGridLocalizationProvider>.CurrentProvider.GetLocalizedString("ExpressionFormResultPreview") + ": ";
      this.radLabelPreview.Text = this.resultPreviewStr;
      this.radButtonAnd.Text = LocalizationProvider<RadGridLocalizationProvider>.CurrentProvider.GetLocalizedString("ExpressionFormAndButton");
      if (!string.IsNullOrEmpty(this.radButtonAnd.Text))
        this.radButtonAnd.DisplayStyle = DisplayStyle.Text;
      this.radButtonOr.Text = LocalizationProvider<RadGridLocalizationProvider>.CurrentProvider.GetLocalizedString("ExpressionFormOrButton");
      if (!string.IsNullOrEmpty(this.radButtonOr.Text))
        this.radButtonOr.DisplayStyle = DisplayStyle.Text;
      this.radButtonNot.Text = LocalizationProvider<RadGridLocalizationProvider>.CurrentProvider.GetLocalizedString("ExpressionFormNotButton");
      if (!string.IsNullOrEmpty(this.radButtonNot.Text))
        this.radButtonNot.DisplayStyle = DisplayStyle.Text;
      this.radButtonOK.Text = LocalizationProvider<RadGridLocalizationProvider>.CurrentProvider.GetLocalizedString("ExpressionFormOKButton");
      this.radButtonCancel.Text = LocalizationProvider<RadGridLocalizationProvider>.CurrentProvider.GetLocalizedString("ExpressionFormCancelButton");
    }

    protected virtual void InitializeTreeNodes()
    {
      ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (RadExpressionEditorForm));
      this.TreeNodeFunctions.Text = LocalizationProvider<RadGridLocalizationProvider>.CurrentProvider.GetLocalizedString("ExpressionFormFunctions");
      this.TreeNodeFunctions.Image = (Image) componentResourceManager.GetObject("treeNodeFunctions.Image");
      this.TreeNodeFunctions.Visible = false;
      this.TreeNodeText.Text = LocalizationProvider<RadGridLocalizationProvider>.CurrentProvider.GetLocalizedString("ExpressionFormFunctionsText");
      this.TreeNodeText.Image = (Image) componentResourceManager.GetObject("treeNodeFunction.Image");
      this.TreeNodeText.Visible = false;
      this.TreeNodeFunctions.Nodes.Add(this.TreeNodeText);
      this.TreeNodeAggregate.Text = LocalizationProvider<RadGridLocalizationProvider>.CurrentProvider.GetLocalizedString("ExpressionFormFunctionsAggregate");
      this.TreeNodeAggregate.Image = (Image) componentResourceManager.GetObject("treeNodeFunction.Image");
      this.TreeNodeAggregate.Visible = false;
      this.TreeNodeFunctions.Nodes.Add(this.TreeNodeAggregate);
      this.TreeNodeDateTime.Text = LocalizationProvider<RadGridLocalizationProvider>.CurrentProvider.GetLocalizedString("ExpressionFormFunctionsDateTime");
      this.TreeNodeDateTime.Image = (Image) componentResourceManager.GetObject("treeNodeFunction.Image");
      this.TreeNodeDateTime.Visible = false;
      this.TreeNodeFunctions.Nodes.Add(this.TreeNodeDateTime);
      this.TreeNodeLogical.Text = LocalizationProvider<RadGridLocalizationProvider>.CurrentProvider.GetLocalizedString("ExpressionFormFunctionsLogical");
      this.TreeNodeLogical.Image = (Image) componentResourceManager.GetObject("treeNodeFunction.Image");
      this.TreeNodeLogical.Visible = false;
      this.TreeNodeFunctions.Nodes.Add(this.TreeNodeLogical);
      this.TreeNodeMath.Text = LocalizationProvider<RadGridLocalizationProvider>.CurrentProvider.GetLocalizedString("ExpressionFormFunctionsMath");
      this.TreeNodeMath.Image = (Image) componentResourceManager.GetObject("treeNodeFunction.Image");
      this.TreeNodeMath.Visible = false;
      this.TreeNodeFunctions.Nodes.Add(this.TreeNodeMath);
      this.TreeNodeOther.Text = LocalizationProvider<RadGridLocalizationProvider>.CurrentProvider.GetLocalizedString("ExpressionFormFunctionsOther");
      this.TreeNodeOther.Image = (Image) componentResourceManager.GetObject("treeNodeFunction.Image");
      this.TreeNodeOther.Visible = false;
      this.TreeNodeFunctions.Nodes.Add(this.TreeNodeOther);
      this.radTreeViewFunctions.Nodes.Add(this.TreeNodeFunctions);
      this.TreeNodeOperators.Text = LocalizationProvider<RadGridLocalizationProvider>.CurrentProvider.GetLocalizedString("ExpressionFormOperators");
      this.TreeNodeOperators.Image = (Image) componentResourceManager.GetObject("treeNodeOperators.Image");
      this.TreeNodeOperators.Visible = false;
      this.radTreeViewFunctions.Nodes.Add(this.TreeNodeOperators);
      this.TreeNodeConstants.Text = LocalizationProvider<RadGridLocalizationProvider>.CurrentProvider.GetLocalizedString("ExpressionFormConstants");
      this.TreeNodeConstants.Image = (Image) componentResourceManager.GetObject("treeNodeConstants.Image");
      this.TreeNodeConstants.Visible = false;
      this.radTreeViewFunctions.Nodes.Add(this.TreeNodeConstants);
      this.TreeNodeFields.Text = LocalizationProvider<RadGridLocalizationProvider>.CurrentProvider.GetLocalizedString("ExpressionFormFields");
      this.TreeNodeFields.Image = (Image) componentResourceManager.GetObject("treeNodeFields.Image");
      this.radTreeViewFunctions.Nodes.Add(this.TreeNodeFields);
      this.radTreeViewFunctions.ExpandAll();
      this.radTreeViewFunctions.SelectedNode = this.TreeNodeFunctions;
    }

    protected virtual void SetVisibleFunctionTreeNodes()
    {
      if (RadExpressionEditorForm.ExpressionItemsList.Exists((Predicate<ExpressionItem>) (item => item.Type == ExpressionItemType.AggregateFunc)))
      {
        this.TreeNodeAggregate.Visible = true;
        this.TreeNodeFunctions.Visible = true;
      }
      if (RadExpressionEditorForm.ExpressionItemsList.Exists((Predicate<ExpressionItem>) (item => item.Type == ExpressionItemType.DateTimeFunc)))
      {
        this.TreeNodeDateTime.Visible = true;
        this.TreeNodeFunctions.Visible = true;
      }
      if (RadExpressionEditorForm.ExpressionItemsList.Exists((Predicate<ExpressionItem>) (item => item.Type == ExpressionItemType.LogicalFunc)))
      {
        this.TreeNodeLogical.Visible = true;
        this.TreeNodeFunctions.Visible = true;
      }
      if (RadExpressionEditorForm.ExpressionItemsList.Exists((Predicate<ExpressionItem>) (item => item.Type == ExpressionItemType.MathFunc)))
      {
        this.TreeNodeMath.Visible = true;
        this.TreeNodeFunctions.Visible = true;
      }
      if (RadExpressionEditorForm.ExpressionItemsList.Exists((Predicate<ExpressionItem>) (item => item.Type == ExpressionItemType.OtherFunc)))
      {
        this.TreeNodeOther.Visible = true;
        this.TreeNodeFunctions.Visible = true;
      }
      if (RadExpressionEditorForm.ExpressionItemsList.Exists((Predicate<ExpressionItem>) (item => item.Type == ExpressionItemType.StringFunc)))
      {
        this.TreeNodeText.Visible = true;
        this.TreeNodeFunctions.Visible = true;
      }
      if (RadExpressionEditorForm.ExpressionItemsList.Exists((Predicate<ExpressionItem>) (item => item.Type == ExpressionItemType.Operator)))
        this.TreeNodeOperators.Visible = true;
      if (!RadExpressionEditorForm.ExpressionItemsList.Exists((Predicate<ExpressionItem>) (item => item.Type == ExpressionItemType.Constant)))
        return;
      this.TreeNodeConstants.Visible = true;
    }

    protected virtual void AddExpressionToTextBox(string expressionValue)
    {
      int selectionStart = this.radTextBoxExpression.SelectionStart;
      string text = this.radTextBoxExpression.Text;
      expressionValue += " ";
      this.radTextBoxExpression.Text = text.Remove(selectionStart, this.radTextBoxExpression.SelectionLength).Insert(selectionStart, expressionValue);
      for (int index = 0; index < expressionValue.Length && (index <= 0 || expressionValue[index - 1] != '('); ++index)
        ++selectionStart;
      this.radTextBoxExpression.SelectionStart = selectionStart;
    }

    private static void ShowForm(
      RadExpressionEditorForm form,
      RadGridView radGridView,
      string expression,
      string columnHeaderText)
    {
      if (form.context == null)
        form.context = new RadThemeComponentBase.ThemeContext((Control) form);
      form.expression = expression;
      form.Owner = radGridView.FindForm();
      form.RightToLeft = radGridView.RightToLeft;
      form.StartPosition = FormStartPosition.CenterScreen;
      if (RadExpressionEditorForm.location != Point.Empty)
      {
        form.StartPosition = FormStartPosition.Manual;
        form.Location = RadExpressionEditorForm.location;
      }
      Screen screen = Screen.FromControl((Control) radGridView);
      if (!screen.Bounds.Contains(form.Location))
        form.Location = Point.Add(screen.Bounds.Location, new Size(50, 50));
      string localizedString = LocalizationProvider<RadGridLocalizationProvider>.CurrentProvider.GetLocalizedString("ExpressionFormTitle");
      if (string.IsNullOrEmpty(columnHeaderText))
        form.Text = localizedString;
      else
        form.Text = string.Format("{0} - [{1}]", (object) localizedString, (object) columnHeaderText);
      if (!string.IsNullOrEmpty(radGridView.ThemeName) && string.IsNullOrEmpty(RadExpressionEditorForm.ThemeName))
        form.SetTheme(radGridView.ThemeName);
      else
        form.SetTheme();
      form.Show();
    }

    protected override void OnLoad(EventArgs e)
    {
      base.OnLoad(e);
      if (RadExpressionEditorForm.ExpressionItemsList.Count == 0)
        RadExpressionEditorForm.ExpressionItemsList.LoadFromXML();
      this.InitializeTreeNodes();
      this.SetVisibleFunctionTreeNodes();
      this.CorrectForMetroTouch();
    }

    private void CorrectForMetroTouch()
    {
      if (this.theme == "TelerikMetroTouch" || RadExpressionEditorForm.themeName == "TelerikMetroTouch" || ThemeResolutionService.ApplicationThemeName == "TelerikMetroTouch")
      {
        this.context.CorrectPositions();
        this.radButtonPlus.Parent.Width += 80;
        this.radButtonPlus.Parent.Left -= 60;
        this.radButtonPlus.Parent.Parent.Width += 60;
        RadButton[] radButtonArray = new RadButton[14]
        {
          this.radButtonPlus,
          this.radButtonMinus,
          this.radButtonMultiply,
          this.radButtonDivide,
          this.radButtonModulo,
          this.radButtonEqual,
          this.radButtonNonequal,
          this.radButtonLess,
          this.radButtonLessOrEqual,
          this.radButtonGreaterOrEqual,
          this.radButtonGreater,
          this.radButtonAnd,
          this.radButtonOr,
          this.radButtonNot
        };
        for (int index = 0; index < radButtonArray.Length; ++index)
        {
          if (radButtonArray[index].Width < 33)
            radButtonArray[index].Width = 33;
          radButtonArray[index].Left += 6 * index;
        }
      }
      else
      {
        if (!TelerikHelper.IsMaterialTheme(this.theme) && !TelerikHelper.IsMaterialTheme(RadExpressionEditorForm.themeName))
          return;
        RadButton[] radButtonArray = new RadButton[14]
        {
          this.radButtonPlus,
          this.radButtonMinus,
          this.radButtonMultiply,
          this.radButtonDivide,
          this.radButtonModulo,
          this.radButtonEqual,
          this.radButtonNonequal,
          this.radButtonLess,
          this.radButtonLessOrEqual,
          this.radButtonGreaterOrEqual,
          this.radButtonGreater,
          this.radButtonAnd,
          this.radButtonOr,
          this.radButtonNot
        };
        foreach (RadButtonBase radButtonBase in radButtonArray)
          radButtonBase.ButtonElement.Padding = Padding.Empty;
        this.ClientSize = new Size(724, 532);
        this.radTextBoxExpression.Location = new Point(0, 0);
        this.radTextBoxExpression.Size = new Size(712, 86);
        this.radPanel2.Location = new Point(82, 1);
        this.radPanel2.Size = new Size(510, 43);
        this.radButtonNot.Location = new Point(459, 3);
        this.radButtonNot.Size = new Size(45, 36);
        this.radButtonOr.Location = new Point(414, 3);
        this.radButtonOr.Size = new Size(45, 36);
        this.radButtonAnd.Location = new Point(369, 3);
        this.radButtonAnd.Size = new Size(45, 36);
        this.radButtonGreater.Location = new Point(321, 3);
        this.radButtonGreater.Size = new Size(28, 36);
        this.radButtonGreaterOrEqual.Location = new Point(293, 3);
        this.radButtonGreaterOrEqual.Size = new Size(28, 36);
        this.radButtonLessOrEqual.Location = new Point(265, 3);
        this.radButtonLessOrEqual.Size = new Size(28, 36);
        this.radButtonLess.Location = new Point(237, 3);
        this.radButtonLess.Size = new Size(28, 36);
        this.radButtonNonequal.Location = new Point(191, 3);
        this.radButtonNonequal.Size = new Size(28, 36);
        this.radButtonEqual.Location = new Point(163, 3);
        this.radButtonEqual.Size = new Size(28, 36);
        this.radButtonModulo.Location = new Point(115, 3);
        this.radButtonModulo.Size = new Size(28, 36);
        this.radButtonDivide.Location = new Point(87, 3);
        this.radButtonDivide.Size = new Size(28, 36);
        this.radButtonMultiply.Location = new Point(59, 3);
        this.radButtonMultiply.Size = new Size(28, 36);
        this.radButtonMinus.Location = new Point(31, 3);
        this.radButtonMinus.Size = new Size(28, 36);
        this.radButtonPlus.Location = new Point(3, 3);
        this.radButtonPlus.Size = new Size(28, 36);
        this.radSeparator1.Location = new Point(148, 3);
        this.radSeparator1.Size = new Size(8, 28);
        this.radSeparator2.Location = new Point(223, 3);
        this.radSeparator2.Size = new Size(8, 28);
        this.radSeparator3.Location = new Point(355, 3);
        this.radSeparator3.Size = new Size(8, 28);
        this.radLabelDescription.Location = new Point(3, 3);
        this.radLabelDescription.Size = new Size(706, 66);
        this.radLabelPreview.Location = new Point(3, 3);
        this.radLabelPreview.Size = new Size(514, 30);
        this.radTreeViewFunctions.Location = new Point(0, 0);
        this.radTreeViewFunctions.Size = new Size(352, 260);
        this.radListControlFunctionsList.Location = new Point(0, 0);
        this.radListControlFunctionsList.Size = new Size(352, 260);
        this.radButtonCancel.Location = new Point(617, 489);
        this.radButtonCancel.Size = new Size(100, 36);
        this.radButtonOK.Location = new Point(531, 489);
        this.radButtonOK.Size = new Size(80, 36);
        this.radPanel5.Location = new Point(5, 489);
        this.radPanel5.Size = new Size(520, 36);
        this.radPanel4.Location = new Point(5, 411);
        this.radPanel4.Size = new Size(712, 72);
        this.radSplitContainer1.Location = new Point(0, 0);
        this.radSplitContainer1.Size = new Size(712, 260);
        this.splitPanel1.Location = new Point(0, 0);
        this.splitPanel1.Size = new Size(352, 260);
        this.splitPanel2.Location = new Point(360, 0);
        this.splitPanel2.Size = new Size(352, 260);
        this.radSplitContainer2.Location = new Point(5, 5);
        this.radSplitContainer2.Size = new Size(712, 400);
        this.splitPanel3.Location = new Point(0, 0);
        this.splitPanel3.Size = new Size(712, 132);
        this.radPanel1.Location = new Point(0, 86);
        this.radPanel1.Size = new Size(712, 46);
        this.splitPanel4.Location = new Point(0, 140);
        this.splitPanel4.Size = new Size(712, 260);
      }
    }

    protected override void OnShown(EventArgs e)
    {
      base.OnShown(e);
      this.radTextBoxExpression.Focus();
      this.radTextBoxExpression.Text = this.expression;
      this.radTextBoxExpression.SelectAll();
    }

    protected override void OnClosing(CancelEventArgs e)
    {
      base.OnClosing(e);
      RadExpressionEditorForm.location = this.Location;
    }

    protected virtual void OnButtonCancelClick(object sender, EventArgs e)
    {
      this.Close();
    }

    protected virtual void OnButtonOKClick(object sender, EventArgs e)
    {
      this.DialogResult = DialogResult.OK;
      this.expression = this.radTextBoxExpression.Text;
      if (this.ReferencedColumn != null)
      {
        try
        {
          this.ReferencedColumn.Expression = this.expression;
        }
        finally
        {
          this.referencedColumn = (GridViewDataColumn) null;
        }
      }
      else if (this.FormattingObject != null)
      {
        try
        {
          this.FormattingObject.Expression = this.expression;
        }
        finally
        {
          this.formattingObject = (ExpressionFormattingObject) null;
        }
      }
      this.Close();
    }

    protected virtual void OnButtonFunctionClick(object sender, EventArgs e)
    {
      switch (((Control) sender).Name)
      {
        case "radButtonNot":
          this.AddExpressionToTextBox("NOT");
          break;
        case "radButtonOr":
          this.AddExpressionToTextBox("OR");
          break;
        case "radButtonAnd":
          this.AddExpressionToTextBox("AND");
          break;
        case "radButtonGreater":
          this.AddExpressionToTextBox(">");
          break;
        case "radButtonGreaterOrEqual":
          this.AddExpressionToTextBox(">=");
          break;
        case "radButtonLessOrEqual":
          this.AddExpressionToTextBox("<=");
          break;
        case "radButtonLess":
          this.AddExpressionToTextBox("<");
          break;
        case "radButtonNonequal":
          this.AddExpressionToTextBox("<>");
          break;
        case "radButtonEqual":
          this.AddExpressionToTextBox("=");
          break;
        case "radButtonModulo":
          this.AddExpressionToTextBox("%");
          break;
        case "radButtonDivide":
          this.AddExpressionToTextBox("/");
          break;
        case "radButtonMultiply":
          this.AddExpressionToTextBox("*");
          break;
        case "radButtonMinus":
          this.AddExpressionToTextBox("-");
          break;
        case "radButtonPlus":
          this.AddExpressionToTextBox("+");
          break;
      }
      this.radTextBoxExpression.Focus();
    }

    protected virtual void OnListControlFunctionsListDoubleClick(object sender, EventArgs e)
    {
      if (this.radListControlFunctionsList.SelectedNode == null || !this.listSelectedItemClicked)
        return;
      this.AddExpressionToTextBox(!(this.radListControlFunctionsList.SelectedNode.DataBoundItem is ExpressionItem) ? this.radListControlFunctionsList.SelectedNode.Text : ((ExpressionItem) this.radListControlFunctionsList.SelectedNode.DataBoundItem).Value);
      this.listSelectedItemClicked = false;
      this.radTextBoxExpression.Focus();
    }

    protected virtual void OnTreeViewFunctionsSelectedNodeChanged(
      object sender,
      RadTreeViewEventArgs e)
    {
      List<ExpressionItem> expressionItemList = (List<ExpressionItem>) null;
      if (e.Node == this.TreeNodeFunctions)
        expressionItemList = RadExpressionEditorForm.ExpressionItemsList.FindAll((Predicate<ExpressionItem>) (item =>
        {
          if (item.Type != ExpressionItemType.StringFunc && item.Type != ExpressionItemType.AggregateFunc && (item.Type != ExpressionItemType.DateTimeFunc && item.Type != ExpressionItemType.LogicalFunc) && item.Type != ExpressionItemType.MathFunc)
            return item.Type == ExpressionItemType.OtherFunc;
          return true;
        }));
      else if (e.Node == this.TreeNodeText)
        expressionItemList = RadExpressionEditorForm.ExpressionItemsList.FindAll((Predicate<ExpressionItem>) (item => item.Type == ExpressionItemType.StringFunc));
      else if (e.Node == this.TreeNodeAggregate)
        expressionItemList = RadExpressionEditorForm.ExpressionItemsList.FindAll((Predicate<ExpressionItem>) (item => item.Type == ExpressionItemType.AggregateFunc));
      else if (e.Node == this.TreeNodeDateTime)
        expressionItemList = RadExpressionEditorForm.ExpressionItemsList.FindAll((Predicate<ExpressionItem>) (item => item.Type == ExpressionItemType.DateTimeFunc));
      else if (e.Node == this.TreeNodeLogical)
        expressionItemList = RadExpressionEditorForm.ExpressionItemsList.FindAll((Predicate<ExpressionItem>) (item => item.Type == ExpressionItemType.LogicalFunc));
      else if (e.Node == this.TreeNodeMath)
        expressionItemList = RadExpressionEditorForm.ExpressionItemsList.FindAll((Predicate<ExpressionItem>) (item => item.Type == ExpressionItemType.MathFunc));
      else if (e.Node == this.TreeNodeOther)
        expressionItemList = RadExpressionEditorForm.ExpressionItemsList.FindAll((Predicate<ExpressionItem>) (item => item.Type == ExpressionItemType.OtherFunc));
      else if (e.Node == this.TreeNodeOperators)
        expressionItemList = RadExpressionEditorForm.ExpressionItemsList.FindAll((Predicate<ExpressionItem>) (item => item.Type == ExpressionItemType.Operator));
      else if (e.Node == this.TreeNodeConstants)
        expressionItemList = RadExpressionEditorForm.ExpressionItemsList.FindAll((Predicate<ExpressionItem>) (item => item.Type == ExpressionItemType.Constant));
      else if (e.Node == this.TreeNodeFields && this.FieldList != null && this.FieldList.Count > 0)
      {
        this.radListControlFunctionsList.DataSource = (object) this.FieldList;
        this.radListControlFunctionsList.SelectedNode = (RadTreeNode) null;
        return;
      }
      this.radListControlFunctionsList.DataSource = (object) expressionItemList;
      this.radListControlFunctionsList.SelectedNode = (RadTreeNode) null;
    }

    protected virtual void OnListControlFunctionsListSelectedNodeChanged(
      object sender,
      RadTreeViewEventArgs e)
    {
      if (string.IsNullOrEmpty(this.funcDescriptionStr))
        this.funcDescriptionStr = "<html><b>" + LocalizationProvider<RadGridLocalizationProvider>.CurrentProvider.GetLocalizedString("ExpressionFormDescription") + "</b> ";
      this.radLabelDescription.Text = this.funcDescriptionStr;
      if (this.radListControlFunctionsList.SelectedNode == null || !(this.radListControlFunctionsList.SelectedNode.DataBoundItem is ExpressionItem))
        return;
      ExpressionItem dataBoundItem = (ExpressionItem) this.radListControlFunctionsList.SelectedNode.DataBoundItem;
      if (string.IsNullOrEmpty(dataBoundItem.Description))
        return;
      this.radLabelDescription.Text = string.Format("<html><b>{0}</b>", (object) dataBoundItem.Syntax) + dataBoundItem.Description;
    }

    protected virtual void OnListControlFunctionsListMouseDown(object sender, MouseEventArgs e)
    {
      this.listSelectedItemClicked = false;
      RadElement elementAtPoint = this.radListControlFunctionsList.ElementTree.GetElementAtPoint(e.Location);
      TreeNodeElement treeNodeElement = elementAtPoint as TreeNodeElement;
      if (treeNodeElement == null && elementAtPoint != null)
        treeNodeElement = elementAtPoint.FindAncestor<TreeNodeElement>();
      if (treeNodeElement == null || this.radListControlFunctionsList.SelectedNode == null || treeNodeElement.Data != this.radListControlFunctionsList.SelectedNode)
        return;
      this.listSelectedItemClicked = true;
    }

    protected virtual void OnTextBoxExpressionTextChanged(object sender, EventArgs e)
    {
      object result = (object) null;
      if (this.ReferencedColumn != null)
      {
        bool flag;
        if (this.ReferencedColumn.OwnerTemplate.RowCount == 0)
        {
          ExpressionNode expressionNode;
          flag = DataUtils.TryParse(this.radTextBoxExpression.Text, false, out expressionNode);
        }
        else
        {
          int currentPosition = this.ReferencedColumn.OwnerTemplate.DataView.CurrentPosition;
          flag = this.ReferencedColumn.OwnerTemplate.ListSource.CollectionView.TryEvaluate(this.radTextBoxExpression.Text, (IEnumerable<GridViewRowInfo>) this.ReferencedColumn.OwnerTemplate.Rows, currentPosition >= 0 ? currentPosition : 0, out result);
        }
        if (string.IsNullOrEmpty(this.radTextBoxExpression.Text) || flag)
          this.radButtonOK.Enabled = true;
        else
          this.radButtonOK.Enabled = false;
      }
      if (string.IsNullOrEmpty(this.resultPreviewStr))
        this.resultPreviewStr = "<html>" + LocalizationProvider<RadGridLocalizationProvider>.CurrentProvider.GetLocalizedString("ExpressionFormResultPreview") + ": ";
      this.radLabelPreview.Text = this.resultPreviewStr + string.Format("<b>{0}", result);
    }

    protected virtual void OnTextBoxExpressionGotFocus(object sender, EventArgs e)
    {
      this.radTextBoxExpression.SelectionLength = 0;
    }

    protected virtual void OnButtonFunctionToolTipTextNeeded(
      object sender,
      ToolTipTextNeededEventArgs e)
    {
      switch (((RadElement) sender).ElementTree.Control.Name)
      {
        case "radButtonNot":
          e.ToolTipText = LocalizationProvider<RadGridLocalizationProvider>.CurrentProvider.GetLocalizedString("ExpressionFormTooltipNot");
          break;
        case "radButtonOr":
          e.ToolTipText = LocalizationProvider<RadGridLocalizationProvider>.CurrentProvider.GetLocalizedString("ExpressionFormTooltipOr");
          break;
        case "radButtonAnd":
          e.ToolTipText = LocalizationProvider<RadGridLocalizationProvider>.CurrentProvider.GetLocalizedString("ExpressionFormTooltipAnd");
          break;
        case "radButtonGreater":
          e.ToolTipText = LocalizationProvider<RadGridLocalizationProvider>.CurrentProvider.GetLocalizedString("ExpressionFormTooltipGreater");
          break;
        case "radButtonGreaterOrEqual":
          e.ToolTipText = LocalizationProvider<RadGridLocalizationProvider>.CurrentProvider.GetLocalizedString("ExpressionFormTooltipGreaterOrEqual");
          break;
        case "radButtonLessOrEqual":
          e.ToolTipText = LocalizationProvider<RadGridLocalizationProvider>.CurrentProvider.GetLocalizedString("ExpressionFormTooltipLessOrEqual");
          break;
        case "radButtonLess":
          e.ToolTipText = LocalizationProvider<RadGridLocalizationProvider>.CurrentProvider.GetLocalizedString("ExpressionFormTooltipLess");
          break;
        case "radButtonNonequal":
          e.ToolTipText = LocalizationProvider<RadGridLocalizationProvider>.CurrentProvider.GetLocalizedString("ExpressionFormTooltipNotEqual");
          break;
        case "radButtonEqual":
          e.ToolTipText = LocalizationProvider<RadGridLocalizationProvider>.CurrentProvider.GetLocalizedString("ExpressionFormTooltipEqual");
          break;
        case "radButtonModulo":
          e.ToolTipText = LocalizationProvider<RadGridLocalizationProvider>.CurrentProvider.GetLocalizedString("ExpressionFormTooltipModulo");
          break;
        case "radButtonDivide":
          e.ToolTipText = LocalizationProvider<RadGridLocalizationProvider>.CurrentProvider.GetLocalizedString("ExpressionFormTooltipDivide");
          break;
        case "radButtonMultiply":
          e.ToolTipText = LocalizationProvider<RadGridLocalizationProvider>.CurrentProvider.GetLocalizedString("ExpressionFormTooltipMultiply");
          break;
        case "radButtonMinus":
          e.ToolTipText = LocalizationProvider<RadGridLocalizationProvider>.CurrentProvider.GetLocalizedString("ExpressionFormTooltipMinus");
          break;
        case "radButtonPlus":
          e.ToolTipText = LocalizationProvider<RadGridLocalizationProvider>.CurrentProvider.GetLocalizedString("ExpressionFormTooltipPlus");
          break;
      }
    }

    protected override void Dispose(bool disposing)
    {
      ExprEditorDragDropManager.Detach();
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (RadExpressionEditorForm));
      this.radTextBoxExpression = new RadTextBox();
      this.radPanel2 = new RadPanel();
      this.radButtonNot = new RadButton();
      this.radButtonOr = new RadButton();
      this.radButtonAnd = new RadButton();
      this.radButtonGreater = new RadButton();
      this.radButtonGreaterOrEqual = new RadButton();
      this.radButtonLessOrEqual = new RadButton();
      this.radButtonLess = new RadButton();
      this.radButtonNonequal = new RadButton();
      this.radButtonEqual = new RadButton();
      this.radButtonModulo = new RadButton();
      this.radButtonDivide = new RadButton();
      this.radButtonMultiply = new RadButton();
      this.radButtonMinus = new RadButton();
      this.radButtonPlus = new RadButton();
      this.radSeparator1 = new RadSeparator();
      this.radSeparator2 = new RadSeparator();
      this.radSeparator3 = new RadSeparator();
      this.radLabelDescription = new RadLabel();
      this.radLabelPreview = new RadLabel();
      this.radTreeViewFunctions = new RadTreeView();
      this.radListControlFunctionsList = new RadTreeView();
      this.radButtonCancel = new RadButton();
      this.radButtonOK = new RadButton();
      this.radPanel5 = new RadPanel();
      this.radPanel4 = new RadPanel();
      this.radSplitContainer1 = new RadSplitContainer();
      this.splitPanel1 = new SplitPanel();
      this.splitPanel2 = new SplitPanel();
      this.radSplitContainer2 = new RadSplitContainer();
      this.splitPanel3 = new SplitPanel();
      this.radPanel1 = new RadPanel();
      this.splitPanel4 = new SplitPanel();
      this.radTextBoxExpression.BeginInit();
      this.radPanel2.BeginInit();
      this.radPanel2.SuspendLayout();
      this.radButtonNot.BeginInit();
      this.radButtonOr.BeginInit();
      this.radButtonAnd.BeginInit();
      this.radButtonGreater.BeginInit();
      this.radButtonGreaterOrEqual.BeginInit();
      this.radButtonLessOrEqual.BeginInit();
      this.radButtonLess.BeginInit();
      this.radButtonNonequal.BeginInit();
      this.radButtonEqual.BeginInit();
      this.radButtonModulo.BeginInit();
      this.radButtonDivide.BeginInit();
      this.radButtonMultiply.BeginInit();
      this.radButtonMinus.BeginInit();
      this.radButtonPlus.BeginInit();
      this.radSeparator1.BeginInit();
      this.radSeparator2.BeginInit();
      this.radSeparator3.BeginInit();
      this.radLabelDescription.BeginInit();
      this.radLabelPreview.BeginInit();
      this.radTreeViewFunctions.BeginInit();
      this.radListControlFunctionsList.BeginInit();
      this.radButtonCancel.BeginInit();
      this.radButtonOK.BeginInit();
      this.radPanel5.BeginInit();
      this.radPanel5.SuspendLayout();
      this.radPanel4.BeginInit();
      this.radPanel4.SuspendLayout();
      this.radSplitContainer1.BeginInit();
      this.radSplitContainer1.SuspendLayout();
      this.splitPanel1.BeginInit();
      this.splitPanel1.SuspendLayout();
      this.splitPanel2.BeginInit();
      this.splitPanel2.SuspendLayout();
      this.radSplitContainer2.BeginInit();
      this.radSplitContainer2.SuspendLayout();
      this.splitPanel3.BeginInit();
      this.splitPanel3.SuspendLayout();
      this.radPanel1.BeginInit();
      this.radPanel1.SuspendLayout();
      this.splitPanel4.BeginInit();
      this.splitPanel4.SuspendLayout();
      this.BeginInit();
      this.SuspendLayout();
      this.radTextBoxExpression.HideSelection = false;
      this.radTextBoxExpression.Location = new Point(0, 0);
      this.radTextBoxExpression.Margin = new Padding(0);
      this.radTextBoxExpression.Multiline = true;
      this.radTextBoxExpression.Name = "radTextBoxExpression";
      this.radTextBoxExpression.Anchor = AnchorStyles.Top | AnchorStyles.Bottom;
      this.radTextBoxExpression.RootElement.StretchVertically = true;
      this.radTextBoxExpression.Size = new Size(651, 76);
      this.radTextBoxExpression.TabIndex = 0;
      this.radTextBoxExpression.TabStop = false;
      this.radTextBoxExpression.TextChanged += new EventHandler(this.OnTextBoxExpressionTextChanged);
      this.radTextBoxExpression.GotFocus += new EventHandler(this.OnTextBoxExpressionGotFocus);
      this.radPanel2.Controls.Add((Control) this.radButtonNot);
      this.radPanel2.Controls.Add((Control) this.radButtonOr);
      this.radPanel2.Controls.Add((Control) this.radButtonAnd);
      this.radPanel2.Controls.Add((Control) this.radButtonGreater);
      this.radPanel2.Controls.Add((Control) this.radButtonGreaterOrEqual);
      this.radPanel2.Controls.Add((Control) this.radButtonLessOrEqual);
      this.radPanel2.Controls.Add((Control) this.radButtonLess);
      this.radPanel2.Controls.Add((Control) this.radButtonNonequal);
      this.radPanel2.Controls.Add((Control) this.radButtonEqual);
      this.radPanel2.Controls.Add((Control) this.radButtonModulo);
      this.radPanel2.Controls.Add((Control) this.radButtonDivide);
      this.radPanel2.Controls.Add((Control) this.radButtonMultiply);
      this.radPanel2.Controls.Add((Control) this.radButtonMinus);
      this.radPanel2.Controls.Add((Control) this.radButtonPlus);
      this.radPanel2.Controls.Add((Control) this.radSeparator1);
      this.radPanel2.Controls.Add((Control) this.radSeparator2);
      this.radPanel2.Controls.Add((Control) this.radSeparator3);
      this.radPanel2.Location = new Point(82, 4);
      this.radPanel2.Name = "radPanel2";
      this.radPanel2.Size = new Size(510, 34);
      this.radPanel2.TabIndex = 1;
      this.radPanel2.GetChildAt(0).GetChildAt(1).Visibility = ElementVisibility.Collapsed;
      this.radButtonNot.Image = (Image) componentResourceManager.GetObject("radButtonNot.Image");
      this.radButtonNot.ImageAlignment = ContentAlignment.MiddleCenter;
      this.radButtonNot.Location = new Point(459, 3);
      this.radButtonNot.Name = "radButtonNot";
      this.radButtonNot.Size = new Size(45, 28);
      this.radButtonNot.TabIndex = 13;
      this.radButtonNot.ToolTipTextNeeded += new ToolTipTextNeededEventHandler(this.OnButtonFunctionToolTipTextNeeded);
      this.radButtonNot.Click += new EventHandler(this.OnButtonFunctionClick);
      this.radButtonOr.Image = (Image) componentResourceManager.GetObject("radButtonOr.Image");
      this.radButtonOr.ImageAlignment = ContentAlignment.MiddleCenter;
      this.radButtonOr.Location = new Point(414, 3);
      this.radButtonOr.Name = "radButtonOr";
      this.radButtonOr.Size = new Size(45, 28);
      this.radButtonOr.TabIndex = 12;
      this.radButtonOr.ToolTipTextNeeded += new ToolTipTextNeededEventHandler(this.OnButtonFunctionToolTipTextNeeded);
      this.radButtonOr.Click += new EventHandler(this.OnButtonFunctionClick);
      this.radButtonAnd.Image = (Image) componentResourceManager.GetObject("radButtonAnd.Image");
      this.radButtonAnd.ImageAlignment = ContentAlignment.MiddleCenter;
      this.radButtonAnd.Location = new Point(369, 3);
      this.radButtonAnd.Name = "radButtonAnd";
      this.radButtonAnd.Size = new Size(45, 28);
      this.radButtonAnd.TabIndex = 11;
      this.radButtonAnd.ToolTipTextNeeded += new ToolTipTextNeededEventHandler(this.OnButtonFunctionToolTipTextNeeded);
      this.radButtonAnd.Click += new EventHandler(this.OnButtonFunctionClick);
      this.radButtonGreater.Image = (Image) componentResourceManager.GetObject("radButtonGreater.Image");
      this.radButtonGreater.ImageAlignment = ContentAlignment.MiddleCenter;
      this.radButtonGreater.Location = new Point(321, 3);
      this.radButtonGreater.Name = "radButtonGreater";
      this.radButtonGreater.Size = new Size(28, 28);
      this.radButtonGreater.TabIndex = 10;
      this.radButtonGreater.ToolTipTextNeeded += new ToolTipTextNeededEventHandler(this.OnButtonFunctionToolTipTextNeeded);
      this.radButtonGreater.Click += new EventHandler(this.OnButtonFunctionClick);
      this.radButtonGreaterOrEqual.Image = (Image) componentResourceManager.GetObject("radButtonGreaterOrEqual.Image");
      this.radButtonGreaterOrEqual.ImageAlignment = ContentAlignment.MiddleCenter;
      this.radButtonGreaterOrEqual.Location = new Point(293, 3);
      this.radButtonGreaterOrEqual.Name = "radButtonGreaterOrEqual";
      this.radButtonGreaterOrEqual.Size = new Size(28, 28);
      this.radButtonGreaterOrEqual.TabIndex = 9;
      this.radButtonGreaterOrEqual.ToolTipTextNeeded += new ToolTipTextNeededEventHandler(this.OnButtonFunctionToolTipTextNeeded);
      this.radButtonGreaterOrEqual.Click += new EventHandler(this.OnButtonFunctionClick);
      this.radButtonLessOrEqual.Image = (Image) componentResourceManager.GetObject("radButtonLessOrEqual.Image");
      this.radButtonLessOrEqual.ImageAlignment = ContentAlignment.MiddleCenter;
      this.radButtonLessOrEqual.Location = new Point(265, 3);
      this.radButtonLessOrEqual.Name = "radButtonLessOrEqual";
      this.radButtonLessOrEqual.Size = new Size(28, 28);
      this.radButtonLessOrEqual.TabIndex = 8;
      this.radButtonLessOrEqual.ToolTipTextNeeded += new ToolTipTextNeededEventHandler(this.OnButtonFunctionToolTipTextNeeded);
      this.radButtonLessOrEqual.Click += new EventHandler(this.OnButtonFunctionClick);
      this.radButtonLess.Image = (Image) componentResourceManager.GetObject("radButtonLess.Image");
      this.radButtonLess.ImageAlignment = ContentAlignment.MiddleCenter;
      this.radButtonLess.Location = new Point(237, 3);
      this.radButtonLess.Name = "radButtonLess";
      this.radButtonLess.Size = new Size(28, 28);
      this.radButtonLess.TabIndex = 7;
      this.radButtonLess.ToolTipTextNeeded += new ToolTipTextNeededEventHandler(this.OnButtonFunctionToolTipTextNeeded);
      this.radButtonLess.Click += new EventHandler(this.OnButtonFunctionClick);
      this.radButtonNonequal.Image = (Image) componentResourceManager.GetObject("radButtonNonequal.Image");
      this.radButtonNonequal.ImageAlignment = ContentAlignment.MiddleCenter;
      this.radButtonNonequal.Location = new Point(191, 3);
      this.radButtonNonequal.Name = "radButtonNonequal";
      this.radButtonNonequal.Size = new Size(28, 28);
      this.radButtonNonequal.TabIndex = 6;
      this.radButtonNonequal.ToolTipTextNeeded += new ToolTipTextNeededEventHandler(this.OnButtonFunctionToolTipTextNeeded);
      this.radButtonNonequal.Click += new EventHandler(this.OnButtonFunctionClick);
      this.radButtonEqual.Image = (Image) componentResourceManager.GetObject("radButtonEqual.Image");
      this.radButtonEqual.ImageAlignment = ContentAlignment.MiddleCenter;
      this.radButtonEqual.Location = new Point(163, 3);
      this.radButtonEqual.Name = "radButtonEqual";
      this.radButtonEqual.Size = new Size(28, 28);
      this.radButtonEqual.TabIndex = 5;
      this.radButtonEqual.ToolTipTextNeeded += new ToolTipTextNeededEventHandler(this.OnButtonFunctionToolTipTextNeeded);
      this.radButtonEqual.Click += new EventHandler(this.OnButtonFunctionClick);
      this.radButtonModulo.Image = (Image) componentResourceManager.GetObject("radButtonModulo.Image");
      this.radButtonModulo.ImageAlignment = ContentAlignment.MiddleCenter;
      this.radButtonModulo.Location = new Point(115, 3);
      this.radButtonModulo.Name = "radButtonModulo";
      this.radButtonModulo.Size = new Size(28, 28);
      this.radButtonModulo.TabIndex = 4;
      this.radButtonModulo.ToolTipTextNeeded += new ToolTipTextNeededEventHandler(this.OnButtonFunctionToolTipTextNeeded);
      this.radButtonModulo.Click += new EventHandler(this.OnButtonFunctionClick);
      this.radButtonDivide.Image = (Image) componentResourceManager.GetObject("radButtonDivide.Image");
      this.radButtonDivide.ImageAlignment = ContentAlignment.MiddleCenter;
      this.radButtonDivide.Location = new Point(87, 3);
      this.radButtonDivide.Name = "radButtonDivide";
      this.radButtonDivide.Size = new Size(28, 28);
      this.radButtonDivide.TabIndex = 3;
      this.radButtonDivide.ToolTipTextNeeded += new ToolTipTextNeededEventHandler(this.OnButtonFunctionToolTipTextNeeded);
      this.radButtonDivide.Click += new EventHandler(this.OnButtonFunctionClick);
      this.radButtonMultiply.Image = (Image) componentResourceManager.GetObject("radButtonMultiply.Image");
      this.radButtonMultiply.ImageAlignment = ContentAlignment.MiddleCenter;
      this.radButtonMultiply.Location = new Point(59, 3);
      this.radButtonMultiply.Name = "radButtonMultiply";
      this.radButtonMultiply.Size = new Size(28, 28);
      this.radButtonMultiply.TabIndex = 2;
      this.radButtonMultiply.ToolTipTextNeeded += new ToolTipTextNeededEventHandler(this.OnButtonFunctionToolTipTextNeeded);
      this.radButtonMultiply.Click += new EventHandler(this.OnButtonFunctionClick);
      this.radButtonMinus.Image = (Image) componentResourceManager.GetObject("radButtonMinus.Image");
      this.radButtonMinus.ImageAlignment = ContentAlignment.MiddleCenter;
      this.radButtonMinus.Location = new Point(31, 3);
      this.radButtonMinus.Name = "radButtonMinus";
      this.radButtonMinus.Size = new Size(28, 28);
      this.radButtonMinus.TabIndex = 1;
      this.radButtonMinus.ToolTipTextNeeded += new ToolTipTextNeededEventHandler(this.OnButtonFunctionToolTipTextNeeded);
      this.radButtonMinus.Click += new EventHandler(this.OnButtonFunctionClick);
      this.radButtonPlus.Image = (Image) componentResourceManager.GetObject("radButtonPlus.Image");
      this.radButtonPlus.ImageAlignment = ContentAlignment.MiddleCenter;
      this.radButtonPlus.Location = new Point(3, 3);
      this.radButtonPlus.Name = "radButtonPlus";
      this.radButtonPlus.Size = new Size(28, 28);
      this.radButtonPlus.TabIndex = 0;
      this.radButtonPlus.ToolTipTextNeeded += new ToolTipTextNeededEventHandler(this.OnButtonFunctionToolTipTextNeeded);
      this.radButtonPlus.Click += new EventHandler(this.OnButtonFunctionClick);
      this.radSeparator1.Location = new Point(148, 3);
      this.radSeparator1.Name = "radSeparator1";
      this.radSeparator1.Orientation = Orientation.Vertical;
      this.radSeparator1.ShadowOffset = new Point(0, 0);
      this.radSeparator1.Size = new Size(8, 28);
      this.radSeparator1.TabIndex = 13;
      this.radSeparator2.Location = new Point(223, 3);
      this.radSeparator2.Name = "radSeparator2";
      this.radSeparator2.Orientation = Orientation.Vertical;
      this.radSeparator2.ShadowOffset = new Point(0, 0);
      this.radSeparator2.Size = new Size(8, 28);
      this.radSeparator2.TabIndex = 14;
      this.radSeparator3.Location = new Point(355, 3);
      this.radSeparator3.Name = "radSeparator3";
      this.radSeparator3.Orientation = Orientation.Vertical;
      this.radSeparator3.ShadowOffset = new Point(0, 0);
      this.radSeparator3.Size = new Size(8, 28);
      this.radSeparator3.TabIndex = 15;
      this.radLabelDescription.AutoSize = false;
      this.radLabelDescription.Dock = DockStyle.Fill;
      this.radLabelDescription.Location = new Point(3, 3);
      this.radLabelDescription.Name = "radLabelDescription";
      this.radLabelDescription.Size = new Size(645, 58);
      this.radLabelDescription.TabIndex = 0;
      this.radLabelPreview.AutoSize = false;
      this.radLabelPreview.Dock = DockStyle.Fill;
      this.radLabelPreview.Location = new Point(3, 3);
      this.radLabelPreview.Name = "radLabelPreview";
      this.radLabelPreview.Size = new Size(473, 18);
      this.radLabelPreview.TabIndex = 0;
      this.radTreeViewFunctions.Cursor = Cursors.Default;
      this.radTreeViewFunctions.Dock = DockStyle.Fill;
      this.radTreeViewFunctions.Font = new Font("Segoe UI", 8.25f);
      this.radTreeViewFunctions.Location = new Point(0, 0);
      this.radTreeViewFunctions.Margin = new Padding(0);
      this.radTreeViewFunctions.Name = "radTreeViewFunctions";
      this.radTreeViewFunctions.RightToLeft = RightToLeft.No;
      this.radTreeViewFunctions.Size = new Size(324, 248);
      this.radTreeViewFunctions.SpacingBetweenNodes = -1;
      this.radTreeViewFunctions.TabIndex = 0;
      this.radTreeViewFunctions.SelectedNodeChanged += new RadTreeView.RadTreeViewEventHandler(this.OnTreeViewFunctionsSelectedNodeChanged);
      ((RadTreeViewElement) this.radTreeViewFunctions.GetChildAt(0)).NodeSpacing = -1;
      this.radListControlFunctionsList.DisplayMember = "Name";
      this.radListControlFunctionsList.Dock = DockStyle.Fill;
      this.radListControlFunctionsList.Location = new Point(0, 0);
      this.radListControlFunctionsList.Name = "radListControlFunctionsList";
      this.radListControlFunctionsList.TreeIndent = 0;
      this.radListControlFunctionsList.Size = new Size(324, 248);
      this.radListControlFunctionsList.SpacingBetweenNodes = -1;
      this.radListControlFunctionsList.TabIndex = 0;
      this.radListControlFunctionsList.SelectedNodeChanged += new RadTreeView.RadTreeViewEventHandler(this.OnListControlFunctionsListSelectedNodeChanged);
      this.radListControlFunctionsList.DoubleClick += new EventHandler(this.OnListControlFunctionsListDoubleClick);
      this.radListControlFunctionsList.MouseDown += new MouseEventHandler(this.OnListControlFunctionsListMouseDown);
      this.radButtonCancel.DialogResult = DialogResult.Cancel;
      this.radButtonCancel.Location = new Point(574, 447);
      this.radButtonCancel.Name = "radButtonCancel";
      this.radButtonCancel.Size = new Size(80, 24);
      this.radButtonCancel.TabIndex = 1;
      this.radButtonCancel.Click += new EventHandler(this.OnButtonCancelClick);
      this.radButtonOK.DialogResult = DialogResult.OK;
      this.radButtonOK.Location = new Point(488, 447);
      this.radButtonOK.Name = "radButtonOK";
      this.radButtonOK.Size = new Size(80, 24);
      this.radButtonOK.TabIndex = 0;
      this.radButtonOK.Click += new EventHandler(this.OnButtonOKClick);
      this.radPanel5.Controls.Add((Control) this.radLabelPreview);
      this.radPanel5.Location = new Point(3, 447);
      this.radPanel5.Name = "radPanel5";
      this.radPanel5.Padding = new Padding(3);
      this.radPanel5.RootElement.Padding = new Padding(3);
      this.radPanel5.Size = new Size(479, 24);
      this.radPanel5.TabIndex = 4;
      this.radPanel4.Controls.Add((Control) this.radLabelDescription);
      this.radPanel4.Location = new Point(3, 377);
      this.radPanel4.Name = "radPanel4";
      this.radPanel4.Padding = new Padding(3);
      this.radPanel4.RootElement.Padding = new Padding(3);
      this.radPanel4.Size = new Size(651, 64);
      this.radPanel4.TabIndex = 3;
      this.radSplitContainer1.Controls.Add((Control) this.splitPanel1);
      this.radSplitContainer1.Controls.Add((Control) this.splitPanel2);
      this.radSplitContainer1.Dock = DockStyle.Fill;
      this.radSplitContainer1.Location = new Point(0, 0);
      this.radSplitContainer1.Name = "radSplitContainer1";
      this.radSplitContainer1.RootElement.MinSize = new Size(25, 25);
      this.radSplitContainer1.Size = new Size(651, 248);
      this.radSplitContainer1.TabIndex = 8;
      this.radSplitContainer1.TabStop = false;
      this.radSplitContainer1.Text = "radSplitContainer1";
      this.splitPanel1.Controls.Add((Control) this.radTreeViewFunctions);
      this.splitPanel1.Location = new Point(0, 0);
      this.splitPanel1.Name = "splitPanel1";
      this.splitPanel1.RootElement.MinSize = new Size(25, 25);
      this.splitPanel1.Size = new Size(324, 248);
      this.splitPanel1.TabIndex = 0;
      this.splitPanel1.TabStop = false;
      this.splitPanel1.Text = "splitPanel1";
      this.splitPanel2.Controls.Add((Control) this.radListControlFunctionsList);
      this.splitPanel2.Location = new Point(327, 0);
      this.splitPanel2.Name = "splitPanel2";
      this.splitPanel2.RootElement.MinSize = new Size(25, 25);
      this.splitPanel2.Size = new Size(324, 248);
      this.splitPanel2.TabIndex = 1;
      this.splitPanel2.TabStop = false;
      this.splitPanel2.Text = "splitPanel2";
      this.radSplitContainer2.Controls.Add((Control) this.splitPanel3);
      this.radSplitContainer2.Controls.Add((Control) this.splitPanel4);
      this.radSplitContainer2.Location = new Point(3, 5);
      this.radSplitContainer2.Name = "radSplitContainer2";
      this.radSplitContainer2.Orientation = Orientation.Horizontal;
      this.radSplitContainer2.RootElement.MinSize = new Size(25, 25);
      this.radSplitContainer2.Size = new Size(651, 366);
      this.radSplitContainer2.TabIndex = 9;
      this.radSplitContainer2.TabStop = false;
      this.radSplitContainer2.Text = "radSplitContainer2";
      this.splitPanel3.Controls.Add((Control) this.radPanel1);
      this.splitPanel3.Controls.Add((Control) this.radTextBoxExpression);
      this.splitPanel3.Location = new Point(0, 0);
      this.splitPanel3.Name = "splitPanel3";
      this.splitPanel3.RootElement.MinSize = new Size(25, 25);
      this.splitPanel3.Size = new Size(651, 115);
      this.splitPanel3.SizeInfo.AutoSizeScale = new SizeF(0.0f, -0.1824512f);
      this.splitPanel3.SizeInfo.SplitterCorrection = new Size(0, -66);
      this.splitPanel3.TabIndex = 0;
      this.splitPanel3.TabStop = false;
      this.splitPanel3.Text = "splitPanel3";
      this.radPanel1.Controls.Add((Control) this.radPanel2);
      this.radPanel1.Dock = DockStyle.Bottom;
      this.radPanel1.Location = new Point(0, 76);
      this.radPanel1.Name = "radPanel1";
      this.radPanel1.Size = new Size(651, 39);
      this.radPanel1.TabIndex = 2;
      this.radPanel1.GetChildAt(0).GetChildAt(1).Visibility = ElementVisibility.Collapsed;
      this.splitPanel4.Controls.Add((Control) this.radSplitContainer1);
      this.splitPanel4.Location = new Point(0, 118);
      this.splitPanel4.Name = "splitPanel4";
      this.splitPanel4.RootElement.MinSize = new Size(25, 25);
      this.splitPanel4.Size = new Size(651, 248);
      this.splitPanel4.SizeInfo.AutoSizeScale = new SizeF(0.0f, 0.1824512f);
      this.splitPanel4.SizeInfo.SplitterCorrection = new Size(0, 66);
      this.splitPanel4.TabIndex = 1;
      this.splitPanel4.TabStop = false;
      this.splitPanel4.Text = "splitPanel4";
      this.AcceptButton = (IButtonControl) this.radButtonOK;
      this.CancelButton = (IButtonControl) this.radButtonCancel;
      this.AutoScaleDimensions = new SizeF(6f, 13f);
      this.AutoScaleMode = AutoScaleMode.None;
      this.ClientSize = new Size(660, 489);
      this.Controls.Add((Control) this.radSplitContainer2);
      this.Controls.Add((Control) this.radPanel5);
      this.Controls.Add((Control) this.radPanel4);
      this.Controls.Add((Control) this.radButtonOK);
      this.Controls.Add((Control) this.radButtonCancel);
      this.FormBorderStyle = FormBorderStyle.FixedToolWindow;
      this.Name = nameof (RadExpressionEditorForm);
      this.RootElement.ApplyShapeToControl = true;
      this.ShowIcon = false;
      this.ShowInTaskbar = false;
      this.StartPosition = FormStartPosition.Manual;
      this.Text = "Expression Editor";
      this.radTextBoxExpression.EndInit();
      this.radPanel2.EndInit();
      this.radPanel2.ResumeLayout(false);
      this.radButtonNot.EndInit();
      this.radButtonOr.EndInit();
      this.radButtonAnd.EndInit();
      this.radButtonGreater.EndInit();
      this.radButtonGreaterOrEqual.EndInit();
      this.radButtonLessOrEqual.EndInit();
      this.radButtonLess.EndInit();
      this.radButtonNonequal.EndInit();
      this.radButtonEqual.EndInit();
      this.radButtonModulo.EndInit();
      this.radButtonDivide.EndInit();
      this.radButtonMultiply.EndInit();
      this.radButtonMinus.EndInit();
      this.radButtonPlus.EndInit();
      this.radSeparator1.EndInit();
      this.radSeparator2.EndInit();
      this.radSeparator3.EndInit();
      this.radLabelDescription.EndInit();
      this.radLabelPreview.EndInit();
      this.radTreeViewFunctions.EndInit();
      this.radListControlFunctionsList.EndInit();
      this.radButtonCancel.EndInit();
      this.radButtonOK.EndInit();
      this.radPanel5.EndInit();
      this.radPanel5.ResumeLayout(false);
      this.radPanel4.EndInit();
      this.radPanel4.ResumeLayout(false);
      this.radSplitContainer1.EndInit();
      this.radSplitContainer1.ResumeLayout(false);
      this.splitPanel1.EndInit();
      this.splitPanel1.ResumeLayout(false);
      this.splitPanel2.EndInit();
      this.splitPanel2.ResumeLayout(false);
      this.radSplitContainer2.EndInit();
      this.radSplitContainer2.ResumeLayout(false);
      this.splitPanel3.EndInit();
      this.splitPanel3.ResumeLayout(false);
      this.splitPanel3.PerformLayout();
      this.radPanel1.EndInit();
      this.radPanel1.ResumeLayout(false);
      this.splitPanel4.EndInit();
      this.splitPanel4.ResumeLayout(false);
      this.EndInit();
      this.ResumeLayout(false);
    }
  }
}
