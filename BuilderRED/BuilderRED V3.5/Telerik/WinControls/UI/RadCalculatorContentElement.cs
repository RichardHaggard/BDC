// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.RadCalculatorContentElement
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;
using Telerik.WinControls.Layouts;

namespace Telerik.WinControls.UI
{
  public class RadCalculatorContentElement : LightVisualElement
  {
    private RadCalculatorDropDownElement calculatorElement;
    private Decimal? value;
    private bool initialNumber;
    private RadCalculatorContentElement.ArithmeticOperation currentOperation;
    private Decimal memoryValue;
    private Decimal input;
    private bool equalsPressed;
    private bool operationFinished;
    private GridLayout container;
    private RadCalculatorOperationButtonElement buttonAdd;
    private RadCalculatorOperationButtonElement buttonSubstract;
    private RadCalculatorOperationButtonElement buttonMultiply;
    private RadCalculatorOperationButtonElement buttonDivide;
    private RadCalculatorOperationButtonElement buttonSqrt;
    private RadCalculatorOperationButtonElement buttonPercent;
    private RadCalculatorOperationButtonElement buttonReciprocal;
    private RadCalculatorOperationButtonElement buttonSign;
    private RadCalculatorEqualsButtonElement buttonEquals;
    private RadCalculatorCommandButtonElement buttonC;
    private RadCalculatorCommandButtonElement buttonCE;
    private RadCalculatorDeleteButtonElement buttonDelete;
    private RadCalculatorMemoryButtonElement buttonMplus;
    private RadCalculatorMemoryButtonElement buttonMminus;
    private RadCalculatorMemoryButtonElement buttonMS;
    private RadCalculatorMemoryButtonElement buttonMR;
    private RadCalculatorMemoryButtonElement buttonMC;
    private RadCalculatorDigitButtonElement button0;
    private RadCalculatorDigitButtonElement button1;
    private RadCalculatorDigitButtonElement button2;
    private RadCalculatorDigitButtonElement button3;
    private RadCalculatorDigitButtonElement button4;
    private RadCalculatorDigitButtonElement button5;
    private RadCalculatorDigitButtonElement button6;
    private RadCalculatorDigitButtonElement button7;
    private RadCalculatorDigitButtonElement button8;
    private RadCalculatorDigitButtonElement button9;
    private RadCalculatorDigitButtonElement buttonPoint;

    public RadCalculatorContentElement(RadCalculatorDropDownElement editorElement)
    {
      this.calculatorElement = editorElement;
      int num = (int) this.SetDefaultValueOverride(RadElement.PaddingProperty, (object) new Padding(6));
    }

    protected override void CreateChildElements()
    {
      base.CreateChildElements();
      this.container = new GridLayout(5, 6);
      int num = (int) this.container.BindProperty(RadElement.PaddingProperty, (RadObject) this, RadElement.PaddingProperty, PropertyBindingOptions.TwoWay);
      this.container.StretchHorizontally = true;
      this.container.StretchVertically = true;
      this.Children.Add((RadElement) this.container);
      this.AddCalculationButtons();
      this.AddCommandButtons();
      this.AddMemoryButtons();
      this.AddDigitButtons();
    }

    protected override void InitializeFields()
    {
      base.InitializeFields();
      this.currentOperation = RadCalculatorContentElement.ArithmeticOperation.None;
      this.MemoryValue = new Decimal(0);
    }

