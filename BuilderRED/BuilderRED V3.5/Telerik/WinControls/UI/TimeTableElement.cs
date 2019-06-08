// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.TimeTableElement
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.Drawing;
using System.Windows.Forms;
using Telerik.WinControls.Layouts;

namespace Telerik.WinControls.UI
{
  public class TimeTableElement : StackLayoutElement
  {
    private const int HeaderHeight = 19;
    private GridLayout contentElement;
    private TimeTableHeaderElement tableHeader;

    protected override void CreateChildElements()
    {
      base.CreateChildElements();
      this.Orientation = Orientation.Vertical;
      this.StretchHorizontally = true;
      this.StretchVertically = false;
      this.FitInAvailableSize = true;
      this.contentElement = new GridLayout();
      this.contentElement.Margin = new Padding(4);
      this.contentElement.StretchVertically = true;
      this.tableHeader = this.CreateHeader();
      this.Children.Add((RadElement) this.tableHeader);
      this.Children.Add((RadElement) this.contentElement);
    }

    protected virtual TimeTableHeaderElement CreateHeader()
    {
      return new TimeTableHeaderElement();
    }

    public TimeTableHeaderElement TableHeader
    {
      get
      {
        return this.tableHeader;
      }
      set
      {
        this.tableHeader = value;
      }
    }

    public GridLayout ContentElement
    {
      get
      {
        return this.contentElement;
      }
    }

    protected override SizeF ArrangeOverride(SizeF finalSize)
    {
      RectangleF clientRectangle = this.GetClientRectangle(finalSize);
      this.TableHeader.Arrange(new RectangleF(clientRectangle.X, clientRectangle.Y, clientRectangle.Width, 19f));
      this.ContentElement.Arrange(new RectangleF(clientRectangle.X, clientRectangle.Y + 19f, clientRectangle.Width, clientRectangle.Height - 19f));
      return finalSize;
    }
  }
}
