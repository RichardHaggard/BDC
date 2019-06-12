// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.RadCommandBarElement
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using System.Xml;
using Telerik.WinControls.Layouts;

namespace Telerik.WinControls.UI
{
  public class RadCommandBarElement : RadCommandBarVisualElement
  {
    private Size dragSize = SystemInformation.DragSize;
    protected RadCommandBarLinesElementCollection lines;
    protected StackLayoutPanel layoutPanel;
    private CommandBarStripInfoHolder stripInfoHolder;

    public event CancelEventHandler BeginDragging;

    public event MouseEventHandler Dragging;

    public event EventHandler EndDragging;

    public event EventHandler OrientationChanged;

    public event CancelEventHandler OrientationChanging;

    public event CancelEventHandler FloatingStripCreating;

    public event CancelEventHandler FloatingStripDocking;

    public event EventHandler FloatingStripCreated;

    public event EventHandler FloatingStripDocked;

    protected override SizeF MeasureOverride(SizeF availableSize)
    {
      SizeF sizeF = base.MeasureOverride(availableSize);
      if (float.IsInfinity(availableSize.Width))
        availableSize.Width = sizeF.Width;
      if (float.IsInfinity(availableSize.Height))
        availableSize.Height = sizeF.Height;
      return availableSize;
    }

    protected override SizeF ArrangeOverride(SizeF finalSize)
    {
      base.ArrangeOverride(finalSize);
      return finalSize;
    }

    protected override void InitializeFields()
    {
      base.InitializeFields();
      this.AllowDrop = true;
    }

    protected override void CreateChildElements()
    {
      base.CreateChildElements();
      this.stripInfoHolder = new CommandBarStripInfoHolder();
      this.Text = "";
      this.layoutPanel = new StackLayoutPanel();
      this.lines = new RadCommandBarLinesElementCollection((RadElement) this.layoutPanel);
      this.lines.ItemTypes = new System.Type[1]
      {
        typeof (CommandBarRowElement)
      };
      this.DrawBorder = false;
      this.DrawFill = true;
      int num = (int) this.SetDefaultValueOverride(RadElement.MinSizeProperty, (object) new Size(30, 30));
      this.Children.Add((RadElement) this.layoutPanel);
      this.StretchHorizontally = false;
      this.StretchVertically = false;
      this.SetOrientationCore(this.Orientation);
      this.WireEvents();
    }

    protected override void DisposeManagedResources()
    {
      this.UnwireEvents();
      base.DisposeManagedResources();
    }

    protected override void OnBubbleEvent(RadElement sender, RoutedEventArgs args)
    {
      base.OnBubbleEvent(sender, args);
      if (args.RoutedEvent == RadCommandBarGrip.BeginDraggingEvent)
      {
        CancelEventArgs originalEventArgs = (CancelEventArgs) args.OriginalEventArgs;
        this.OnBeginDragging((object) sender, originalEventArgs);
        args.Canceled = originalEventArgs.Cancel;
      }
      if (args.RoutedEvent == RadCommandBarGrip.EndDraggingEvent)
      {
        EventArgs originalEventArgs = args.OriginalEventArgs;
        this.OnEndDragging((object) sender, originalEventArgs);
      }
      if (args.RoutedEvent != RadCommandBarGrip.DraggingEvent)
        return;
      MouseEventArgs originalEventArgs1 = (MouseEventArgs) args.OriginalEventArgs;
      this.OnDragging((object) sender, originalEventArgs1);
    }

    protected override void OnTunnelEvent(RadElement sender, RoutedEventArgs args)
    {
      base.OnTunnelEvent(sender, args);
      if (args.RoutedEvent != RootRadElement.OnRoutedImageListChanged)
        return;
      RadControl control = this.ElementTree.Control as RadControl;
      if (control == null)
        return;
      foreach (CommandBarStripElement stripInfo in this.stripInfoHolder.StripInfoList)
      {
        if (stripInfo.FloatingForm != null)
          stripInfo.FloatingForm.ItemsHostControl.ImageList = control.ImageList;
      }
    }

