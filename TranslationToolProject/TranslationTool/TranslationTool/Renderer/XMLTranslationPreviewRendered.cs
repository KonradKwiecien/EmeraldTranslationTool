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
public class XMLTranslationPreviewRendered : ITranslationPreviewRendered
{
  private RendererConfig? _rendererConfig;

  public void SetConfig(RendererConfig config)
  {
    _rendererConfig = config;
  }

  public void FormatTranslations(TextBlock textBlock, XDocument? xmlDocument)
  {
    textBlock.Text = null;

    if (xmlDocument == null)
    {
      // xmlDocument is null - nothing to render.
      return;
    }

    if (!(_rendererConfig?.HasXmlElementsToRender() ?? false))
    {
      // Get all elements from the XML document without any rendering.
      textBlock.Text = xmlDocument.ToString();
      return;
    }

    var rootElementToShow = xmlDocument.Descendants(_rendererConfig.RootElementName).FirstOrDefault();
    if (rootElementToShow == null)
    {
      // Root element not found - nothing to render.
      return;
    }


    //.ForEach(e => rootElementToShow.Descendants(e).ToList().ForEach(e => e.Remove()));
    List<XElement> xmlElements = rootElementToShow.Descendants()
                                   .Where(e => _rendererConfig.XmlElementsToRender.Any(name => name.Contains(e.Name.LocalName)))
                                   .ToList();
                                            

    foreach (XElement element in xmlElements)
    {
      textBlock.Inlines.Add(new Run() { Text = element.ToString() });
      textBlock.Inlines.Add(new LineBreak());
    }
  }

  public void FormatTranslations(TextBlock textBlock, string[]? xmlLines,
                                 List<string> xmlElementList, List<string> xmlAttributeList)
  {
    if (xmlLines == null)
    {
      return;
    }

    Color elementColor = (textBlock.ActualTheme == ElementTheme.Light) ? Colors.MediumBlue : Colors.RoyalBlue;

    bool xmlElementOpened = false;
    foreach (string line in xmlLines)
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
          Color attrColor = (textBlock.ActualTheme == ElementTheme.Light) ? Colors.DimGray : Colors.DarkGray;
          Color customizedColor;
          //if (attributeColorDictionary.ContainsKey(textBlock.ActualTheme)
          //  && attributeColorDictionary[textBlock.ActualTheme].TryGetValue(attributesToFormat[i].ArttributeName, out customizedColor))
          //{
          //  attrColor = customizedColor;
          //}
          SolidColorBrush attributeForegroundColor = new SolidColorBrush(attrColor);
          textBlock.Inlines.Add(new Run() { Text = textWithoutFormat[i] });
          textBlock.Inlines.Add(new Run() { Text = attributesToFormat[i].Text, Foreground = attributeForegroundColor });
        }
        textBlock.Inlines.Add(new Run() { Text = textWithoutFormat.Last() });
      }
      else
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
        }
        else
        {
          textBlock.Inlines.Add(new Run() { Text = line });
        }
      } // Check XML elements
      textBlock.Inlines.Add(new LineBreak());
    } // foreach line
  }
}