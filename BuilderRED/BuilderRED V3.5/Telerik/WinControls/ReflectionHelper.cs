// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.ReflectionHelper
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Reflection;

namespace Telerik.WinControls
{
  public static class ReflectionHelper
  {
    public static void CopyFields<T>(T target, T source) where T : class
    {
      BindingFlags bindingAttr = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;
      FieldInfo[] fields = target.GetType().GetFields(bindingAttr);
      Type attributeType = typeof (NonSerializedAttribute);
      foreach (FieldInfo fieldInfo in fields)
      {
        object[] customAttributes = fieldInfo.GetCustomAttributes(attributeType, true);
        if ((customAttributes == null || customAttributes.Length <= 0) && !fieldInfo.FieldType.IsAssignableFrom(typeof (Delegate)))
        {
          object obj = fieldInfo.GetValue((object) source);
          fieldInfo.SetValue((object) target, obj);
        }
      }
      ((object) target as IRadCloneable)?.OnFieldsCopied();
    }

    public static T Clone<T>(T source) where T : class, new()
    {
      T target = new T();
      ReflectionHelper.CopyFields<T>(target, source);
      ((object) target as IRadCloneable)?.OnCloneComplete();
      return target;
    }
  }
}
