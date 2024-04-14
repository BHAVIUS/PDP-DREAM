// PORTAL-DOORS Project Copyright (c) 2006-2024 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.CoreDataLib.Models;

[KnownType(typeof(ANpdsXsgValidationItem)), XmlSchemaProvider(null, IsAny = true)]
public abstract class ANpdsXsgValidationItem : ANpdsXsgBaseItem<string>
{
  protected ANpdsXsgValidationItem() : base() { }
  protected ANpdsXsgValidationItem(string status) : base(status) { }
  protected ANpdsXsgValidationItem(string status, DateTime? teston) : base(status) { InfosetTestedOn = teston; }

  public string InfosetStatus
  {
    get { return ItemValue; }
    set { ItemValue = value; }
  }

  public DateTime? InfosetTestedOn { set; get; } = null;

  public override void WriteXml(XmlWriter xWriter)
  {
    var writer = (NpdsXmlWrappingWriter)xWriter;
    if (ItemHasValue || writer.WRACE.ItemDoesVerbose)
    {
      // write open tag for element
      writer.WriteStartElement(ItemXnam); // PortalValidation and DoorsValidation
      if (ItemHasValue && writer.WRACE.ItemCanBeAccessed)
      {
        // write attributes for element

        // write content for element PdsItemValue
        writer.WriteElementString(ItemStatusXnam, InfosetStatus.ToString());
        // write other content
        if (InfosetTestedOn.HasValue && (writer.WRACE.ItemDoesVerbose || writer.WRACE.ItemDoesArchive))
        {
          writer.WriteStartElement(ItemTestedOnXnam);
          if (InfosetTestedOn.HasValue)
          {
            writer.WriteString(InfosetTestedOn.Value.ToUniversalTime().ToString(UnivDateTimeFormat));
          }
          writer.WriteEndElement();
        }
      }
      // write close tag for element
      writer.WriteEndElement();
    }
  }

  public override void ReadXml(XmlReader xReader)
  {
    var reader = (NpdsXmlWrappingReader)xReader;
    reader.MoveToContent();
    if (reader.IsStartElement(ItemXnam))
    {
      reader.Read();
      InfosetStatus = reader.ReadElementString(ItemStatusXnam);
      try
      {
        InfosetTestedOn = Convert.ToDateTime(reader.ReadElementString(ItemTestedOnXnam));
      }
      catch
      {
        InfosetTestedOn = null;
      }
      reader.Read();
    }
  }
}