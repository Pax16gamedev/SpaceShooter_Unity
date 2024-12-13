using System.Collections;
using UnityEngine;

public class VisualFeedback : MonoBehaviour
{
    [Header("Feedback Config")]
    [SerializeField] private Color damageColor = Color.red;
    [SerializeField] private Color healColor = Color.green;
    [SerializeField] private Color speedColor = Color.gray;
    [SerializeField] private Color damageBoostColor = Color.red;
    [SerializeField] private float feedbackDuration = 0.15f;

    private SpriteRenderer spriteRenderer;
    private Color originalColor;

    float duration = .15f;

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
        StartCoroutine(ChangeColor(damageColor));
    }

    public void TriggerHealFeedback()
    {
        StartCoroutine(ChangeColor(healColor));
    }

    public void TriggerSpeedBoostColor(float duration)
    {
        StartCoroutine(ChangeColor(speedColor, duration));
    }

    public void TriggerDamageBoostColor(float duration)
    {
        StartCoroutine(ChangeColor(damageBoostColor, duration));
    }

    private IEnumerator ChangeColor(Color color, float duration = 0.2f)
    {
        spriteRenderer.color = color;
        yield return new WaitForSeconds(duration);
        spriteRenderer.color = originalColor;
    }
}
