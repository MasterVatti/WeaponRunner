using System;
using System.Collections.Generic;
using CodeBase.Infrastructure.Factory;
using CodeBase.Services.Level;
using CodeBase.Services.StaticData;
using Zenject;

namespace CodeBase.Infrastructure.States
{
  public class GameStateMachine : IInitializable
  {
    private Dictionary<Type, IExitableState> _states;
    private IExitableState _activeState;

    public GameStateMachine(SceneLoader sceneLoader, LoadingCurtain loadingCurtain, IGameFactory gameFactory,
      IStaticDataService dataService, ILevelService levelService)
    {
      _states = new Dictionary<Type, IExitableState>
      {
        [typeof(BootstrapState)] = new BootstrapState(this, sceneLoader),
        [typeof(LoadLevelState)] = new LoadLevelState(this, sceneLoader, loadingCurtain, gameFactory, dataService),
        [typeof(GameState)] = new GameState(this, levelService),
      };
    }

    public void Initialize()
    {
      Enter<BootstrapState>();
    }

    public void Enter<TState>() where TState : class, IState
    {
      IState state = ChangeState<TState>();
      state.Enter();
    }

    public void Enter<TState, TPayload>(TPayload payload) where TState : class, IPayloadedState<TPayload>
    {
      TState state = ChangeState<TState>();
      state.Enter(payload);
    }

    private TState ChangeState<TState>() where TState : class, IExitableState
    {
      _activeState?.Exit();

      TState state = GetState<TState>();
      _activeState = state;

      return state;
    }

    private TState GetState<TState>() where TState : class, IExitableState =>
      _states[typeof(TState)] as TState;
  }
}