using System.Threading.Tasks;
using CodeBase.Infrastructure.Factory;
using UnityEngine;

namespace CodeBase.Infrastructure.States
{
  public class LoadLevelState : IPayloadedState<string>
  {
    private const string InitialPointTag = "InitialPoint";
    private readonly GameStateMachine _stateMachine;
    private readonly SceneLoader _sceneLoader;
    private readonly LoadingCurtain _loadingCurtain;
    private readonly IGameFactory _gameFactory;

    public LoadLevelState(GameStateMachine gameStateMachine, SceneLoader sceneLoader, LoadingCurtain loadingCurtain,
      IGameFactory gameFactory)
    {
      _stateMachine = gameStateMachine;
      _sceneLoader = sceneLoader;
      _loadingCurtain = loadingCurtain;
      _gameFactory = gameFactory;
    }

    public void Enter(string sceneName)
    {
      _loadingCurtain.Show();
      _sceneLoader.Load(sceneName, OnLoaded);
    }

    public void Exit() =>
      _loadingCurtain.Hide();

    private async void OnLoaded()
    {
      await InitGameWorld();

      _stateMachine.Enter<GameState>();
    }

    private async Task InitGameWorld()
    {
      // GameObject player = await InitPlayer();
      // GameObject hud = await InitHud();
      // CameraFollow(player);
    }

    private async Task<GameObject> InitPlayer() =>
      await _gameFactory.CreatePlayer(GameObject.FindWithTag(InitialPointTag).transform.position);

    private async Task<GameObject> InitHud() => await _gameFactory.CreateHud();

    // private void CameraFollow(GameObject player) => Camera.main.GetComponent<CameraFollow>().Follow(player);
  }
}