using System.Threading.Tasks;
using CodeBase.Infrastructure.Factory;
using CodeBase.Infrastructure.StaticData;
using CodeBase.Services;
using UnityEngine;

namespace CodeBase.Infrastructure.States
{
  public class LoadLevelState : IPayloadedState<string>
  {
    private readonly GameStateMachine _stateMachine;
    private readonly SceneLoader _sceneLoader;
    private readonly LoadingCurtain _loadingCurtain;
    private readonly IGameFactory _gameFactory;
    private readonly IStaticDataService _dataService;

    public LoadLevelState(GameStateMachine gameStateMachine, SceneLoader sceneLoader, LoadingCurtain loadingCurtain,
      IGameFactory gameFactory, IStaticDataService dataService)
    {
      _stateMachine = gameStateMachine;
      _sceneLoader = sceneLoader;
      _loadingCurtain = loadingCurtain;
      _gameFactory = gameFactory;
      _dataService = dataService;
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
      _dataService.Load();
      await InitGameWorld();

      _stateMachine.Enter<GameState>();
    }

    private async Task InitGameWorld()
    {
      await InitLocation();
      GameObject player = await InitPlayer();
      await InitEnemies();
      GameObject hud = await InitHud();
      // CameraFollow(player);
    }

    private async Task<GameObject> InitLocation() => await _gameFactory.CreateGameObject(_dataService.ForLevel(1).Location);

    private async Task<GameObject> InitPlayer() =>
      await _gameFactory.CreatePlayer(_dataService.ForLevel(1).InitialHeroPosition);

    private async Task InitEnemies()
    {
      foreach (EnemySpawnPositions points in _dataService.ForLevel(1).EnemySpawnPositions)
        foreach (Vector3 position in points.Positions)
          await _gameFactory.CreateGameObject(_dataService.ForLevel(1).Enemy, position, Quaternion.identity);
    }

    private async Task<GameObject> InitHud() => await _gameFactory.CreateHud();
    // private void CameraFollow(GameObject player) => Camera.main.GetComponent<CameraFollow>().Follow(player);
  }
}