using UnityEngine;
using UnityEngine.Pool;

public class BulletPooling : MonoBehaviour
{
    [SerializeField] Bullet bulletPrefab;
    [SerializeField] Bullet bulletDmgBoostPrefab;

    Bullet bulletToSpawn;

    private ObjectPool<Bullet> pool;

    private void Awake()
    {
        pool = new ObjectPool<Bullet>(CreateBullet, null, ReleaseBullet, DestroyBullet);
    }

    private void Start()
    {
        bulletToSpawn = bulletPrefab;
    }

    private Bullet CreateBullet()
    {
        var bulletCopy = Instantiate(bulletToSpawn, transform.position, Quaternion.identity);   
        bulletCopy.Pool = pool;
        return bulletCopy;
    }

    private void ReleaseBullet(Bullet bullet)
    {
        bullet.gameObject.SetActive(false);
    }

    private void DestroyBullet(Bullet bullet)
    {
        Destroy(bullet.gameObject);
    }

    public Bullet InstantiateBullet(Transform bulletSpawnPoint)
    {
        var bullet = pool.Get();
        bullet.transform.position = bulletSpawnPoint.position;
        bullet.gameObject.SetActive(true);
        return bullet;
    }
    
    public Bullet InstantiateMultipleBullet(float angle, Vector3 playerOffset)
    {
        var bullet = pool.Get();
        bullet.gameObject.SetActive(true);
        bullet.transform.position = transform.position + playerOffset;
        bullet.transform.eulerAngles = new Vector3(0, 0, angle);
        return bullet;
    }

    public void ChangeToBoostDamageBullet()
    {
        bulletToSpawn = bulletDmgBoostPrefab;
    }

    public void ResetBulletPrefab()
    {
        bulletToSpawn = bulletPrefab;
    }
}