    protected virtual bool OnFloatingStripCreating(object sender)
    {
      if (this.FloatingStripCreating == null)
        return false;
      CancelEventArgs e = new CancelEventArgs();
      this.FloatingStripCreating(sender, e);
      return e.Cancel;
    }

    protected virtual void OnFloatingStripCreated(object sender)
    {
      if (this.FloatingStripCreated == null)
        return;
      this.FloatingStripCreated(sender, EventArgs.Empty);
    }

    protected virtual bool OnFloatingStripDocking(object sender)
    {
      if (this.FloatingStripDocking == null)
        return false;
      CancelEventArgs e = new CancelEventArgs();
      this.FloatingStripDocking(sender, e);
      return e.Cancel;
    }

    internal bool CallOnFloatingStripDocking(object sender)
    {
      return this.OnFloatingStripDocking(sender);
    }

    internal void CallOnFloatingStripDocked(object sender)
    {
      this.OnFloatingStripDocked(sender);
    }

    protected virtual void OnFloatingStripDocked(object sender)
    {
      if (this.FloatingStripDocked == null)
        return;
      this.FloatingStripDocked(sender, EventArgs.Empty);
    }

    protected virtual void OnBeginDragging(object sender, CancelEventArgs args)
    {
      if (this.BeginDragging == null)
        return;
      this.BeginDragging(sender, args);
    }

    protected virtual void OnEndDragging(object sender, EventArgs args)
    {
      if (this.EndDragging == null)
        return;
      this.EndDragging(sender, args);
    }

    protected virtual void OnDragging(object sender, MouseEventArgs args)
    {
      if (this.Dragging == null)
        return;
      this.Dragging(sender, args);
    }

    protected virtual void OnOrientationChanged(EventArgs e)
    {
      if (this.OrientationChanged == null)
        return;
      this.OrientationChanged((object) this, e);
    }

    protected virtual bool OnOrientationChanging(CancelEventArgs e)
    {
      if (this.OrientationChanging == null)
        return false;
      this.OrientationChanging((object) this, e);
      return e.Cancel;
    }

    public void MoveToUpperLine(CommandBarStripElement element, CommandBarRowElement currentHolder)
    {
      int index = this.Rows.IndexOf(currentHolder) - 1;
      if (index < 0 && currentHolder.Strips.Count == 1)
      {
        this.CreateFloatingStrip(element, currentHolder, Control.MousePosition);
      }
      else
      {
        if (index < 0)
        {
          this.Rows.Insert(0, RadCommandBarToolstripsHolderFactory.CreateLayoutPanel(this));
          index = 0;
        }
        bool capture = element.Grip.Capture;
        currentHolder.Strips.Remove((RadItem) element);
        this.Rows[index].Strips.Add(element);
        if (currentHolder.Strips.Count == 0)
          this.Rows.Remove((RadItem) currentHolder);
        element.Grip.Capture = capture;
      }
    }

    public void MoveToDownerLine(CommandBarStripElement element, CommandBarRowElement currentHolder)
    {
      int index = this.Rows.IndexOf(currentHolder) + 1;
      if (index >= this.Rows.Count && currentHolder.Strips.Count == 1)
      {
        this.CreateFloatingStrip(element, currentHolder, Control.MousePosition);
      }
      else
      {
        if (index >= this.Rows.Count)
        {
          this.Rows.Add(RadCommandBarToolstripsHolderFactory.CreateLayoutPanel(this));
          index = this.Rows.Count - 1;
        }
        bool capture = element.Grip.Capture;
        currentHolder.Strips.Remove((RadItem) element);
        this.Rows[index].Strips.Add(element);
        if (currentHolder.Strips.Count == 0)
          this.Rows.Remove((RadItem) currentHolder);
        element.Grip.Capture = capture;
      }
    }

    public void SaveLayout(string filename)
    {
      this.SaveLayoutCore().Save(filename);
    }

    public void SaveLayout(Stream destination)
    {
      this.SaveLayoutCore().Save(destination);
    }

    public void SaveLayout(XmlWriter writer)
    {
      this.SaveLayoutCore().Save(writer);
    }

