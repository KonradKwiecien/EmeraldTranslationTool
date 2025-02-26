using System.Xml.Linq;
using System.Xml.Serialization;
using TranslationTool.Model;

public class Program
{
  const string XML_FILE = @".\..\..\..\..\..\TestFiles\Core\POSClient.en-US.POSClient.en-US.xml";

  private static void Main(string[] args)
  {
    //ReadXMLFile();
    DeserializeFromXml();

    Console.ReadKey();
  }

  private static void DeserializeFromXml()
  {
    XmlPosClientData? model;
    string xmlString = File.ReadAllText(XML_FILE);

    // Create XML Serializer
    XmlSerializer serializer = new(typeof(XmlPosClientData));
    // Create a StringReader with the value from the file
    using (StringReader sr = new(xmlString))
    {
      // Convert the string to a product
      model = (XmlPosClientData?)serializer.Deserialize(sr);
    }

    // Display Product
    Console.WriteLine(model);
  }

  private static void ReadXMLFile()
  {
    var xDoc = XDocument.Load(XML_FILE);
    IEnumerable<XElement>? dataTags = xDoc.Root?.Elements("data");


    List<POSClientResourceModel> pOSClientResources = new();

    if (dataTags != null)
    {
      foreach (XElement data in dataTags)
      {// http://www.w3.org/XML/1998/namespace}space}	System.Xml.Linq.XName}space}	System.Xml.Linq.XName

        string? attrName = data.Attribute("name")?.Value;
        string? attrSpace = data.Attribute(XNamespace.Xml + "space")?.Value;
        string? value = data.Element("value")?.Value;
        if ((attrName is not null) && (value is not null))
        {
          var pOSClientResourceModel = new POSClientResourceModel { Key = attrName, Space = attrSpace, Translation = value };
          Console.WriteLine($"POSClientResource: {pOSClientResourceModel}");
        }
      }
    }
  }
}