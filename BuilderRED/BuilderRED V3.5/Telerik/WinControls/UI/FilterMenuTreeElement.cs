// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.FilterMenuTreeElement
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Windows.Forms;
using Telerik.WinControls.Localization;
using Telerik.WinControls.UI.Localization;

namespace Telerik.WinControls.UI
{
  public class FilterMenuTreeElement : LightVisualElement, IRadListFilterElement
  {
    private RadHostItem hostItem;
    private RadTreeNode allNode;
    private RadTreeNode nullNode;
    private RadTreeNode nonNullNode;
    private RadListFilterDistinctValuesTable distinctValues;
    private RadListFilterDistinctValuesTable selectedValues;
    private bool enableBlanks;
    private Hashtable treeValuesHash;
    private ListFilterSelectedMode selectedMode;
    private bool isFiltered;
    private RadTreeView treeView;
    private bool groupedDateValues;

    public bool GroupedDateValues
    {
      get
      {
        return this.groupedDateValues;
      }
      set
      {
        this.groupedDateValues = value;
      }
    }

    public RadTreeView TreeView
    {
      get
      {
        return (RadTreeView) this.hostItem.HostedControl;
      }
    }

    protected override void InitializeFields()
    {
      base.InitializeFields();
      this.BorderGradientStyle = GradientStyles.Solid;
      this.BorderColor = Color.FromArgb(156, 189, 232);
    }

    protected override void CreateChildElements()
    {
      base.CreateChildElements();
      this.treeView = new RadTreeView();
      this.treeView.TriStateMode = true;
      this.treeView.CheckBoxes = true;
      this.treeView.ShowExpandCollapse = false;
      this.treeView.ShowLines = true;
      this.treeView.ShowRootLines = false;
      this.treeView.NodeCheckedChanged += new TreeNodeCheckedEventHandler(this.treeView_NodeCheckedChanged);
      this.treeView.NodeExpandedChanging += new RadTreeView.RadTreeViewCancelEventHandler(this.treeView_NodeExpandedChanging);
      this.treeView.NodeMouseClick += new RadTreeView.TreeViewEventHandler(this.treeView_NodeMouseClick);
      this.nullNode = new RadTreeNode();
      this.nonNullNode = new RadTreeNode();
      this.treeView.Nodes.Add(this.nullNode);
      this.treeView.Nodes.Add(this.nonNullNode);
      this.allNode = new RadTreeNode();
      this.allNode.Expanded = true;
      this.treeView.Nodes.Add(this.allNode);
      this.hostItem = new RadHostItem((Control) this.treeView);
      this.Children.Add((RadElement) this.hostItem);
    }

    protected override void DisposeManagedResources()
    {
      this.treeView.NodeCheckedChanged -= new TreeNodeCheckedEventHandler(this.treeView_NodeCheckedChanged);
      this.treeView.NodeExpandedChanging -= new RadTreeView.RadTreeViewCancelEventHandler(this.treeView_NodeExpandedChanging);
      this.treeView.NodeMouseClick -= new RadTreeView.TreeViewEventHandler(this.treeView_NodeMouseClick);
      base.DisposeManagedResources();
    }

    private void treeView_NodeMouseClick(object sender, RadTreeViewEventArgs e)
    {
      e.Node.Checked = !e.Node.Checked;
    }

    private void treeView_NodeExpandedChanging(object sender, RadTreeViewCancelEventArgs e)
    {
      if (this.groupedDateValues || !e.Node.Expanded)
        return;
      e.Cancel = true;
    }

    private void treeView_NodeCheckedChanged(object sender, RadTreeViewEventArgs e)
    {
      this.TreeView.NodeCheckedChanged -= new TreeNodeCheckedEventHandler(this.treeView_NodeCheckedChanged);
      if (e.Node == this.allNode && (this.allNode.CheckState == Telerik.WinControls.Enumerations.ToggleState.On || this.allNode.CheckState == Telerik.WinControls.Enumerations.ToggleState.Indeterminate))
      {
        this.nullNode.CheckState = Telerik.WinControls.Enumerations.ToggleState.Off;
        this.nonNullNode.CheckState = Telerik.WinControls.Enumerations.ToggleState.Off;
      }
      else if (e.Node == this.nullNode && this.nullNode.CheckState == Telerik.WinControls.Enumerations.ToggleState.On)
      {
        this.allNode.CheckState = Telerik.WinControls.Enumerations.ToggleState.Off;
        this.nonNullNode.CheckState = Telerik.WinControls.Enumerations.ToggleState.Off;
        this.selectedValues.Clear();
      }
      else if (e.Node == this.nonNullNode && this.nonNullNode.CheckState == Telerik.WinControls.Enumerations.ToggleState.On)
      {
        this.allNode.CheckState = Telerik.WinControls.Enumerations.ToggleState.Off;
        this.nullNode.CheckState = Telerik.WinControls.Enumerations.ToggleState.Off;
        this.selectedValues.Clear();
      }
      this.UpdateNodeSelectionOnCheckedChanged(e.Node);
      this.TreeView.NodeCheckedChanged += new TreeNodeCheckedEventHandler(this.treeView_NodeCheckedChanged);
      this.OnSelectionChanged();
    }

