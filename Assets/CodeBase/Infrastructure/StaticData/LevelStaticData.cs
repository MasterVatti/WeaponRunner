using System.Collections.Generic;
using UnityEngine;

namespace CodeBase.Infrastructure.StaticData
{
  [CreateAssetMenu(fileName = "LevelData", menuName = "Static Data/Level")]
  public class LevelStaticData : ScriptableObject
  {
    public int LevelNumber;
    public Vector3 InitialHeroPosition;
    public List<Vector3> PlayerStopPositions;
    public List<Vector3> EnemySpawnPositions;
    public GameObject Location;
  }
}