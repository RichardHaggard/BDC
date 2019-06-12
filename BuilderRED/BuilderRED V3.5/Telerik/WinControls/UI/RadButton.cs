// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.RadButton
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using Telerik.Licensing;
using Telerik.WinControls.CodedUI;
using Telerik.WinControls.Primitives;

namespace Telerik.WinControls.UI
{
  [DefaultEvent("Click")]
  [Description("Responds to user clicks.")]
  [DefaultBindingProperty("Text")]
  [ToolboxItem(true)]
  [DefaultProperty("Text")]
  [LicenseProvider(typeof (TelerikLicenseProvider))]
  public class RadButton : RadButtonBase, IButtonControl
  {
    [DefaultValue(DialogResult.None)]
    public DialogResult DialogResult
    {
      get
      {
        return ((IButtonControl) this.ButtonElement).DialogResult;
      }
      set
      {
        ((IButtonControl) this.ButtonElement).DialogResult = value;
      }
    }

    public void NotifyDefault(bool value)
    {
      ((IButtonControl) this.ButtonElement).NotifyDefault(value);
    }

    public override void PerformClick()
    {
      Form form = this.FindForm();
      if (((int) Telerik.WinControls.NativeMethods.GetKeyState(27) & 32768) == 32768 && (form == null || form.CancelButton != this))
        return;
      base.PerformClick();
    }

    protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
    {
      if (this.Focused && keyData == Keys.Escape && this.ButtonElement.IsPressed)
      {
        int num = (int) this.ButtonElement.SetValue(RadButtonItem.IsPressedProperty, (object) false);
        this.ButtonElement.IsCancelClicked = true;
      }
      return base.ProcessCmdKey(ref msg, keyData);
    }

    protected override void ProcessCodedUIMessage(ref IPCMessage request)
    {
      base.ProcessCodedUIMessage(ref request);
      if (this.ButtonElement == null || request == null)
        return;
      if (this.ButtonElement.IsInValidState(true) && request.Type == IPCMessage.MessageTypes.ExecuteMethod && request.Message == "ButtonClick")
      {
        this.ButtonElement.CallDoClick(EventArgs.Empty);
      }
      else
      {
        if (request.Type != IPCMessage.MessageTypes.GetPropertyValue || !(request.Message == "IsPressed"))
          return;
        request.Data = (object) this.ButtonElement.IsPressed;
      }
    }

    protected override void SetBackColorThemeOverrides()
    {
      this.ButtonElement.SuspendApplyOfThemeSettings();
      List<string> availableVisualStates = this.ButtonElement.GetAvailableVisualStates();
      availableVisualStates.Add("");
      foreach (string state in availableVisualStates)
      {
        this.ButtonElement.SetThemeValueOverride(VisualElement.BackColorProperty, (object) this.BackColor, state);
        this.ButtonElement.SetThemeValueOverride(VisualElement.BackColorProperty, (object) this.BackColor, state, "ButtonFill");
        this.ButtonElement.SetThemeValueOverride(FillPrimitive.GradientStyleProperty, (object) GradientStyles.Solid, state, "ButtonFill");
      }
      this.ButtonElement.ResumeApplyOfThemeSettings();
    }

    protected override void ResetBackColorThemeOverrides()
    {
      this.ButtonElement.SuspendApplyOfThemeSettings();
      this.ButtonElement.ResetThemeValueOverride(VisualElement.BackColorProperty);
      this.ButtonElement.ResetThemeValueOverride(FillPrimitive.GradientStyleProperty);
      this.ButtonElement.ResumeApplyOfThemeSettings();
    }

    protected override void SetForeColorThemeOverrides()
    {
      this.ButtonElement.SuspendApplyOfThemeSettings();
      List<string> availableVisualStates = this.ButtonElement.GetAvailableVisualStates();
      availableVisualStates.Add("");
      foreach (string state in availableVisualStates)
      {
        this.ButtonElement.SetThemeValueOverride(VisualElement.ForeColorProperty, (object) this.ForeColor, state);
        this.ButtonElement.SetThemeValueOverride(VisualElement.ForeColorProperty, (object) this.ForeColor, state, typeof (TextPrimitive));
      }
      this.ButtonElement.ResumeApplyOfThemeSettings();
    }

    protected override void ResetForeColorThemeOverrides()
    {
      this.ButtonElement.SuspendApplyOfThemeSettings();
      this.ButtonElement.ResetThemeValueOverride(VisualElement.ForeColorProperty);
      int num = (int) this.ButtonElement.TextElement.ResetValue(VisualElement.ForeColorProperty, ValueResetFlags.Style);
      this.ButtonElement.ElementTree.ApplyThemeToElementTree();
      this.ButtonElement.ResumeApplyOfThemeSettings();
    }
  }
}
