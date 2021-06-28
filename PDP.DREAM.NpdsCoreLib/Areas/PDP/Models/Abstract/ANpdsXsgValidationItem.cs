using System;
using System.Runtime.Serialization;
using System.Xml;
using System.Xml.Serialization;

namespace PDP.DREAM.NpdsCoreLib.Models
{
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
      var writer = (PdpPrcXmlWrappingWriter)xWriter;
      if (ItemHasValue || writer.PRC.ItemDoesVerbose)
      {
        // write open tag for element
        writer.WriteStartElement(ItemXnam); // PortalValidation and DoorsValidation
        if (ItemHasValue && writer.PRC.ItemCanBeAccessed)
        {
          // write attributes for element

          // write content for element PdsItemValue
          writer.WriteElementString(NpdsConst.ItemStatusXnam, InfosetStatus.ToString());
          // write other content
          if (InfosetTestedOn.HasValue && (writer.PRC.ItemDoesVerbose || writer.PRC.ItemDoesArchive))
          {
            writer.WriteStartElement(NpdsConst.ItemTestedOnXnam);
            if (InfosetTestedOn.HasValue)
            {
              writer.WriteString(InfosetTestedOn.Value.ToUniversalTime().ToString(NpdsConst.UnivDateTimeFormat));
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
      var reader = (PdpPrcXmlWrappingReader)xReader;
      reader.MoveToContent();
      if (reader.IsStartElement(ItemXnam))
      {
        reader.Read();
        InfosetStatus = reader.ReadElementString(NpdsConst.ItemStatusXnam);
        try
        {
          InfosetTestedOn = Convert.ToDateTime(reader.ReadElementString(NpdsConst.ItemTestedOnXnam));
        }
        catch
        {
          InfosetTestedOn = null;
        }
        reader.Read();
      }
    }
  }
}