// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.TextBoxInputHandler
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Drawing;
using System.Windows.Forms;

namespace Telerik.WinControls.UI
{
  public class TextBoxInputHandler : ITextBoxInputHandler, IDisposable
  {
    private readonly RadTextBoxControlElement textBoxElement;
    private readonly Timer scrollTimer;
    private RadContextMenu defaultContextMenu;
    private Point? mouseDownLocation;

    public TextBoxInputHandler(RadTextBoxControlElement textBoxElement)
    {
      this.textBoxElement = textBoxElement;
      this.scrollTimer = new Timer();
      this.scrollTimer.Enabled = false;
      this.scrollTimer.Interval = 20;
      this.scrollTimer.Tick += new EventHandler(this.OnScrollTimerTick);
    }

    ~TextBoxInputHandler()
    {
      this.Dispose(false);
      GC.SuppressFinalize((object) this);
    }

    public void Dispose()
    {
      this.Dispose(true);
    }

    protected virtual void Dispose(bool disposing)
    {
      if (this.scrollTimer == null)
        return;
      this.scrollTimer.Tick -= new EventHandler(this.OnScrollTimerTick);
      this.scrollTimer.Dispose();
    }

    protected RadTextBoxControlElement TextBox
    {
      get
      {
        return this.textBoxElement;
      }
    }

    protected virtual bool ShouldHandleMouseInput(Point mousePosition)
    {
      if (this.textBoxElement.ControlBoundingRectangle.Contains(mousePosition) && !this.textBoxElement.VScrollBar.ControlBoundingRectangle.Contains(mousePosition))
        return !this.textBoxElement.HScrollBar.ControlBoundingRectangle.Contains(mousePosition);
      return false;
    }

    public virtual bool ProcessMouseDown(MouseEventArgs e)
    {
      this.mouseDownLocation = new Point?(e.Location);
      if (!this.ShouldHandleMouseInput(e.Location))
        return false;
      if (e.Button == MouseButtons.Left)
      {
        this.textBoxElement.Navigator.CaretPosition = this.textBoxElement.Navigator.GetPositionFromPoint((PointF) e.Location);
        if (this.textBoxElement.Multiline)
          this.textBoxElement.Navigator.ScrollToCaret();
        return true;
      }
      if (e.Button == MouseButtons.Right)
        return this.ProcessContextMenu(e.Location);
      return false;
    }

    public virtual bool ProcessMouseUp(MouseEventArgs e)
    {
      this.scrollTimer.Stop();
      this.mouseDownLocation = new Point?();
      return false;
    }

    public virtual bool ProcessMouseMove(MouseEventArgs e)
    {
      bool flag1 = e.Button == MouseButtons.Left;
      bool flag2 = this.mouseDownLocation.HasValue && this.ShouldHandleMouseInput(this.mouseDownLocation.Value);
      this.SetCurrentCursor(e.Location);
      if (!this.ShouldHandleMouseInput(e.Location))
      {
        if (flag1 && flag2 && !this.scrollTimer.Enabled)
          this.scrollTimer.Start();
        return false;
      }
      if (flag1 && flag2)
        return this.ProcessMouseSelection(e.Location);
      return false;
    }

    public virtual void ProcessMouseLeave(EventArgs e)
    {
      this.textBoxElement.ElementTree.Control.Cursor = Cursors.Default;
    }

    public virtual void PrcessMouseEnter(EventArgs e)
    {
    }

    protected virtual bool ProcessMouseSelection(Point location)
    {
      ITextBoxNavigator navigator = this.textBoxElement.Navigator;
      TextPosition positionFromPoint = navigator.GetPositionFromPoint((PointF) location);
      if (navigator.CaretPosition == (TextPosition) null)
        navigator.CaretPosition = positionFromPoint;
      else if (navigator.Select(navigator.SelectionStart, positionFromPoint))
        this.ClampSelection();
      return true;
    }

