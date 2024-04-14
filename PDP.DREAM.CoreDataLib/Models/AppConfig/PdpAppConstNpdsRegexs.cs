// PORTAL-DOORS Project Copyright (c) 2006-2024 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.CoreDataLib.Models;

public static partial class PdpAppConst
{
  // TODO: Solve parsing problem on regexes for validators on bound fields
  // to allow trailing spaces that do not trigger regex violation but then
  // those trailing spaces are trimmed prior to storage in database

  public const string UnivDateTimeFormat = "yyyy'-'MM'-'dd'T'HH':'mm':'ss'Z'";

  // Note regexPrincipalTag expression more restrictive than official XML Schema datatype NCName
  // This version requires 0 or more non-initial characters
  // Const regexPrincipalTag As String = "[A-Za-z][A-Za-z0-9._-]*"
  // This version requires 1 or more non-initial characters

  public const string RgxsSafeGeneric = "[^<%>]+";
  public const string RgxsTagTokenLeadingChar = "[A-Z]";
  public const string RgxsTagTokenTrailingGroup = "[A-Z0-9._-]+";
  public const string RgxsRequiredTagToken = RgxsTagTokenLeadingChar + RgxsTagTokenTrailingGroup;
  public const string RgxsOptionalTagToken = "(" + RgxsRequiredTagToken + ")??";
  public const string RgxsServerTypeToken = "(Components|Diristry|Registry|Directory|Registrar)";
  public const string RgxsReadOnlyServiceTypeToken = "(Nexus|PORTAL|DOORS)";
  public const string RgxsReadWriteServiceTypeToken = "(Nexus|PORTAL|DOORS|Scribe)";
  public const string RgxsReadOnlyServiceTagToken = RgxsOptionalTagToken + RgxsReadOnlyServiceTypeToken + RgxsTagTokenTrailingGroup;
  public const string RgxsReadWriteServiceTagToken = RgxsOptionalTagToken + RgxsReadWriteServiceTypeToken + RgxsTagTokenTrailingGroup;
  public const string RgxsInfosetStatusToken = "(AnyAndAll|None|Invalid|Valid|Pending)";
  public const string RgxsResRepListXnams = @"(EntityLabels|SupportingTags|SupportingLabels|CrossReferences|OtherTexts|Locations|Descriptions|Provenances|Distributions|Snapshots)";

  // new for Net 6 with migration from convention routing to attribute routing
  public const string RgxsServerTypes = @"^(Components|Diristry|Registry|Directory|Registrar)$";
  public const string RgxsDiristryServiceTypes = @"^(Nexus|PORTAL|DOORS|Core)$";
  public const string RgxsRegistryServiceTypes = @"^(PORTAL|Core)$";
  public const string RgxsDirectoryServiceTypes = @"^(DOORS|Core)$";
  public const string RgxsRegistrarServiceTypes = @"^(Nexus|PORTAL|DOORS|Scribe|Core)$";

  // https://docs.microsoft.com/en-us/dotnet/standard/base-types/quantifiers-in-regular-expressions
  // use of ? in (\s*.*?) makes it non-greedy (ie, finds capture group before the first lookahead pattern)
  // instead of (\s*.*) with default as greedy (ie, finds capture group before the last lookahead pattern)
  // ATTN: hyphen first in positive char group (else interpreted as a range)
  // TODO: review use of the C# regex \b
  // https://stackoverflow.com/questions/38529685/using-b-in-a-net-regex
  // https://docs.microsoft.com/en-us/dotnet/api/system.text.regularexpressions.regex?view=net-6.0
  // TODO: regex for minimal length PrincipalTag
  public const string RgxsPrincipalTag = @"^([A-Za-z][A-Za-z0-9-_.]+)$";
  public const string RgxsCitationKey = @"([A-Za-z][A-Za-z0-9]+?[-_.:]??[A-Za-z0-9]+?)";
  public const string RgxsReferenceType = @"([A-Za-z][A-Za-z0-9]+?[-_.]??[A-Za-z0-9]+?)";
  public const string RgxsRefAttribName = @"([A-Za-z][A-Za-z0-9]+?[-_]??[A-Za-z0-9]+?)";
  public const string RgxsRefAttribValue = @"(.*?)";
  public const string RgxsInfosetStatus = @"^(AnyAndAll|None|Invalid|Valid|Pending)$";