    public void LoadLayout(string filename)
    {
      XmlDocument doc = new XmlDocument();
      doc.LoadXml(File.ReadAllText(filename));
      this.LoadLayoutCore(doc);
    }

    public void LoadLayout(Stream source)
    {
      XmlDocument doc = new XmlDocument();
      doc.Load(source);
      this.LoadLayoutCore(doc);
    }

    public void LoadLayout(XmlReader xmlReader)
    {
      XmlDocument doc = new XmlDocument();
      doc.Load(xmlReader);
      this.LoadLayoutCore(doc);
    }

    public void CreateFloatingStrip(
      CommandBarStripElement stripElement,
      CommandBarRowElement currentRow,
      Point initialLocation)
    {
      if (!stripElement.EnableFloating || this.OnFloatingStripCreating((object) stripElement))
        return;
      CommandBarFloatingForm commandBarFloatingForm = new CommandBarFloatingForm();
      commandBarFloatingForm.ParentControl = this.ElementTree.Control.Parent;
      Point offset = Point.Empty;
      if (!this.RightToLeft)
      {
        commandBarFloatingForm.RightToLeft = RightToLeft.No;
        commandBarFloatingForm.Location = Point.Add(initialLocation, new Size(-5, -5));
        offset = new Point(5, 5);
      }
      else
      {
        commandBarFloatingForm.RightToLeft = RightToLeft.Yes;
        commandBarFloatingForm.Location = Point.Add(Point.Add(initialLocation, new Size(-stripElement.Size.Width, 0)), new Size(5, -5));
        offset = new Point(stripElement.Size.Width - 5, 5);
      }
      commandBarFloatingForm.Text = stripElement.DisplayName;
      stripElement.EnableDragging = false;
      stripElement.Capture = false;
      stripElement.ForceEndDrag();
      Size size = Screen.GetWorkingArea(this.ElementTree.Control).Size;
      stripElement.Orientation = Orientation.Horizontal;
      commandBarFloatingForm.ClientSize = stripElement.GetExpectedSize((SizeF) size).ToSize();
      stripElement.FloatingForm = commandBarFloatingForm;
      commandBarFloatingForm.StripElement = stripElement;
      currentRow?.Strips.Remove((RadItem) stripElement);
      commandBarFloatingForm.StripInfoHolder.AddStripInfo(stripElement);
      commandBarFloatingForm.Show();
      this.OnFloatingStripCreated((object) stripElement);
      commandBarFloatingForm.InitializeMove(offset);
      Cursor.Current = Cursors.SizeAll;
      if (currentRow == null || currentRow.Strips.Count != 0)
        return;
      this.Rows.Remove((RadItem) currentRow);
    }

    protected internal void SetOrientationCore(Orientation newOrientation)
    {
      switch (newOrientation)
      {
        case Orientation.Horizontal:
          this.layoutPanel.Orientation = Orientation.Vertical;
          this.layoutPanel.StretchHorizontally = false;
          this.layoutPanel.StretchVertically = true;
          break;
        case Orientation.Vertical:
          this.layoutPanel.Orientation = Orientation.Horizontal;
          this.layoutPanel.StretchHorizontally = true;
          this.layoutPanel.StretchVertically = false;
          break;
      }
      foreach (CommandBarRowElement line in this.lines)
        line.SetOrientationCore(newOrientation);
      base.Orientation = newOrientation;
    }

    protected virtual void WireEvents()
    {
      this.lines.ItemsChanged += new RadCommandBarLinesElementCollectionItemChangedDelegate(this.ItemsChanged);
    }

    protected virtual void UnwireEvents()
    {
      this.lines.ItemsChanged -= new RadCommandBarLinesElementCollectionItemChangedDelegate(this.ItemsChanged);
    }

    protected virtual void ItemsChanged(
      RadCommandBarLinesElementCollection changed,
      CommandBarRowElement target,
      ItemsChangeOperation operation)
    {
      if (operation != ItemsChangeOperation.Inserted)
        return;
      CommandBarRowElement commandBarRowElement = target;
      if (commandBarRowElement == null)
        return;
      commandBarRowElement.Owner = this;
      commandBarRowElement.Orientation = this.Orientation;
    }

