// Decompiled with JetBrains decompiler
// Type: BuilderRED.AssessmentHelpers
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using Telerik.WinControls;

namespace BuilderRED
{
  [StandardModule]
  internal sealed class AssessmentHelpers
  {
    public static Size GetButtonSize
    {
      get
      {
        AssessmentHelpers.CheckButtonWithValue checkButtonWithValue = new AssessmentHelpers.CheckButtonWithValue();
        checkButtonWithValue.Height = checked (mdUtility.fMainForm.Font.Height * 2 + 4);
        return checkButtonWithValue.Size;
      }
    }

    public static void SaveInspector(DataRow row)
    {
      if (Information.IsDBNull(RuntimeHelpers.GetObjectValue(row["InspectorLink"])) || Microsoft.VisualBasic.CompilerServices.Operators.ConditionalCompareObjectEqual(row["InspectorLink"], (object) "", false))
        row["InspectorLink"] = (object) mdUtility.strCurrentInspector;
      else if (Microsoft.VisualBasic.CompilerServices.Operators.ConditionalCompareObjectNotEqual(row["InspectorLink"], (object) mdUtility.strCurrentInspector, false) && Interaction.MsgBox((object) "Current inspector is different than previously saved inspector.  Overwrite with new inspector?", MsgBoxStyle.YesNo, (object) "Save with new inspector?") == MsgBoxResult.Yes)
        row["InspectorLink"] = (object) mdUtility.strCurrentInspector;
    }

    public static string CommentDialog(string strCaption, string sComment, IWin32Window owner = null)
    {
      frmComment frmComment = new frmComment();
      string str = sComment;
      try
      {
        if (frmComment.EditComment(strCaption, (object) sComment, true, owner) == DialogResult.Yes)
          str = frmComment.Comment;
      }
      catch (Exception ex)
      {
        ProjectData.SetProjectError(ex);
        mdUtility.Errorhandler(ex, "Comment Dialog", "btnComment_Click");
        ProjectData.ClearProjectError();
      }
      finally
      {
        frmComment.Dispose();
      }
      return str;
    }

    public static void FontResizer(params object[] args)
    {
      int integer = Conversions.ToInteger(args[checked (((IEnumerable<object>) args).Count<object>() - 1)]);
      object[] objArray = args;
      int index = 0;
      while (index < objArray.Length)
      {
        object objectValue = RuntimeHelpers.GetObjectValue(objArray[index]);
        if (objectValue is RadControl)
        {
          RadControl radControl = (RadControl) objectValue;
          radControl.Font = new Font(radControl.Font.FontFamily, radControl.Font.Size + (float) integer);
        }
        if (objectValue is Control)
        {
          Control control = (Control) objectValue;
          NewLateBinding.LateSet(objectValue, (System.Type) null, "Font", new object[1]
          {
            (object) new Font(control.Font.FontFamily, control.Font.Size + (float) integer)
          }, (string[]) null, (System.Type[]) null);
        }
        checked { ++index; }
      }
    }

    public class TextBoxValidator
    {
      private bool nonNumberEntered;
      private System.Windows.Forms.TextBox txtValidate;
      private System.Windows.Forms.Label lblUnitOfMeasure;

      public System.Windows.Forms.Label UOM
      {
        get
        {
          return this.lblUnitOfMeasure;
        }
        set
        {
          this.lblUnitOfMeasure = value;
        }
      }

      public TextBoxValidator(
        ref System.Windows.Forms.TextBox QuantityTextBox,
        ref System.Windows.Forms.Label UOMLabel = null,
        AssessmentHelpers.TextBoxValidator.ValidationTypes sType = AssessmentHelpers.TextBoxValidator.ValidationTypes.Quantity)
      {
        this.nonNumberEntered = false;
        this.txtValidate = QuantityTextBox;
        this.lblUnitOfMeasure = UOMLabel;
        if (sType != AssessmentHelpers.TextBoxValidator.ValidationTypes.Quantity)
          return;
        this.txtValidate.KeyDown += new KeyEventHandler(this.txtValidate_KeyDown);
        this.txtValidate.KeyPress += new KeyPressEventHandler(this.txtValidate_KeyPress);
        this.txtValidate.TextChanged += new EventHandler(this.txtValidate_TextChanged);
      }