    private void OnSelectionChanged()
    {
      if (this.SelectionChanged == null)
        return;
      this.SelectionChanged((object) this, new EventArgs());
    }

    private void UpdateNodeSelectionOnCheckedChanged(RadTreeNode node)
    {
      if (this.selectedValues == null)
      {
        this.ValidateSelectedValues();
      }
      else
      {
        if (this.groupedDateValues)
        {
          if (node.Level == 3)
          {
            RadTreeNode parent = node.Parent;
            DateTime dateTime = new DateTime(Convert.ToInt32(node.Parent.Parent.Text), Convert.ToInt32(parent.Text), Convert.ToInt32(node.Text));
            if ((node.Visible || node.IsInDesignMode) && node.Checked)
              this.selectedValues.Add(dateTime.ToString(), (object) dateTime);
            else
              this.selectedValues.Remove((object) dateTime.ToString());
          }
        }
        else if ((node.Visible || node.IsInDesignMode) && (this.treeView.TreeViewElement.FilterPredicate == null || this.treeView.TreeViewElement.FilterPredicate(node)) && (node.CheckState == Telerik.WinControls.Enumerations.ToggleState.On && this.treeValuesHash.ContainsKey((object) node)))
        {
          if (this.selectedValues.ContainsStringKey(node.Text))
            this.selectedValues[node.Text] = (ArrayList) this.treeValuesHash[(object) node];
          else
            this.selectedValues.Add(node.Text, (ArrayList) this.treeValuesHash[(object) node]);
        }
        else
          this.selectedValues.Remove((object) node.Text);
        this.UpdateSelectionMode();
      }
    }

    private void ValidateSelectedValues()
    {
      if (this.selectedValues == null)
        this.selectedValues = new RadListFilterDistinctValuesTable();
      this.selectedValues.Clear();
      if (this.treeValuesHash == null)
        return;
      if (this.groupedDateValues)
      {
        foreach (RadTreeNode node1 in (Collection<RadTreeNode>) this.allNode.Nodes)
        {
          if (node1.Visible || node1.IsInDesignMode)
          {
            foreach (RadTreeNode node2 in (Collection<RadTreeNode>) node1.Nodes)
            {
              if (node2.Visible || node2.IsInDesignMode)
              {
                foreach (RadTreeNode node3 in (Collection<RadTreeNode>) node2.Nodes)
                {
                  if ((node3.Visible || node3.IsInDesignMode) && node3.Checked)
                  {
                    DateTime dateTime = new DateTime(Convert.ToInt32(node1.Text), Convert.ToInt32(node2.Text), Convert.ToInt32(node3.Text));
                    this.selectedValues.Add(dateTime.ToString(), (object) dateTime);
                  }
                }
              }
            }
          }
        }
      }
      else
      {
        foreach (DictionaryEntry dictionaryEntry in this.treeValuesHash)
        {
          RadTreeNode key = (RadTreeNode) dictionaryEntry.Key;
          if ((key.Visible || key.IsInDesignMode) && (this.treeView.TreeViewElement.FilterPredicate == null || this.treeView.TreeViewElement.FilterPredicate(key)) && key.CheckState == Telerik.WinControls.Enumerations.ToggleState.On)
            this.selectedValues.Add(key.Text, (ArrayList) dictionaryEntry.Value);
        }
      }
      this.UpdateSelectionMode();
    }

