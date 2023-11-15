using UnityEngine;

namespace CodeBase.Player
{
  public class Bullet : MonoBehaviour
  {
    [SerializeField] private float _bulletSpeed;

    private float _bulletSpeedCoefficient;

    private void FixedUpdate()
    {
      transform.position += transform.forward * _bulletSpeed * _bulletSpeedCoefficient * Time.deltaTime;
    }

    public void Initialize(float bulletSpeedCoefficient) => _bulletSpeedCoefficient = bulletSpeedCoefficient;
  }
}