    protected override void DisposeManagedResources()
    {
      this.buttonAdd.MouseUp -= new MouseEventHandler(this.buttonAdd_MouseUp);
      this.buttonAdd.KeyPress -= new KeyPressEventHandler(this.button_KeyPress);
      this.buttonAdd.KeyDown -= new KeyEventHandler(this.button_KeyDown);
      this.buttonSubstract.MouseUp -= new MouseEventHandler(this.buttonSubstract_MouseUp);
      this.buttonSubstract.KeyPress -= new KeyPressEventHandler(this.button_KeyPress);
      this.buttonSubstract.KeyDown -= new KeyEventHandler(this.button_KeyDown);
      this.buttonMultiply.MouseUp -= new MouseEventHandler(this.buttonMultiply_MouseUp);
      this.buttonMultiply.KeyPress -= new KeyPressEventHandler(this.button_KeyPress);
      this.buttonMultiply.KeyDown -= new KeyEventHandler(this.button_KeyDown);
      this.buttonDivide.MouseUp -= new MouseEventHandler(this.buttonDivide_MouseUp);
      this.buttonDivide.KeyPress -= new KeyPressEventHandler(this.button_KeyPress);
      this.buttonDivide.KeyDown -= new KeyEventHandler(this.button_KeyDown);
      this.buttonSqrt.MouseUp -= new MouseEventHandler(this.buttonSqrt_MouseUp);
      this.buttonSqrt.KeyPress -= new KeyPressEventHandler(this.button_KeyPress);
      this.buttonSqrt.KeyDown -= new KeyEventHandler(this.button_KeyDown);
      this.buttonPercent.MouseUp -= new MouseEventHandler(this.buttonPercent_MouseUp);
      this.buttonPercent.KeyPress -= new KeyPressEventHandler(this.button_KeyPress);
      this.buttonPercent.KeyDown -= new KeyEventHandler(this.button_KeyDown);
      this.buttonReciprocal.MouseUp -= new MouseEventHandler(this.buttonReciprocal_MouseUp);
      this.buttonReciprocal.KeyPress -= new KeyPressEventHandler(this.button_KeyPress);
      this.buttonReciprocal.KeyDown -= new KeyEventHandler(this.button_KeyDown);
      this.buttonSign.MouseUp -= new MouseEventHandler(this.buttonSign_MouseUp);
      this.buttonSign.KeyPress -= new KeyPressEventHandler(this.button_KeyPress);
      this.buttonSign.KeyDown -= new KeyEventHandler(this.button_KeyDown);
      this.buttonEquals.MouseUp -= new MouseEventHandler(this.buttonEquals_MouseUp);
      this.buttonEquals.KeyPress -= new KeyPressEventHandler(this.button_KeyPress);
      this.buttonEquals.KeyDown -= new KeyEventHandler(this.button_KeyDown);
      this.buttonC.MouseUp -= new MouseEventHandler(this.buttonC_MouseUp);
      this.buttonC.KeyPress -= new KeyPressEventHandler(this.button_KeyPress);
      this.buttonC.KeyDown -= new KeyEventHandler(this.button_KeyDown);
      this.buttonCE.MouseUp -= new MouseEventHandler(this.buttonCE_MouseUp);
      this.buttonCE.KeyPress -= new KeyPressEventHandler(this.button_KeyPress);
      this.buttonCE.KeyDown -= new KeyEventHandler(this.button_KeyDown);
      this.buttonDelete.MouseUp -= new MouseEventHandler(this.buttonDelete_MouseUp);
      this.buttonDelete.KeyPress -= new KeyPressEventHandler(this.button_KeyPress);
      this.buttonDelete.KeyDown -= new KeyEventHandler(this.button_KeyDown);
      this.buttonMplus.MouseUp -= new MouseEventHandler(this.buttonMplus_MouseUp);
      this.buttonMplus.KeyPress -= new KeyPressEventHandler(this.button_KeyPress);
      this.buttonMplus.KeyDown -= new KeyEventHandler(this.button_KeyDown);
      this.buttonMminus.MouseUp -= new MouseEventHandler(this.buttonMminus_MouseUp);
      this.buttonMminus.KeyPress -= new KeyPressEventHandler(this.button_KeyPress);
      this.buttonMminus.KeyDown -= new KeyEventHandler(this.button_KeyDown);
      this.buttonMS.MouseUp -= new MouseEventHandler(this.buttonMS_MouseUp);
      this.buttonMS.KeyPress -= new KeyPressEventHandler(this.button_KeyPress);
      this.buttonMS.KeyDown -= new KeyEventHandler(this.button_KeyDown);
      this.buttonMR.MouseUp -= new MouseEventHandler(this.buttonMR_MouseUp);
      this.buttonMR.KeyPress -= new KeyPressEventHandler(this.button_KeyPress);
      this.buttonMR.KeyDown -= new KeyEventHandler(this.button_KeyDown);
      this.buttonMC.MouseUp -= new MouseEventHandler(this.buttonMC_MouseUp);
      this.buttonMC.KeyPress -= new KeyPressEventHandler(this.button_KeyPress);
      this.buttonMC.KeyDown -= new KeyEventHandler(this.button_KeyDown);
      this.button0.MouseUp -= new MouseEventHandler(this.digitButton_MouseUp);
      this.button0.KeyPress -= new KeyPressEventHandler(this.button_KeyPress);
      this.button0.KeyDown -= new KeyEventHandler(this.button_KeyDown);
      this.button1.MouseUp -= new MouseEventHandler(this.digitButton_MouseUp);
      this.button1.KeyPress -= new KeyPressEventHandler(this.button_KeyPress);
      this.button1.KeyDown -= new KeyEventHandler(this.button_KeyDown);
      this.button2.MouseUp -= new MouseEventHandler(this.digitButton_MouseUp);
      this.button2.KeyPress -= new KeyPressEventHandler(this.button_KeyPress);
      this.button2.KeyDown -= new KeyEventHandler(this.button_KeyDown);
      this.button3.MouseUp -= new MouseEventHandler(this.digitButton_MouseUp);
      this.button3.KeyPress -= new KeyPressEventHandler(this.button_KeyPress);
      this.button3.KeyDown -= new KeyEventHandler(this.button_KeyDown);
      this.button4.MouseUp -= new MouseEventHandler(this.digitButton_MouseUp);
      this.button4.KeyPress -= new KeyPressEventHandler(this.button_KeyPress);
      this.button4.KeyDown -= new KeyEventHandler(this.button_KeyDown);
      this.button5.MouseUp -= new MouseEventHandler(this.digitButton_MouseUp);
      this.button5.KeyPress -= new KeyPressEventHandler(this.button_KeyPress);
      this.button5.KeyDown -= new KeyEventHandler(this.button_KeyDown);
      this.button6.MouseUp -= new MouseEventHandler(this.digitButton_MouseUp);
      this.button6.KeyPress -= new KeyPressEventHandler(this.button_KeyPress);
      this.button6.KeyDown -= new KeyEventHandler(this.button_KeyDown);
      this.button7.MouseUp -= new MouseEventHandler(this.digitButton_MouseUp);
      this.button7.KeyPress -= new KeyPressEventHandler(this.button_KeyPress);
      this.button7.KeyDown -= new KeyEventHandler(this.button_KeyDown);
      this.button8.MouseUp -= new MouseEventHandler(this.digitButton_MouseUp);
      this.button8.KeyPress -= new KeyPressEventHandler(this.button_KeyPress);
      this.button8.KeyDown -= new KeyEventHandler(this.button_KeyDown);
      this.button9.MouseUp -= new MouseEventHandler(this.digitButton_MouseUp);
      this.button9.KeyPress -= new KeyPressEventHandler(this.button_KeyPress);
      this.button9.KeyDown -= new KeyEventHandler(this.button_KeyDown);
      this.buttonPoint.MouseUp -= new MouseEventHandler(this.digitButton_MouseUp);
      this.buttonPoint.KeyPress -= new KeyPressEventHandler(this.button_KeyPress);
      this.buttonPoint.KeyDown -= new KeyEventHandler(this.button_KeyDown);
      base.DisposeManagedResources();
    }

    public RadCalculatorDropDownElement CalculatorElement
    {
      get
      {
        return this.calculatorElement;
      }
    }

    public Decimal MemoryValue
    {
      get
      {
        return this.memoryValue;
      }
      set
      {
        this.memoryValue = value;
        if (this.calculatorElement == null || this.calculatorElement.MemoryElement == null)
          return;
        if (value != new Decimal(0))
          this.calculatorElement.MemoryElement.Text = "M";
        else
          this.calculatorElement.MemoryElement.Text = string.Empty;
      }
    }

    public GridLayout GridLayout
    {
      get
      {
        return this.container;
      }
    }

    public RadCalculatorOperationButtonElement ButtonAdd
    {
      get
      {
        return this.buttonAdd;
      }
    }

    public RadCalculatorOperationButtonElement ButtonSubstract
    {
      get
      {
        return this.buttonSubstract;
      }
    }

    public RadCalculatorOperationButtonElement ButtonMultiply
    {
      get
      {
        return this.buttonMultiply;
      }
    }

    public RadCalculatorOperationButtonElement ButtonDivide
    {
      get
      {
        return this.buttonDivide;
      }
    }

    public RadCalculatorOperationButtonElement ButtonSqrt
    {
      get
      {
        return this.buttonSqrt;
      }
    }

    public RadCalculatorOperationButtonElement ButtonPercent
    {
      get
      {
        return this.buttonPercent;
      }
    }

    public RadCalculatorOperationButtonElement ButtonReciprocal
    {
      get
      {
        return this.buttonReciprocal;
      }
    }

    public RadCalculatorOperationButtonElement ButtonSign
    {
      get
      {
        return this.buttonSign;
      }
    }

    public RadCalculatorEqualsButtonElement ButtonEquals
    {
      get
      {
        return this.buttonEquals;
      }
    }

    public RadCalculatorCommandButtonElement ButtonC
    {
      get
      {
        return this.buttonC;
      }
    }

    public RadCalculatorCommandButtonElement ButtonCE
    {
      get
      {
        return this.buttonCE;
      }
    }

    public RadCalculatorDeleteButtonElement ButtonDelete
    {
      get
      {
        return this.buttonDelete;
      }
    }

    public RadCalculatorMemoryButtonElement ButtonMplus
    {
      get
      {
        return this.buttonMplus;
      }
    }

