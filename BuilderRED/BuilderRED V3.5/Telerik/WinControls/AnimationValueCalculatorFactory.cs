// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.AnimationValueCalculatorFactory
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections.Specialized;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;

namespace Telerik.WinControls
{
  public class AnimationValueCalculatorFactory
  {
    private static HybridDictionary registeredCalculators = new HybridDictionary();
    private static CultureInfo serializationCulture = CultureInfo.GetCultureInfo("en-US");

    static AnimationValueCalculatorFactory()
    {
      AnimationValueCalculatorFactory.RegisterAnimationValueCalculatorType(typeof (bool), typeof (AnimationValueBoolCalculator));
      AnimationValueCalculatorFactory.RegisterAnimationValueCalculatorType(typeof (int), typeof (AnimationValueIntCalculator));
      AnimationValueCalculatorFactory.RegisterAnimationValueCalculatorType(typeof (Rectangle), typeof (AnimationValueRectangleCalculator));
      AnimationValueCalculatorFactory.RegisterAnimationValueCalculatorType(typeof (Color), typeof (AnimationValueColorCalculator));
      AnimationValueCalculatorFactory.RegisterAnimationValueCalculatorType(typeof (Font), typeof (AnimationValueFontCalculator));
      AnimationValueCalculatorFactory.RegisterAnimationValueCalculatorType(typeof (float), typeof (AnimationValueFloatCalculator));
      AnimationValueCalculatorFactory.RegisterAnimationValueCalculatorType(typeof (double), typeof (AnimationValueDoubleCalculator));
      AnimationValueCalculatorFactory.RegisterAnimationValueCalculatorType(typeof (Padding), typeof (AnimationValuePaddingCalculator));
      AnimationValueCalculatorFactory.RegisterAnimationValueCalculatorType(typeof (Size), typeof (AnimationValueSizeCalculator));
      AnimationValueCalculatorFactory.RegisterAnimationValueCalculatorType(typeof (SizeF), typeof (AnimationValueSizeFCalculator));
      AnimationValueCalculatorFactory.RegisterAnimationValueCalculatorType(typeof (Point), typeof (AnimationValuePointCalculator));
      AnimationValueCalculatorFactory.RegisterAnimationValueCalculatorType(typeof (PointF), typeof (AnimationValuePointFCalculator));
    }

    public static CultureInfo SerializationCulture
    {
      get
      {
        return AnimationValueCalculatorFactory.serializationCulture;
      }
    }

    public static void RegisterAnimationValueCalculatorType(System.Type objectType, System.Type calculatorType)
    {
      if (!calculatorType.IsSubclassOf(typeof (AnimationValueCalculator)))
        throw new InvalidOperationException("calculatorType should inherit from class " + typeof (AnimationValueCalculator).FullName);
      AnimationValueCalculatorFactory.registeredCalculators[(object) objectType] = (object) calculatorType;
    }

    public static AnimationValueCalculator GetCalculator(System.Type objectType)
    {
      System.Type registeredCalculator = (System.Type) AnimationValueCalculatorFactory.registeredCalculators[(object) objectType];
      if ((object) registeredCalculator == null)
        return (AnimationValueCalculator) null;
      return (AnimationValueCalculator) Activator.CreateInstance(registeredCalculator);
    }
  }
}
