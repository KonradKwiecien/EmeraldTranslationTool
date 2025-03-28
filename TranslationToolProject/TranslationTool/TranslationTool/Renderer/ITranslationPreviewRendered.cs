
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System;
using System.Collections.Generic;
using System.Xml.Linq;
using TranslationTool.Model;
using Windows.UI;

namespace TranslationTool.Renderer;
public interface ITranslationPreviewRendered
{
  void SetConfig(RendererConfig config);

  void FormatTranslations(TextBlock textBlock, XDocument? xmlDocument);

  void FormatTranslations(TextBlock textBlock, string[]? xmlLines,
                          List<string> xmlElements, List<string> xmlAttribute);
}