    public RadCalculatorMemoryButtonElement ButtonMminus
    {
      get
      {
        return this.buttonMminus;
      }
    }

    public RadCalculatorMemoryButtonElement ButtonMS
    {
      get
      {
        return this.buttonMS;
      }
    }

    public RadCalculatorMemoryButtonElement ButtonMR
    {
      get
      {
        return this.buttonMR;
      }
    }

    public RadCalculatorMemoryButtonElement ButtonMC
    {
      get
      {
        return this.buttonMC;
      }
    }

    public RadCalculatorDigitButtonElement Button0
    {
      get
      {
        return this.button0;
      }
    }

    public RadCalculatorDigitButtonElement Button1
    {
      get
      {
        return this.button1;
      }
    }

    public RadCalculatorDigitButtonElement Button2
    {
      get
      {
        return this.button2;
      }
    }

    public RadCalculatorDigitButtonElement Button3
    {
      get
      {
        return this.button3;
      }
    }

    public RadCalculatorDigitButtonElement Button4
    {
      get
      {
        return this.button4;
      }
    }

    public RadCalculatorDigitButtonElement Button5
    {
      get
      {
        return this.button5;
      }
    }

    public RadCalculatorDigitButtonElement Button6
    {
      get
      {
        return this.button6;
      }
    }

    public RadCalculatorDigitButtonElement Button7
    {
      get
      {
        return this.button7;
      }
    }

    public RadCalculatorDigitButtonElement Button8
    {
      get
      {
        return this.button8;
      }
    }

    public RadCalculatorDigitButtonElement Button9
    {
      get
      {
        return this.button9;
      }
    }

    public RadCalculatorDigitButtonElement ButtonPoint
    {
      get
      {
        return this.buttonPoint;
      }
    }

    protected override void OnKeyPress(KeyPressEventArgs e)
    {
      base.OnKeyPress(e);
      this.ProcessKeyPress(e);
    }

    protected override void OnKeyDown(KeyEventArgs e)
    {
      base.OnKeyDown(e);
      this.ProcessKeyDown(e);
    }

    protected override SizeF MeasureOverride(SizeF availableSize)
    {
      if ((double) availableSize.Width < 1.0 || (double) availableSize.Height < 1.0)
        return availableSize;
      SizeF availableSize1 = availableSize;
      if ((double) availableSize1.Width < (double) this.calculatorElement.MinPopupWidth)
        availableSize1.Width = (float) this.calculatorElement.MinPopupWidth;
      if ((double) availableSize1.Height < (double) this.calculatorElement.MinPopupHeight)
        availableSize1.Height = (float) this.calculatorElement.MinPopupHeight;
      this.container.Measure(availableSize1);
      return new SizeF(availableSize1.Width, availableSize1.Height);
    }

    protected override SizeF ArrangeOverride(SizeF finalSize)
    {
      if ((double) finalSize.Width < 1.0 || (double) finalSize.Height < 1.0)
        return finalSize;
      this.container.Arrange(new RectangleF(0.0f, 0.0f, finalSize.Width, finalSize.Height));
      return finalSize;
    }

    private void AddCalculationButtons()
    {
      this.buttonAdd = new RadCalculatorOperationButtonElement("+");
      this.buttonAdd.KeyPress += new KeyPressEventHandler(this.button_KeyPress);
      this.buttonAdd.KeyDown += new KeyEventHandler(this.button_KeyDown);
      int num1 = (int) this.buttonAdd.SetValue(GridLayout.ColumnIndexProperty, (object) 3);
      int num2 = (int) this.buttonAdd.SetValue(GridLayout.RowIndexProperty, (object) 5);
      int num3 = (int) this.buttonAdd.SetValue(GridLayout.CellPaddingProperty, (object) new Padding(3));
      this.buttonAdd.MouseUp += new MouseEventHandler(this.buttonAdd_MouseUp);
      this.buttonSubstract = new RadCalculatorOperationButtonElement("-");
      this.buttonSubstract.KeyPress += new KeyPressEventHandler(this.button_KeyPress);
      this.buttonSubstract.KeyDown += new KeyEventHandler(this.button_KeyDown);
      int num4 = (int) this.buttonSubstract.SetValue(GridLayout.ColumnIndexProperty, (object) 3);
      int num5 = (int) this.buttonSubstract.SetValue(GridLayout.RowIndexProperty, (object) 4);
      int num6 = (int) this.buttonSubstract.SetValue(GridLayout.CellPaddingProperty, (object) new Padding(3));
      this.buttonSubstract.MouseUp += new MouseEventHandler(this.buttonSubstract_MouseUp);
      this.buttonMultiply = new RadCalculatorOperationButtonElement("*");
      this.buttonMultiply.KeyPress += new KeyPressEventHandler(this.button_KeyPress);
      this.buttonMultiply.KeyDown += new KeyEventHandler(this.button_KeyDown);
      int num7 = (int) this.buttonMultiply.SetValue(GridLayout.ColumnIndexProperty, (object) 3);
      int num8 = (int) this.buttonMultiply.SetValue(GridLayout.RowIndexProperty, (object) 3);
      int num9 = (int) this.buttonMultiply.SetValue(GridLayout.CellPaddingProperty, (object) new Padding(3));
      this.buttonMultiply.MouseUp += new MouseEventHandler(this.buttonMultiply_MouseUp);
      this.buttonDivide = new RadCalculatorOperationButtonElement("/");
      this.buttonDivide.KeyPress += new KeyPressEventHandler(this.button_KeyPress);
      this.buttonDivide.KeyDown += new KeyEventHandler(this.button_KeyDown);
      int num10 = (int) this.buttonDivide.SetValue(GridLayout.ColumnIndexProperty, (object) 3);
      int num11 = (int) this.buttonDivide.SetValue(GridLayout.RowIndexProperty, (object) 2);
      int num12 = (int) this.buttonDivide.SetValue(GridLayout.CellPaddingProperty, (object) new Padding(3));
      this.buttonDivide.MouseUp += new MouseEventHandler(this.buttonDivide_MouseUp);
      this.buttonSqrt = new RadCalculatorOperationButtonElement("√");
      this.buttonSqrt.KeyPress += new KeyPressEventHandler(this.button_KeyPress);
      this.buttonSqrt.KeyDown += new KeyEventHandler(this.button_KeyDown);
      int num13 = (int) this.buttonSqrt.SetValue(GridLayout.ColumnIndexProperty, (object) 4);
      int num14 = (int) this.buttonSqrt.SetValue(GridLayout.RowIndexProperty, (object) 1);
      int num15 = (int) this.buttonSqrt.SetValue(GridLayout.CellPaddingProperty, (object) new Padding(3));
      this.buttonSqrt.MouseUp += new MouseEventHandler(this.buttonSqrt_MouseUp);
      this.buttonPercent = new RadCalculatorOperationButtonElement("%");
      this.buttonPercent.KeyPress += new KeyPressEventHandler(this.button_KeyPress);
      this.buttonPercent.KeyDown += new KeyEventHandler(this.button_KeyDown);
      int num16 = (int) this.buttonPercent.SetValue(GridLayout.ColumnIndexProperty, (object) 4);
      int num17 = (int) this.buttonPercent.SetValue(GridLayout.RowIndexProperty, (object) 2);
      int num18 = (int) this.buttonPercent.SetValue(GridLayout.CellPaddingProperty, (object) new Padding(3));
      this.buttonPercent.MouseUp += new MouseEventHandler(this.buttonPercent_MouseUp);
      this.buttonReciprocal = new RadCalculatorOperationButtonElement("1/x");
      this.buttonReciprocal.KeyPress += new KeyPressEventHandler(this.button_KeyPress);
      this.buttonReciprocal.KeyDown += new KeyEventHandler(this.button_KeyDown);
      int num19 = (int) this.buttonReciprocal.SetValue(GridLayout.ColumnIndexProperty, (object) 4);
      int num20 = (int) this.buttonReciprocal.SetValue(GridLayout.RowIndexProperty, (object) 3);
      int num21 = (int) this.buttonReciprocal.SetValue(GridLayout.CellPaddingProperty, (object) new Padding(3));
      this.buttonReciprocal.MouseUp += new MouseEventHandler(this.buttonReciprocal_MouseUp);
      this.buttonSign = new RadCalculatorOperationButtonElement("±");
      this.buttonSign.KeyPress += new KeyPressEventHandler(this.button_KeyPress);
      this.buttonSign.KeyDown += new KeyEventHandler(this.button_KeyDown);
      int num22 = (int) this.buttonSign.SetValue(GridLayout.ColumnIndexProperty, (object) 3);
      int num23 = (int) this.buttonSign.SetValue(GridLayout.RowIndexProperty, (object) 1);
      int num24 = (int) this.buttonSign.SetValue(GridLayout.CellPaddingProperty, (object) new Padding(3));
      this.buttonSign.MouseUp += new MouseEventHandler(this.buttonSign_MouseUp);
      this.buttonEquals = new RadCalculatorEqualsButtonElement("=");
      this.buttonEquals.KeyPress += new KeyPressEventHandler(this.button_KeyPress);
      this.buttonEquals.KeyDown += new KeyEventHandler(this.button_KeyDown);
      int num25 = (int) this.buttonEquals.SetValue(GridLayout.ColumnIndexProperty, (object) 4);
      int num26 = (int) this.buttonEquals.SetValue(GridLayout.RowIndexProperty, (object) 4);
      int num27 = (int) this.buttonEquals.SetValue(GridLayout.RowSpanProperty, (object) 2);
      int num28 = (int) this.buttonEquals.SetValue(GridLayout.CellPaddingProperty, (object) new Padding(3));
      this.buttonEquals.MouseUp += new MouseEventHandler(this.buttonEquals_MouseUp);
      this.container.Children.Add((RadElement) this.buttonAdd);
      this.container.Children.Add((RadElement) this.buttonSubstract);
      this.container.Children.Add((RadElement) this.buttonMultiply);
      this.container.Children.Add((RadElement) this.buttonDivide);
      this.container.Children.Add((RadElement) this.buttonSqrt);
      this.container.Children.Add((RadElement) this.buttonPercent);
      this.container.Children.Add((RadElement) this.buttonReciprocal);
      this.container.Children.Add((RadElement) this.buttonSign);
      this.container.Children.Add((RadElement) this.buttonEquals);
    }

