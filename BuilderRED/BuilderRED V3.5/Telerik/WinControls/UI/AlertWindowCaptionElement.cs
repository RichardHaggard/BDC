// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.AlertWindowCaptionElement
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.Windows.Forms;
using Telerik.WinControls.Layouts;

namespace Telerik.WinControls.UI
{
  public class AlertWindowCaptionElement : RadElement
  {
    private AlertWindowCaptionGrip captionGrip;
    private AlertWindowTextAndSystemButtonsElement textAndButtonsElement;
    private StackLayoutPanel mainLayoutPanel;

    public AlertWindowCaptionGrip CaptionGrip
    {
      get
      {
        return this.captionGrip;
      }
    }

    public AlertWindowTextAndSystemButtonsElement TextAndButtonsElement
    {
      get
      {
        return this.textAndButtonsElement;
      }
    }

    protected override void InitializeFields()
    {
      base.InitializeFields();
      this.captionGrip = new AlertWindowCaptionGrip();
      this.textAndButtonsElement = new AlertWindowTextAndSystemButtonsElement();
      this.mainLayoutPanel = new StackLayoutPanel();
    }

    protected override void CreateChildElements()
    {
      base.CreateChildElements();
      this.mainLayoutPanel.Class = "AlertWindowCaptionLayoutPanel";
      this.mainLayoutPanel.Orientation = Orientation.Vertical;
      this.mainLayoutPanel.Children.Add((RadElement) this.captionGrip);
      this.mainLayoutPanel.Children.Add((RadElement) this.textAndButtonsElement);
      this.Children.Add((RadElement) this.mainLayoutPanel);
    }
  }
}
