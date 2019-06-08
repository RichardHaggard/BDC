// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.RadTokenizedTextItemCollection
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using Telerik.Collections.Generic;
using Telerik.WinControls.Data;

namespace Telerik.WinControls.UI
{
  public class RadTokenizedTextItemCollection : ReadOnlyCollection<RadTokenizedTextItem>, INotifyCollectionChanged
  {
    private readonly RadAutoCompleteBoxElement textBox;

    public RadTokenizedTextItemCollection(RadAutoCompleteBoxElement textBox)
      : this(textBox, (IList<RadTokenizedTextItem>) new AvlTree<RadTokenizedTextItem>())
    {
    }

    protected RadTokenizedTextItemCollection(
      RadAutoCompleteBoxElement textBox,
      IList<RadTokenizedTextItem> list)
      : base(list)
    {
      this.textBox = textBox;
      this.textBox.ViewElement.ChildrenChanged += new ChildrenChangedEventHandler(this.OnViewElementChildrenChanged);
      this.textBox.ListElement.DataBindingComplete += new ListBindingCompleteEventHandler(this.OnListElementDataBindingComplete);
      this.textBox.ListElement.ItemsChanging += new NotifyCollectionChangingEventHandler(this.OnListElementItemsChanging);
    }

    protected RadAutoCompleteBoxElement TextBox
    {
      get
      {
        return this.textBox;
      }
    }

    public event NotifyCollectionChangedEventHandler CollectionChanged;

    protected virtual void OnCollectionChanged(NotifyCollectionChangedEventArgs e)
    {
      NotifyCollectionChangedEventHandler collectionChanged = this.CollectionChanged;
      if (collectionChanged == null)
        return;
      collectionChanged((object) this, e);
    }

    protected virtual void Attach(ITextBlock textBlock)
    {
      TokenizedTextBlockElement textBlockElement = textBlock as TokenizedTextBlockElement;
      if (textBlockElement == null)
        return;
      textBlockElement.ContentElement.TextChanging += new TextChangingEventHandler(this.OnContentElementTextChanging);
      textBlockElement.Item.PropertyChanged += new PropertyChangedEventHandler(this.OnTokenItemPropertyChanged);
    }

    protected virtual void Detach(ITextBlock textBlock)
    {
      TokenizedTextBlockElement textBlockElement = textBlock as TokenizedTextBlockElement;
      if (textBlockElement == null)
        return;
      textBlockElement.ContentElement.TextChanging += new TextChangingEventHandler(this.OnContentElementTextChanging);
      textBlockElement.Item.PropertyChanged -= new PropertyChangedEventHandler(this.OnTokenItemPropertyChanged);
    }

    protected virtual void OnClearBlocks()
    {
      foreach (RadElement child in this.TextBox.ViewElement.Children)
        this.Detach(child as ITextBlock);
      this.Items.Clear();
    }

    protected virtual RadTokenizedTextItem OnRemoveBlock(ITextBlock textBlock)
    {
      TokenizedTextBlockElement textBlockElement = textBlock as TokenizedTextBlockElement;
      RadTokenizedTextItem tokenizedTextItem = (RadTokenizedTextItem) null;
      if (textBlockElement != null)
      {
        tokenizedTextItem = textBlockElement.Item;
        this.Items.Remove(tokenizedTextItem);
      }
      this.Detach(textBlock);
      return tokenizedTextItem;
    }

    protected virtual RadTokenizedTextItem OnInsertBlock(ITextBlock textBlock)
    {
      string text = textBlock.Text;
      RadTokenizedTextItem tokenizedTextItem = (RadTokenizedTextItem) null;
      TokenizedTextBlockElement textBlockElement = textBlock as TokenizedTextBlockElement;
      if (textBlockElement != null)
      {
        tokenizedTextItem = textBlockElement.Item;
        RadListDataItem radListDataItem = this.textBox.ListElement.Find(text);
        tokenizedTextItem.Value = radListDataItem?.Value;
        this.Items.Add(tokenizedTextItem);
      }
      this.Attach(textBlock);
      return tokenizedTextItem;
    }

    protected virtual void OnListElementDataBindingComplete(ListBindingCompleteEventArgs e)
    {
      foreach (RadTokenizedTextItem tokenizedTextItem in (IEnumerable<RadTokenizedTextItem>) this.Items)
      {
        RadListDataItem radListDataItem = this.textBox.ListElement.Find(tokenizedTextItem.Text);
        tokenizedTextItem.Value = radListDataItem?.Value;
      }
    }

