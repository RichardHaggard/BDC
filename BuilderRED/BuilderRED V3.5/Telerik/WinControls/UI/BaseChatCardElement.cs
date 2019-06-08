// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.BaseChatCardElement
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace Telerik.WinControls.UI
{
  public class BaseChatCardElement : LightVisualElement
  {
    private BaseChatCardDataItem item;

    protected override void InitializeFields()
    {
      base.InitializeFields();
      this.MaxSize = new Size(320, 0);
      this.ShouldHandleMouseInput = false;
      this.BorderColor = Color.Gray;
      this.BorderWidth = 2f;
      this.DrawBorder = true;
      this.GradientStyle = GradientStyles.Solid;
      this.BackColor = Color.White;
      this.ForeColor = Color.Black;
      this.DrawFill = true;
      this.Shape = (ElementShape) new RoundRectShape(20);
      this.StretchVertically = false;
      this.SmoothingMode = SmoothingMode.AntiAlias;
    }

    public BaseChatCardElement(BaseChatCardDataItem item)
    {
      this.item = item;
      this.Synchronise();
      this.item.PropertyChanged += new PropertyChangedEventHandler(this.OnDataItemPropertyChanged);
    }

    protected virtual void OnDataItemPropertyChanged(object sender, PropertyChangedEventArgs e)
    {
      this.Synchronise();
    }

    protected virtual void Synchronise()
    {
    }

    public BaseChatCardDataItem DataItem
    {
      get
      {
        return this.item;
      }
    }

    public event CardActionEventHandler CardActionClicked;

    protected virtual void OnCardActionClicked(CardActionEventArgs e)
    {
      if (this.CardActionClicked == null)
        return;
      this.CardActionClicked((object) this, e);
    }

    protected override void DisposeManagedResources()
    {
      if (this.item != null)
      {
        this.item.PropertyChanged -= new PropertyChangedEventHandler(this.OnDataItemPropertyChanged);
        this.item = (BaseChatCardDataItem) null;
      }
      base.DisposeManagedResources();
    }
  }
}
