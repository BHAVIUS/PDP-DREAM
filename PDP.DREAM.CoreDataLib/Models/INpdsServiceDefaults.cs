// INpdsServiceDefaults.cs 
// Copyright (c) 2007 - 2021 Brain Health Alliance. All Rights Reserved. 
// Code license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.CoreDataLib.Models
{
  public interface INpdsServiceDefaults
  {
    public NpdsConst.DatabaseType NpdsDatabaseType { get; set; }
    public NpdsConst.DatabaseAccess NpdsDatabaseAccess { get; set; }
    public NpdsConst.RecordAccess NpdsRecordAccess { get; set; }
    public NpdsConst.NodeType NpdsNodeType { get; set; }
    public NpdsConst.ServiceType NpdsServiceType { get; set; }
    public NpdsConst.ServerType NpdsServerType { get; set; }
    public NpdsConst.SearchScope NpdsSearchScope { get; set; }
    public NpdsConst.SearchFilter NpdsSearchFilter { get; set; }
    public NpdsConst.FieldRule NpdsFieldRule { get; set; }
    public NpdsConst.FieldFormat NpdsFieldFormat { get; set; }
    public NpdsConst.ResrepFormat NpdsResrepFormat { get; set; }
    public NpdsConst.MessageFormat NpdsMessageFormat { get; set; }
    public NpdsConst.InfosetStatus NpdsInfosetStatus { get; set; }
    public NpdsConst.EntityType NpdsEntityType { get; set; }

  } // interface

} // namespace