    private void AddCommandButtons()
    {
      this.buttonC = new RadCalculatorCommandButtonElement("C");
      this.buttonC.KeyPress += new KeyPressEventHandler(this.button_KeyPress);
      this.buttonC.KeyDown += new KeyEventHandler(this.button_KeyDown);
      int num1 = (int) this.buttonC.SetValue(GridLayout.ColumnIndexProperty, (object) 2);
      int num2 = (int) this.buttonC.SetValue(GridLayout.RowIndexProperty, (object) 1);
      int num3 = (int) this.buttonC.SetValue(GridLayout.CellPaddingProperty, (object) new Padding(3));
      this.buttonC.MouseUp += new MouseEventHandler(this.buttonC_MouseUp);
      this.buttonCE = new RadCalculatorCommandButtonElement("CE");
      this.buttonCE.KeyPress += new KeyPressEventHandler(this.button_KeyPress);
      this.buttonCE.KeyDown += new KeyEventHandler(this.button_KeyDown);
      int num4 = (int) this.buttonCE.SetValue(GridLayout.ColumnIndexProperty, (object) 1);
      int num5 = (int) this.buttonCE.SetValue(GridLayout.RowIndexProperty, (object) 1);
      int num6 = (int) this.buttonCE.SetValue(GridLayout.CellPaddingProperty, (object) new Padding(3));
      this.buttonCE.MouseUp += new MouseEventHandler(this.buttonCE_MouseUp);
      this.buttonDelete = new RadCalculatorDeleteButtonElement("");
      this.buttonDelete.KeyPress += new KeyPressEventHandler(this.button_KeyPress);
      this.buttonDelete.KeyDown += new KeyEventHandler(this.button_KeyDown);
      int num7 = (int) this.buttonDelete.SetValue(GridLayout.ColumnIndexProperty, (object) 0);
      int num8 = (int) this.buttonDelete.SetValue(GridLayout.RowIndexProperty, (object) 1);
      int num9 = (int) this.buttonDelete.SetValue(GridLayout.CellPaddingProperty, (object) new Padding(3));
      this.buttonDelete.MouseUp += new MouseEventHandler(this.buttonDelete_MouseUp);
      this.container.Children.Add((RadElement) this.buttonC);
      this.container.Children.Add((RadElement) this.buttonCE);
      this.container.Children.Add((RadElement) this.buttonDelete);
    }

