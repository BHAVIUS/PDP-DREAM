using PDP.DREAM.ScribeDataLib.Stores;

namespace PDP.DREAM.ScribeWebLib.Components
{
  public class ResrepRecordsViewComponent : ScribeParentViewComponent
  {
    public ResrepRecordsViewComponent() { }
  }

  public class StatusViewComponent : ScribeChildViewComponent
  {
    public StatusViewComponent(ScribeDbsqlContext dataCntxt) : base(dataCntxt) { }
  }

  public class EntityLabelsViewComponent : ScribeChildViewComponent
  {
    public EntityLabelsViewComponent(ScribeDbsqlContext dataCntxt) : base(dataCntxt) { }
  }
  public class SupportingTagsViewComponent : ScribeChildViewComponent
  {
    public SupportingTagsViewComponent(ScribeDbsqlContext dataCntxt) : base(dataCntxt) { }
  }
  public class SupportingLabelsViewComponent : ScribeChildViewComponent
  {
    public SupportingLabelsViewComponent(ScribeDbsqlContext dataCntxt) : base(dataCntxt) { }
  }
  public class CrossReferencesViewComponent : ScribeChildViewComponent
  {
    public CrossReferencesViewComponent(ScribeDbsqlContext dataCntxt) : base(dataCntxt) { }
  }
  public class OtherTextsViewComponent : ScribeChildViewComponent
  {
    public OtherTextsViewComponent(ScribeDbsqlContext dataCntxt) : base(dataCntxt) { }
  }

  public class LocationsViewComponent : ScribeChildViewComponent
  {
    public LocationsViewComponent(ScribeDbsqlContext dataCntxt) : base(dataCntxt) { }
  }
  public class DescriptionsViewComponent : ScribeChildViewComponent
  {
    public DescriptionsViewComponent(ScribeDbsqlContext dataCntxt) : base(dataCntxt) { }
  }
  public class ProvenancesViewComponent : ScribeChildViewComponent
  {
    public ProvenancesViewComponent(ScribeDbsqlContext dataCntxt) : base(dataCntxt) { }
  }
  public class DistributionsViewComponent : ScribeChildViewComponent
  {
    public DistributionsViewComponent(ScribeDbsqlContext dataCntxt) : base(dataCntxt) { }
  }
  public class FairMetricsViewComponent : ScribeChildViewComponent
  {
    public FairMetricsViewComponent(ScribeDbsqlContext dataCntxt) : base(dataCntxt) { }
  }

  public class NexusSnapshotsViewComponent : ScribeChildViewComponent
  {
    public NexusSnapshotsViewComponent(ScribeDbsqlContext dataCntxt) : base(dataCntxt) { }
  }

}
