// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.FadeAnimationTypeEditor
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace Telerik.WinControls.UI
{
  public class FadeAnimationTypeEditor : UITypeEditor
  {
    private FadeAnimationTypeEditor.FadeAnimationTypeEditorUI fadeAnimationTypeEditorUI;

    public override object EditValue(
      ITypeDescriptorContext context,
      System.IServiceProvider provider,
      object value)
    {
      if (provider != null)
      {
        IWindowsFormsEditorService service = (IWindowsFormsEditorService) provider.GetService(typeof (IWindowsFormsEditorService));
        if (service == null)
          return value;
        if (this.fadeAnimationTypeEditorUI == null)
          this.fadeAnimationTypeEditorUI = new FadeAnimationTypeEditor.FadeAnimationTypeEditorUI((FadeAnimationType) value);
        service.DropDownControl((Control) this.fadeAnimationTypeEditorUI);
        value = (object) this.fadeAnimationTypeEditorUI.Result;
      }
      return value;
    }

    public override UITypeEditorEditStyle GetEditStyle(
      ITypeDescriptorContext context)
    {
      return UITypeEditorEditStyle.DropDown;
    }

    public override void PaintValue(PaintValueEventArgs e)
    {
      base.PaintValue(e);
    }

    private class FadeAnimationTypeEditorUI : Control
    {
      private RadToggleButton fadeInToggleButton;
      private RadToggleButton fadeOutToggleButton;
      private FadeAnimationType result;

      internal FadeAnimationTypeEditorUI(FadeAnimationType input)
      {
        this.result = input;
        this.InitializeComponent();
      }

      private void InitializeComponent()
      {
        this.fadeInToggleButton = new RadToggleButton();
        this.fadeOutToggleButton = new RadToggleButton();
        this.fadeInToggleButton.Text = "Fade In";
        this.fadeOutToggleButton.Text = "Fade Out";
        this.Size = new Size(80, 80);
        this.fadeInToggleButton.Dock = DockStyle.Top;
        this.fadeOutToggleButton.Dock = DockStyle.Bottom;
        this.fadeInToggleButton.Height = 40;
        this.fadeOutToggleButton.Height = 40;
        this.fadeInToggleButton.ToggleState = (this.result & FadeAnimationType.FadeIn) != FadeAnimationType.None ? Telerik.WinControls.Enumerations.ToggleState.On : Telerik.WinControls.Enumerations.ToggleState.Off;
        this.fadeOutToggleButton.ToggleState = (this.result & FadeAnimationType.FadeOut) != FadeAnimationType.None ? Telerik.WinControls.Enumerations.ToggleState.On : Telerik.WinControls.Enumerations.ToggleState.Off;
        this.fadeInToggleButton.ToggleStateChanged += new StateChangedEventHandler(this.fadeInToggleButton_ToggleStateChanged);
        this.fadeOutToggleButton.ToggleStateChanged += new StateChangedEventHandler(this.fadeOutToggleButton_ToggleStateChanged);
        this.Controls.Add((Control) this.fadeInToggleButton);
        this.Controls.Add((Control) this.fadeOutToggleButton);
      }

      internal FadeAnimationType Result
      {
        get
        {
          return this.result;
        }
      }

      private void fadeOutToggleButton_ToggleStateChanged(object sender, StateChangedEventArgs args)
      {
        if (args.ToggleState == Telerik.WinControls.Enumerations.ToggleState.On)
          this.result |= FadeAnimationType.FadeOut;
        else
          this.result &= ~FadeAnimationType.FadeOut;
      }

      private void fadeInToggleButton_ToggleStateChanged(object sender, StateChangedEventArgs args)
      {
        if (args.ToggleState == Telerik.WinControls.Enumerations.ToggleState.On)
          this.result |= FadeAnimationType.FadeIn;
        else
          this.result &= ~FadeAnimationType.FadeIn;
      }
    }
  }
}
