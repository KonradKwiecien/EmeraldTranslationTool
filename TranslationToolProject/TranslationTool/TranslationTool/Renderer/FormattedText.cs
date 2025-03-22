namespace TranslationTool.Renderer;

public class FormattedText
{
  public string? Text { get; private set; }

  public FormattedText(string line, string elementName, bool elementOpened)
  {
    int startIndex = 0;
    if (!elementOpened)
    {
      startIndex = line.IndexOf($"<{elementName}>");
      if (startIndex == -1)
      { // XML start element not found.
        return;
      }

      startIndex = startIndex + elementName.Length + 2;
    }

    int endIndex = line.IndexOf($"</{elementName}>");
    if (endIndex == -1)
    {
      // No ending XML element was found - get everything up to end of the line.
      endIndex = line.Length;
    }

    Text = line[startIndex..endIndex];
  }
}
