using UnityEngine;

public class DeathZone : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag(Constants.TAGS.BULLET))
        {
            Destroy(collision.gameObject);
        }

        if(collision.CompareTag(Constants.TAGS.ENEMY))
        {
            Destroy(collision.gameObject);
        }
    }
}
