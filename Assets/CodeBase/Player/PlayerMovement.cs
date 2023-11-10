// using System.Collections.Generic;
// using CodeBase.Services;
// using UnityEngine;
// using UnityEngine.AI;
// using Zenject;
//
// namespace CodeBase.Player
// {
//   public class PlayerMovement : MonoBehaviour
//   {
//     [SerializeField] private NavMeshAgent _navMeshAgent;
//
//     private IStaticDataService _dataService;
//     private List<Vector3> _playerStopPositions;
//
//     public PlayerMovement()
//     {
//       _playerStopPositions = _dataService.ForLevel(1).PlayerStopPositions;
//     }
//
//     [Inject]
//     private void Constructor(IStaticDataService dataService)
//     {
//       _dataService = dataService;
//     }
//
//     private void Start()
//     {
//       Vector3 targetPosition = _playerStopPositions[0];
//       _navMeshAgent.SetDestination(targetPosition);
//
//
//     }
//
//     private void FixedUpdate()
//     {
//       if (!_navMeshAgent.pathPending && _navMeshAgent.remainingDistance < 0.5f)
//       {
//
//         Vector3 targetPosition = _playerStopPositions.First();
//         _navMeshAgent.SetDestination(targetPosition);
//       }
//     }
//   }
// }