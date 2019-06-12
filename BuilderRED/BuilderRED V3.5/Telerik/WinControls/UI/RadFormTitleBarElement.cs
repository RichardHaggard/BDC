// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.RadFormTitleBarElement
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Telerik.WinControls.Styles;

namespace Telerik.WinControls.UI
{
  [ToolboxItem(false)]
  [ComVisible(false)]
  public class RadFormTitleBarElement : RadTitleBarElement, INotifyPropertyChanged
  {
    static RadFormTitleBarElement()
    {
      ItemStateManagerFactoryRegistry.AddStateManagerFactory((ItemStateManagerFactoryBase) new RadFormTitleBarElementStateManager(), typeof (RadFormTitleBarElement));
    }

    protected override System.Type ThemeEffectiveType
    {
      get
      {
        return typeof (RadTitleBarElement);
      }
    }

    protected override void CreateChildElements()
    {
      base.CreateChildElements();
      this.layout.Children.Remove((RadElement) this.titleBarIcon);
      this.layout.Children.Add((RadElement) this.titleBarIcon);
      this.layout.Children.Remove(this.CaptionElement);
      this.layout.Children.Add(this.CaptionElement);
    }

    public override void HandleMouseMove(MouseEventArgs e, Form form)
    {
    }

    protected override void OnPropertyChanged(RadPropertyChangedEventArgs e)
    {
      base.OnPropertyChanged(e);
      if (e.Property != RadElement.VisibilityProperty)
        return;
      ComponentThemableElementTree elementTree = this.ElementTree;
      if (elementTree == null)
        return;
      Control control = elementTree.Control;
      if (control == null)
        return;
      RadFormControlBase radFormControlBase = control as RadFormControlBase;
      if (radFormControlBase == null || radFormControlBase.FormBorderStyle == FormBorderStyle.None)
        return;
      radFormControlBase.InvalidateIfNotSuspended();
      radFormControlBase.CallSetClientSizeCore(radFormControlBase.ClientSize.Width, radFormControlBase.ClientSize.Height);
    }
  }
}
