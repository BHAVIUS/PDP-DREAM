namespace PDP.DREAM.NpdsCoreLib.Models
{
  // TODO: separate into PdpConstants and NpdsConstants
  //    ie what is part of PDP applications and what is aprt of NPDS specification model
  // after separation then rename the NpdsConstants group accordingly
  public static partial class NpdsConst
  {
    // this class contains nothing but constants and enums only
    // if enum value not set in code, it defaults to 0

    // https://madskristensen.net/blog/validate-a-guid-in-c/
    public const string RegexGuid = @"^(\{){0,1}[0-9a-fA-F]{8}\-[0-9a-fA-F]{4}\-[0-9a-fA-F]{4}\-[0-9a-fA-F]{4}\-[0-9a-fA-F]{12}(\}){0,1}$";

    // TODO: Solve parsing problem on regexes for validators on bound fields
    // to allow trailing spaces that do not trigger regex violation but then
    // those trailing spaces are trimmed prior to storage in database

    public const string UnivDateTimeFormat = "yyyy'-'MM'-'dd'T'HH':'mm':'ss'Z'";
 
    // Note regexPrincipalTag expression more restrictive than official XML Schema datatype NCName
    // This version requires 0 or more non-initial characters
    // Const regexPrincipalTag As String = "[A-Za-z][A-Za-z0-9._-]*"
    // This version requires 1 or more non-initial characters

    public const string RegexSafeGeneric = "[^<%>]+";
    public const string RegexTagTokenLeadingChar = "[A-Z]";
    public const string RegexTagTokenTrailingGroup = "[-_A-Z0-9]*";
    public const string RegexRequiredTagToken = RegexTagTokenLeadingChar + RegexTagTokenTrailingGroup;
    public const string RegexOptionalTagToken = "(" + RegexRequiredTagToken + ")??";
    public const string RegexServerTypeToken = "(Components|Diristry|Registry|Directory|Registrar)";
    public const string RegexReadOnlyServiceTypeToken = "(Nexus|PORTAL|DOORS)";
    public const string RegexReadWriteServiceTypeToken = "(Nexus|PORTAL|DOORS|Scribe)";
    public const string RegexReadOnlyServiceTagToken = RegexOptionalTagToken + RegexReadOnlyServiceTypeToken + RegexTagTokenTrailingGroup;
    public const string RegexReadWriteServiceTagToken = RegexOptionalTagToken + RegexReadWriteServiceTypeToken + RegexTagTokenTrailingGroup;
    public const string RegexInfosetStatusToken = "(AnyAndAll|Unknown|Invalid|Valid|Pending)";
    public const string RegexResRepListXnams = @"(EntityLabels|SupportingTags|SupportingLabels|CrossReferences|OtherTexts|Locations|Descriptions|Provenances|Distributions|Snapshots)";

    // TODO: recode to eliminate differences between TagToken and PrincipalTag
    // that were merged from different code sources; check distinctions with XML Schema types
    // RegexLabelUri sourced from RegexBuddy library RFC 3986
    public const string RegexLabelUri = @"^((?# Scheme)[A-Za-z][A-Za-z0-9+.-]*:((?# Authority & path)//([A-Za-z0-9._~%!$&'()*+,;=-]+@)?(?# User)([A-Za-z0-9._~%-]+(?# Named host)|\[[A-Fa-f0-9:.]+\](?# IPv6 host)|\[v[A-Fa-f0-9][A-Za-z0-9._~%!$&'()*+,;=:-]+\])(?# IPvFuture host)(:[0-9]+)?(?# Port)(/[A-Za-z0-9._~%!$&'()*+,;=:@-]+)*/?(?# Path)|(?# Path without authority)(/?[A-Za-z0-9._~%!$&'()*+,;=:@-]+(/[A-Za-z0-9._~%!$&'()*+,;=:@-]+)*/?)?)|(?# Relative URL (no scheme or authority)([A-Za-z0-9._~%!$&'()*+,;=@-]+(/[A-Za-z0-9._~%!$&'()*+,;=:@-]+)*/?(?# Relative path)|(/[A-Za-z0-9._~%!$&'()*+,;=:@-]+)+/?)(?# Absolute path))(?# Query)(\?[A-Za-z0-9._~%!$&'()*+,;=:@/?-]*)?(?# Fragment)(#[A-Za-z0-9._~%!$&'()*+,;=:@/?-]*)?$";
    public const string RegexPrincipalTag = @"[A-Za-z][A-Za-z0-9._-]+";
    public const string RegexEntityLabel = RegexLabelUri;
    public const string RegexSupportingTag = RegexSafeGeneric;
    public const string RegexSupportingLabel = RegexLabelUri;
    public const string RegexCrossReference = RegexLabelUri;

    // URL Regex from RegexBuddy for RFC 3986
    // TODO: ?? check for equivalence/equality with RegexLabelUri ??
    public const string RegexLocationUrl = @"^((?# Scheme)[a-zA-Z][a-zA-Z0-9+.-]*:((?# Authority & path)//([a-zA-Z0-9._~%!$&'()*+,;=-]+@)?(?# User)([a-zA-Z0-9._~%-]+(?# Named host)|\[[a-fA-F0-9:.]+\](?# IPv6 host)|\[v[a-fA-F0-9][a-zA-Z0-9._~%!$&'()*+,;=:-]+\])(?# IPvFuture host)(:[0-9]+)?(?# Port)(/[a-zA-Z0-9._~%!$&'()*+,;=:@-]+)*/?(?# Path)|(?# Path without authority)(/?[a-zA-Z0-9._~%!$&'()*+,;=:@-]+(/[a-zA-Z0-9._~%!$&'()*+,;=:@-]+)*/?)?)|(?# Relative URL (no scheme or authority)([a-zA-Z0-9._~%!$&'()*+,;=@-]+(/[a-zA-Z0-9._~%!$&'()*+,;=:@-]+)*/?(?# Relative path)|(/[a-zA-Z0-9._~%!$&'()*+,;=:@-]+)+/?)(?# Absolute path))(?# Query)(\?[a-zA-Z0-9._~%!$&'()*+,;=:@/?-]*)?(?# Fragment)(#[a-zA-Z0-9._~%!$&'()*+,;=:@/?-]*)?$";
    // from RegexBuddy library based on RFC 2822 (simplified)
    public const string RegexEmailAddress = @"[A-Za-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[A-Za-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[A-Za-z0-9](?:[A-Za-z0-9-]*[A-Za-z0-9])?\.)+[A-Za-z0-9](?:[A-Za-z0-9-]*[A-Za-z0-9])?";

  }

}
