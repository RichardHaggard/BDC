// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.BaseChatOverlay
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

namespace Telerik.WinControls.UI
{
  public class BaseChatOverlay : LightVisualElement
  {
    private RadChatElement chatElement;
    private bool isInline;
    private object currentValue;

    public object CurrentValue
    {
      get
      {
        return this.currentValue;
      }
      set
      {
        this.SetCurrentValue(value);
      }
    }

    public RadChatElement ChatElement
    {
      get
      {
        return this.chatElement;
      }
      internal set
      {
        this.chatElement = value;
      }
    }

    public bool IsInline
    {
      get
      {
        return this.isInline;
      }
    }

    protected virtual void SetCurrentValue(object value)
    {
      this.currentValue = value;
    }

    public virtual void PrepareForPopupDisplay()
    {
      this.isInline = true;
    }

    public virtual void PrepareForOverlayDisplay()
    {
      this.isInline = false;
    }
  }
}
