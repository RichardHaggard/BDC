// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.ChatToolbarElement
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Telerik.WinControls.UI
{
  public class ChatToolbarElement : HorizontalScrollableStackElement
  {
    private bool scrollLeft;
    private Timer scrollTimer;
    private RadChatElement chatElement;
    private ChatToolbarScrollLeftButtonElement scrollLeftElement;
    private ChatToolbarScrollRightButtonElement scrollRightElement;
    private SizeF availableSize;
    private SizeF finalSize;

    protected override void CreateChildElements()
    {
      base.CreateChildElements();
      this.scrollLeftElement = this.CreateScrollLeftElement();
      this.scrollRightElement = this.CreateScrollRightElement();
      this.Children.Add((RadElement) this.scrollLeftElement);
      this.Children.Add((RadElement) this.scrollRightElement);
    }

    protected virtual ChatToolbarScrollLeftButtonElement CreateScrollLeftElement()
    {
      ChatToolbarScrollLeftButtonElement leftButtonElement = new ChatToolbarScrollLeftButtonElement();
      leftButtonElement.CaptureOnMouseDown = true;
      leftButtonElement.DrawFill = true;
      leftButtonElement.GradientStyle = GradientStyles.Solid;
      leftButtonElement.BackColor = Color.FromArgb(128, Color.Gray);
      leftButtonElement.Text = "❮";
      leftButtonElement.Visibility = ElementVisibility.Collapsed;
      return leftButtonElement;
    }

    protected virtual ChatToolbarScrollRightButtonElement CreateScrollRightElement()
    {
      ChatToolbarScrollRightButtonElement rightButtonElement = new ChatToolbarScrollRightButtonElement();
      rightButtonElement.CaptureOnMouseDown = true;
      rightButtonElement.DrawFill = true;
      rightButtonElement.GradientStyle = GradientStyles.Solid;
      rightButtonElement.BackColor = Color.FromArgb(128, Color.Gray);
      rightButtonElement.Text = "❯";
      rightButtonElement.Visibility = ElementVisibility.Collapsed;
      return rightButtonElement;
    }

    public ChatToolbarElement(RadChatElement chatElement)
    {
      this.chatElement = chatElement;
      this.ScrollBar.ValueChanged += new EventHandler(this.ScrollBar_ValueChanged);
      this.scrollLeftElement.MouseDown += new MouseEventHandler(this.ScrollLeftElement_MouseDown);
      this.scrollLeftElement.MouseUp += new MouseEventHandler(this.ScrollLeftElement_MouseUp);
      this.scrollRightElement.MouseDown += new MouseEventHandler(this.ScrollRightElement_MouseDown);
      this.scrollRightElement.MouseUp += new MouseEventHandler(this.ScrollRightElement_MouseUp);
      this.scrollTimer = new Timer();
      this.scrollTimer.Interval = 10;
      this.scrollTimer.Tick += new EventHandler(this.ScrollTimer_Tick);
    }

    protected override void DisposeManagedResources()
    {
      base.DisposeManagedResources();
      this.scrollTimer.Tick -= new EventHandler(this.ScrollTimer_Tick);
      this.scrollTimer.Dispose();
    }

    public RadChatElement ChatElement
    {
      get
      {
        return this.chatElement;
      }
    }

    public ChatToolbarScrollLeftButtonElement ScrollLeftElement
    {
      get
      {
        return this.scrollLeftElement;
      }
    }

    public ChatToolbarScrollRightButtonElement ScrollRightElement
    {
      get
      {
        return this.scrollRightElement;
      }
    }

    public void AddToolbarActions(IEnumerable<ToolbarActionDataItem> items)
    {
      foreach (ToolbarActionDataItem toolbarActionDataItem in items)
        this.AddToolbarAction(toolbarActionDataItem);
    }

    public void AddToolbarAction(ToolbarActionDataItem item)
    {
      ToolbarActionElement toolbarActionElement = this.ChatElement.ChatFactory.CreateToolbarActionElement(item);
      this.ItemsLayout.Children.Add((RadElement) toolbarActionElement);
      toolbarActionElement.Click += new EventHandler(this.Element_Click);
    }

    public void ClearToolbarActions()
    {
      foreach (RadElement child in this.ItemsLayout.Children)
        child.Click -= new EventHandler(this.Element_Click);
      this.ItemsLayout.Children.Clear();
    }

    protected virtual void UpdateScrollButtonsVisibility()
    {
      if (this.availableSize == SizeF.Empty || this.finalSize == SizeF.Empty)
        return;
      if ((double) this.availableSize.Width > (double) this.finalSize.Width)
      {
        this.scrollLeftElement.Visibility = ElementVisibility.Collapsed;
        this.scrollRightElement.Visibility = ElementVisibility.Collapsed;
      }
      else
      {
        this.scrollLeftElement.Visibility = ElementVisibility.Visible;
        this.scrollRightElement.Visibility = ElementVisibility.Visible;
        if (this.ScrollBar.Value == 0)
          this.scrollLeftElement.Visibility = ElementVisibility.Collapsed;
        if (this.ScrollBar.Value != this.ScrollBar.Maximum - this.ScrollBar.LargeChange + 1)
          return;
        this.scrollRightElement.Visibility = ElementVisibility.Collapsed;
      }
    }

    protected override void UpdateScrollBar(SizeF availableSize)
    {
      base.UpdateScrollBar(availableSize);
      this.UpdateScrollButtonsVisibility();
    }

    private void Element_Click(object sender, EventArgs e)
    {
      this.ChatElement.OnToolbarActionClick(new ToolbarActionEventArgs((sender as ToolbarActionElement).DataItem));
    }

    private void ScrollLeftElement_MouseUp(object sender, MouseEventArgs e)
    {
      this.scrollTimer.Stop();
    }

    private void ScrollLeftElement_MouseDown(object sender, MouseEventArgs e)
    {
      this.scrollLeft = true;
      this.scrollTimer.Start();
    }

    private void ScrollRightElement_MouseUp(object sender, MouseEventArgs e)
    {
      this.scrollTimer.Stop();
    }

    private void ScrollRightElement_MouseDown(object sender, MouseEventArgs e)
    {
      this.scrollLeft = false;
      this.scrollTimer.Start();
    }

    private void ScrollTimer_Tick(object sender, EventArgs e)
    {
      if (this.scrollLeft)
        this.ScrollBar.PerformSmallDecrement(4);
      else
        this.ScrollBar.PerformSmallIncrement(4);
    }

    private void ScrollBar_ValueChanged(object sender, EventArgs e)
    {
      this.UpdateScrollButtonsVisibility();
    }

    protected override SizeF MeasureOverride(SizeF availableSize)
    {
      this.availableSize = availableSize;
      return base.MeasureOverride(availableSize);
    }

    protected override SizeF ArrangeOverride(SizeF finalSize)
    {
      this.finalSize = finalSize;
      SizeF sizeF = base.ArrangeOverride(finalSize);
      this.scrollLeftElement.Arrange(new RectangleF(0.0f, 0.0f, this.scrollLeftElement.DesiredSize.Width, finalSize.Height));
      this.scrollRightElement.Arrange(new RectangleF(this.availableSize.Width - this.scrollRightElement.DesiredSize.Width, 0.0f, this.scrollRightElement.DesiredSize.Width, finalSize.Height));
      return sizeF;
    }
  }
}
