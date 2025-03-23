using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using System.Xml.Serialization;
using TranslationTool.Model;

namespace TranslationTool.Data;
public class POSClientResxResourceProvider : IPOSClientResxResourceProvider
{
  public IPosClientTranslationModel? LoadToXDocumentFromRexsFile(string fullResxFile)
  {
    IPosClientTranslationModel? posClientTranslationModel = null;
    if (File.Exists(fullResxFile))
    {
      posClientTranslationModel = new PosClientTranslationModel() { XmlTranslationsDocument = XDocument.Load(fullResxFile) };
    }

    return posClientTranslationModel;
  }

  public IPosClientTranslationModel? LoadAsTextFileFromRexsFile(string fullResxFile)
  {
    IPosClientTranslationModel? posClientTranslationModel = null;
    if (File.Exists(fullResxFile))
    {
      posClientTranslationModel = new PosClientTranslationModel() { XMLlTranslationsFile = File.ReadLines(fullResxFile).ToArray() };
    }

    return posClientTranslationModel;
  }

  [Obsolete("DeserializeFromResxFile is deprecated, please use LoadXmlFromRexsFile instead.")]
  public IPosClientTranslationModel? DeserializeFromRexsFile(string fullResxFile)
  {
    PosClientModel? posClientTranslationModel = null;

    if ((fullResxFile is not null) && File.Exists(fullResxFile))
    {
      string xmlString = File.ReadAllText(fullResxFile);
      // Create XML Serializer
      XmlSerializer serializer = new(typeof(XmlPosClientData));
      // Create a StringReader with the value from the file
      using (StringReader stringReader = new(xmlString))
      {
        // Convert the string to a product
        XmlPosClientData? xmldata = (XmlPosClientData?)serializer.Deserialize(stringReader);
        if (xmldata is not null)
        {
          posClientTranslationModel = new PosClientModel();
          if (xmldata.Resheaders is not null)
          {
            foreach (XmlResheader xmlResheader in xmldata.Resheaders)
            {
              posClientTranslationModel.AddResheader(
                new Resheader()
                { Key = xmlResheader.Key, Value = xmlResheader.Value });
            }
          }

          if (xmldata.Metadata is not null)
          {
            foreach (XmlMetadata xmlMetatada in xmldata.Metadata)
            {
              posClientTranslationModel.AddMetadada(
                new Metadata()
                {
                  Key = xmlMetatada.Key, Value = xmlMetatada.Value, Space = xmlMetatada.Space
                });
            }
          }

          if (xmldata.Translatons is not null)
          {
            foreach (XmlTranslation xmlTranslation in xmldata.Translatons)
            {
              posClientTranslationModel.AddTranslation(
                new Translation()
                {
                  Key = xmlTranslation.Key ??= string.Empty, Text = xmlTranslation.Text ??= string.Empty, Space = xmlTranslation.Space
                });
            }
          }
        }
      }
    }

    return posClientTranslationModel;
  }
}
