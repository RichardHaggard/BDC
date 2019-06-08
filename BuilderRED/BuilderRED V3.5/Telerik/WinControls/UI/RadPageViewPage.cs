// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.RadPageViewPage
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.ComponentModel;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;
using System.Windows.Forms.Layout;
using Telerik.WinControls.CodedUI;

namespace Telerik.WinControls.UI
{
  [ToolboxItem(false)]
  [Designer("Telerik.WinControls.UI.Design.RadPageViewPageDesigner, Telerik.WinControls.UI.Design, Version=2018.3.1016.20, Culture=neutral, PublicKeyToken=5bb2a467cbec794e")]
  public class RadPageViewPage : Panel, IPCHost
  {
    private RadPageView owner;
    private RadPageViewItem item;
    private Color localBackColor;
    private bool isContentVisible;
    private string title;
    private string description;
    private string toolTipText;
    private ContentAlignment textAlignment;
    private ContentAlignment imageAlignment;
    private TextImageRelation textImageRelation;
    private Image image;
    private RadObject stylePlaceholder;
    private IntPtr ipcContext;

    public RadPageViewPage()
    {
      this.SetStyle(ControlStyles.SupportsTransparentBackColor | ControlStyles.OptimizedDoubleBuffer, true);
      this.Visible = false;
      this.stylePlaceholder = new RadObject();
      this.TextImageRelation = TextImageRelation.ImageBeforeText;
      this.ImageAlignment = ContentAlignment.MiddleLeft;
      this.TextAlignment = ContentAlignment.MiddleLeft;
      this.stylePlaceholder.PropertyValues.SetLocalValuesAsDefault();
    }

    public RadPageViewPage(string text)
      : this()
    {
      this.Text = text;
    }

    protected internal virtual void Attach(RadPageView view)
    {
      this.owner = view;
      this.CallBackColorChanged();
    }

    protected internal virtual void Detach()
    {
      this.owner = (RadPageView) null;
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing)
        this.Detach();
      RadPageViewItem radPageViewItem = this.item;
      base.Dispose(disposing);
      radPageViewItem?.Dispose();
    }

    [System.ComponentModel.Description("Gets or sets the title of the Page. Title appears in the Header area of the owning RadPageView.")]
    [DefaultValue(TextImageRelation.ImageBeforeText)]
    [Category("Appearance")]
    public TextImageRelation TextImageRelation
    {
      get
      {
        return this.textImageRelation;
      }
      set
      {
        this.textImageRelation = value;
        int num = (int) this.stylePlaceholder.SetValue(LightVisualElement.TextImageRelationProperty, (object) value);
        this.UpdateItemStyle();
        if (this.textImageRelation == value)
          return;
        this.OnTextImageRelationChanged(EventArgs.Empty);
      }
    }

    [DefaultValue(ContentAlignment.MiddleLeft)]
    [Category("Appearance")]
    [System.ComponentModel.Description("Gets or sets the title of the Page. Title appears in the Header area of the owning RadPageView.")]
    public ContentAlignment ImageAlignment
    {
      get
      {
        return this.imageAlignment;
      }
      set
      {
        this.imageAlignment = value;
        int num = (int) this.stylePlaceholder.SetValue(LightVisualElement.ImageAlignmentProperty, (object) value);
        this.UpdateItemStyle();
        if (this.imageAlignment == value)
          return;
        this.OnImageAlignmentChanged(EventArgs.Empty);
      }
    }

    [Category("Appearance")]
    [DefaultValue(ContentAlignment.MiddleLeft)]
    [System.ComponentModel.Description("Gets or sets the title of the Page. Title appears in the Header area of the owning RadPageView.")]
    public ContentAlignment TextAlignment
    {
      get
      {
        return this.textAlignment;
      }
      set
      {
        this.textAlignment = value;
        int num = (int) this.stylePlaceholder.SetValue(LightVisualElement.TextAlignmentProperty, (object) value);
        this.UpdateItemStyle();
        if (this.textAlignment == value)
          return;
        this.OnTextAlignmentChanged(EventArgs.Empty);
      }
    }

