// NpdsRecordDirectoryItem.cs 
// PORTAL-DOORS Project Copyright (c) 2007 - 2023 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.CoreDataLib.Models;

[KnownType(typeof(NpdsRecordDirectoryItem)), XmlSchemaProvider(null, IsAny = true)]
public class NpdsRecordDirectoryItem : ANpdsXsgTypedLabelItem
{
  public NpdsRecordDirectoryItem() : base() { this.Initialize(); }
  public NpdsRecordDirectoryItem(PdpAppConst.NpdsFieldRule rul) : base() { this.Initialize(rul); }
  public NpdsRecordDirectoryItem(PdpAppConst.NpdsFieldRule rul, string val) : base(val) { this.Initialize(rul); }

  private void Initialize(PdpAppConst.NpdsFieldRule rul = default(PdpAppConst.NpdsFieldRule))
  { base.InitNpdsItem(rul, PdpAppConst.DirectoryItemXnam, PdpAppConst.DirectoryListXnam); }

  public string Directory
  {
    get { return ItemValue; }
    set { ItemValue = value; }
  }
}

