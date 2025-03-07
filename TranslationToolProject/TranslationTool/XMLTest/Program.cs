using System.Xml.Serialization;

namespace XMLTest;

public class Program
{
  const string XML_FILE = @".\..\..\..\..\..\..\..\..\TTestFiles\Core\POSClient.en-US.POSClient.en-US.xml";

  private static void Main(string[] args)
  {
    //DeserializeFromXml();

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
}