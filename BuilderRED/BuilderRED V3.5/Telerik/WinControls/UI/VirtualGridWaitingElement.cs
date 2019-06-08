// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.VirtualGridWaitingElement
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.Drawing;

namespace Telerik.WinControls.UI
{
  public class VirtualGridWaitingElement : LightVisualElement
  {
    private RadWaitingBarElement waitingBarElement;

    public RadWaitingBarElement WaitingBarElement
    {
      get
      {
        return this.waitingBarElement;
      }
    }

    protected override void CreateChildElements()
    {
      base.CreateChildElements();
      this.BackColor = Color.FromArgb(128, (int) byte.MaxValue, (int) byte.MaxValue, (int) byte.MaxValue);
      this.DrawFill = true;
      this.GradientStyle = GradientStyles.Solid;
      this.StretchHorizontally = this.StretchVertically = true;
      this.waitingBarElement = new RadWaitingBarElement();
      this.waitingBarElement.StretchHorizontally = this.waitingBarElement.StretchVertically = false;
      this.waitingBarElement.MinSize = new Size(150, 20);
      this.waitingBarElement.MaxSize = new Size(150, 20);
      this.waitingBarElement.Alignment = ContentAlignment.MiddleCenter;
      this.Children.Add((RadElement) this.waitingBarElement);
    }

    public void StartWaiting()
    {
      this.Visibility = ElementVisibility.Visible;
      this.waitingBarElement.StartWaiting();
    }

    public void StopWaiting()
    {
      this.waitingBarElement.StopWaiting();
      this.Visibility = ElementVisibility.Collapsed;
    }
  }
}
