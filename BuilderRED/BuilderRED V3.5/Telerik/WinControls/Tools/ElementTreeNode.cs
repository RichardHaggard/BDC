// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.Tools.ElementTreeNode
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.Windows.Forms;

namespace Telerik.WinControls.Tools
{
  public class ElementTreeNode : TreeNode
  {
    private RadElement element;
    private object obj;

    public ElementTreeNode(RadElement element, string text)
    {
      this.Element = element;
      this.Text = element.GetType().Name + text;
    }

    public ElementTreeNode(RadElement element)
    {
      this.Element = element;
      this.Text = element.GetType().Name;
    }

    public ElementTreeNode(object obj)
    {
      this.obj = obj;
      this.Text = obj.GetType().Name;
    }

    public RadElement Element
    {
      get
      {
        return this.element;
      }
      set
      {
        this.element = value;
      }
    }

    public object Object
    {
      get
      {
        return this.obj;
      }
      set
      {
        this.obj = value;
      }
    }
  }
}
