using UnityEngine;
using UnityEngine.Pool;

public class BulletPooling : MonoBehaviour
{
    [SerializeField] Bullet bulletPrefab;

    private ObjectPool<Bullet> pool;

    private void Awake()
    {
        pool = new ObjectPool<Bullet>(CreateBullet, null, ReleaseBullet, DestroyBullet);
    }

    private Bullet CreateBullet()
    {
        var bulletCopy = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
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

    // Play sfx
    public Bullet InstantiateMultipleBullet(float angle, Vector3 playerOffset)
    {
        var bullet = pool.Get();
        bullet.gameObject.SetActive(true);
        bullet.transform.position = transform.position + playerOffset;
        bullet.transform.eulerAngles = new Vector3(0, 0, angle);
        return bullet;
    }
}
