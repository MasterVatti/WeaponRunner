using System.Threading.Tasks;
using UnityEngine;

namespace CodeBase.Infrastructure.Factory
{
  public interface IGameFactory
  {
    Task<GameObject> CreatePlayer(Vector3 at);
    Task<GameObject> CreateHud();
    GameObject PlayerGameObject { get; }
  }
}