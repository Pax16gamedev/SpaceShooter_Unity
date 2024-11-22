using UnityEngine;

public class DeathZone : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag(Constants.bulletTag))
        {
            var bullet = collision.GetComponent<Bullet>();
            bullet.ReleaseBullet();
        }

        if(collision.CompareTag(Constants.enemyTag))
        {
            Destroy(collision.gameObject);
        }
    }
}
