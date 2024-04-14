// PORTAL-DOORS Project Copyright (c) 2006-2024 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.CoreDataLib.Models;

[KnownType(typeof(NpdsDoorsServerItem)), XmlSchemaProvider(null, IsAny = true)]
public class NpdsDoorsServerItem : ANpdsXsgBaseItem<XElement>
{
  public NpdsDoorsServerItem() : base() { this.Initialize(NPDSCD.ResrepFormatDOORS); }
  public NpdsDoorsServerItem(NpdsResrepFormat rrf) : base() { this.Initialize(rrf); }

  private void Initialize(NpdsResrepFormat rrf)
  {
    base.InitNpdsItem(NpdsFieldRule.Required, DoorsServerXnam);
  }

  public override void WriteXml(XmlWriter xWriter)
  {
    var writer = (NpdsXmlWrappingWriter)xWriter;
    writer.WriteStartElement(ItemXnam);
    if (writer.WRACE.DoorsRecords != null)
    {
      writer.WRACE.DoorsRecords.WriteXml(writer);
    }
    else if (writer.WRACE.VerboseFormat)
    {
      writer.WriteAttributeString(ListFacetCountXnam, "0");
    }
    writer.WriteEndElement();
  }

  public override void ReadXml(XmlReader xReader)
  {
    var reader = (NpdsXmlWrappingReader)xReader;
    reader.MoveToContent();
    if (reader.IsEmptyElement)
    {
      reader.ReadStartElement();
    }
    reader.ReadToFollowing(ResponseXnam);
    if (reader.IsStartElement(ResponseXnam))
    {
      reader.Read();
      reader.WRACE.ResponseStatus = reader.ReadElementString(ResponseStatusXnam);
      reader.WRACE.ResponseAnswer.ReadXml(reader);
      reader.WRACE.ResponseRelated.ReadXml(reader);
      reader.WRACE.ResponseReferred.ReadXml(reader);
    }

  }

}

