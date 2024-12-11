using System.Collections;
using UnityEngine;

public class DamageFeedback : MonoBehaviour
{
    [Header("Feedback Config")]
    [SerializeField] private Color damageColor = Color.red;
    [SerializeField] private float feedbackDuration = 0.15f;

    private SpriteRenderer spriteRenderer;
    private Color originalColor;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        originalColor = spriteRenderer.color;
    }

    public void TriggerDamageFeedback()
    {
        StartCoroutine(ChangeColor());
    }

    private IEnumerator ChangeColor()
    {
        spriteRenderer.color = damageColor;
        yield return new WaitForSeconds(feedbackDuration);
        spriteRenderer.color = originalColor;
    }
}