    protected virtual XmlDocument SaveLayoutCore()
    {
      XmlDocument doc = new XmlDocument();
      XmlDeclaration xmlDeclaration = doc.CreateXmlDeclaration("1.0", (string) null, (string) null);
      doc.AppendChild((XmlNode) xmlDeclaration);
      XmlElement element1 = doc.CreateElement(nameof (RadCommandBarElement));
      element1.SetAttribute("Orientation", this.Orientation.ToString());
      element1.SetAttribute("Name", this.Name);
      element1.SetAttribute("RTL", this.RightToLeft.ToString());
      for (int index1 = 0; index1 < this.Rows.Count; ++index1)
      {
        CommandBarRowElement row = this.Rows[index1];
        XmlElement element2 = doc.CreateElement("CommandBarRowElement");
        element2.SetAttribute("Orientation", row.Orientation.ToString());
        element2.SetAttribute("Name", row.Name);
        element2.SetAttribute("LineIndex", index1.ToString());
        foreach (CommandBarStripElement strip in row.Strips)
        {
          XmlElement element3 = doc.CreateElement("CommandBarStripElement");
          element3.SetAttribute("Orientation", strip.Orientation.ToString());
          element3.SetAttribute("Name", strip.Name);
          element3.SetAttribute("DesiredLocationX", strip.DesiredLocation.X.ToString());
          element3.SetAttribute("DesiredLocationY", strip.DesiredLocation.Y.ToString());
          element3.SetAttribute("VisibleInCommandBar", strip.VisibleInCommandBar.ToString());
          element3.SetAttribute("StretchHorizontally", strip.StretchHorizontally.ToString());
          element3.SetAttribute("StretchVertically", strip.StretchVertically.ToString());
          element3.SetAttribute("EnableFloating", strip.EnableFloating.ToString());
          element3.SetAttribute("EnableDragging", strip.EnableDragging.ToString());
          int num = 0;
          for (int index2 = 0; index2 < strip.Items.Count; ++index2)
          {
            RadCommandBarBaseItem commandBarBaseItem = strip.Items[index2];
            XmlElement element4 = doc.CreateElement("RadCommandBarBaseItem");
            element4.SetAttribute("Orientation", commandBarBaseItem.Orientation.ToString());
            element4.SetAttribute("Name", commandBarBaseItem.Name);
            element4.SetAttribute("VisibleInStrip", commandBarBaseItem.VisibleInStrip.ToString());
            element4.SetAttribute("StretchHorizontally", commandBarBaseItem.StretchHorizontally.ToString());
            element4.SetAttribute("StretchVertically", commandBarBaseItem.StretchVertically.ToString());
            element4.SetAttribute("Index", num.ToString());
            element3.AppendChild((XmlNode) element4);
            ++num;
          }
          for (int index2 = 0; index2 < strip.OverflowButton.OverflowPanel.Layout.Children.Count; ++index2)
          {
            RadCommandBarBaseItem child = strip.OverflowButton.OverflowPanel.Layout.Children[index2] as RadCommandBarBaseItem;
            if (child != null)
            {
              XmlElement element4 = doc.CreateElement("RadCommandBarBaseItem");
              element4.SetAttribute("Orientation", child.Orientation.ToString());
              element4.SetAttribute("Name", child.Name);
              element4.SetAttribute("VisibleInStrip", child.VisibleInStrip.ToString());
              element4.SetAttribute("StretchHorizontally", child.StretchHorizontally.ToString());
              element4.SetAttribute("StretchVertically", child.StretchVertically.ToString());
              element4.SetAttribute("Index", num.ToString());
              element3.AppendChild((XmlNode) element4);
              ++num;
            }
          }
          element2.AppendChild((XmlNode) element3);
        }
        element1.AppendChild((XmlNode) element2);
      }
      XmlElement xmlElement = this.SaveFloatingStripsLayout(doc);
      element1.AppendChild((XmlNode) xmlElement);
      doc.AppendChild((XmlNode) element1);
      return doc;
    }

