// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.BackstageTabItem
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Telerik.WinControls.Styles;

namespace Telerik.WinControls.UI
{
  public class BackstageTabItem : BackstageVisualElement
  {
    public static readonly RadProperty SelectedProperty = RadProperty.Register(nameof (Selected), typeof (bool), typeof (BackstageTabItem), (RadPropertyMetadata) new RadElementPropertyMetadata((object) false, ElementPropertyOptions.AffectsMeasure | ElementPropertyOptions.AffectsArrange));
    private BackstageViewPage page;
    private bool cachedSelected;

    static BackstageTabItem()
    {
      ItemStateManagerFactoryRegistry.AddStateManagerFactory((ItemStateManagerFactoryBase) new BackstageTabItem.BackstageTabItemStateManagerFactory(), typeof (BackstageTabItem));
    }

    public BackstageTabItem()
    {
    }

    public BackstageTabItem(string text)
    {
      this.Text = text;
    }

    [Browsable(false)]
    [Description("Indicates whether this tab is selected.")]
    public bool Selected
    {
      get
      {
        return this.cachedSelected;
      }
      internal set
      {
        if (this.cachedSelected == value)
          return;
        this.cachedSelected = value;
        int num = (int) this.SetValue(BackstageTabItem.SelectedProperty, (object) value);
        this.OnSelectedChanged();
      }
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    [Description("Gets or sets the page that is associated with this tab item.")]
    public BackstageViewPage Page
    {
      get
      {
        if (this.page == null)
          this.SetPageCore(new BackstageViewPage(), false);
        return this.page;
      }
      set
      {
        this.SetPageCore(value, true);
      }
    }

    public event EventHandler SelectedChanged;

    public event EventHandler PageChagned;

    protected virtual void OnSelectedChanged()
    {
      if (this.SelectedChanged == null)
        return;
      this.SelectedChanged((object) this, EventArgs.Empty);
    }

    protected virtual void OnPageChanged()
    {
      if (this.PageChagned == null)
        return;
      this.PageChagned((object) this, EventArgs.Empty);
    }

    protected override void InitializeFields()
    {
      base.InitializeFields();
      this.cachedSelected = false;
      this.Class = nameof (BackstageTabItem);
      this.ThemeRole = nameof (BackstageTabItem);
      this.DrawFill = true;
      this.MinSize = new Size(0, 37);
      this.TextAlignment = ContentAlignment.MiddleLeft;
      this.Padding = new Padding(20, 0, 20, 0);
    }

    protected override void OnClick(EventArgs e)
    {
      base.OnClick(e);
      if (this.ElementTree == null || this.ElementTree.Control == null)
        return;
      BackstageItemsPanelElement parent = this.Parent as BackstageItemsPanelElement;
      parent.Owner.OnItemClicked((BackstageVisualElement) this);
      parent.Owner.SelectedItem = this;
    }

    protected override void OnParentChanged(RadElement previousParent)
    {
      base.OnParentChanged(previousParent);
      if (this.page == null)
        return;
      if (this.Parent != null && this.ElementTree != null)
        this.page.Parent = this.ElementTree.Control;
      else
        this.page.Parent = (Control) null;
    }

    private void SetPageCore(BackstageViewPage value, bool firePageChanged)
    {
      if (value == this.page)
        return;
      this.page = value;
      if (this.page == null)
      {
        if (!firePageChanged)
          return;
        this.OnPageChanged();
      }
      else
      {
        this.page.Item = this;
        if (!this.Selected)
          this.page.Visible = false;
        if (this.ElementTree != null)
        {
          this.page.Parent = this.ElementTree.Control;
          this.page.BringToFront();
        }
        if (!firePageChanged)
          return;
        this.OnPageChanged();
      }
    }

    private class BackstageTabItemStateManagerFactory : BackstageItemStateManagerFactory
    {
      protected override StateNodeBase CreateSpecificStates()
      {
        StateNodeWithCondition nodeWithCondition = new StateNodeWithCondition("Selected", (Condition) new SimpleCondition(BackstageTabItem.SelectedProperty, (object) true));
        CompositeStateNode compositeStateNode = new CompositeStateNode("backstage tab item states");
        compositeStateNode.AddState((StateNodeBase) nodeWithCondition);
        return (StateNodeBase) compositeStateNode;
      }

      protected override ItemStateManagerBase CreateStateManager()
      {
        ItemStateManagerBase stateManager = base.CreateStateManager();
        stateManager.AddDefaultVisibleState("Selected");
        return stateManager;
      }
    }
  }
}
