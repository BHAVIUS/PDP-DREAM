// NpdsRecordDirectoryItem.cs 
// PORTAL-DOORS Project Copyright (c) 2006-2024 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.CoreDataLib.Models;

[KnownType(typeof(NpdsRecordDirectoryItem)), XmlSchemaProvider(null, IsAny = true)]
public class NpdsRecordDirectoryItem : ANpdsXsgEntityLabelItem
{
  public NpdsRecordDirectoryItem() : base() { this.Initialize(); }
  public NpdsRecordDirectoryItem(NpdsFieldRule rul) : base() { this.Initialize(rul); }
  public NpdsRecordDirectoryItem(NpdsFieldRule rul, Uri? val) : base(val) { this.Initialize(rul); }

  private void Initialize(NpdsFieldRule rul = default(NpdsFieldRule))
  { base.InitNpdsItem(rul, DirectoryItemXnam, DirectoryListXnam); }

  public string? Directory
  {
    get { return ItemValue.AbsoluteUri; }
    set { ParseEntityLabel(value); }
  }
}

