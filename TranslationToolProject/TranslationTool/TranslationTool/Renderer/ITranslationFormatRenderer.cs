
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System;
using System.Collections.Generic;
using System.Xml.Linq;
using TranslationTool.Model;
using Windows.UI;

namespace TranslationTool.Renderer;
public interface ITranslationFormatRenderer
{
  void SetColorForAttributes(ElementTheme theme, string attrName, Color attrColor);

  void FormatTranslations(TextBlock textBlock, XDocument? xmlDocument,
                          List<string> xmlElements, List<string> xmlAttribute);

  [Obsolete("FormatTranslations(TextBlock, IPosClientTranslationModel) is deprecated, please use FormatTranslations(TextBlock, XMLDocument) instead.")]
  void FormatTranslations(TextBlock textBlock, IPosClientTranslationModel posClientTranslationModel);
}
