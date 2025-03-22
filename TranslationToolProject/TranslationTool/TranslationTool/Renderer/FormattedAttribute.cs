using System;

namespace TranslationTool.Renderer;

public class FormattedAttribute
{
  public string Text { get; private set; } = string.Empty;
  public string ArttributeName { get; }

  public FormattedAttribute(string line, string arttributeName)
  {
    ArttributeName = arttributeName;
    int startIndex = line.IndexOf($" {arttributeName}=\"") + 1;
    if (startIndex > 0)
    {
      int endIndex = line.IndexOf("\"", startIndex + arttributeName.Length + 2) + 1;
      Text = line[startIndex..endIndex];
    }
  }
}
