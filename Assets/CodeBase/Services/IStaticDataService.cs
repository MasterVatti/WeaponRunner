using CodeBase.Infrastructure.StaticData;

namespace CodeBase.Services
{
  public interface IStaticDataService
  {
    void Load();
    LevelStaticData ForLevel(int level);
  }
}