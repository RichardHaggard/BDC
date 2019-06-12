// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.ImageEditor.Dialogs.ImageEditorBaseDialog
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Telerik.WinControls.UI.ImageEditor.Dialogs
{
  public class ImageEditorBaseDialog : RadForm
  {
    private readonly RadImageEditorElement imageEditorElement;
    private IContainer components;

    public ImageEditorBaseDialog()
    {
      this.InitializeComponent();
    }

    public ImageEditorBaseDialog(RadImageEditorElement imageEditorElement)
    {
      this.InitializeComponent();
      this.imageEditorElement = imageEditorElement;
    }

    public RadImageEditorElement ImageEditorElement
    {
      get
      {
        return this.imageEditorElement;
      }
    }

    protected override void OnLoad(EventArgs e)
    {
      base.OnLoad(e);
      if (string.IsNullOrEmpty(ThemeResolutionService.ApplicationThemeName) && this.imageEditorElement != null)
        ThemeResolutionService.ApplyThemeToControlTree((Control) this, this.imageEditorElement.ElementTree.ThemeName);
      this.LocalizeStrings();
      this.WireEvents();
    }

    protected override void OnClosed(EventArgs e)
    {
      base.OnClosed(e);
      this.UnwireEvents();
    }

    protected void RadTrackBar_TickFormatting(object sender, TickFormattingEventArgs e)
    {
      e.TickElement.Line1.GradientStyle = GradientStyles.Solid;
      e.TickElement.Line2.GradientStyle = GradientStyles.Solid;
      e.TickElement.Line1.BackColor = Color.Transparent;
      e.TickElement.Line2.BackColor = Color.Transparent;
    }

    protected virtual void ApplySettings()
    {
    }

    protected virtual void WireEvents()
    {
    }

    protected virtual void UnwireEvents()
    {
    }

    protected virtual void LocalizeStrings()
    {
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      this.components = (IContainer) new Container();
      this.AutoScaleMode = AutoScaleMode.Font;
      this.Text = nameof (ImageEditorBaseDialog);
    }
  }
}
