namespace TranslationTool.Model;

public class POSClientResourceModel
{
  public string Key { get; set; } = string.Empty;
  public string? Space { get; set; }
  public string Translation { get; set; } = string.Empty;

  public override string ToString()
  {
    return string.Format("Key={0}, Space={1}, Translation={2}", Key, Space is null ? "not set" : Space, Translation);
  }
}
