// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.AutoCompleteBoxInputHandler
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Drawing;
using System.Windows.Forms;

namespace Telerik.WinControls.UI
{
  public class AutoCompleteBoxInputHandler : TextBoxInputHandler
  {
    public AutoCompleteBoxInputHandler(RadAutoCompleteBoxElement textBoxElement)
      : base((RadTextBoxControlElement) textBoxElement)
    {
    }

    public override bool ProcessMouseDown(MouseEventArgs e)
    {
      bool flag = base.ProcessMouseDown(e);
      if (e.Button == MouseButtons.Left && flag)
      {
        this.TextBox.CloseDropDown();
        ITextBlock textBlockAtPoint = this.GetTextBlockAtPoint(e.Location);
        TokenizedTextBlockElement textBlockElement = textBlockAtPoint as TokenizedTextBlockElement;
        if (textBlockElement != null && !textBlockElement.RemoveButton.ControlBoundingRectangle.Contains(e.Location))
        {
          LineInfo line = this.TextBox.ViewElement.Lines.BinarySearchByBlockIndex(textBlockAtPoint.Index);
          this.TextBox.Navigator.Select(new TextPosition(line, textBlockAtPoint, 0), new TextPosition(line, textBlockAtPoint, textBlockAtPoint.Length));
        }
      }
      return flag;
    }

    protected override bool ProcessNavigationKey(KeyEventArgs e)
    {
      if (this.TextBox.CanPerformAutoComplete && (e.KeyCode == Keys.Left || e.KeyCode == Keys.Right))
        this.TextBox.CloseDropDown();
      return base.ProcessNavigationKey(e);
    }

    protected override bool ProcessListNavigation(KeyEventArgs e)
    {
      if (this.TextBox.CanPerformAutoComplete && (e.KeyCode == Keys.Up || e.KeyCode == Keys.Down) && (this.TextBox.Multiline && !this.TextBox.IsAutoCompleteDropDownOpen))
        return false;
      return base.ProcessListNavigation(e);
    }

    protected override bool ProcessEnterKey(KeyEventArgs e)
    {
      if (this.TextBox.CanPerformAutoComplete && this.TextBox.AcceptsReturn)
        return false;
      return base.ProcessEnterKey(e);
    }

    protected override bool SetCurrentCursor(Point location)
    {
      if (!(this.GetTextBlockAtPoint(location) is TokenizedTextBlockElement))
        return base.SetCurrentCursor(location);
      this.TextBox.ElementTree.Control.Cursor = Cursors.Default;
      return false;
    }

    protected ITextBlock GetTextBlockAtPoint(Point location)
    {
      for (RadElement radElement = this.TextBox.ElementTree.GetElementAtPoint(location, (Predicate<RadElement>) null); radElement != null; radElement = radElement.Parent)
      {
        ITextBlock textBlock = radElement as ITextBlock;
        if (textBlock != null)
          return textBlock;
      }
      return (ITextBlock) null;
    }
  }
}