    private void AddMemoryButtons()
    {
      this.buttonMplus = new RadCalculatorMemoryButtonElement("M+");
      this.buttonMplus.KeyPress += new KeyPressEventHandler(this.button_KeyPress);
      this.buttonMplus.KeyDown += new KeyEventHandler(this.button_KeyDown);
      int num1 = (int) this.buttonMplus.SetValue(GridLayout.ColumnIndexProperty, (object) 3);
      int num2 = (int) this.buttonMplus.SetValue(GridLayout.RowIndexProperty, (object) 0);
      int num3 = (int) this.buttonMplus.SetValue(GridLayout.CellPaddingProperty, (object) new Padding(3, 3, 3, 8));
      this.buttonMplus.MouseUp += new MouseEventHandler(this.buttonMplus_MouseUp);
      this.buttonMminus = new RadCalculatorMemoryButtonElement("M-");
      this.buttonMminus.KeyPress += new KeyPressEventHandler(this.button_KeyPress);
      this.buttonMminus.KeyDown += new KeyEventHandler(this.button_KeyDown);
      int num4 = (int) this.buttonMminus.SetValue(GridLayout.ColumnIndexProperty, (object) 4);
      int num5 = (int) this.buttonMminus.SetValue(GridLayout.RowIndexProperty, (object) 0);
      int num6 = (int) this.buttonMminus.SetValue(GridLayout.CellPaddingProperty, (object) new Padding(3, 3, 3, 8));
      this.buttonMminus.MouseUp += new MouseEventHandler(this.buttonMminus_MouseUp);
      this.buttonMS = new RadCalculatorMemoryButtonElement("MS");
      this.buttonMS.KeyPress += new KeyPressEventHandler(this.button_KeyPress);
      this.buttonMS.KeyDown += new KeyEventHandler(this.button_KeyDown);
      int num7 = (int) this.buttonMS.SetValue(GridLayout.ColumnIndexProperty, (object) 2);
      int num8 = (int) this.buttonMS.SetValue(GridLayout.RowIndexProperty, (object) 0);
      int num9 = (int) this.buttonMS.SetValue(GridLayout.CellPaddingProperty, (object) new Padding(3, 3, 3, 8));
      this.buttonMS.MouseUp += new MouseEventHandler(this.buttonMS_MouseUp);
      this.buttonMR = new RadCalculatorMemoryButtonElement("MR");
      this.buttonMR.KeyPress += new KeyPressEventHandler(this.button_KeyPress);
      this.buttonMR.KeyDown += new KeyEventHandler(this.button_KeyDown);
      int num10 = (int) this.buttonMR.SetValue(GridLayout.ColumnIndexProperty, (object) 1);
      int num11 = (int) this.buttonMR.SetValue(GridLayout.RowIndexProperty, (object) 0);
      int num12 = (int) this.buttonMR.SetValue(GridLayout.CellPaddingProperty, (object) new Padding(3, 3, 3, 8));
      this.buttonMR.MouseUp += new MouseEventHandler(this.buttonMR_MouseUp);
      this.buttonMC = new RadCalculatorMemoryButtonElement("MC");
      this.buttonMC.KeyPress += new KeyPressEventHandler(this.button_KeyPress);
      this.buttonMC.KeyDown += new KeyEventHandler(this.button_KeyDown);
      int num13 = (int) this.buttonMC.SetValue(GridLayout.ColumnIndexProperty, (object) 0);
      int num14 = (int) this.buttonMC.SetValue(GridLayout.RowIndexProperty, (object) 0);
      int num15 = (int) this.buttonMC.SetValue(GridLayout.CellPaddingProperty, (object) new Padding(3, 3, 3, 8));
      this.buttonMC.MouseUp += new MouseEventHandler(this.buttonMC_MouseUp);
      this.container.Children.Add((RadElement) this.buttonMplus);
      this.container.Children.Add((RadElement) this.buttonMminus);
      this.container.Children.Add((RadElement) this.buttonMS);
      this.container.Children.Add((RadElement) this.buttonMR);
      this.container.Children.Add((RadElement) this.buttonMC);
    }