    private void ClampSelection()
    {
      ITextBoxNavigator navigator = this.textBoxElement.Navigator;
      TextPosition startPosition = navigator.SelectionStart;
      TextPosition endPosition = navigator.SelectionEnd;
      if (startPosition == (TextPosition) null || endPosition == (TextPosition) null)
        return;
      bool flag = false;
      TextPosition.Swap(ref startPosition, ref endPosition);
      if (TextBoxWrapPanel.IsLineFeed(startPosition.TextBlock.Text))
      {
        startPosition = navigator.GetNextPosition(startPosition);
        startPosition.CharPosition = 0;
        flag = true;
      }
      if (TextBoxWrapPanel.IsCarriageReturn(endPosition.TextBlock.Text))
      {
        endPosition = navigator.GetPreviousPosition(endPosition);
        endPosition.CharPosition = endPosition.TextBlock.Length;
        flag = true;
      }
      if (!flag)
        return;
      if (navigator.SelectionStart > navigator.SelectionEnd)
      {
        TextPosition textPosition = startPosition;
        startPosition = endPosition;
        endPosition = textPosition;
      }
      navigator.SuspendNotifications();
      navigator.Select(startPosition, endPosition);
      navigator.ResumeNotifications();
    }

    protected virtual bool SetCurrentCursor(Point location)
    {
      Control control = this.textBoxElement.ElementTree.Control;
      if (this.ShouldHandleMouseInput(location))
      {
        control.Cursor = Cursors.IBeam;
        return true;
      }
      control.Cursor = Cursors.Default;
      return false;
    }

    public virtual bool ProcessMouseWheel(MouseEventArgs e)
    {
      int num1 = Math.Max(1, e.Delta / SystemInformation.MouseWheelScrollDelta);
      int num2 = Math.Sign(e.Delta) * num1 * SystemInformation.MouseWheelScrollLines;
      TextBoxScroller vscroller = this.textBoxElement.ViewElement.VScroller;
      int num3 = vscroller.Value - num2 * vscroller.SmallChange;
      vscroller.Value = num3;
      return false;
    }

    protected virtual bool ProcessContextMenu(Point location)
    {
      RadContextMenu menu = this.textBoxElement.ContextMenu;
      if (menu == null)
      {
        if (this.defaultContextMenu != null)
          this.defaultContextMenu.Dispose();
        this.defaultContextMenu = (RadContextMenu) new TextBoxControlDefaultContextMenu(this.textBoxElement);
        menu = this.defaultContextMenu;
      }
      if (!this.textBoxElement.OnContextMenuOpenting(menu))
        return false;
      menu.Show(this.textBoxElement.ElementTree.Control, location);
      return true;
    }

    public virtual bool ProcessDoubleClick(EventArgs e)
    {
      ITextBoxNavigator navigator = this.textBoxElement.Navigator;
      TextPosition caretPosition = navigator.CaretPosition;
      if (caretPosition == (TextPosition) null)
        return false;
      LineInfo line = caretPosition.Line;
      ITextBlock textBlock = caretPosition.TextBlock;
      TextPosition wordStartPosition = this.GetWordStartPosition(line, textBlock);
      TextPosition wordEndPosition = this.GetWordEndPosition(line, textBlock);
      return navigator.Select(wordStartPosition, wordEndPosition);
    }

    private TextPosition GetWordStartPosition(
      LineInfo currentLine,
      ITextBlock currentBlock)
    {
      TextBoxViewElement viewElement = this.textBoxElement.ViewElement;
      bool flag1 = TextBoxWrapPanel.IsTabOrWhitespace(currentBlock.Text);
      bool flag2 = false;
      ITextBlock child;
      for (; currentBlock.Index > 0; currentBlock = child)
      {
        child = viewElement.Children[currentBlock.Index - 1] as ITextBlock;
        bool flag3 = TextBoxWrapPanel.IsTabOrWhitespace(child.Text);
        bool flag4 = TextBoxWrapPanel.IsCarriageReturn(child.Text);
        bool flag5 = TextBoxWrapPanel.IsLineFeed(child.Text);
        if (!flag4 && !flag5)
        {
          if (flag1)
          {
            if (!flag2 || !flag3)
            {
              if (!flag2)
                flag2 = !flag3;
            }
            else
              break;
          }
          else if (flag3)
            break;
        }
        else
          break;
      }
      currentLine = viewElement.Lines.BinarySearchByBlockIndex(currentBlock.Index);
      return new TextPosition(currentLine, currentBlock, 0);
    }