    private XmlElement SaveFloatingStripsLayout(XmlDocument doc)
    {
      XmlElement element1 = doc.CreateElement("FloatingStrips");
      foreach (CommandBarStripElement stripInfo in this.stripInfoHolder.StripInfoList)
      {
        if (stripInfo.FloatingForm != null && !stripInfo.FloatingForm.IsDisposed)
        {
          XmlElement element2 = doc.CreateElement("CommandBarStripElement");
          element2.SetAttribute("Orientation", stripInfo.Orientation.ToString());
          element2.SetAttribute("Name", stripInfo.Name);
          element2.SetAttribute("DesiredLocationX", stripInfo.DesiredLocation.X.ToString());
          element2.SetAttribute("DesiredLocationY", stripInfo.DesiredLocation.Y.ToString());
          element2.SetAttribute("FormLocationX", stripInfo.FloatingForm.Location.X.ToString());
          element2.SetAttribute("FormLocationY", stripInfo.FloatingForm.Location.Y.ToString());
          element2.SetAttribute("VisibleInCommandBar", stripInfo.VisibleInCommandBar.ToString());
          element2.SetAttribute("StretchHorizontally", stripInfo.StretchHorizontally.ToString());
          element2.SetAttribute("StretchVertically", stripInfo.StretchVertically.ToString());
          element2.SetAttribute("RTL", stripInfo.RightToLeft.ToString());
          element2.SetAttribute("EnableFloating", stripInfo.EnableFloating.ToString());
          element2.SetAttribute("EnableDragging", stripInfo.EnableDragging.ToString());
          int num = 0;
          for (int index = 0; index < stripInfo.Items.Count; ++index)
          {
            RadCommandBarBaseItem commandBarBaseItem = stripInfo.Items[index];
            XmlElement element3 = doc.CreateElement("RadCommandBarBaseItem");
            element3.SetAttribute("Orientation", commandBarBaseItem.Orientation.ToString());
            element3.SetAttribute("Name", commandBarBaseItem.Name);
            element3.SetAttribute("VisibleInStrip", commandBarBaseItem.VisibleInStrip.ToString());
            element3.SetAttribute("StretchHorizontally", commandBarBaseItem.StretchHorizontally.ToString());
            element3.SetAttribute("StretchVertically", commandBarBaseItem.StretchVertically.ToString());
            element3.SetAttribute("Index", num.ToString());
            element2.AppendChild((XmlNode) element3);
            ++num;
          }
          for (int index = 0; index < stripInfo.FloatingForm.ItemsHostControl.Element.Layout.Children.Count; ++index)
          {
            RadCommandBarBaseItem child = stripInfo.FloatingForm.ItemsHostControl.Element.Layout.Children[index] as RadCommandBarBaseItem;
            if (child != null)
            {
              XmlElement element3 = doc.CreateElement("RadCommandBarBaseItem");
              element3.SetAttribute("Orientation", child.Orientation.ToString());
              element3.SetAttribute("Name", child.Name);
              element3.SetAttribute("VisibleInStrip", child.VisibleInStrip.ToString());
              element3.SetAttribute("StretchHorizontally", child.StretchHorizontally.ToString());
              element3.SetAttribute("StretchVertically", child.StretchVertically.ToString());
              element3.SetAttribute("Index", num.ToString());
              element2.AppendChild((XmlNode) element3);
              ++num;
            }
          }
          element1.AppendChild((XmlNode) element2);
        }
      }
      return element1;
    }

