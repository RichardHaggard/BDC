// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.RadCardViewCustomizeDialog
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Windows.Forms;

namespace Telerik.WinControls.UI
{
  public class RadCardViewCustomizeDialog : RadLayoutControlCustomizeDialog
  {
    private RadCardViewElement cardViewElement;

    public RadCardViewCustomizeDialog(
      RadCardViewElement cardViewElement,
      RadLayoutControl layoutControl)
      : base(layoutControl)
    {
      this.cardViewElement = cardViewElement;
      this.SplitterItem.ListView.Items.Remove(this.SplitterItem);
    }

    protected override void HandleItemDrop(
      BaseListViewVisualItem draggedItem,
      DraggableLayoutControlItem target)
    {
      base.HandleItemDrop(draggedItem, target);
      if (draggedItem.Data != null && draggedItem.Data.Group == this.HiddenItemsGroup && !(draggedItem.Data.Tag is LayoutControlItemBase))
        return;
      (this.cardViewElement.ViewElement as CardListViewElement)?.UpdateItemsLayout();
    }

    protected override void OnSaveLayoutButtonClick(object sender, EventArgs e)
    {
      using (SaveFileDialog saveFileDialog = new SaveFileDialog())
      {
        saveFileDialog.DefaultExt = ".xml";
        saveFileDialog.Filter = "XML files (*.xml)|*.xml|All files (*.*)|*.*";
        if (saveFileDialog.ShowDialog() != DialogResult.OK)
          return;
        (this.cardViewElement.ViewElement as CardListViewElement)?.SaveCardTemplateLayout(saveFileDialog.FileName);
      }
    }

    protected override void OnLoadLayoutButtonClick(object sender, EventArgs e)
    {
      using (OpenFileDialog openFileDialog = new OpenFileDialog())
      {
        openFileDialog.DefaultExt = ".xml";
        openFileDialog.Filter = "XML files (*.xml)|*.xml|All files (*.*)|*.*";
        if (openFileDialog.ShowDialog() != DialogResult.OK)
          return;
        (this.cardViewElement.ViewElement as CardListViewElement)?.LoadCardTemplateLayout(openFileDialog.FileName);
      }
    }
  }
}