    protected RadTokenizedTextItem Find(string text)
    {
      AvlTree<RadTokenizedTextItem> items = this.Items as AvlTree<RadTokenizedTextItem>;
      RadTokenizedTextItem tokenizedTextItem1 = (RadTokenizedTextItem) null;
      if (items != null)
      {
        tokenizedTextItem1 = items.Find(new RadTokenizedTextItem(text, (object) null));
      }
      else
      {
        foreach (RadTokenizedTextItem tokenizedTextItem2 in (IEnumerable<RadTokenizedTextItem>) this.Items)
        {
          if (string.Compare(tokenizedTextItem2.Text, text) == 0)
          {
            tokenizedTextItem1 = tokenizedTextItem2;
            break;
          }
        }
      }
      return tokenizedTextItem1;
    }

    protected IEnumerable<RadTokenizedTextItem> FinAll(string text)
    {
      foreach (RadTokenizedTextItem tokenizedTextItem in (IEnumerable<RadTokenizedTextItem>) this.Items)
      {
        if (string.Compare(tokenizedTextItem.Text, text) == 0)
          yield return tokenizedTextItem;
      }
    }

    private void OnTokenItemPropertyChanged(object sender, PropertyChangedEventArgs e)
    {
      if (!(e.PropertyName == "Text"))
        return;
      RadTokenizedTextItem tokenizedTextItem = sender as RadTokenizedTextItem;
      RadListDataItem radListDataItem = this.textBox.ListElement.Find(tokenizedTextItem.Text);
      tokenizedTextItem.Value = radListDataItem?.Value;
    }

    private void OnListElementDataBindingComplete(object sender, ListBindingCompleteEventArgs e)
    {
      this.OnListElementDataBindingComplete(e);
    }

    private void OnListElementItemsChanging(object sender, NotifyCollectionChangingEventArgs e)
    {
      this.OnListElementItemsChanging(e);
    }

    protected virtual void OnListElementItemsChanging(NotifyCollectionChangingEventArgs e)
    {
      if (e.Action == NotifyCollectionChangedAction.Reset)
      {
        foreach (RadTokenizedTextItem tokenizedTextItem in (IEnumerable<RadTokenizedTextItem>) this.Items)
          tokenizedTextItem.Value = (object) null;
      }
      else if (e.Action == NotifyCollectionChangedAction.Remove)
      {
        RadListDataItem oldItem = e.OldItems[0] as RadListDataItem;
        if (oldItem.Value == null)
          return;
        foreach (RadTokenizedTextItem tokenizedTextItem in this.FinAll(oldItem.Text))
          tokenizedTextItem.Value = (object) null;
      }
      else
      {
        if (e.Action != NotifyCollectionChangedAction.Add)
          return;
        RadListDataItem newItem = e.NewItems[0] as RadListDataItem;
        if (newItem.Value == null)
          return;
        foreach (RadTokenizedTextItem tokenizedTextItem in this.FinAll(newItem.Text))
          tokenizedTextItem.Value = newItem.Value;
      }
    }

    private void OnViewElementChildrenChanged(object sender, ChildrenChangedEventArgs e)
    {
      this.OnViewElementChildrenChanged(e);
    }

    protected virtual void OnViewElementChildrenChanged(ChildrenChangedEventArgs e)
    {
      NotifyCollectionChangedAction action = NotifyCollectionChangedAction.Reset;
      RadTokenizedTextItem tokenizedTextItem = (RadTokenizedTextItem) null;
      bool flag = false;
      if (e.ChangeOperation == ItemsChangeOperation.Clearing)
      {
        if (this.Items.Count > 0)
        {
          this.OnClearBlocks();
          flag = true;
        }
      }
      else if (e.Child is TokenizedTextBlockElement)
      {
        ITextBlock child = e.Child as ITextBlock;
        if (e.ChangeOperation == ItemsChangeOperation.Inserted)
        {
          tokenizedTextItem = this.OnInsertBlock(child);
          flag = true;
          action = NotifyCollectionChangedAction.Add;
        }
        else if (e.ChangeOperation == ItemsChangeOperation.Removed)
        {
          tokenizedTextItem = this.OnRemoveBlock(child);
          flag = true;
          action = NotifyCollectionChangedAction.Remove;
        }
      }
      if (!flag)
        return;
      this.OnCollectionChanged(new NotifyCollectionChangedEventArgs(action, (object) tokenizedTextItem));
    }

    private void OnContentElementTextChanging(object sender, TextChangingEventArgs e)
    {
      this.OnContentElementTextChanging(e);
    }

    protected virtual void OnContentElementTextChanging(TextChangingEventArgs e)
    {
    }
  }
}