    protected virtual void LoadLayoutCore(XmlDocument doc)
    {
      this.SuspendLayout(true);
      foreach (XmlNode childNode1 in doc.ChildNodes)
      {
        if (!(childNode1.Name != nameof (RadCommandBarElement)) && childNode1.Attributes["Name"] != null && !(childNode1.Attributes["Name"].Value != this.Name))
        {
          if (childNode1.Attributes["Orientation"] != null)
            this.Orientation = childNode1.Attributes["Orientation"].Value == "Vertical" ? Orientation.Vertical : Orientation.Horizontal;
          if (childNode1.Attributes["RTL"] != null)
            this.RightToLeft = childNode1.Attributes["RTL"].Value == "True";
          int num = 0;
          foreach (XmlNode childNode2 in childNode1.ChildNodes)
          {
            int result = 0;
            if (childNode2.Name == "CommandBarRowElement" && childNode2.Attributes["LineIndex"] != null && int.TryParse(childNode2.Attributes["LineIndex"].Value, out result))
              num = Math.Max(num, result);
          }
          while (num + 1 > this.lines.Count)
            this.lines.Add(new CommandBarRowElement());
          foreach (XmlNode childNode2 in childNode1.ChildNodes)
          {
            if (childNode2.Name == "CommandBarRowElement")
              this.LoadStripsLayout(childNode2, num);
            else if (childNode2.Name == "FloatingStrips")
              this.LoadFloatingStripsLayout(childNode2);
          }
        }
      }
      this.RemoveUnusedLines();
      this.ResumeLayout(true, true);
    }

    private void LoadStripsLayout(XmlNode lineNode, int maxLineIndex)
    {
      int result = 0;
      int.TryParse(lineNode.Attributes["LineIndex"].Value, out result);
      if (result < 0 || result > maxLineIndex)
        result = 0;
      foreach (XmlNode childNode1 in lineNode.ChildNodes)
      {
        if (!(childNode1.Name != "CommandBarStripElement"))
        {
          CommandBarStripElement stripByName = this.GetStripByName(childNode1.Attributes["Name"].Value);
          if (stripByName != null)
          {
            if (stripByName.FloatingForm != null && !stripByName.FloatingForm.IsDisposed)
              stripByName.FloatingForm.TryDocking(this.ElementTree.Control as RadCommandBar);
            if (stripByName.FloatingForm == null || stripByName.FloatingForm.IsDisposed)
            {
              stripByName.SuspendLayout(true);
              stripByName.EnableFloating = false;
              for (int index = 0; index < childNode1.Attributes.Count; ++index)
              {
                switch (childNode1.Attributes[index].Name)
                {
                  case "Orientation":
                    stripByName.Orientation = childNode1.Attributes["Orientation"].Value == "Vertical" ? Orientation.Vertical : Orientation.Horizontal;
                    break;
                  case "VisibleInCommandBar":
                    stripByName.VisibleInCommandBar = childNode1.Attributes["VisibleInCommandBar"].Value == "True";
                    break;
                  case "StretchHorizontally":
                    stripByName.StretchHorizontally = childNode1.Attributes["StretchHorizontally"].Value == "True";
                    break;
                  case "StretchVertically":
                    stripByName.StretchVertically = childNode1.Attributes["StretchVertically"].Value == "True";
                    break;
                  case "EnableFloating":
                    stripByName.EnableFloating = childNode1.Attributes["EnableFloating"].Value == "True";
                    break;
                  case "EnableDragging":
                    stripByName.EnableDragging = childNode1.Attributes["EnableDragging"].Value == "True";
                    break;
                }
              }
              if (childNode1.Attributes["DesiredLocationX"] != null && childNode1.Attributes["DesiredLocationY"] != null)
                stripByName.DesiredLocation = new PointF(float.Parse(childNode1.Attributes["DesiredLocationX"].Value), float.Parse(childNode1.Attributes["DesiredLocationY"].Value));
              (stripByName.Parent as CommandBarRowElement)?.Strips.Remove((RadItem) stripByName);
              this.lines[result].Strips.Add(stripByName);
              foreach (XmlNode childNode2 in childNode1.ChildNodes)
              {
                if (!(childNode2.Name != "RadCommandBarBaseItem"))
                {
                  RadCommandBarBaseItem itemByName = this.GetItemByName(childNode2.Attributes["Name"].Value);
                  if (itemByName != null)
                  {
                    for (int index1 = 0; index1 < childNode2.Attributes.Count; ++index1)
                    {
                      switch (childNode2.Attributes[index1].Name)
                      {
                        case "Orientation":
                          itemByName.Orientation = childNode2.Attributes["Orientation"].Value == "Vertical" ? Orientation.Vertical : Orientation.Horizontal;
                          break;
                        case "VisibleInStrip":
                          itemByName.VisibleInStrip = childNode2.Attributes["VisibleInStrip"].Value == "True";
                          break;
                        case "StretchHorizontally":
                          itemByName.StretchHorizontally = childNode2.Attributes["StretchHorizontally"].Value == "True";
                          break;
                        case "StretchVertically":
                          itemByName.StretchVertically = childNode2.Attributes["StretchVertically"].Value == "True";
                          break;
                        case "Index":
                          int index2 = int.Parse(childNode2.Attributes["Index"].Value);
                          if (index2 < stripByName.Items.Count)
                          {
                            itemByName.Parent.Children.Remove((RadElement) itemByName);
                            stripByName.Items.Remove((RadItem) itemByName);
                            stripByName.Items.Insert(index2, itemByName);
                            break;
                          }
                          break;
                      }
                    }
                  }
                }
              }
              stripByName.ResumeLayout(true, true);
              stripByName.EnableFloating = true;
            }
          }
        }
      }
    }

