using Microsoft.UI.Xaml.Documents;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Xml.Serialization;
using System.Xml;
using TranslationTool.Model;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI;
using Microsoft.UI.Xaml;
using Windows.UI;
using System.Xml.Linq;
using System.Collections;
using System.Linq;

namespace TranslationTool.Renderer;
public class XmlITranslationFormatRenderer : ITranslationFormatRenderer
{

  public void FormatTranslations(TextBlock textBlock, XDocument? xmlDocument,
                                 List<string> xmlElements, List<string> xmlAttribute)
  {
    if (xmlDocument == null)
    {
      return;
    }

    Color elementColor = textBlock.ActualTheme == ElementTheme.Light ? Colors.DeepSkyBlue : Colors.LightSkyBlue;
    Color attributeNameColor = textBlock.ActualTheme == ElementTheme.Light ? Colors.Red : Colors.Red;
    Color attributeValueColor = textBlock.ActualTheme == ElementTheme.Light ? Colors.Yellow : Colors.DarkGreen;

    List<string> xmlStartElements = xmlElements.Select(element => $"<{element}>").ToList();
    List<string> xmlEndElements = xmlElements.Select(element => $"</{element}>").ToList();
    List<string> xmlStartAttributes = xmlAttribute.Select(element => $"<{element}>").ToList();

    string[] xmlDocToString = xmlDocument.ToString().Split(Environment.NewLine);
    string myStrings = "fghjK";
    string checkThis = "abc";

    if (myStrings.Any(checkThis.Contains))
    { }

    bool xmlElementOpened = false;
    // bool newLineAdded;
    foreach (string line in xmlDocToString)
    {
      //newLineAdded = false;
      string? lineToFormat = null;
      string startXmlElement = string.Empty, endXmlElement = string.Empty;
      for (int i = 0; i < xmlElements.Count; i++)
      {
        if (line.TrimStart().StartsWith(xmlStartElements[i]))
        {
          lineToFormat = line;
          xmlElementOpened = true;
          startXmlElement = lineToFormat[..(lineToFormat.IndexOf(xmlStartElements[i]) + xmlStartElements[i].Length)];
          lineToFormat = lineToFormat[startXmlElement.Length..];
        }
        if (line.EndsWith(xmlEndElements[i]))
        {
          xmlElementOpened = false;
          endXmlElement = xmlEndElements[i];
          lineToFormat = (lineToFormat ??= line)[..^xmlEndElements[i].Length];
        }

        if (lineToFormat is not null)
        {
          break;
        }
      }

      if (lineToFormat is not null || xmlElementOpened)
      {
        textBlock.Inlines.Add(new Run() { Text = startXmlElement });
        textBlock.Inlines.Add(new Run() { Text = lineToFormat ?? line, Foreground = new SolidColorBrush(elementColor)/*, FontWeight = FontWeights.Bold */});
        textBlock.Inlines.Add(new Run() { Text = endXmlElement });
      } else
      {
        // No formatting needed
        textBlock.Inlines.Add(new Run() { Text = line });
      }
      textBlock.Inlines.Add(new LineBreak());
    }

  }

  [Obsolete("FormatTranslations(TextBlock, IPosClientTranslationModel) is deprecated, please use FormatTranslations(TextBlock, XMLDocument) instead.")]
  public void FormatTranslations(TextBlock textBlock, IPosClientTranslationModel posClientTranslationModel)
  {
    // Create empty namespaces with empty value
    XmlSerializerNamespaces emptyNamespaces = new XmlSerializerNamespaces();
    emptyNamespaces.Add("", "");

    XmlWriterSettings writterSettings = new()
    {
      Indent = true,
      OmitXmlDeclaration = true,
    };

    XmlPosClientData xmlData = new XmlPosClientData((PosClientModel)posClientTranslationModel);

    HashSet<string> attributeNameList = new();
    HashSet<string> elementNameList = new();

    List<Type> elementsToRenderer = new List<Type> { typeof(XmlResheader), typeof(XmlMetadata), typeof(XmlTranslation) };
    foreach (Type xmlType in elementsToRenderer)
    {
      PropertyInfo[]? propertyInfos = xmlType.GetProperties();
      if ((propertyInfos is not null) && propertyInfos.Length > 0)
      {
        foreach (var property in propertyInfos)
        {
          Attribute[] propXmlAttributeList = Attribute.GetCustomAttributes(property, typeof(XmlAttributeAttribute), false);
          foreach (XmlAttributeAttribute propXmlAttribute in propXmlAttributeList)
          {
            // XML attribute found 
            attributeNameList.Add(propXmlAttribute.AttributeName);
          }

          Attribute[] propXmlElementList = Attribute.GetCustomAttributes(property, typeof(XmlElementAttribute), false);
          foreach (XmlElementAttribute propXmlElement in propXmlElementList)
          {
            // XML element found
            elementNameList.Add(propXmlElement.ElementName);
          }
        }
      }
    }

    using (StringWriter stream = new StringWriter())
    using (var writer = XmlWriter.Create(stream, writterSettings))
    {
      XmlSerializer serializer = new(typeof(XmlPosClientData));
      serializer.Serialize(writer, xmlData, emptyNamespaces);

      Color elementColor = textBlock.ActualTheme == ElementTheme.Light ? Colors.DeepSkyBlue : Colors.LightSkyBlue;
      Color attributeNameColor = textBlock.ActualTheme == ElementTheme.Light ? Colors.Red : Colors.Red;
      Color attributeValueColor = textBlock.ActualTheme == ElementTheme.Light ? Colors.Yellow : Colors.DarkGreen;

      string[] lines = stream.ToString().Split(Environment.NewLine);
      List<Run> formattedLines = new();
      bool xmlElementOpened = false;
      foreach (string line in lines)
      {
        foreach (string elementName in elementNameList)
        {
          string startXmlElementName = "<" + elementName + ">";
          string endXmlElementName = "</" + elementName + ">";
          string startXmlElement = string.Empty;
          string endXmlElement = string.Empty;
          string? lineToFormat = null;
          if (line.Trim().StartsWith(startXmlElementName))
          {
            xmlElementOpened = true;
            lineToFormat = line;
            startXmlElement = lineToFormat[..(lineToFormat.IndexOf(startXmlElementName) + startXmlElementName.Length)];
            lineToFormat = lineToFormat[startXmlElement.Length..];
          }
          if (line.Trim().EndsWith(endXmlElementName))
          {
            xmlElementOpened = false;
            endXmlElement = (lineToFormat ??= line)[^endXmlElementName.Length..];
            lineToFormat = lineToFormat[..^endXmlElementName.Length];
          }

          if ((lineToFormat is not null) || xmlElementOpened)
          {
            textBlock.Inlines.Add(new Run() { Text = startXmlElement });
            textBlock.Inlines.Add(new Run() { Text = lineToFormat ?? line, Foreground = new SolidColorBrush(elementColor)/*, FontWeight = FontWeights.Bold */});
            textBlock.Inlines.Add(new Run() { Text = endXmlElement });
          } else
          {
            // No formatting
            textBlock.Inlines.Add(new Run() { Text = line });
          }

          textBlock.Inlines.Add(new LineBreak());
          if ((startXmlElement is not null) || (endXmlElement is not null))
          {
            // There can only be one XML element per line.
            break;
          }
        }
      }
    }
  }
}