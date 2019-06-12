// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.TimePickerDoneButtonElement
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Drawing;
using Telerik.WinControls.Localization;

namespace Telerik.WinControls.UI
{
  public class TimePickerDoneButtonElement : LightVisualElement
  {
    private RadButtonElement buttonElement;
    private RadTimePickerContentElement owner;

    public TimePickerDoneButtonElement(RadTimePickerContentElement owner)
    {
      this.owner = owner;
    }

    protected override void CreateChildElements()
    {
      base.CreateChildElements();
      this.StretchVertically = false;
      this.buttonElement = new RadButtonElement();
      this.buttonElement.Text = LocalizationProvider<RadTimePickerLocalizationProvider>.CurrentProvider.GetLocalizedString("CloseButtonText");
      this.buttonElement.Click += new EventHandler(this.buttonElement_Click);
      this.buttonElement.StretchHorizontally = false;
      this.Children.Add((RadElement) this.buttonElement);
    }

    public RadButtonElement ButtonElement
    {
      get
      {
        return this.buttonElement;
      }
    }

    public RadTimePickerContentElement Owner
    {
      get
      {
        return this.owner;
      }
      set
      {
        this.owner = value;
      }
    }

    private void buttonElement_Click(object sender, EventArgs e)
    {
      if (this.Owner == null)
        return;
      this.Owner.SetValueAndClose();
    }

    protected override SizeF ArrangeOverride(SizeF finalSize)
    {
      this.buttonElement.Arrange(new RectangleF((float) ((double) finalSize.Width / 2.0 - (double) this.buttonElement.DesiredSize.Width / 2.0), (float) ((double) finalSize.Height / 2.0 - (double) this.buttonElement.DesiredSize.Height / 2.0), this.buttonElement.DesiredSize.Width, this.buttonElement.DesiredSize.Height));
      return finalSize;
    }
  }
}