    private void LoadFloatingStripsLayout(XmlNode floatingStripsNode)
    {
      foreach (XmlNode childNode1 in floatingStripsNode.ChildNodes)
      {
        if (!(childNode1.Name != "CommandBarStripElement"))
        {
          CommandBarStripElement stripByName = this.GetStripByName(childNode1.Attributes["Name"].Value);
          if (stripByName != null)
          {
            stripByName.SuspendLayout(true);
            bool rightToLeft = this.RightToLeft;
            this.RightToLeft = false;
            for (int index = 0; index < childNode1.Attributes.Count; ++index)
            {
              switch (childNode1.Attributes[index].Name)
              {
                case "Orientation":
                  stripByName.Orientation = childNode1.Attributes["Orientation"].Value == "Vertical" ? Orientation.Vertical : Orientation.Horizontal;
                  break;
                case "VisibleInCommandBar":
                  stripByName.VisibleInCommandBar = childNode1.Attributes["VisibleInCommandBar"].Value == "True";
                  break;
                case "StretchHorizontally":
                  stripByName.StretchHorizontally = childNode1.Attributes["StretchHorizontally"].Value == "True";
                  break;
                case "StretchVertically":
                  stripByName.StretchVertically = childNode1.Attributes["StretchVertically"].Value == "True";
                  break;
                case "EnableFloating":
                  stripByName.EnableFloating = childNode1.Attributes["EnableFloating"].Value == "True";
                  break;
                case "EnableDragging":
                  stripByName.EnableDragging = childNode1.Attributes["EnableDragging"].Value == "True";
                  break;
              }
            }
            if (childNode1.Attributes["FormLocationX"] != null && childNode1.Attributes["FormLocationY"] != null)
            {
              Point initialLocation = new Point(int.Parse(childNode1.Attributes["FormLocationX"].Value), int.Parse(childNode1.Attributes["FormLocationY"].Value));
              if (stripByName.FloatingForm != null && !stripByName.FloatingForm.IsDisposed)
                stripByName.FloatingForm.Location = initialLocation;
              else
                this.CreateFloatingStrip(stripByName, stripByName.Parent as CommandBarRowElement, initialLocation);
            }
            this.RightToLeft = rightToLeft;
            if (stripByName.FloatingForm != null)
              stripByName.FloatingForm.EndMove();
            if (childNode1.Attributes["DesiredLocationX"] != null && childNode1.Attributes["DesiredLocationY"] != null)
              stripByName.DesiredLocation = new PointF(float.Parse(childNode1.Attributes["DesiredLocationX"].Value), float.Parse(childNode1.Attributes["DesiredLocationY"].Value));
            foreach (XmlNode childNode2 in childNode1.ChildNodes)
            {
              if (!(childNode2.Name != "RadCommandBarBaseItem"))
              {
                RadCommandBarBaseItem itemByName = this.GetItemByName(childNode2.Attributes["Name"].Value);
                if (itemByName != null)
                {
                  for (int index1 = 0; index1 < childNode2.Attributes.Count; ++index1)
                  {
                    switch (childNode2.Attributes[index1].Name)
                    {
                      case "Orientation":
                        itemByName.Orientation = childNode2.Attributes["Orientation"].Value == "Vertical" ? Orientation.Vertical : Orientation.Horizontal;
                        break;
                      case "VisibleInStrip":
                        itemByName.VisibleInStrip = childNode2.Attributes["VisibleInStrip"].Value == "True";
                        break;
                      case "StretchHorizontally":
                        itemByName.StretchHorizontally = childNode2.Attributes["StretchHorizontally"].Value == "True";
                        break;
                      case "StretchVertically":
                        itemByName.StretchVertically = childNode2.Attributes["StretchVertically"].Value == "True";
                        break;
                      case "Index":
                        int index2 = int.Parse(childNode2.Attributes["Index"].Value);
                        itemByName.Parent.Children.Remove((RadElement) itemByName);
                        stripByName.Items.Remove((RadItem) itemByName);
                        if (stripByName.FloatingForm != null && !stripByName.FloatingForm.IsDisposed)
                        {
                          stripByName.FloatingForm.ItemsHostControl.Element.Layout.Children.Insert(index2, (RadElement) itemByName);
                          break;
                        }
                        if (index2 >= stripByName.Items.Count)
                        {
                          stripByName.Items.Add(itemByName);
                          break;
                        }
                        stripByName.Items.Insert(index2, itemByName);
                        break;
                    }
                  }
                }
              }
            }
            stripByName.ResumeLayout(true, true);
          }
        }
      }
    }

