using UnityEngine;

namespace CodeBase.Player
{
  public class Shooting : MonoBehaviour
  {
    [SerializeField] private GameObject _bulletPrefab;
    
    [SerializeField] private Transform _spawn;
    [SerializeField] private float _bulletSpeedCoefficient;

    private void Update()
    {
      CheckInputTouch();
    }

    private void CheckInputTouch()
    {
      if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
      {
        Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 1000)) FireBullet(hit.point);
      }
    }

    private void FireBullet(Vector3 target)
    {
      GameObject bullet = Instantiate(_bulletPrefab, _spawn.position, Quaternion.identity);
      bullet.transform.LookAt(target);
      bullet.GetComponent<Bullet>().Initialize(_bulletSpeedCoefficient);
    }
  }
}