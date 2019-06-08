// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.ToolbarActionElement
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.Drawing;
using System.Windows.Forms;
using Telerik.WinControls.Styles;

namespace Telerik.WinControls.UI
{
  public class ToolbarActionElement : LightVisualElement
  {
    private ToolbarActionDataItem dataItem;

    static ToolbarActionElement()
    {
      ItemStateManagerFactoryRegistry.AddStateManagerFactory((ItemStateManagerFactoryBase) new ItemStateManagerFactory(), typeof (ToolbarActionElement));
    }

    public ToolbarActionElement(ToolbarActionDataItem dataItem)
    {
      this.dataItem = dataItem;
      this.Image = dataItem.Image;
      this.Tag = dataItem.UserData;
    }

    protected override void InitializeFields()
    {
      base.InitializeFields();
      this.NotifyParentOnMouseInput = true;
      this.MaxSize = new Size(100, 100);
      this.Padding = new Padding(8);
      this.DrawImage = true;
    }

    public ToolbarActionDataItem DataItem
    {
      get
      {
        return this.dataItem;
      }
    }
  }
}
