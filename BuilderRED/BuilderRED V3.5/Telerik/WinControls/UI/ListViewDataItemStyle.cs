// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.ListViewDataItemStyle
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Telerik.WinControls.UI
{
  internal class ListViewDataItemStyle : INotifyPropertyChanged
  {
    public static readonly Color DefaultForeColor = Color.Empty;
    public static readonly Color DefaultBackColor = Color.Empty;
    public static readonly Color DefaultBackColor2 = Color.Empty;
    public static readonly Color DefaultBackColor3 = Color.Empty;
    public static readonly Color DefaultBackColor4 = Color.Empty;
    public static readonly Color DefaultBorderColor = Color.Empty;
    public static readonly Font DefaultFont = (Font) null;
    public static readonly int DefaultNumberOfColors = 4;
    public static readonly float DefaultGradientPercentage = 0.5f;
    public static readonly float DefaultGradientPercentage2 = 0.5f;
    public static readonly float DefaultGradientAngle = 90f;
    public static readonly GradientStyles DefaultGradientStyle = GradientStyles.Linear;
    public static readonly ContentAlignment DefaultTextAlignment = ContentAlignment.MiddleLeft;
    public static readonly ContentAlignment DefaultImageAlignment = ContentAlignment.MiddleLeft;
    public static readonly TextImageRelation DefaultTextImageRelation = TextImageRelation.ImageBeforeText;
    private Color foreColor = ListViewDataItemStyle.DefaultForeColor;
    private Color backColor = ListViewDataItemStyle.DefaultBackColor;
    private Color backColor2 = ListViewDataItemStyle.DefaultBackColor2;
    private Color backColor3 = ListViewDataItemStyle.DefaultBackColor3;
    private Color backColor4 = ListViewDataItemStyle.DefaultBackColor4;
    private Color borderColor = ListViewDataItemStyle.DefaultBorderColor;
    private Font font = ListViewDataItemStyle.DefaultFont;
    private int numberOfColors = ListViewDataItemStyle.DefaultNumberOfColors;
    private float gradientPercentage = ListViewDataItemStyle.DefaultGradientPercentage;
    private float gradientPercentage2 = ListViewDataItemStyle.DefaultGradientPercentage2;
    private float gradientAngle = ListViewDataItemStyle.DefaultGradientAngle;
    private GradientStyles gradientStyle = ListViewDataItemStyle.DefaultGradientStyle;
    private ContentAlignment textAlignment = ListViewDataItemStyle.DefaultTextAlignment;
    private ContentAlignment imageAlignment = ListViewDataItemStyle.DefaultImageAlignment;
    private TextImageRelation textImageRelation = ListViewDataItemStyle.DefaultTextImageRelation;

    public TextImageRelation TextImageRelation
    {
      get
      {
        return this.textImageRelation;
      }
      set
      {
        this.textImageRelation = value;
      }
    }

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

    public ContentAlignment ImageAlignment
    {
      get
      {
        return this.imageAlignment;
      }
      set
      {
        this.imageAlignment = value;
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