    private TextPosition GetWordEndPosition(
      LineInfo currentLine,
      ITextBlock currentBlock)
    {
      TextBoxViewElement viewElement = this.textBoxElement.ViewElement;
      bool flag1 = TextBoxWrapPanel.IsTabOrWhitespace(currentBlock.Text);
      bool flag2 = false;
      ITextBlock child;
      for (; currentBlock.Index < viewElement.Children.Count - 1; currentBlock = child)
      {
        child = viewElement.Children[currentBlock.Index + 1] as ITextBlock;
        bool flag3 = TextBoxWrapPanel.IsTabOrWhitespace(child.Text);
        bool flag4 = TextBoxWrapPanel.IsCarriageReturn(child.Text);
        bool flag5 = TextBoxWrapPanel.IsLineFeed(child.Text);
        if (!flag4 && !flag5)
        {
          if (flag1)
          {
            if (!flag3)
              break;
          }
          else if (!flag2 || flag3)
          {
            if (!flag2)
              flag2 = flag3;
          }
          else
            break;
        }
        else
          break;
      }
      currentLine = viewElement.Lines.BinarySearchByBlockIndex(currentBlock.Index);
      return new TextPosition(currentLine, currentBlock, currentBlock.Length);
    }

    public virtual bool ProcessKeyDown(KeyEventArgs e)
    {
      this.TextBox.Caret.SuspendBlinking();
      switch (e.KeyCode)
      {
        case Keys.Back:
          return this.ProcessDelete(false);
        case Keys.Tab:
          return this.ProcessTabKey(e);
        case Keys.Return:
          return this.ProcessEnterKey(e);
        case Keys.Prior:
        case Keys.Next:
          return this.ProcessPageKey(e);
        case Keys.End:
        case Keys.Home:
        case Keys.Left:
        case Keys.Up:
        case Keys.Right:
        case Keys.Down:
          return this.ProcessNavigationKey(e);
        case Keys.Delete:
          return this.ProcessDelete(true);
        case Keys.A:
          if (e.Control && !e.Alt)
            return this.ProcessSelectAll();
          break;
        case Keys.C:
          if (e.Control && !e.Alt)
            return this.ProcessCopy();
          break;
        case Keys.V:
          if (e.Control && !e.Alt)
            return this.ProcessPaste();
          break;
        case Keys.X:
          if (e.Control && !e.Alt)
            return this.ProcessCut();
          break;
      }
      return true;
    }

    protected virtual bool ProcessDelete(bool moveNext)
    {
      if (this.textBoxElement.IsReadOnly)
        return false;
      return this.textBoxElement.Delete(moveNext);
    }

    protected virtual bool ProcessSelectAll()
    {
      this.textBoxElement.SelectAll();
      return true;
    }

    protected virtual bool ProcessCopy()
    {
      return this.textBoxElement.Copy();
    }

    protected virtual bool ProcessPaste()
    {
      if (this.textBoxElement.IsReadOnly)
        return false;
      return this.textBoxElement.Paste();
    }

    protected virtual bool ProcessCut()
    {
      if (this.textBoxElement.IsReadOnly)
        return false;
      return this.textBoxElement.Cut();
    }

    protected virtual bool ProcessTabKey(KeyEventArgs e)
    {
      if (this.textBoxElement.Multiline && this.textBoxElement.AcceptsTab)
        return false;
      return this.SelectNextControl(!e.Shift);
    }

    protected virtual bool SelectNextControl(bool forward)
    {
      Control control = this.textBoxElement.ElementTree.Control;
      Form form = control.FindForm();
      if (form != null)
        return form.SelectNextControl(control, forward, true, true, true);
      return false;
    }

    protected virtual bool ProcessNavigationKey(KeyEventArgs e)
    {
      if (!this.ProcessListNavigation(e))
        return this.textBoxElement.Navigator.Navigate(e);
      return true;
    }

    protected virtual bool ProcessListNavigation(KeyEventArgs e)
    {
      if (!this.textBoxElement.CanPerformAutoComplete || e.KeyCode != Keys.Up && e.KeyCode != Keys.Down || this.textBoxElement.AutoCompleteMode == AutoCompleteMode.Suggest && !this.textBoxElement.IsAutoCompleteDropDownOpen)
        return false;
      this.textBoxElement.ListElement.ProcessKeyboardSelection(e.KeyCode);
      return true;
    }