    [System.ComponentModel.Description("Gets or sets the image to be displayed by the associated RadPageViewItem instance.")]
    [DefaultValue(null)]
    public Image Image
    {
      get
      {
        return this.image;
      }
      set
      {
        this.image = value;
        int num = (int) this.stylePlaceholder.SetValue(LightVisualElement.ImageProperty, (object) value);
        this.UpdateItemStyle();
        if (this.image == value)
          return;
        this.OnImageChanged(EventArgs.Empty);
      }
    }

    [System.ComponentModel.Description("Gets or sets the title of the Page. Title appears in the Header area of the owning RadPageView.")]
    public string Title
    {
      get
      {
        return this.title;
      }
      set
      {
        if (this.title == value)
          return;
        this.title = value;
        this.OnTitleChanged(EventArgs.Empty);
      }
    }

    private bool ShouldSerializeTitle()
    {
      return !string.IsNullOrEmpty(this.title);
    }

    [System.ComponentModel.Description("Gets or sets the title of the Page. Title appears in the Header area of the owning RadPageView.")]
    public string Description
    {
      get
      {
        return this.description;
      }
      set
      {
        if (this.description == value)
          return;
        this.description = value;
        this.OnDescriptionChanged(EventArgs.Empty);
      }
    }

    private bool ShouldSerializeDescription()
    {
      return !string.IsNullOrEmpty(this.title);
    }

    [System.ComponentModel.Description("Gets or sets the length of the current page. The length represents the fixed amount of space in pixels the page will take when the layout of the control is performed. Note: This property is only functional when the control is in ExplorerBar mode and its content size mode is set to FixedLength.")]
    public int PageLength
    {
      get
      {
        if (this.item != null)
          return this.item.PageLength;
        return -1;
      }
      set
      {
        if (this.item == null)
          return;
        this.item.PageLength = value;
      }
    }

    private bool ShouldSerializePageLength()
    {
      if (this.owner == null)
        return false;
      RadPageViewExplorerBarElement viewElement = this.owner.ViewElement as RadPageViewExplorerBarElement;
      return viewElement != null && this.item != null && (viewElement.ContentSizeMode == ExplorerBarContentSizeMode.FixedLength && this.item.PageLength != 300);
    }

    [Browsable(false)]
    public bool IsContentVisible
    {
      get
      {
        return this.isContentVisible;
      }
      set
      {
        if (value == this.isContentVisible)
          return;
        this.isContentVisible = value;
        this.OnIsContentVisibleChanged();
      }
    }

    protected virtual void OnIsContentVisibleChanged()
    {
      if (this.item == null)
        return;
      this.item.IsContentVisible = this.isContentVisible;
    }

    private bool ShouldSerializeIsContentVisible()
    {
      if (this.owner == null || !(this.owner.ViewElement is RadPageViewExplorerBarElement))
        return false;
      return this.isContentVisible;
    }

    [Localizable(true)]
    [System.ComponentModel.Description("Gets or sets the tooltip to be displayed when the item hovers page's associated item.")]
    public string ToolTipText
    {
      get
      {
        return this.toolTipText;
      }
      set
      {
        if (this.toolTipText == value)
          return;
        this.toolTipText = value;
        this.OnToolTipTextChanged(EventArgs.Empty);
      }
    }

    private bool ShouldSerializeToolTipText()
    {
      return !string.IsNullOrEmpty(this.toolTipText);
    }

