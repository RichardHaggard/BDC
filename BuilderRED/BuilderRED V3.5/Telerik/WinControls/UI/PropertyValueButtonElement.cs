// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.PropertyValueButtonElement
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.Drawing;
using System.Windows.Forms;
using Telerik.WinControls.Styles;

namespace Telerik.WinControls.UI
{
  public class PropertyValueButtonElement : LightVisualElement
  {
    public static RadProperty IsModifiedProperty = RadProperty.Register("IsModified", typeof (bool), typeof (PropertyValueButtonElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) false, ElementPropertyOptions.AffectsDisplay));

    static PropertyValueButtonElement()
    {
      ItemStateManagerFactoryRegistry.AddStateManagerFactory((ItemStateManagerFactoryBase) new PropertyGridValueButtonStateManager(), typeof (PropertyValueButtonElement));
    }

    protected override void InitializeFields()
    {
      base.InitializeFields();
      this.StretchHorizontally = false;
      this.StretchVertically = false;
      this.Alignment = ContentAlignment.MiddleRight;
      this.NotifyParentOnMouseInput = false;
    }

    protected override void OnMouseMove(MouseEventArgs e)
    {
      base.OnMouseMove(e);
      this.ElementTree.Control.Cursor = Cursors.Default;
    }
  }
}