    private void UpdateSelectionMode()
    {
      if (this.allNode.CheckState == Telerik.WinControls.Enumerations.ToggleState.On && !this.isFiltered)
        this.selectedMode = ListFilterSelectedMode.All;
      else if (this.nullNode.CheckState == Telerik.WinControls.Enumerations.ToggleState.On)
        this.selectedMode = ListFilterSelectedMode.Null;
      else if (this.nonNullNode.CheckState == Telerik.WinControls.Enumerations.ToggleState.On)
        this.selectedMode = ListFilterSelectedMode.NotNull;
      else if (this.selectedValues.Count > 0)
        this.selectedMode = ListFilterSelectedMode.Custom;
      else
        this.selectedMode = ListFilterSelectedMode.None;
    }

    public bool EnableBlanks
    {
      set
      {
        this.enableBlanks = value;
      }
    }

    public RadListFilterDistinctValuesTable DistinctListValues
    {
      get
      {
        return this.distinctValues;
      }
      set
      {
        this.distinctValues = value;
      }
    }

    public RadListFilterDistinctValuesTable SelectedValues
    {
      get
      {
        return this.selectedValues;
      }
      set
      {
        this.selectedValues = value;
      }
    }

    public ListFilterSelectedMode SelectedMode
    {
      get
      {
        return this.selectedMode;
      }
      set
      {
        this.selectedMode = value;
      }
    }

    public void Initialize()
    {
      this.allNode.Nodes.Clear();
      this.nullNode.Visible = this.enableBlanks;
      this.nonNullNode.Visible = this.enableBlanks;
      this.allNode.Text = LocalizationProvider<RadGridLocalizationProvider>.CurrentProvider.GetLocalizedString("FilterMenuSelectionAll");
      this.nullNode.Text = LocalizationProvider<RadGridLocalizationProvider>.CurrentProvider.GetLocalizedString("FilterMenuSelectionNull");
      this.nonNullNode.Text = LocalizationProvider<RadGridLocalizationProvider>.CurrentProvider.GetLocalizedString("FilterMenuSelectionNotNull");
      this.TreeView.NodeCheckedChanged -= new TreeNodeCheckedEventHandler(this.treeView_NodeCheckedChanged);
      this.TreeView.BeginUpdate();
      bool flag1 = true;
      if (this.selectedValues.Count > 0 || this.selectedMode == ListFilterSelectedMode.None)
        flag1 = false;
      if (this.selectedValues.Count > 0 && this.selectedValues.Count < this.distinctValues.Count)
        this.allNode.CheckState = Telerik.WinControls.Enumerations.ToggleState.Indeterminate;
      if (this.selectedMode == ListFilterSelectedMode.Null)
      {
        this.nullNode.CheckState = Telerik.WinControls.Enumerations.ToggleState.On;
        flag1 = false;
      }
      else if (this.selectedMode == ListFilterSelectedMode.NotNull)
      {
        this.nonNullNode.CheckState = Telerik.WinControls.Enumerations.ToggleState.On;
        flag1 = false;
      }
      if (this.distinctValues != null)
      {
        SortedList sortedList = new SortedList((IDictionary) this.distinctValues, (IComparer) new ListFilterComparer((IDictionary) this.distinctValues));
        this.treeValuesHash = new Hashtable();
        if (this.groupedDateValues)
        {
          this.treeView.ShowExpandCollapse = true;
          foreach (DictionaryEntry dictionaryEntry in sortedList)
          {
            object obj = ((ArrayList) dictionaryEntry.Value)[0];
            if (obj != null && obj != DBNull.Value)
            {
              DateTime dateTime = (DateTime) obj;
              RadTreeNode radTreeNode1 = !this.allNode.Nodes.Contains(dateTime.Year.ToString()) ? this.allNode.Nodes.Add(dateTime.Year.ToString()) : this.allNode.Nodes[dateTime.Year.ToString()];
              RadTreeNode radTreeNode2 = !radTreeNode1.Nodes.Contains(dateTime.Month.ToString()) ? radTreeNode1.Nodes.Add(dateTime.Month.ToString()) : radTreeNode1.Nodes[dateTime.Month.ToString()];
              RadTreeNode radTreeNode3 = !radTreeNode2.Nodes.Contains(dateTime.Day.ToString()) ? radTreeNode2.Nodes.Add(dateTime.Day.ToString()) : radTreeNode2.Nodes[dateTime.Day.ToString()];
              if (this.selectedValues.ContainsFilterValue((object) dateTime) || this.selectedValues.ContainsFilterValue((object) dateTime.Date))
                radTreeNode3.CheckState = Telerik.WinControls.Enumerations.ToggleState.On;
            }
          }
          this.allNode.Expand();
        }
        else
        {
          foreach (DictionaryEntry dictionaryEntry in sortedList)
          {
            string text = (string) dictionaryEntry.Key;
            bool flag2 = false;
            if (string.IsNullOrEmpty(text))
            {
              text = LocalizationProvider<RadGridLocalizationProvider>.CurrentProvider.GetLocalizedString("FilterMenuBlanks");
              flag2 = true;
            }
            RadTreeNode radTreeNode = new RadTreeNode(text);
            this.allNode.Nodes.Add(radTreeNode);
            if (flag2)
            {
              foreach (DictionaryEntry selectedValue in this.selectedValues)
              {
                foreach (object obj in (ArrayList) selectedValue.Value)
                {
                  if (obj == null || TelerikHelper.StringIsNullOrWhiteSpace(obj.ToString()))
                  {
                    radTreeNode.CheckState = Telerik.WinControls.Enumerations.ToggleState.Off;
                    radTreeNode.CheckState = Telerik.WinControls.Enumerations.ToggleState.On;
                    break;
                  }
                }
              }
            }
            else
            {
              foreach (object obj in (ArrayList) dictionaryEntry.Value)
              {
                if (this.selectedValues.ContainsFilterValue(obj))
                {
                  radTreeNode.CheckState = Telerik.WinControls.Enumerations.ToggleState.Off;
                  radTreeNode.CheckState = Telerik.WinControls.Enumerations.ToggleState.On;
                  break;
                }
              }
            }
            this.treeValuesHash.Add((object) radTreeNode, dictionaryEntry.Value);
          }
        }
      }
      if (flag1)
        this.allNode.CheckState = Telerik.WinControls.Enumerations.ToggleState.On;
      this.TreeView.EndUpdate();
      this.ValidateSelectedValues();
      this.OnSelectionChanged();
      this.TreeView.NodeCheckedChanged += new TreeNodeCheckedEventHandler(this.treeView_NodeCheckedChanged);
    }