    public override Color BackColor
    {
      get
      {
        if (this.localBackColor != Color.Empty)
          return this.localBackColor;
        if (this.owner != null)
        {
          if (this.owner.PageBackColor != Color.Empty)
            return this.owner.PageBackColor;
          RadPageViewContentAreaElement contentAreaForItem = this.owner.ViewElement.GetContentAreaForItem(this.item);
          if (contentAreaForItem != null && contentAreaForItem.ElementState == ElementState.Loaded)
            return contentAreaForItem.BackColor;
        }
        return base.BackColor;
      }
      set
      {
        Color backColor = this.BackColor;
        this.localBackColor = value;
        base.BackColor = value;
        if (!(backColor != this.BackColor))
          return;
        this.OnBackColorChanged(EventArgs.Empty);
      }
    }

    private bool ShouldSerializeBackColor()
    {
      return this.localBackColor != Color.Empty;
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public override DockStyle Dock
    {
      get
      {
        return base.Dock;
      }
      set
      {
        base.Dock = DockStyle.None;
      }
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public new bool Visible
    {
      get
      {
        return base.Visible;
      }
      set
      {
        base.Visible = value;
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public new int TabIndex
    {
      get
      {
        return base.TabIndex;
      }
      set
      {
        base.TabIndex = value;
      }
    }

    [Browsable(true)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public override string Text
    {
      get
      {
        return base.Text;
      }
      set
      {
        base.Text = value;
      }
    }

    [Browsable(false)]
    public RadPageView Owner
    {
      get
      {
        return this.owner;
      }
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public RadPageViewItem Item
    {
      get
      {
        return this.item;
      }
      internal set
      {
        this.item = value;
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public override AnchorStyles Anchor
    {
      get
      {
        return base.Anchor;
      }
      set
      {
        base.Anchor = value;
      }
    }

    [Browsable(true)]
    [System.ComponentModel.Description("Gets or sets the size of the item of RadPageView.This size will be used in is PageViewItemSizeMode.Individual mode.")]
    [DefaultValue(typeof (SizeF), "0,0")]
    public virtual SizeF ItemSize
    {
      get
      {
        if (this.item != null)
          return this.item.ForcedLayoutSize;
        return SizeF.Empty;
      }
      set
      {
        if (this.item == null)
          return;
        this.item.ForcedLayoutSize = value;
      }
    }

    protected internal virtual void CallBackColorChanged()
    {
      this.OnBackColorChanged(EventArgs.Empty);
    }

    public override string ToString()
    {
      return "RadPageViewPage [" + this.Text + "]";
    }

    protected override void OnEnabledChanged(EventArgs e)
    {
      base.OnEnabledChanged(e);
      if (this.item == null)
        return;
      this.item.Enabled = this.Enabled;
    }

    protected internal virtual void OnPageBackColorChanged(EventArgs e)
    {
      this.OnBackColorChanged(EventArgs.Empty);
    }

    protected virtual void OnTextImageRelationChanged(EventArgs e)
    {
    }

    protected virtual void OnImageAlignmentChanged(EventArgs e)
    {
    }

    protected virtual void OnTextAlignmentChanged(EventArgs e)
    {
    }

    protected virtual void OnImageChanged(EventArgs e)
    {
    }

    protected virtual void OnDescriptionChanged(EventArgs e)
    {
      if (this.item == null)
        return;
      this.item.Description = this.description;
    }

    protected virtual void OnToolTipTextChanged(EventArgs e)
    {
      if (this.item == null)
        return;
      this.item.ToolTipText = this.toolTipText;
    }

    protected virtual void OnTitleChanged(EventArgs e)
    {
      if (this.item == null)
        return;
      this.item.Title = this.title;
    }

    protected override void OnTextChanged(EventArgs e)
    {
      base.OnTextChanged(e);
      if (this.item == null)
        return;
      this.item.Text = this.Text;
    }

    public void SetValue(RadProperty property, object value)
    {
      int num = (int) this.stylePlaceholder.SetValue(property, value);
    }

    public void ResetValue(RadProperty property)
    {
      this.ResetValue(property, ValueResetFlags.All);
    }

    public void ResetValue(RadProperty property, ValueResetFlags flags)
    {
      int num = (int) this.stylePlaceholder.ResetValue(property, flags);
      this.UpdateItemStyle();
    }

    public void UpdateItemStyle()
    {
      if (this.Item == null)
        return;
      if (this.stylePlaceholder.GetValueSource(LightVisualElement.ImageProperty) == ValueSource.Local)
      {
        this.Item.Image = this.Image;
      }
      else
      {
        int num1 = (int) this.Item.ResetValue(LightVisualElement.ImageProperty, ValueResetFlags.Local);
      }
      if (this.stylePlaceholder.GetValueSource(LightVisualElement.ImageAlignmentProperty) == ValueSource.Local)
      {
        this.Item.ImageAlignment = this.ImageAlignment;
      }
      else
      {
        int num2 = (int) this.Item.ResetValue(LightVisualElement.ImageAlignmentProperty, ValueResetFlags.Local);
      }
      if (this.stylePlaceholder.GetValueSource(LightVisualElement.TextAlignmentProperty) == ValueSource.Local)
      {
        this.Item.TextAlignment = this.TextAlignment;
      }
      else
      {
        int num3 = (int) this.Item.ResetValue(LightVisualElement.TextAlignmentProperty, ValueResetFlags.Local);
      }
      if (this.stylePlaceholder.GetValueSource(LightVisualElement.TextImageRelationProperty) == ValueSource.Local)
      {
        this.Item.TextImageRelation = this.TextImageRelation;
      }
      else
      {
        int num4 = (int) this.Item.ResetValue(LightVisualElement.TextImageRelationProperty, ValueResetFlags.Local);
      }
    }

    internal bool HasFocusedChildControl()
    {
      foreach (Control control in (ArrangedElementCollection) this.Controls)
      {
        if (control.ContainsFocus)
          return true;
      }
      return false;
    }

    protected override void OnControlAdded(ControlEventArgs e)
    {
      base.OnControlAdded(e);
      e.Control.GotFocus += new EventHandler(this.Control_GotFocus);
    }

    protected override void OnControlRemoved(ControlEventArgs e)
    {
      base.OnControlRemoved(e);
      e.Control.GotFocus -= new EventHandler(this.Control_GotFocus);
    }

    private void Control_GotFocus(object sender, EventArgs e)
    {
      if (this.owner == null || !(this.item.Owner is RadPageViewExplorerBarElement) || !this.owner.autoScroll)
        return;
      ((RadPageViewExplorerBarElement) this.item.Owner).ProcessAutoScroll((Control) sender, false);
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    public IntPtr Context
    {
      get
      {
        return this.ipcContext;
      }
      set
      {
        this.ipcContext = value;
      }
    }

    protected override void WndProc(ref Message m)
    {
      if (IPCServer.ProcessRequest(ref m, (IPCHost) this))
        return;
      base.WndProc(ref m);
    }

    protected virtual void ProcessCodedUIMessage(ref IPCMessage request)
    {
      if (request == null)
        return;
      if (request.Type == IPCMessage.MessageTypes.GetChildPropertyValue)
      {
        if (request.Message == "Selected")
        {
          bool flag = this.Owner.SelectedPage == this;
          request.Data = (object) flag;
        }
        else
        {
          PropertyInfo property = this.GetType().GetProperty(request.Message, BindingFlags.Instance | BindingFlags.Public);
          if ((object) property == null)
            return;
          request.Data = property.GetValue((object) this, new object[0]);
        }
      }
      else
      {
        if (request.Type != IPCMessage.MessageTypes.SetPropertyValue)
          return;
        PropertyInfo property = this.GetType().GetProperty(request.Message, BindingFlags.Instance | BindingFlags.Public);
        if ((object) property == null)
          return;
        property.SetValue((object) this, request.Data, new object[0]);
      }
    }

    public void ProcessMessage(ref IPCMessage request)
    {
      this.ProcessCodedUIMessage(ref request);
    }
  }
}
