using System.Xml.Serialization;

[XmlType(AnonymousType = true)]
[XmlRoot("root", Namespace = "", IsNullable = false)]
public partial class XMLPosClientModel
{
  [XmlElement("resheader")]
  public Resheader[]? Resheaders;

  [XmlElement("metadata")]
  public Metadata[]? Metadata;

  [XmlElement("data")]
  public TranslationData[]? TranslaionData;
}

[XmlType(AnonymousType = true)]
public partial class Resheader
{
  [XmlElement("value")]
  public string? Value;

  [XmlAttribute("name")]
  public string? NameAttribute;
}

[XmlType(AnonymousType = true)]
public partial class Metadata
{
  [XmlElement("value")]
  public string? Value;

  [XmlAttribute("name")]
  public string? NameAttribute;

  [XmlAttribute(Form = System.Xml.Schema.XmlSchemaForm.Qualified, Namespace = "http://www.w3.org/XML/1998/namespace")]
  public string? space;
}

[XmlType(AnonymousType = true)]
public partial class TranslationData
{
  [XmlElement("value")]
  public string? Value;

  [XmlAttribute("name")]
  public string? NameAttribute;

  [XmlAttribute(Form = System.Xml.Schema.XmlSchemaForm.Qualified, Namespace = "http://www.w3.org/XML/1998/namespace")]
  public string? space;
}

