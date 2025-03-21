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
using static System.Runtime.InteropServices.JavaScript.JSType;

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

    Color fordegroundColor;
    Color elementColor = textBlock.ActualTheme == ElementTheme.Light ? Colors.DeepSkyBlue : Colors.LightSkyBlue;
    Color attributeColor = textBlock.ActualTheme == ElementTheme.Light ? Colors.Red : Colors.Red;

    List<string> xmlStartElements = xmlElements.Select(element => $"<{element}>").ToList();
    List<string> xmlEndElements = xmlElements.Select(element => $"</{element}>").ToList();
    List<string> xmlStartAttributes = xmlAttribute.Select(attribute => $" {attribute}=\"").ToList();

    string[] xmlDocToString = xmlDocument.ToString().Split(Environment.NewLine);

    bool xmlElementOpened = false;
    foreach (string line in xmlDocToString)
    {
      string textNotFormattedStart = string.Empty, textNotFormattedEnd = string.Empty;
      string? formattedText = null;

      string? firstAttribute = xmlStartAttributes
               .Select(Word => new { Word, Index = line.IndexOf(Word) })
               .Where(p => p.Index >= 0)
               .OrderBy(p => p.Index)
               .Select(p => p.Word)
               .FirstOrDefault();

      if (firstAttribute is not null)
      {
         fordegroundColor = attributeColor;
        // At least one attribute was found.
        textNotFormattedStart = line[..(line.IndexOf(firstAttribute) + 1)];
        formattedText = line[(line.IndexOf(firstAttribute) + 1)..^1];
        textNotFormattedEnd = line[^1..];
      } else
      {
        fordegroundColor = elementColor;
        // Check value for the XML elements-
        for (int i = 0; i < xmlElements.Count; i++)
        {
          string[] tokens = line.Split(xmlStartElements[i]);
          if (line.TrimStart().StartsWith(xmlStartElements[i]))
          {
            // Start XML element was found
            formattedText = line;
            xmlElementOpened = true;
            textNotFormattedStart = formattedText[..(formattedText.IndexOf(xmlStartElements[i]) + xmlStartElements[i].Length)];
            formattedText = formattedText[textNotFormattedStart.Length..];
          }
          if (xmlElementOpened && line.EndsWith(xmlEndElements[i]))
          {
            // End XML element was found
            xmlElementOpened = false;
            textNotFormattedEnd = xmlEndElements[i];
            formattedText = (formattedText ??= line)[..^xmlEndElements[i].Length];
            // It must be the last xml element in the line - we don't need any check more.
            break;
          }
          if ((formattedText is not null) || xmlElementOpened)
          {
            break;
          }
        }
      } // Check XML elements

      if ((formattedText is not null) || xmlElementOpened)
      {
        // Formatting needed
        textBlock.Inlines.Add(new Run() { Text = textNotFormattedStart });
        textBlock.Inlines.Add(new Run() { Text = formattedText ?? line, Foreground = new SolidColorBrush(fordegroundColor)/*, FontWeight = FontWeights.Bold */});
        textBlock.Inlines.Add(new Run() { Text = textNotFormattedEnd });
      } else
      {
        // No formatting for XML element was found
        textBlock.Inlines.Add(new Run() { Text = line });
      }

      textBlock.Inlines.Add(new LineBreak());
    } // for each line
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