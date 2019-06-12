// Decompiled with JetBrains decompiler
// Type: Telerik.Data.Expressions.ExpressionItemsList
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Xml;

namespace Telerik.Data.Expressions
{
  public class ExpressionItemsList : List<ExpressionItem>
  {
    public void LoadFromXML()
    {
      this.LoadFromXML(Assembly.GetExecutingAssembly().GetManifestResourceStream("Telerik.WinControls.Data.Expressions.DescribeItemsList.ExpressionItemsListData.xml"));
    }

    public void LoadFromXML(string path)
    {
      this.LoadFromXML((Stream) File.OpenRead(path));
    }

    public void LoadFromXML(Stream stream)
    {
      if (stream == null)
        return;
      this.Clear();
      XmlDocument xmlDocument = new XmlDocument();
      xmlDocument.Load(stream);
      foreach (XmlNode xmlNode in xmlDocument.GetElementsByTagName("ExpressionItem"))
      {
        ExpressionItem expressionItem = new ExpressionItem();
        expressionItem.Name = xmlNode.Attributes["Name"].Value;
        expressionItem.Syntax = xmlNode.Attributes["Syntax"].Value;
        expressionItem.Value = xmlNode.Attributes["Value"].Value;
        expressionItem.Type = (ExpressionItemType) Enum.Parse(typeof (ExpressionItemType), xmlNode.Attributes["Type"].Value, true);
        foreach (XmlNode childNode in xmlNode.ChildNodes)
        {
          if (childNode.Name == "Description")
          {
            expressionItem.Description = childNode.InnerText;
            break;
          }
        }
        this.Add(expressionItem);
      }
    }
  }
}