    public void Filter(string filter)
    {
      this.isFiltered = false;
      if (this.treeValuesHash == null)
        return;
      this.TreeView.BeginUpdate();
      this.allNode.Text = string.IsNullOrEmpty(filter) ? LocalizationProvider<RadGridLocalizationProvider>.CurrentProvider.GetLocalizedString("FilterMenuSelectionAll") : LocalizationProvider<RadGridLocalizationProvider>.CurrentProvider.GetLocalizedString("FilterMenuSelectionAllSearched");
      if (this.groupedDateValues)
      {
        foreach (RadTreeNode node1 in (Collection<RadTreeNode>) this.allNode.Nodes)
        {
          if (string.IsNullOrEmpty(filter) || node1.Text.ToLower().Contains(filter.ToLower()))
          {
            node1.Visible = true;
            node1.Collapse();
          }
          else
          {
            node1.Visible = false;
            this.isFiltered = true;
          }
          foreach (RadTreeNode node2 in (Collection<RadTreeNode>) node1.Nodes)
          {
            if (string.IsNullOrEmpty(filter) || node1.Text.ToLower().Contains(filter.ToLower()) || node2.Text.ToLower().Contains(filter.ToLower()))
            {
              node1.Visible = true;
              if (!string.IsNullOrEmpty(filter))
                node1.Expand();
              node2.Visible = true;
            }
            else
            {
              node2.Visible = false;
              this.isFiltered = true;
            }
            foreach (RadTreeNode node3 in (Collection<RadTreeNode>) node2.Nodes)
            {
              if (string.IsNullOrEmpty(filter) || node2.Text.ToLower().Contains(filter.ToLower()) || node3.Text.ToLower().Contains(filter.ToLower()))
              {
                node1.Visible = true;
                node2.Visible = true;
                if (!string.IsNullOrEmpty(filter))
                {
                  node1.Expand();
                  node2.Expand();
                }
                node3.Visible = true;
              }
              else
              {
                node3.Visible = false;
                this.isFiltered = true;
              }
            }
          }
        }
      }
      else
      {
        this.treeView.Filter = (object) filter;
        this.isFiltered = !string.IsNullOrEmpty(filter);
      }
      this.allNode.InvalidateOnState();
      this.TreeView.EndUpdate();
      this.ValidateSelectedValues();
    }

    public event EventHandler SelectionChanged;
  }
}
