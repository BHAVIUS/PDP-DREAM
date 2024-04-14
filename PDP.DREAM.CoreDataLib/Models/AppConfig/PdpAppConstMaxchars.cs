// PORTAL-DOORS Project Copyright (c) 2006-2024 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).


namespace PDP.DREAM.CoreDataLib.Models;

public static partial class PdpAppConst
{
  public const int EntityNameMaxchars = 256;
  public const int EntityNatureMaxchars = 1024;
  public const int EntityLabelMaxchars = 256;
  public const int TagTokenMaxchars = 64;
  public const int LabelUriMaxchars = 128;

  public const int PrincipalTagMaxchars = 64;
  public const int SupportingTagMaxchars = 256;
  public const int SupportingLabelMaxchars = 256;
  public const int CrossReferenceMaxchars = 256;

  // OtherText, Location, Description, Provenance, Distribution
  // are all nvarchar(max) without length restriction in database
}