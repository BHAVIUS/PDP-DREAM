// PORTAL-DOORS Project Copyright (c) 2006-2024 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.CoreDataLib.Models;

public static partial class PdpAppConst
{
  // TODO: build collection of Regex objects to go with the Regex strings
  // in file PdpAppConstNpdsRegexs and PdpAppConstNpdsRegexo
  //  and use prefixes Rgxs* (s String) and Rgxo* (o Object)
  public static Regex RgxoLabelUri = new Regex(RgxsLabelUri);

} // end class

// end file