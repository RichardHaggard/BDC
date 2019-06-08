// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.RadCardViewElement
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.ComponentModel;
using System.Windows.Forms;

namespace Telerik.WinControls.UI
{
  public class RadCardViewElement : RadListViewElement
  {
    private RadLayoutControl cardTemplate;

    public RadCardViewElement()
    {
      base.ViewType = ListViewType.IconsView;
    }

    protected override BaseListViewElement CreateViewElement()
    {
      return (BaseListViewElement) new CardListViewElement((RadListViewElement) this);
    }

    public RadLayoutControl CardTemplate
    {
      get
      {
        if (this.cardTemplate == null && this.Site == null)
        {
          this.cardTemplate = new RadLayoutControl();
          this.cardTemplate.LoadElementTree();
          this.cardTemplate.CustomizeDialog = (RadLayoutControlCustomizeDialog) new RadCardViewCustomizeDialog(this, this.cardTemplate);
        }
        return this.cardTemplate;
      }
    }

    [DefaultValue(ListViewType.IconsView)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    public override ListViewType ViewType
    {
      get
      {
        return ListViewType.IconsView;
      }
      set
      {
      }
    }

    public void ShowCustomizeDialog()
    {
      (this.ViewElement as CardListViewElement).ShowCustomizeDialog();
    }

    public void CloseCustomizeDialog()
    {
      (this.ViewElement as CardListViewElement).CloseCustomizeDialog();
    }

    public override bool BeginEdit()
    {
      if (this.CurrentColumn == null)
        return false;
      return base.BeginEdit();
    }

    protected override void OnLoaded()
    {
      base.OnLoaded();
      if (!this.ElementTree.Control.Controls.Contains((Control) this.CardTemplate))
        this.ElementTree.Control.Controls.Add((Control) this.CardTemplate);
      if (this.IsDesignMode)
        return;
      this.CardTemplate.Hide();
    }

    protected override void InitializeEditor(
      BaseListViewVisualItem visualItem,
      ISupportInitialize initializable,
      IInputEditor editor)
    {
      this.ActiveEditor.Initialize((object) visualItem, this.SelectedItem[this.CurrentColumn]);
      initializable?.EndInit();
      this.OnEditorInitialized(new ListViewItemEditorInitializedEventArgs(visualItem, (IValueEditor) editor));
      this.ActiveEditor.BeginEdit();
      this.cachedOldValue = this.SelectedItem[this.CurrentColumn];
    }

    protected override void SetSelectedItemValue(
      ListViewItemValueChangingEventArgs valueChangingArgs,
      object newValue)
    {
      if (this.OnValueChanging(valueChangingArgs))
        return;
      this.SelectedItem[this.CurrentColumn] = newValue;
      this.OnValueChanged(new ListViewItemValueChangedEventArgs(this.SelectedItem));
    }

    public event CardViewItemCreatingEventHandler CardViewItemCreating;

    protected internal virtual void OnCardViewItemCreating(CardViewItemCreatingEventArgs args)
    {
      if (this.CardViewItemCreating == null)
        return;
      this.CardViewItemCreating((object) this, args);
    }

    public event CardViewItemFormattingEventHandler CardViewItemFormatting;

    protected internal virtual void OnCardViewItemFormatting(CardViewItemFormattingEventArgs args)
    {
      if (this.CardViewItemFormatting == null)
        return;
      this.CardViewItemFormatting((object) this, args);
    }
  }
}
