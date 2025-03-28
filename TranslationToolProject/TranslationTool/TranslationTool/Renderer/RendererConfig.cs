using System.Collections.Generic;
using Microsoft.UI.Xaml;
using Windows.UI;

namespace TranslationTool.Renderer
{
  public class RendererConfig(string rootElementName)
  {
    private List<string> _xmlElementsToRender = [];
    private Dictionary<ElementTheme, Dictionary<string, Color>> _xmlAttributeColorDictionary = [];
    private Dictionary<ElementTheme, Dictionary<string, Color>> _xmlValueColorDictionary = [];

    public string RootElementName { get => rootElementName; }
    public List<string> XmlElementsToRender { get => _xmlElementsToRender; }
    public Dictionary<ElementTheme, Dictionary<string, Color>> AttributeColorDictionary { get => _xmlAttributeColorDictionary; }
    public Dictionary<ElementTheme, Dictionary<string, Color>> ValueColorDictionary { get => _xmlValueColorDictionary; }

    public void AddXmlElementsToRender(string[] xmlElementsName)
    {
      _xmlElementsToRender.AddRange(xmlElementsName);
    }

    public bool HasXmlElementsToRender()
    {
      return _xmlElementsToRender.Count > 0;
    }

    public void AddXmlAttributeColors(string name, Color colorLightTheme, Color colorDarkTheme)
    {
      AddColor(_xmlAttributeColorDictionary, ElementTheme.Light, name, colorLightTheme);
      AddColor(_xmlAttributeColorDictionary, ElementTheme.Dark, name, colorDarkTheme);
    }

    public void AddXmlValueColors(string name, Color colorLightTheme, Color colorDarkTheme)
    {
      AddColor(_xmlValueColorDictionary, ElementTheme.Light, name, colorLightTheme);
      AddColor(_xmlValueColorDictionary, ElementTheme.Dark, name, colorDarkTheme);
    }

    // Adds a color to the specified dictionary for the given theme and name.
    private void AddColor(Dictionary<ElementTheme, Dictionary<string, Color>> colorDictionary, ElementTheme theme, string name, Color color)
    {
      if (colorDictionary.TryGetValue(theme, out Dictionary<string, Color>? colors))
      {
        colors[name] = color;
      }
      else
      {
        colorDictionary[theme] = new Dictionary<string, Color>() { { name, color } };
      }
    }
  }
}
