// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.ZoomMenuItem
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.Drawing;
using System.Windows.Forms;

namespace Telerik.WinControls.UI
{
  public class ZoomMenuItem : RadMenuItem
  {
    private RadHostItem hostItem;
    private PlusMinusEditor editor;

    public RadHostItem HostItem
    {
      get
      {
        return this.hostItem;
      }
    }

    public PlusMinusEditor Editor
    {
      get
      {
        return this.editor;
      }
    }

    protected override void CreateChildElements()
    {
      base.CreateChildElements();
      this.editor = new PlusMinusEditor();
      this.editor.Size = new Size(100, 21);
      this.hostItem = new RadHostItem((Control) this.editor);
      this.hostItem.StretchHorizontally = false;
      this.hostItem.StretchVertically = false;
      this.hostItem.Alignment = ContentAlignment.MiddleCenter;
      this.Children.Add((RadElement) this.hostItem);
    }

    protected override SizeF MeasureOverride(SizeF availableSize)
    {
      SizeF sizeF = base.MeasureOverride(availableSize);
      sizeF.Width = (float) this.editor.Size.Width + this.Layout.DesiredSize.Width;
      return sizeF;
    }

    protected override SizeF ArrangeOverride(SizeF finalSize)
    {
      base.ArrangeOverride(finalSize);
      RectangleF clientRectangle = this.GetClientRectangle(finalSize);
      this.hostItem.Arrange(new RectangleF(clientRectangle.Right - (float) this.editor.Width, clientRectangle.Top, (float) this.editor.Width, clientRectangle.Height));
      return finalSize;
    }

    protected override void DisposeManagedResources()
    {
      base.DisposeManagedResources();
      this.editor.Dispose();
      this.hostItem.Dispose();
    }

    protected override System.Type ThemeEffectiveType
    {
      get
      {
        return typeof (RadMenuItem);
      }
    }
  }
}
