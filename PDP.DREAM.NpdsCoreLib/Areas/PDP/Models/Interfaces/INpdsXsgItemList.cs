
namespace PDP.DREAM.NpdsCoreLib.Models
{
  public interface INpdsXsgListItem
  {
    NpdsConst.FieldRule ItemRule { get; }
    string ItemXnam { get; }
    string ItemListXnam { get; }
  }

}