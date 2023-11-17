using System.Collections.Generic;
using CodeBase.Services;
using UnityEngine;
using UnityEngine.AI;
using Zenject;

namespace CodeBase.Player
{
  public class PlayerMovement : MonoBehaviour
  {
    [SerializeField] private NavMeshAgent _navMeshAgent;
    [SerializeField] private PlayerAnimator _animator;
    
    private IStaticDataService _dataService;
    private List<Vector3> _playerStopPositions;
    private int _pointAchievedQuantity;

    [Inject]
    private void Constructor(IStaticDataService dataService)
    {
      _dataService = dataService;
    }

    private void Start()
    {
      _playerStopPositions = _dataService.ForLevel(1).PlayerStopPositions;
      _animator.PlayRunning();
      MoveToPoint();
    }

    private void FixedUpdate()
    {
      if (_pointAchievedQuantity + 1 < _playerStopPositions.Count && !_navMeshAgent.pathPending && _navMeshAgent.remainingDistance < 0.5f)
      {
        _animator.ResetToIdle();
        return;
        _pointAchievedQuantity += 1;
        MoveToPoint();
      }
    }

    public void MoveToPoint()
    {

      Vector3 targetPosition = _playerStopPositions[_pointAchievedQuantity];
      _navMeshAgent.SetDestination(targetPosition);
    }
  }
}