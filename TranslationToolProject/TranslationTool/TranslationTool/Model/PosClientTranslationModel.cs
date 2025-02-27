using System.Collections.Generic;

namespace TranslationTool.Model;

public class PosClientTranslationModel : IPosClientTranslationModel
{
  private List<IResheader>? _resheaderList;
  private List<IMetadata>? _metadataList;
  private List<ITranslation>? _translatonList;

  #region Properties
  public List<IResheader>? Resheaders { get => _resheaderList; }
  public List<IMetadata>? Metadata { get => _metadataList; }
  public List<ITranslation>? Translations { get => _translatonList; }
  #endregion

  public void AddResheader(IResheader resheader)
  {
    if (resheader is not null)
    {
      (_resheaderList ??= []).Add(resheader);
    }
  }

  public void AddMetadada(IMetadata metadata)
  {
    if (metadata is not null)
    {
      (_metadataList ??= []).Add(metadata);
    }
  }

  public void AddTranslation(ITranslation translation)
  {
    if (translation is not null)
    {
      (_translatonList ??= []).Add(translation);
    }
  }

  public void AddRangeResheaders(List<IResheader> resheaderList)
  {
    if (resheaderList is not null)
    {
      (_resheaderList ??= []).AddRange(resheaderList);
    }
  }

  public void AddRangeMetadata(List<IMetadata>? metadataList)
  {
    if (metadataList is not null)
    {
      (_metadataList ??= []).AddRange(metadataList);
    }
  }

  public void AddRangeTranslation(List<ITranslation> translationList)
  {
    if (translationList is not null)
    {
      (_translatonList ??= []).AddRange(translationList);
    }
  }
}

public class Resheader : IResheader
{
  required public string Value { get; set; }
  required public string Key { get; set; }
}

public class Metadata : IMetadata
{
  public string? Space { set; get; }
  required public string Value { set; get; }
  required public string Key { set; get; }
}

public class Translation : ITranslation
{
  public string? Space { set; get; }
  required public string Text { set; get; }
  required public string Key { set; get; }
}
