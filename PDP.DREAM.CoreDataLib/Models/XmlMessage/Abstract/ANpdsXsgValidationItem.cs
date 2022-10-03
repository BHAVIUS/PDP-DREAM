// PORTAL-DOORS Project Copyright (c) 2007 - 2022 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

using System;
using System.Runtime.Serialization;
using System.Xml;
using System.Xml.Serialization;

using PDP.DREAM.CoreDataLib.Types;
using PDP.DREAM.CoreDataLib.Utilities;

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
    if (ItemHasValue || writer.QURC.ItemDoesVerbose)
    {
      // write open tag for element
      writer.WriteStartElement(ItemXnam); // PortalValidation and DoorsValidation
      if (ItemHasValue && writer.QURC.ItemCanBeAccessed)
      {
        // write attributes for element

        // write content for element PdsItemValue
        writer.WriteElementString(PdpAppConst.ItemStatusXnam, InfosetStatus.ToString());
        // write other content
        if (InfosetTestedOn.HasValue && (writer.QURC.ItemDoesVerbose || writer.QURC.ItemDoesArchive))
        {
          writer.WriteStartElement(PdpAppConst.ItemTestedOnXnam);
          if (InfosetTestedOn.HasValue)
          {
            writer.WriteString(InfosetTestedOn.Value.ToUniversalTime().ToString(PdpAppConst.UnivDateTimeFormat));
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
      InfosetStatus = reader.ReadElementString(PdpAppConst.ItemStatusXnam);
      try
      {
        InfosetTestedOn = Convert.ToDateTime(reader.ReadElementString(PdpAppConst.ItemTestedOnXnam));
      }
      catch
      {
        InfosetTestedOn = null;
      }
      reader.Read();
    }
  }
}