    private void RemoveUnusedLines()
    {
      List<CommandBarRowElement> commandBarRowElementList = new List<CommandBarRowElement>();
      foreach (CommandBarRowElement line in this.lines)
      {
        if (line.Children.Count == 0)
          commandBarRowElementList.Add(line);
      }
      foreach (RadItem radItem in commandBarRowElementList)
        this.lines.Remove(radItem);
    }

    private CommandBarStripElement GetStripByName(string name)
    {
      foreach (CommandBarStripElement stripInfo in this.stripInfoHolder.StripInfoList)
      {
        if (stripInfo.Name == name)
          return stripInfo;
      }
      return (CommandBarStripElement) null;
    }

    private RadCommandBarBaseItem GetItemByName(string name)
    {
      foreach (CommandBarStripElement stripInfo in this.stripInfoHolder.StripInfoList)
      {
        foreach (RadCommandBarBaseItem commandBarBaseItem in stripInfo.Items)
        {
          if (commandBarBaseItem.Name == name)
            return commandBarBaseItem;
        }
        foreach (RadElement child in stripInfo.OverflowButton.ItemsLayout.Children)
        {
          if (child is RadCommandBarBaseItem && (child as RadCommandBarBaseItem).Name == name)
            return child as RadCommandBarBaseItem;
        }
      }
      return (RadCommandBarBaseItem) null;
    }

    public CommandBarStripInfoHolder StripInfoHolder
    {
      get
      {
        return this.stripInfoHolder;
      }
      set
      {
        this.stripInfoHolder = value;
      }
    }

    [DefaultValue(typeof (Size), "4,4")]
    [Description("Gets or sets the size in pixels when current strip is being Drag and Drop in next or previous row")]
    public Size DragSize
    {
      get
      {
        return this.dragSize;
      }
      set
      {
        this.dragSize = value;
      }
    }

    [DefaultValue(Orientation.Horizontal)]
    [Browsable(false)]
    [Description("Gets or sets the orientation of the RadCommandBarElement")]
    public override Orientation Orientation
    {
      get
      {
        return base.Orientation;
      }
      set
      {
        if (this.Orientation == value || this.OnOrientationChanging(new CancelEventArgs()))
          return;
        this.SetOrientationCore(value);
        this.OnOrientationChanged(new EventArgs());
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [RefreshProperties(RefreshProperties.All)]
    [RadEditItemsAction]
    [RadNewItem("", false, true, true)]
    public RadCommandBarLinesElementCollection Rows
    {
      get
      {
        return this.lines;
      }
    }
  }
}
