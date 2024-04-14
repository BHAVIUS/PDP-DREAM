// PORTAL-DOORS Project Copyright (c) 2006-2024 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.CoreDataLib.Models;

[NotMapped]
public abstract class FormTaskUxmBase
{
  public string? ConcatNames(string? first, string? last)
  { PersonName = first + " " + last; return PersonName; }
  public string? ConcatNames(string? first, string? middle, string? last)
  { PersonName = first + " " + middle + " " + last; return PersonName; }
  [NotMapped, ScaffoldColumn(false)]
  public string? PersonName { get; set; } = ESS;

  [NotMapped, ScaffoldColumn(false)]
  public string? FormTitle { get; set; } = ESS;
  [NotMapped, ScaffoldColumn(false)]
  public string? FormMessage { get; set; } = ESS;
  [NotMapped, ScaffoldColumn(false)]
  public bool FormCompleted { get; set; } = false;
  [NotMapped, ScaffoldColumn(false)]
  public bool NoticeSent { get; set; } = false;
  [NotMapped, ScaffoldColumn(false)]
  public bool ErrorOccurred { get; set; } = false;
  [NotMapped, ScaffoldColumn(false)]
  public Exception? FormError { get; set; } = null;
}

// end file