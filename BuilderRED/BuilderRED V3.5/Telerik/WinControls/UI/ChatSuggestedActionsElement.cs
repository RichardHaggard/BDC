// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.ChatSuggestedActionsElement
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Telerik.WinControls.UI
{
  public class ChatSuggestedActionsElement : HorizontalScrollableStackElement
  {
    private RadChatElement chatElement;

    protected override void InitializeFields()
    {
      base.InitializeFields();
      this.Margin = new Padding(11, 16, 11, 16);
    }

    public ChatSuggestedActionsElement(RadChatElement chatElement)
    {
      this.chatElement = chatElement;
    }

    public RadChatElement ChatElement
    {
      get
      {
        return this.chatElement;
      }
    }

    public void AddActions(IEnumerable<SuggestedActionDataItem> actions)
    {
      foreach (SuggestedActionDataItem action in actions)
        this.AddAction(action);
    }

    public void AddAction(SuggestedActionDataItem action)
    {
      SuggestedActionElement suggestedActionElement = this.ChatElement.ChatFactory.CreateSuggestedActionElement(action);
      this.ItemsLayout.Children.Add((RadElement) suggestedActionElement);
      suggestedActionElement.Click += new EventHandler(this.Element_Click);
    }

    public void ClearActions()
    {
      foreach (RadElement child in this.ItemsLayout.Children)
        child.Click -= new EventHandler(this.Element_Click);
      this.ItemsLayout.Children.Clear();
    }

    private void Element_Click(object sender, EventArgs e)
    {
      this.Visibility = ElementVisibility.Collapsed;
      this.ChatElement.OnSuggestedActionClicked(new SuggestedActionEventArgs((sender as SuggestedActionElement).DataItem));
    }
  }
}