    protected virtual bool ProcessEnterKey(KeyEventArgs e)
    {
      if (this.textBoxElement.CanPerformAutoComplete && !this.TextBox.Multiline)
      {
        TextPosition completePosition1 = this.textBoxElement.GetFirstAutoCompletePosition();
        TextPosition completePosition2 = this.textBoxElement.GetLastAutoCompletePosition();
        if (completePosition1 != (TextPosition) null && completePosition2 != (TextPosition) null)
          this.textBoxElement.Navigator.Select(completePosition1, completePosition2);
      }
      return true;
    }

    protected virtual bool ProcessPageKey(KeyEventArgs e)
    {
      if (this.textBoxElement.CanPerformAutoComplete)
        return this.textBoxElement.ListElement.ProcessKeyboardSelection(e.KeyCode);
      if (!this.textBoxElement.Multiline)
        return false;
      Keys keyCode = e.KeyCode;
      TextBoxScroller vscroller = this.textBoxElement.ViewElement.VScroller;
      int num1 = vscroller.Value;
      int height = (int) vscroller.ClientSize.Height;
      int num2 = keyCode != Keys.Prior ? num1 + height : num1 - height;
      PointF position = (PointF) this.textBoxElement.Caret.Position;
      vscroller.Value = num2;
      if (vscroller.Value != num2)
        return false;
      ITextBoxNavigator navigator = this.textBoxElement.Navigator;
      TextPosition positionFromPoint = navigator.GetPositionFromPoint(position);
      navigator.CaretPosition = positionFromPoint;
      return true;
    }

    public virtual bool ProcessKeyPress(KeyPressEventArgs e)
    {
      if (this.textBoxElement.IsReadOnly)
        return false;
      bool flag1 = e.KeyChar == '\r';
      bool flag2 = e.KeyChar == '\n';
      bool flag3 = e.KeyChar == '\t';
      if (char.IsControl(e.KeyChar) && (!this.textBoxElement.Multiline || flag3 && !this.textBoxElement.AcceptsTab || flag1 && !this.textBoxElement.AcceptsReturn || !flag1 && !flag2 && !flag3))
        return false;
      string text = flag1 || flag2 ? Environment.NewLine : e.KeyChar.ToString();
      if (string.IsNullOrEmpty(text))
        return false;
      switch (this.textBoxElement.CharacterCasing)
      {
        case CharacterCasing.Upper:
          text = text.ToUpper();
          break;
        case CharacterCasing.Lower:
          text = text.ToLower();
          break;
      }
      return this.ProcessInsert(text);
    }

    protected virtual bool ProcessInsert(string text)
    {
      return this.textBoxElement.Insert(text);
    }

    public virtual bool ProcessKeyUp(KeyEventArgs e)
    {
      this.TextBox.Caret.ResumeBlinking();
      return true;
    }

    private void OnScrollTimerTick(object sender, EventArgs e)
    {
      TextBoxViewElement viewElement = this.textBoxElement.ViewElement;
      Rectangle boundingRectangle = viewElement.ControlBoundingRectangle;
      Point point = viewElement.ElementTree == null ? Point.Empty : viewElement.ElementTree.Control.PointToClient(Control.MousePosition);
      if (viewElement.ElementTree == null || boundingRectangle.Contains(point))
      {
        this.scrollTimer.Stop();
      }
      else
      {
        if (this.textBoxElement.Multiline)
        {
          int num = 0;
          if (point.Y > boundingRectangle.Bottom)
            num = point.Y - boundingRectangle.Bottom;
          else if (point.Y < boundingRectangle.Y)
            num = point.Y - boundingRectangle.Y;
          if (num != 0)
          {
            viewElement.VScroller.Value += num;
            point.Y -= num;
          }
        }
        int num1 = 0;
        if (point.X < boundingRectangle.X)
          num1 = point.X - boundingRectangle.X;
        else if (point.X > boundingRectangle.Right)
          num1 = point.X - boundingRectangle.Right;
        if (num1 != 0)
        {
          viewElement.HScroller.Value += num1;
          point.X -= num1;
        }
        this.ProcessMouseSelection(point);
      }
    }
  }
}
