// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.WizardAeroTopElement
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Drawing;

namespace Telerik.WinControls.UI
{
  public class WizardAeroTopElement : BaseWizardElement
  {
    private WizardAeroButtonElement backButton;

    public WizardAeroTopElement()
    {
    }

    public WizardAeroTopElement(RadWizardElement wizardElement)
    {
      this.Owner = wizardElement;
    }

    protected override void DisposeManagedResources()
    {
      this.backButton.Click -= new EventHandler(this.BackButton_Click);
      base.DisposeManagedResources();
    }

    protected override void CreateChildElements()
    {
      base.CreateChildElements();
      this.backButton = new WizardAeroButtonElement();
      this.backButton.UseDefaultDisabledPaint = false;
      this.backButton.MinSize = new Size(28, 28);
      this.backButton.Click += new EventHandler(this.BackButton_Click);
      this.Children.Add((RadElement) this.backButton);
    }

    public WizardAeroButtonElement BackButton
    {
      get
      {
        return this.backButton;
      }
    }

    private void BackButton_Click(object sender, EventArgs e)
    {
      this.Owner.SelectPreviousPage();
    }

    protected override SizeF MeasureOverride(SizeF availableSize)
    {
      if ((double) this.Owner.PageHeaderHeight > -1.0)
        return base.MeasureOverride(new SizeF(availableSize.Width, this.Owner.PageHeaderHeight));
      this.backButton.Measure(availableSize);
      return new SizeF(this.backButton.DesiredSize.Width + (float) this.Padding.Left + (float) this.Padding.Right, this.backButton.DesiredSize.Height + (float) this.Padding.Top + (float) this.Padding.Bottom);
    }

    protected override SizeF ArrangeOverride(SizeF finalSize)
    {
      this.backButton.Arrange(new RectangleF(0.0f, 0.0f, this.backButton.DesiredSize.Width, this.backButton.DesiredSize.Height));
      return finalSize;
    }
  }
}
