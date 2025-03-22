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
using System.Xml.Linq;
using System.Linq;
using Microsoft.UI.Text;
using Windows.UI;

namespace TranslationTool.Renderer;
public class XmlITranslationFormatRenderer : ITranslationFormatRenderer
{

  private Dictionary<ElementTheme, Dictionary<string, Color>> attributeColorDictionary = new Dictionary<ElementTheme, Dictionary<string, Color>>();

  public void SetColorForAttributes(ElementTheme theme, string attrName, Color attrColor)
  {
    if (attributeColorDictionary.TryGetValue(theme, out Dictionary<string, Color>? colors))
    {
      colors[attrName] = attrColor;
    } else
    {
      attributeColorDictionary[theme] = new Dictionary<string, Color>() { { attrName, attrColor } };
    }
  }

  public void FormatTranslations(TextBlock textBlock, XDocument? xmlDocument,
                                 List<string> xmlElementList, List<string> xmlAttributeList)
  {
    if (xmlDocument == null)
    {
      return;
    }

    Color elementColor = (textBlock.ActualTheme == ElementTheme.Light) ? Colors.MediumBlue : Colors.RoyalBlue;
    string[] xmlDocToString = xmlDocument.ToString().Split(Environment.NewLine);

    bool xmlElementOpened = false;
    foreach (string line in xmlDocToString)
    {
      string textNotFormattedStart = string.Empty, textNotFormattedEnd = string.Empty;

      FormattedAttribute[] attributesToFormat = xmlAttributeList
                                .Select(attr => new FormattedAttribute(line, attr))
                                .Where(p => p.Text.Length > 0)
                                .ToArray();

      if (attributesToFormat.Length > 0)
      {
        // Line with XMl attributes was found
        string[] textWithoutFormat = line.Split(attributesToFormat.Select(t => t.Text).ToArray(), StringSplitOptions.None);
        for (int i = 0; i < attributesToFormat.Length; i++)
        {
          Color attrColor = (textBlock.ActualTheme == ElementTheme.Light) ? Colors.DarkGray : Colors.Gray;
          Color customizedColor;
          if (attributeColorDictionary.ContainsKey(textBlock.ActualTheme) 
            && attributeColorDictionary[textBlock.ActualTheme].TryGetValue(attributesToFormat[i].ArttributeName, out customizedColor))
          {
            attrColor = customizedColor;
          }
          SolidColorBrush attributeForegroundColor = new SolidColorBrush(attrColor);
          textBlock.Inlines.Add(new Run() { Text = textWithoutFormat[i] });
          textBlock.Inlines.Add(new Run() { Text = attributesToFormat[i].Text, Foreground = attributeForegroundColor });
        }
        textBlock.Inlines.Add(new Run() { Text = textWithoutFormat.Last() });
      } else
      {
        // Line with XMl element was found
        string? textToFormat = xmlElementList
                               .Select(elem => new FormattedText(line, elem, xmlElementOpened))
                               .Where(p => p.Text is not null)
                               .Select(t => t.Text)
                              .FirstOrDefault();

        if (textToFormat is not null)
        {
          string[] textWithoutFormat = line.Split(textToFormat);
          xmlElementOpened = (!xmlElementOpened && (textWithoutFormat.Length == 1))
            || (textWithoutFormat.Last().Length == 0);
          textBlock.Inlines.Add(new Run() { Text = textWithoutFormat.First() });
          textBlock.Inlines.Add(new Run() { Text = textToFormat, Foreground = new SolidColorBrush(elementColor), FontWeight = FontWeights.Bold });
          if (!xmlElementOpened || (xmlElementOpened && !textWithoutFormat.First().Equals(textWithoutFormat.Last())))
          {
            textBlock.Inlines.Add(new Run() { Text = textWithoutFormat.Last() });
          }
        } else
        {
          textBlock.Inlines.Add(new Run() { Text = line });
        }
      } // Check XML elements
      textBlock.Inlines.Add(new LineBreak());
    } // foreach line
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