    private void AddDigitButtons()
    {
      this.button0 = new RadCalculatorDigitButtonElement("0");
      this.button0.KeyPress += new KeyPressEventHandler(this.button_KeyPress);
      this.button0.KeyDown += new KeyEventHandler(this.button_KeyDown);
      this.button0.Tag = (object) "0";
      int num1 = (int) this.button0.SetValue(GridLayout.ColumnIndexProperty, (object) 0);
      int num2 = (int) this.button0.SetValue(GridLayout.RowIndexProperty, (object) 5);
      int num3 = (int) this.button0.SetValue(GridLayout.ColSpanProperty, (object) 2);
      int num4 = (int) this.button0.SetValue(GridLayout.CellPaddingProperty, (object) new Padding(3));
      this.button0.MouseUp += new MouseEventHandler(this.digitButton_MouseUp);
      this.button1 = new RadCalculatorDigitButtonElement("1");
      this.button1.KeyPress += new KeyPressEventHandler(this.button_KeyPress);
      this.button1.KeyDown += new KeyEventHandler(this.button_KeyDown);
      this.button1.Tag = (object) "1";
      int num5 = (int) this.button1.SetValue(GridLayout.ColumnIndexProperty, (object) 0);
      int num6 = (int) this.button1.SetValue(GridLayout.RowIndexProperty, (object) 4);
      int num7 = (int) this.button1.SetValue(GridLayout.CellPaddingProperty, (object) new Padding(3));
      this.button1.MouseUp += new MouseEventHandler(this.digitButton_MouseUp);
      this.button2 = new RadCalculatorDigitButtonElement("2");
      this.button2.KeyPress += new KeyPressEventHandler(this.button_KeyPress);
      this.button2.KeyDown += new KeyEventHandler(this.button_KeyDown);
      this.button2.Tag = (object) "2";
      int num8 = (int) this.button2.SetValue(GridLayout.ColumnIndexProperty, (object) 1);
      int num9 = (int) this.button2.SetValue(GridLayout.RowIndexProperty, (object) 4);
      int num10 = (int) this.button2.SetValue(GridLayout.CellPaddingProperty, (object) new Padding(3));
      this.button2.MouseUp += new MouseEventHandler(this.digitButton_MouseUp);
      this.button3 = new RadCalculatorDigitButtonElement("3");
      this.button3.KeyPress += new KeyPressEventHandler(this.button_KeyPress);
      this.button3.KeyDown += new KeyEventHandler(this.button_KeyDown);
      this.button3.Tag = (object) "3";
      int num11 = (int) this.button3.SetValue(GridLayout.ColumnIndexProperty, (object) 2);
      int num12 = (int) this.button3.SetValue(GridLayout.RowIndexProperty, (object) 4);
      int num13 = (int) this.button3.SetValue(GridLayout.CellPaddingProperty, (object) new Padding(3));
      this.button3.MouseUp += new MouseEventHandler(this.digitButton_MouseUp);
      this.button4 = new RadCalculatorDigitButtonElement("4");
      this.button4.KeyPress += new KeyPressEventHandler(this.button_KeyPress);
      this.button4.KeyDown += new KeyEventHandler(this.button_KeyDown);
      this.button4.Tag = (object) "4";
      int num14 = (int) this.button4.SetValue(GridLayout.ColumnIndexProperty, (object) 0);
      int num15 = (int) this.button4.SetValue(GridLayout.RowIndexProperty, (object) 3);
      int num16 = (int) this.button4.SetValue(GridLayout.CellPaddingProperty, (object) new Padding(3));
      this.button4.MouseUp += new MouseEventHandler(this.digitButton_MouseUp);
      this.button5 = new RadCalculatorDigitButtonElement("5");
      this.button5.KeyPress += new KeyPressEventHandler(this.button_KeyPress);
      this.button5.KeyDown += new KeyEventHandler(this.button_KeyDown);
      this.button5.Tag = (object) "5";
      int num17 = (int) this.button5.SetValue(GridLayout.ColumnIndexProperty, (object) 1);
      int num18 = (int) this.button5.SetValue(GridLayout.RowIndexProperty, (object) 3);
      int num19 = (int) this.button5.SetValue(GridLayout.CellPaddingProperty, (object) new Padding(3));
      this.button5.MouseUp += new MouseEventHandler(this.digitButton_MouseUp);
      this.button6 = new RadCalculatorDigitButtonElement("6");
      this.button6.KeyPress += new KeyPressEventHandler(this.button_KeyPress);
      this.button6.KeyDown += new KeyEventHandler(this.button_KeyDown);
      this.button6.Tag = (object) "6";
      int num20 = (int) this.button6.SetValue(GridLayout.ColumnIndexProperty, (object) 2);
      int num21 = (int) this.button6.SetValue(GridLayout.RowIndexProperty, (object) 3);
      int num22 = (int) this.button6.SetValue(GridLayout.CellPaddingProperty, (object) new Padding(3));
      this.button6.MouseUp += new MouseEventHandler(this.digitButton_MouseUp);
      this.button7 = new RadCalculatorDigitButtonElement("7");
      this.button7.KeyPress += new KeyPressEventHandler(this.button_KeyPress);
      this.button7.KeyDown += new KeyEventHandler(this.button_KeyDown);
      this.button7.Tag = (object) "7";
      int num23 = (int) this.button7.SetValue(GridLayout.ColumnIndexProperty, (object) 0);
      int num24 = (int) this.button7.SetValue(GridLayout.RowIndexProperty, (object) 2);
      int num25 = (int) this.button7.SetValue(GridLayout.CellPaddingProperty, (object) new Padding(3));
      this.button7.MouseUp += new MouseEventHandler(this.digitButton_MouseUp);
      this.button8 = new RadCalculatorDigitButtonElement("8");
      this.button8.KeyPress += new KeyPressEventHandler(this.button_KeyPress);
      this.button8.KeyDown += new KeyEventHandler(this.button_KeyDown);
      this.button8.Tag = (object) "8";
      int num26 = (int) this.button8.SetValue(GridLayout.ColumnIndexProperty, (object) 1);
      int num27 = (int) this.button8.SetValue(GridLayout.RowIndexProperty, (object) 2);
      int num28 = (int) this.button8.SetValue(GridLayout.CellPaddingProperty, (object) new Padding(3));
      this.button8.MouseUp += new MouseEventHandler(this.digitButton_MouseUp);
      this.button9 = new RadCalculatorDigitButtonElement("9");
      this.button9.KeyPress += new KeyPressEventHandler(this.button_KeyPress);
      this.button9.KeyDown += new KeyEventHandler(this.button_KeyDown);
      this.button9.Tag = (object) "9";
      int num29 = (int) this.button9.SetValue(GridLayout.ColumnIndexProperty, (object) 2);
      int num30 = (int) this.button9.SetValue(GridLayout.RowIndexProperty, (object) 2);
      int num31 = (int) this.button9.SetValue(GridLayout.CellPaddingProperty, (object) new Padding(3));
      this.button9.MouseUp += new MouseEventHandler(this.digitButton_MouseUp);
      this.buttonPoint = new RadCalculatorDigitButtonElement(CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator);
      this.buttonPoint.KeyPress += new KeyPressEventHandler(this.button_KeyPress);
      this.buttonPoint.KeyDown += new KeyEventHandler(this.button_KeyDown);
      this.buttonPoint.Tag = (object) CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator;
      int num32 = (int) this.buttonPoint.SetValue(GridLayout.ColumnIndexProperty, (object) 2);
      int num33 = (int) this.buttonPoint.SetValue(GridLayout.RowIndexProperty, (object) 5);
      int num34 = (int) this.buttonPoint.SetValue(GridLayout.CellPaddingProperty, (object) new Padding(3));
      this.buttonPoint.MouseUp += new MouseEventHandler(this.digitButton_MouseUp);
      this.container.Children.Add((RadElement) this.button0);
      this.container.Children.Add((RadElement) this.button1);
      this.container.Children.Add((RadElement) this.button2);
      this.container.Children.Add((RadElement) this.button3);
      this.container.Children.Add((RadElement) this.button4);
      this.container.Children.Add((RadElement) this.button5);
      this.container.Children.Add((RadElement) this.button6);
      this.container.Children.Add((RadElement) this.button7);
      this.container.Children.Add((RadElement) this.button8);
      this.container.Children.Add((RadElement) this.button9);
      this.container.Children.Add((RadElement) this.buttonPoint);
    }

    internal void ProcessKeyPress(KeyPressEventArgs e)
    {
      switch (e.KeyChar)
      {
        case '%':
          this.Percent();
          break;
        case '*':
          this.Multiply();
          break;
        case '+':
          this.Add();
          break;
        case '-':
          this.Substract();
          break;
        case '/':
          this.Divide();
          break;
        case '0':
          this.Digit("0");
          break;
        case '1':
          this.Digit("1");
          break;
        case '2':
          this.Digit("2");
          break;
        case '3':
          this.Digit("3");
          break;
        case '4':
          this.Digit("4");
          break;
        case '5':
          this.Digit("5");
          break;
        case '6':
          this.Digit("6");
          break;
        case '7':
          this.Digit("7");
          break;
        case '8':
          this.Digit("8");
          break;
        case '9':
          this.Digit("9");
          break;
        case '=':
          this.Equals();
          break;
        default:
          if (!(e.KeyChar.ToString() == CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator))
            break;
          this.Digit(CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator);
          break;
      }
    }

    internal void ProcessKeyDown(KeyEventArgs e)
    {
      switch (e.KeyValue)
      {
        case 8:
          this.Delete();
          break;
        case 13:
          this.Equals();
          break;
        default:
          switch (e.KeyData)
          {
            case Keys.R:
              this.Reciprocal();
              return;
            case Keys.F9:
              this.Sign();
              return;
            case Keys.L | Keys.Control:
              this.MC();
              return;
            case Keys.M | Keys.Control:
              this.MS();
              return;
            case Keys.P | Keys.Control:
              this.Mplus();
              return;
            case Keys.Q | Keys.Control:
              this.Mminus();
              return;
            case Keys.R | Keys.Control:
              this.MR();
              return;
            default:
              if (!e.Shift || e.KeyValue != 50)
                return;
              this.Sqrt();
              return;
          }
      }
    }

    internal void ProcessOperation(bool isPopupClosing)
    {
      if (!isPopupClosing || this.equalsPressed)
        return;
      this.ProcessOperation();
    }