  // for Telerik Kendo Grid
  public const string RgxsNexusTKGAC = @"^AnonNexusTkgr$";
  public const string RgxsScribeTKGAC = @"^(AgentResreps|AgentScribeTkgr|AuthorResreps|AuthorScribeTkgr|EditorResreps|EditorScribeTkgr|AdminResreps|AdminScribeTkgr)$";

  // Microsoft SQL Server Guid
  // https://madskristensen.net/blog/validate-a-guid-in-c/
  public const string RgxsSqlsGuid = @"^(\{){0,1}[0-9a-fA-F]{8}\-[0-9a-fA-F]{4}\-[0-9a-fA-F]{4}\-[0-9a-fA-F]{4}\-[0-9a-fA-F]{12}(\}){0,1}$";

  // TODO: simplified regex for URI that disallows # so can be used in Telerik KendoUI grid
  // RegexLabelUri sourced from RegexBuddy library RFC 3986
  public const string RgxsLabelUri = @"^((?# Scheme)[A-Za-z][A-Za-z0-9+.-]*:((?# Authority & path)//([A-Za-z0-9._~%!$&'()*+,;=-]+@)?(?# User)([A-Za-z0-9._~%-]+(?# Named host)|\[[A-Fa-f0-9:.]+\](?# IPv6 host)|\[v[A-Fa-f0-9][A-Za-z0-9._~%!$&'()*+,;=:-]+\])(?# IPvFuture host)(:[0-9]+)?(?# Port)(/[A-Za-z0-9._~%!$&'()*+,;=:@-]+)*/?(?# Path)|(?# Path without authority)(/?[A-Za-z0-9._~%!$&'()*+,;=:@-]+(/[A-Za-z0-9._~%!$&'()*+,;=:@-]+)*/?)?)|(?# Relative URL (no scheme or authority)([A-Za-z0-9._~%!$&'()*+,;=@-]+(/[A-Za-z0-9._~%!$&'()*+,;=:@-]+)*/?(?# Relative path)|(/[A-Za-z0-9._~%!$&'()*+,;=:@-]+)+/?)(?# Absolute path))(?# Query)(\?[A-Za-z0-9._~%!$&'()*+,;=:@/?-]*)?(?# Fragment)(#[A-Za-z0-9._~%!$&'()*+,;=:@/?-]*)?$";
  public const string RgxsEntityLabel = RgxsLabelUri;
  public const string RgxsSupportingTag = RgxsSafeGeneric;
  public const string RgxsSupportingLabel = RgxsLabelUri;
  public const string RgxsCrossReference = RgxsLabelUri;

  // URL Regex from RegexBuddy for RFC 3986
  public const string RgxsLocationUrl = @"^((?# Scheme)[A-Za-z][A-Za-z0-9+.-]*:((?# Authority & path)//([A-Za-z0-9._~%!$&'()*+,;=-]+@)?(?# User)([A-Za-z0-9._~%-]+(?# Named host)|\[[A-Fa-f0-9:.]+\](?# IPv6 host)|\[v[A-Fa-f0-9][A-Za-z0-9._~%!$&'()*+,;=:-]+\])(?# IPvFuture host)(:[0-9]+)?(?# Port)(/[A-Za-z0-9._~%!$&'()*+,;=:@-]+)*/?(?# Path)|(?# Path without authority)(/?[A-Za-z0-9._~%!$&'()*+,;=:@-]+(/[A-Za-z0-9._~%!$&'()*+,;=:@-]+)*/?)?)|(?# Relative URL (no scheme or authority)([A-Za-z0-9._~%!$&'()*+,;=@-]+(/[A-Za-z0-9._~%!$&'()*+,;=:@-]+)*/?(?# Relative path)|(/[A-Za-z0-9._~%!$&'()*+,;=:@-]+)+/?)(?# Absolute path))(?# Query)(\?[A-Za-z0-9._~%!$&'()*+,;=:@/?-]*)?(?# Fragment)(#[A-Za-z0-9._~%!$&'()*+,;=:@/?-]*)?$";
  // from RegexBuddy library based on RFC 2822 (simplified)
  public const string RgxsEmailAddress = @"[A-Za-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[A-Za-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[A-Za-z0-9](?:[A-Za-z0-9-]*[A-Za-z0-9])?\.)+[A-Za-z0-9](?:[A-Za-z0-9-]*[A-Za-z0-9])?";

} // end class

// end file