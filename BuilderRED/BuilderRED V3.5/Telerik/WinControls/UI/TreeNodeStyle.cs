// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.TreeNodeStyle
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.ComponentModel;
using System.Drawing;

namespace Telerik.WinControls.UI
{
  public class TreeNodeStyle : INotifyPropertyChanged
  {
    private Color foreColor = Color.Empty;
    private Color backColor = Color.Empty;
    private Color backColor2 = Color.Empty;
    private Color backColor3 = Color.Empty;
    private Color backColor4 = Color.Empty;
    private Color borderColor = Color.Empty;
    private int numberOfColors = 4;
    private float gradientPercentage = 0.5f;
    private float gradientPercentage2 = 0.5f;
    private float gradientAngle = 90f;
    private GradientStyles gradientStyle = GradientStyles.Linear;
    private ContentAlignment textAlignment = ContentAlignment.MiddleLeft;
    private Font font;

    public Font Font
    {
      get
      {
        return this.font;
      }
      set
      {
        this.font = value;
        this.OnNotifyPropertyChanged(nameof (Font));
      }
    }

    public Color ForeColor
    {
      get
      {
        return this.foreColor;
      }
      set
      {
        this.foreColor = value;
        this.OnNotifyPropertyChanged(nameof (ForeColor));
      }
    }

    public Color BorderColor
    {
      get
      {
        return this.borderColor;
      }
      set
      {
        this.borderColor = value;
        this.OnNotifyPropertyChanged(nameof (BorderColor));
      }
    }

    public Color BackColor4
    {
      get
      {
        return this.backColor4;
      }
      set
      {
        this.backColor4 = value;
        this.OnNotifyPropertyChanged(nameof (BackColor4));
      }
    }

    public Color BackColor3
    {
      get
      {
        return this.backColor3;
      }
      set
      {
        this.backColor3 = value;
        this.OnNotifyPropertyChanged(nameof (BackColor3));
      }
    }

    public Color BackColor2
    {
      get
      {
        return this.backColor2;
      }
      set
      {
        this.backColor2 = value;
        this.OnNotifyPropertyChanged("backColor2");
      }
    }

    public Color BackColor
    {
      get
      {
        return this.backColor;
      }
      set
      {
        this.backColor = value;
        this.OnNotifyPropertyChanged(nameof (BackColor));
      }
    }

    public int NumberOfColors
    {
      get
      {
        return this.numberOfColors;
      }
      set
      {
        this.numberOfColors = value;
        this.OnNotifyPropertyChanged(nameof (NumberOfColors));
      }
    }

    public float GradientPercentage2
    {
      get
      {
        return this.gradientPercentage2;
      }
      set
      {
        this.gradientPercentage2 = value;
        this.OnNotifyPropertyChanged(nameof (GradientPercentage2));
      }
    }

    public float GradientPercentage
    {
      get
      {
        return this.gradientPercentage;
      }
      set
      {
        this.gradientPercentage = value;
        this.OnNotifyPropertyChanged(nameof (GradientPercentage));
      }
    }

    public float GradientAngle
    {
      get
      {
        return this.gradientAngle;
      }
      set
      {
        this.gradientAngle = value;
        this.OnNotifyPropertyChanged(nameof (GradientAngle));
      }
    }

    public GradientStyles GradientStyle
    {
      get
      {
        return this.gradientStyle;
      }
      set
      {
        this.gradientStyle = value;
        this.OnNotifyPropertyChanged(nameof (GradientStyle));
      }
    }

    public ContentAlignment TextAlignment
    {
      get
      {
        return this.textAlignment;
      }
      set
      {
        this.textAlignment = value;
        this.OnNotifyPropertyChanged(nameof (TextAlignment));
      }
    }

    public event PropertyChangedEventHandler PropertyChanged;

    protected virtual void OnNotifyPropertyChanged(string name)
    {
      this.OnNotifyPropertyChanged(new PropertyChangedEventArgs(name));
    }

    protected virtual void OnNotifyPropertyChanged(PropertyChangedEventArgs args)
    {
      PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
      if (propertyChanged == null)
        return;
      propertyChanged((object) this, args);
    }
  }
}
