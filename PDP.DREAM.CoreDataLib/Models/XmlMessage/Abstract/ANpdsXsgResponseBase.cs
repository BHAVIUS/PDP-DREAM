// ANpdsXsgResponseBase.cs 
// PORTAL-DOORS Project Copyright (c) 2007 - 2022 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

using System;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace PDP.DREAM.CoreDataLib.Models
{
  // TODO: recode with alternate approach that eliminates this base file for NpdsRoot and NpdsMessage
  // TODO: create alternative that is more appropriate for diverse API response messages
  public abstract class ANpdsXsgResponseBase : IXmlSerializable
  {
    public ANpdsXsgResponseBase() { }
    public ANpdsXsgResponseBase(NpdsResrepList answer) { InitResponse(answer); }
    public ANpdsXsgResponseBase(NpdsResrepList answer, NpdsResrepList related, NpdsResrepList referred)
    { InitResponse(answer, related, referred); }

    protected virtual void InitResponse(NpdsResrepList answer, NpdsResrepList? related = null, NpdsResrepList? referred = null)
    {
      if (answer != null) { NpdsResponseAnswer = answer; }
      if (related != null) { NpdsResponseRelated = related; }
      if (referred != null) { NpdsResponseReferred = referred; }
    }

    //          DNS messages have Header, Question, Answer, Authority, Additional
    // analogue PDS messages with Header, Question, Answer, Referred, Related

    public NpdsResrepList? NpdsResponseAnswer { set; get; }
    public NpdsResrepList? NpdsResponseRelated { set; get; }
    public NpdsResrepList? NpdsResponseReferred { set; get; }

    public virtual void WriteXml(XmlWriter writer) { throw new NotImplementedException(); }

    public virtual void ReadXml(XmlReader writer) { throw new NotImplementedException(); }

    public XmlSchema GetSchema()
    {
      var xs = new XmlSchema();
      return xs;
    }

  }

}