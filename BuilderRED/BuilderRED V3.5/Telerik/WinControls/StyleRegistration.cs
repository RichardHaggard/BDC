// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.StyleRegistration
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.Windows.Forms;

namespace Telerik.WinControls
{
  public class StyleRegistration
  {
    private string registrationType = "ElementTypeControlType";
    private string elementType = "";
    private string controlType = "";
    private string elementName = "";
    private string controlName = "";

    public StyleRegistration()
    {
    }

    public StyleRegistration(string elementType)
    {
      this.registrationType = "ElementTypeDefault";
      this.elementType = elementType;
    }

    public StyleRegistration(StyleRegistration registration)
    {
      this.registrationType = registration.registrationType;
      this.elementType = registration.elementType;
      this.controlType = registration.controlType;
      this.elementName = registration.elementName;
      this.controlName = registration.controlName;
    }

    public StyleRegistration(
      string registrationType,
      string elementType,
      string controlType,
      string elementName,
      string controlName)
    {
      this.registrationType = registrationType;
      this.elementType = elementType;
      this.controlType = controlType;
      this.elementName = elementName;
      this.controlName = controlName;
    }

    public string RegistrationType
    {
      get
      {
        return this.registrationType;
      }
      set
      {
        this.registrationType = value;
      }
    }

    public string ElementType
    {
      get
      {
        return this.elementType;
      }
      set
      {
        this.elementType = value;
      }
    }

    public string ControlType
    {
      get
      {
        return this.controlType;
      }
      set
      {
        this.controlType = value;
      }
    }

    public string ElementName
    {
      get
      {
        return this.elementName;
      }
      set
      {
        this.elementName = value;
      }
    }

    public string ControlName
    {
      get
      {
        return this.controlName;
      }
      set
      {
        this.controlName = value;
      }
    }

    public bool IsCompatible(Control control)
    {
      if (this.registrationType == null || this.registrationType == "ElementTypeControlType")
      {
        IComponentTreeHandler componentTreeHandler = control as IComponentTreeHandler;
        if (componentTreeHandler != null && componentTreeHandler.ThemeClassName == this.ControlType)
        {
          if (this.ControlType != "Telerik.WinControls.UI.RadPageView" && componentTreeHandler.RootElement.GetThemeEffectiveType().FullName == this.ElementType || this.ElementType == "Telerik.WinControls.RootRadElement" && componentTreeHandler.RootElement.GetThemeEffectiveType().BaseType.FullName == this.ElementType)
            return true;
          return this.FindCompatibleChild((IStylableNode) componentTreeHandler.RootElement);
        }
      }
      return false;
    }

    private bool FindCompatibleChild(IStylableNode node)
    {
      foreach (IStylableNode child in node.Children)
      {
        if (node.GetThemeEffectiveType().FullName == this.ElementType)
          return true;
      }
      foreach (IStylableNode child in node.Children)
      {
        if (this.FindCompatibleChild(child))
          return true;
      }
      return false;
    }

    public bool IsCompatible(string controlType)
    {
      if (!(this.registrationType == "ElementTypeControlType"))
        return false;
      if (this.controlType == "Telerik.WinControls.UI.RadLabel")
      {
        if (controlType == this.controlType)
          return this.elementType == "Telerik.WinControls.UI.RadLabelRootElement";
        return false;
      }
      if (controlType == this.controlType)
        return this.elementType == "Telerik.WinControls.RootRadElement";
      return false;
    }

    public bool IsCompatible(IStylableNode item)
    {
      if (this.registrationType == "ElementTypeDefault")
        return this.elementType == item.GetThemeEffectiveType().FullName;
      if (string.IsNullOrEmpty(this.registrationType) || this.registrationType == "ElementTypeControlType")
      {
        RadElement radElement = item as RadElement;
        if (radElement != null && radElement.ElementTree != null)
        {
          IComponentTreeHandler control = radElement.ElementTree.Control as IComponentTreeHandler;
          if (control != null && this.elementType == item.GetThemeEffectiveType().FullName)
            return control.ThemeClassName == this.controlType;
        }
      }
      return false;
    }

    public bool IsCompatible(StyleRegistration registration)
    {
      if (this.compareStrings(this.elementName, registration.elementName) && this.compareStrings(this.elementType, registration.elementType) && (this.compareStrings(this.controlName, registration.controlName) && this.compareStrings(this.controlType, registration.controlType)))
        return this.compareStrings(this.registrationType, registration.registrationType);
      return false;
    }

    private bool compareStrings(string str1, string str2)
    {
      if (string.IsNullOrEmpty(str1) && string.IsNullOrEmpty(str2))
        return true;
      return str1 == str2;
    }
  }
}
