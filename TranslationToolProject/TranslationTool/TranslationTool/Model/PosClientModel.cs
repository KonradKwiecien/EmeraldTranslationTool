using System;
using System.Collections.Generic;
using System.Xml.Linq;

namespace TranslationTool.Model;
public class Resheader
{
  required public string? Value { get; set; }
  required public string? Key { get; set; }
}

public class Metadata
{
  public string? Space { set; get; }
  required public string? Value { set; get; }
  required public string? Key { set; get; }
}

public class Translation : ITranslation
{
  public string? Space { set; get; }
  required public string Text { set; get; }
  required public string Key { set; get; }
}

[Obsolete("PosClientModel is deprecated, please use PosClientTranslationModel instead.")]
public class PosClientModel : IPosClientTranslationModel
{
  private List<Resheader>? _resheaderList;
  private List<Metadata>? _metadataList;
  private List<ITranslation>? _translatonList;

  #region Properties
  public List<Resheader>? Resheaders { get => _resheaderList; }
  public List<Metadata>? Metadata { get => _metadataList; }

  public XDocument? XmlTranslationsDocument => throw new NotImplementedException();
  public List<ITranslation>? Translations { get => _translatonList; }
  #endregion

  public void AddResheader(Resheader resheader)
  {
    if (resheader is not null)
    {
      (_resheaderList ??= []).Add(resheader);
    }
  }

  public void AddMetadada(Metadata metadata)
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

  public void AddRangeResheaders(List<Resheader> resheaderList)
  {
    if (resheaderList is not null)
    {
      (_resheaderList ??= []).AddRange(resheaderList);
    }
  }

  public void AddRangeMetadata(List<Metadata>? metadataList)
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