    private void ProcessOperation()
    {
      this.calculatorElement.InputtingDigits = false;
      if (this.calculatorElement.Value == null)
        return;
      Decimal result;
      if (!Decimal.TryParse(this.calculatorElement.Value.ToString(), out result))
        return;
      try
      {
        if (this.equalsPressed || !this.value.HasValue || this.initialNumber)
        {
          this.value = new Decimal?(result);
        }
        else
        {
          switch (this.currentOperation)
          {
            case RadCalculatorContentElement.ArithmeticOperation.Add:
              RadCalculatorContentElement calculatorContentElement1 = this;
              Decimal? nullable1 = calculatorContentElement1.value;
              Decimal num1 = result;
              calculatorContentElement1.value = nullable1.HasValue ? new Decimal?(nullable1.GetValueOrDefault() + num1) : new Decimal?();
              break;
            case RadCalculatorContentElement.ArithmeticOperation.Substract:
              RadCalculatorContentElement calculatorContentElement2 = this;
              Decimal? nullable2 = calculatorContentElement2.value;
              Decimal num2 = result;
              calculatorContentElement2.value = nullable2.HasValue ? new Decimal?(nullable2.GetValueOrDefault() - num2) : new Decimal?();
              break;
            case RadCalculatorContentElement.ArithmeticOperation.Multiply:
              RadCalculatorContentElement calculatorContentElement3 = this;
              Decimal? nullable3 = calculatorContentElement3.value;
              Decimal num3 = result;
              calculatorContentElement3.value = nullable3.HasValue ? new Decimal?(nullable3.GetValueOrDefault() * num3) : new Decimal?();
              break;
            case RadCalculatorContentElement.ArithmeticOperation.Divide:
              RadCalculatorContentElement calculatorContentElement4 = this;
              Decimal? nullable4 = calculatorContentElement4.value;
              Decimal num4 = result;
              calculatorContentElement4.value = nullable4.HasValue ? new Decimal?(nullable4.GetValueOrDefault() / num4) : new Decimal?();
              break;
            default:
              this.value = new Decimal?(result);
              break;
          }
        }
        this.calculatorElement.Value = (object) this.value;
      }
      catch (ArithmeticException ex)
      {
        this.calculatorElement.Value = (object) 0;
        RadMessageBox.SetThemeName(this.ElementTree.ThemeName);
        int num = (int) RadMessageBox.Show("Invalid operation.", "Arithmetic error.");
      }
      finally
      {
        this.value = new Decimal?();
        this.initialNumber = false;
        this.equalsPressed = false;
      }
    }

    public void Reset()
    {
      this.value = new Decimal?();
      this.currentOperation = RadCalculatorContentElement.ArithmeticOperation.None;
      this.initialNumber = true;
      this.equalsPressed = false;
      this.calculatorElement.InputtingDigits = false;
    }

    private void Add()
    {
      if (this.currentOperation == RadCalculatorContentElement.ArithmeticOperation.None)
        this.currentOperation = RadCalculatorContentElement.ArithmeticOperation.Add;
      this.ProcessOperation();
      this.currentOperation = RadCalculatorContentElement.ArithmeticOperation.Add;
    }

    private void Substract()
    {
      if (this.currentOperation == RadCalculatorContentElement.ArithmeticOperation.None)
        this.currentOperation = RadCalculatorContentElement.ArithmeticOperation.Substract;
      this.ProcessOperation();
      this.currentOperation = RadCalculatorContentElement.ArithmeticOperation.Substract;
    }

    private void Multiply()
    {
      if (this.currentOperation == RadCalculatorContentElement.ArithmeticOperation.None)
        this.currentOperation = RadCalculatorContentElement.ArithmeticOperation.Multiply;
      this.ProcessOperation();
      this.currentOperation = RadCalculatorContentElement.ArithmeticOperation.Multiply;
    }

    private void Divide()
    {
      if (this.currentOperation == RadCalculatorContentElement.ArithmeticOperation.None)
        this.currentOperation = RadCalculatorContentElement.ArithmeticOperation.Divide;
      this.ProcessOperation();
      this.currentOperation = RadCalculatorContentElement.ArithmeticOperation.Divide;
    }

    private void Sqrt()
    {
      this.calculatorElement.InputtingDigits = false;
      if (this.calculatorElement.Value == null)
        return;
      double result;
      if (!double.TryParse(this.calculatorElement.Value.ToString(), out result))
        return;
      try
      {
        double d = Math.Sqrt(result);
        if (double.IsNaN(d) || double.IsPositiveInfinity(d) || double.IsNegativeInfinity(d))
        {
          this.calculatorElement.Value = (object) 0;
          RadMessageBox.SetThemeName(this.ElementTree.ThemeName);
          int num = (int) RadMessageBox.Show("Invalid operation.", "Arithmetic error.");
          return;
        }
        this.calculatorElement.Value = (object) d;
      }
      catch (ArithmeticException ex)
      {
        this.calculatorElement.Value = (object) 0;
        RadMessageBox.SetThemeName(this.ElementTree.ThemeName);
        int num = (int) RadMessageBox.Show("Invalid operation.", "Arithmetic error.");
      }
      this.operationFinished = true;
    }

    private void Percent()
    {
      this.calculatorElement.InputtingDigits = false;
      if (this.calculatorElement.Value == null)
        return;
      Decimal result;
      if (!Decimal.TryParse(this.calculatorElement.Value.ToString(), out result))
        return;
      try
      {
        RadCalculatorDropDownElement calculatorElement = this.calculatorElement;
        Decimal? nullable1 = this.value;
        Decimal num = result;
        Decimal? nullable2 = nullable1.HasValue ? new Decimal?(nullable1.GetValueOrDefault() * num) : new Decimal?();
        // ISSUE: variable of a boxed type
        __Boxed<Decimal?> local = (ValueType) (nullable2.HasValue ? new Decimal?(nullable2.GetValueOrDefault() / new Decimal(100)) : new Decimal?());
        calculatorElement.Value = (object) local;
      }
      catch (ArithmeticException ex)
      {
        this.calculatorElement.Value = (object) 0;
        RadMessageBox.SetThemeName(this.ElementTree.ThemeName);
        int num = (int) RadMessageBox.Show("Invalid operation.", "Arithmetic error.");
      }
      this.operationFinished = true;
    }

    private void Reciprocal()
    {
      this.calculatorElement.InputtingDigits = false;
      if (this.calculatorElement.Value == null)
        return;
      Decimal result;
      if (!Decimal.TryParse(this.calculatorElement.Value.ToString(), out result))
        return;
      try
      {
        this.calculatorElement.Value = (object) (new Decimal(1) / result);
      }
      catch (ArithmeticException ex)
      {
        this.calculatorElement.Value = (object) 0;
        RadMessageBox.SetThemeName(this.ElementTree.ThemeName);
        int num = (int) RadMessageBox.Show("Invalid operation.", "Arithmetic error.");
      }
      this.operationFinished = true;
    }

    private void Sign()
    {
      this.calculatorElement.InputtingDigits = false;
      Decimal result;
      if (this.calculatorElement.Value == null || !Decimal.TryParse(this.calculatorElement.Value.ToString(), out result))
        return;
      this.calculatorElement.Value = (object) -result;
    }

