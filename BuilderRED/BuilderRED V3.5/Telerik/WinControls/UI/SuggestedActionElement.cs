// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.SuggestedActionElement
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using Telerik.WinControls.Styles;

namespace Telerik.WinControls.UI
{
  public class SuggestedActionElement : LightVisualElement
  {
    private SuggestedActionDataItem dataItem;

    static SuggestedActionElement()
    {
      ItemStateManagerFactoryRegistry.AddStateManagerFactory((ItemStateManagerFactoryBase) new ItemStateManagerFactory(), typeof (SuggestedActionElement));
    }

    public SuggestedActionElement(SuggestedActionDataItem dataItem)
    {
      this.dataItem = dataItem;
      this.Text = dataItem.Text;
      this.Image = dataItem.Image;
      this.Tag = dataItem.UserData;
    }

    protected override void InitializeFields()
    {
      base.InitializeFields();
      this.NotifyParentOnMouseInput = true;
      this.SmoothingMode = SmoothingMode.HighQuality;
      this.Font = new Font(this.Font.FontFamily, 9.75f);
      this.Padding = new Padding(10, 5, 10, 5);
      this.Margin = new Padding(5, 0, 5, 0);
      this.Shape = (ElementShape) new RoundRectShape(12);
      this.DrawBorder = true;
    }

    public SuggestedActionDataItem DataItem
    {
      get
      {
        return this.dataItem;
      }
    }
  }
}