      private void txtValidate_KeyDown(object sender, KeyEventArgs e)
      {
        this.nonNumberEntered = false;
        if (e.KeyCode >= Keys.D0 && e.KeyCode <= Keys.D9 || e.KeyCode >= Keys.NumPad0 && e.KeyCode <= Keys.NumPad9 || (e.KeyCode == Keys.Back || e.KeyCode == Keys.Delete))
          return;
        if ((uint) Microsoft.VisualBasic.CompilerServices.Operators.CompareString(this.lblUnitOfMeasure.Text, "EA", false) > 0U)
        {
          if (e.KeyCode != Keys.Decimal && e.KeyCode != Keys.OemPeriod || Microsoft.VisualBasic.Strings.InStr(this.txtValidate.Text, ".", CompareMethod.Binary) > 1)
            this.nonNumberEntered = true;
        }
        else
          this.nonNumberEntered = true;
      }

      private void txtValidate_KeyPress(object sender, KeyPressEventArgs e)
      {
        if (!this.nonNumberEntered)
          return;
        e.Handled = true;
      }

      private void txtValidate_TextChanged(object sender, EventArgs e)
      {
        try
        {
          if (!this.txtValidate.Text.StartsWith("."))
            return;
          this.txtValidate.Text = "0.";
          this.txtValidate.SelectionStart = this.txtValidate.TextLength;
        }
        catch (Exception ex)
        {
          ProjectData.SetProjectError(ex);
          mdUtility.Errorhandler(ex, "TextBox Validation", nameof (txtValidate_TextChanged));
          ProjectData.ClearProjectError();
        }
        finally
        {
        }
      }

      public enum ValidationTypes
      {
        Quantity,
        Percent,
        Comment,
      }
    }

    public class LabelWithMargins : System.Windows.Forms.Label
    {
      protected override Padding DefaultMargin
      {
        get
        {
          return new Padding(3);
        }
      }

      protected override Padding DefaultPadding
      {
        get
        {
          return new Padding(1);
        }
      }

      public override Font Font
      {
        get
        {
          return base.Font;
        }
        set
        {
          base.Font = value;
        }
      }
    }

    public class WatermarkTextBox : System.Windows.Forms.TextBox
    {
      private string m_watermarkText;

      public string WatermarkText
      {
        get
        {
          return this.m_watermarkText;
        }
        set
        {
          this.m_watermarkText = value;
          if (this.m_watermarkText == null | Microsoft.VisualBasic.CompilerServices.Operators.CompareString(this.m_watermarkText, "", false) == 0)
            CueProvider.ClearCue((System.Windows.Forms.TextBox) this);
          else
            CueProvider.SetCue((System.Windows.Forms.TextBox) this, this.WatermarkText);
        }
      }
    }

    public class CheckButtonWithValue : CheckBox, IButtonControl
    {
      private DialogResult myDialogResult;
      private string m_Value;

      public string Value
      {
        get
        {
          return this.m_Value;
        }
        set
        {
          this.m_Value = value;
        }
      }

      public override Color BackColor
      {
        get
        {
          Color color;
          return color;
        }
        set
        {
        }
      }

      public DialogResult DialogResult
      {
        get
        {
          return this.myDialogResult;
        }
        set
        {
          if (!Enum.IsDefined(typeof (DialogResult), (object) value))
            return;
          this.myDialogResult = value;
        }
      }

      public void NotifyDefault(bool value)
      {
        if (this.IsDefault == value)
          return;
        this.IsDefault = value;
      }

      public void PerformClick()
      {
        if (!this.CanSelect)
          return;
        this.OnClick(EventArgs.Empty);
      }
    }
  }
}