    private void Equals()
    {
      this.calculatorElement.InputtingDigits = false;
      Decimal result;
      if (this.calculatorElement.Value == null || !Decimal.TryParse(this.calculatorElement.Value.ToString(), out result))
        return;
      if (!this.equalsPressed)
      {
        this.input = result;
        if (this.value.HasValue)
          result = this.value.Value;
      }
      try
      {
        switch (this.currentOperation)
        {
          case RadCalculatorContentElement.ArithmeticOperation.Add:
            result += this.input;
            break;
          case RadCalculatorContentElement.ArithmeticOperation.Substract:
            result -= this.input;
            break;
          case RadCalculatorContentElement.ArithmeticOperation.Multiply:
            result *= this.input;
            break;
          case RadCalculatorContentElement.ArithmeticOperation.Divide:
            result /= this.input;
            break;
          default:
            return;
        }
        this.calculatorElement.Value = (object) result;
      }
      catch (ArithmeticException ex)
      {
        this.calculatorElement.Value = (object) 0;
        this.currentOperation = RadCalculatorContentElement.ArithmeticOperation.None;
        RadMessageBox.SetThemeName(this.ElementTree.ThemeName);
        int num = (int) RadMessageBox.Show("Invalid operation.", "Arithmetic error.");
      }
      this.equalsPressed = true;
    }

    private void C()
    {
      this.calculatorElement.Value = (object) 0;
      this.currentOperation = RadCalculatorContentElement.ArithmeticOperation.None;
      this.value = new Decimal?();
      this.initialNumber = true;
    }

    private void CE()
    {
      Decimal result;
      if (!this.value.HasValue && Decimal.TryParse(this.calculatorElement.Value.ToString(), out result))
        this.value = new Decimal?(result);
      this.calculatorElement.Value = (object) 0;
    }

    private void Delete()
    {
      if (this.calculatorElement.Value == null || this.calculatorElement.Value.ToString() == "0")
        return;
      string str = this.calculatorElement.Value.ToString();
      if (str.Length < 2)
        this.calculatorElement.Value = (object) "0";
      else
        this.calculatorElement.Value = (object) str.Substring(0, str.Length - 1);
    }

    private void Mplus()
    {
      Decimal result;
      if (this.calculatorElement.Value == null || !Decimal.TryParse(this.calculatorElement.Value.ToString(), out result))
        return;
      this.MemoryValue += result;
    }

    private void Mminus()
    {
      Decimal result;
      if (this.calculatorElement.Value == null || !Decimal.TryParse(this.calculatorElement.Value.ToString(), out result))
        return;
      this.MemoryValue -= result;
    }

    private void MS()
    {
      Decimal result;
      if (this.calculatorElement.Value == null || !Decimal.TryParse(this.calculatorElement.Value.ToString(), out result))
        return;
      this.MemoryValue = result;
    }

    private void MR()
    {
      this.calculatorElement.Value = (object) this.MemoryValue;
    }

    private void MC()
    {
      this.MemoryValue = new Decimal(0);
    }

    private void Digit(string input)
    {
      this.calculatorElement.InputtingDigits = true;
      if (this.calculatorElement.Value == null || this.calculatorElement.Value.ToString() == string.Empty)
        this.initialNumber = true;
      if (this.equalsPressed || this.operationFinished)
      {
        this.calculatorElement.Value = (object) input;
        this.value = new Decimal?(new Decimal(0));
        this.initialNumber = true;
        this.equalsPressed = false;
        this.operationFinished = false;
      }
      else
      {
        if (!this.value.HasValue)
        {
          Decimal result;
          this.value = !Decimal.TryParse(this.calculatorElement.Value.ToString(), out result) ? new Decimal?(new Decimal(0)) : new Decimal?(result);
          this.calculatorElement.Value = (object) input;
        }
        else if (this.calculatorElement.Value.ToString() == "0")
          this.calculatorElement.Value = (object) input;
        else if (input != CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator || !this.calculatorElement.Value.ToString().Contains(CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator))
          this.calculatorElement.Value = (object) (this.calculatorElement.Value.ToString() + input);
        if (!(this.calculatorElement.Value.ToString() == CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator))
          return;
        this.calculatorElement.Value = (object) ("0" + this.calculatorElement.Value);
      }
    }

    private void button_KeyPress(object sender, KeyPressEventArgs e)
    {
      this.ProcessKeyPress(e);
    }

    private void button_KeyDown(object sender, KeyEventArgs e)
    {
      this.ProcessKeyDown(e);
    }

    private void buttonAdd_MouseUp(object sender, MouseEventArgs e)
    {
      this.Add();
    }

    private void buttonSubstract_MouseUp(object sender, MouseEventArgs e)
    {
      this.Substract();
    }

    private void buttonMultiply_MouseUp(object sender, MouseEventArgs e)
    {
      this.Multiply();
    }

    private void buttonDivide_MouseUp(object sender, MouseEventArgs e)
    {
      this.Divide();
    }

    private void buttonSqrt_MouseUp(object sender, MouseEventArgs e)
    {
      this.Sqrt();
    }

    private void buttonPercent_MouseUp(object sender, MouseEventArgs e)
    {
      this.Percent();
    }

    private void buttonReciprocal_MouseUp(object sender, MouseEventArgs e)
    {
      this.Reciprocal();
    }

    private void buttonSign_MouseUp(object sender, MouseEventArgs e)
    {
      this.Sign();
    }

    private void buttonEquals_MouseUp(object sender, MouseEventArgs e)
    {
      this.Equals();
    }

    private void buttonC_MouseUp(object sender, MouseEventArgs e)
    {
      this.C();
    }

    private void buttonCE_MouseUp(object sender, MouseEventArgs e)
    {
      this.CE();
    }

    private void buttonDelete_MouseUp(object sender, MouseEventArgs e)
    {
      this.Delete();
    }

    private void buttonMplus_MouseUp(object sender, MouseEventArgs e)
    {
      this.Mplus();
    }

    private void buttonMminus_MouseUp(object sender, MouseEventArgs e)
    {
      this.Mminus();
    }

    private void buttonMS_MouseUp(object sender, MouseEventArgs e)
    {
      this.MS();
    }

    private void buttonMR_MouseUp(object sender, MouseEventArgs e)
    {
      this.MR();
    }

    private void buttonMC_MouseUp(object sender, MouseEventArgs e)
    {
      this.MC();
    }

    private void digitButton_MouseUp(object sender, MouseEventArgs e)
    {
      LightVisualElement lightVisualElement = sender as LightVisualElement;
      if (lightVisualElement.Tag == null)
        return;
      this.Digit(lightVisualElement.Tag.ToString());
    }

    private enum ArithmeticOperation
    {
      None,
      Add,
      Substract,
      Multiply,
      Divide,
    }
  }
}
