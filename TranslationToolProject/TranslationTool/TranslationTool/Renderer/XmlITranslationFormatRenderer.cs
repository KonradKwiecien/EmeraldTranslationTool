using Microsoft.UI.Xaml.Documents;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Xml.Serialization;
using System.Xml;
using TranslationTool.Model;
using Microsoft.UI.Xaml.Controls;

namespace TranslationTool.Renderer;
public class XmlITranslationFormatRenderer : ITranslationFormatRenderer
{
  public void FormatTranslations(TextBlock textBlock, PosClientTranslationModel posClientTranslationModel)
  {
    // Create empty namespaces with empty value
    XmlSerializerNamespaces emptyNamespaces = new XmlSerializerNamespaces();
    emptyNamespaces.Add("", "");

    XmlWriterSettings settings = new()
    {
      Indent = true,
      OmitXmlDeclaration = true,
    };

    XmlPosClientData xmlData = new XmlPosClientData();

    foreach(Resheader resheader in posClientTranslationModel.)
    {

    }

    XmlResheader xmlRes = new() { Key = resheader.Key, Value = resheader.Value };

    FieldInfo[] fields = xmlRes.GetType().GetFields();
    List<string> attributeNameList = new();
    List<string> elementNameList = new();
    foreach (var field in fields)
    {
      Attribute[] propXmlAttributeList = Attribute.GetCustomAttributes(field, typeof(XmlAttributeAttribute), false);
      foreach (XmlAttributeAttribute propXmlAttribute in propXmlAttributeList)
      {
        // XML attribute found 
        attributeNameList.Add($@"""{field.GetValue(xmlRes)}""");
      }

      Attribute[] propXmlElementList = Attribute.GetCustomAttributes(field, typeof(XmlElementAttribute), false);
      foreach (XmlElementAttribute propXmlElement in propXmlElementList)
      {
        // XML element found
        elementNameList.Add($"{field.GetValue(xmlRes)}");
      }
    }

    using (StringWriter stream = new StringWriter())
    using (var writer = XmlWriter.Create(stream, settings))
    {
      XmlSerializer serializer = new(typeof(XmlResheader)); ;
      serializer.Serialize(writer, xmlRes, emptyNamespaces);

      string[] lines = stream.ToString().Split(Environment.NewLine);
      List<Run> formattedLines = new();
      foreach (string line in lines)
      {

      }
      // new string[] { Environment.NewLine },
      //StringSplitOptions.None);

      return formattedLines;
    }
  }
}