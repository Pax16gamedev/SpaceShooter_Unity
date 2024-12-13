using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement settings")]
    [SerializeField] float speed = 20f;

    const string horizontalInput = "Horizontal";
    const string verticalInput = "Vertical";

    float horizontal;
    float vertical;

    float currentSpeed;
    float baseSpeed;

    VisualFeedback visualFeedback;
    Vector2 bottomLimit;
    Vector2 topLimit;
    [SerializeField] float clampMargin = 0.5f;

    private void Awake()
    {
        visualFeedback = GetComponentInChildren<VisualFeedback>();
    }

    private void Start()
    {
        currentSpeed = speed;
        baseSpeed = speed;
    }

    void Update()
    {
        GetInputs();
        Move();
    }

    void GetInputs()
    {
        horizontal = Input.GetAxisRaw(horizontalInput);
        vertical = Input.GetAxisRaw(verticalInput);
    }

    void Move()
    {
        transform.Translate(new Vector2(horizontal, vertical).normalized * currentSpeed * Time.deltaTime, Space.World);
        ClampMovement();
    }

    void ClampMovement()
    {
        // Obtener los limites visibles de la camara
        Vector2 cameraBottomLeft = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
        Vector2 cameraTopRight = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));

        float xMin = cameraBottomLeft.x + clampMargin;
        float xMax = cameraTopRight.x - clampMargin;
        float yMin = cameraBottomLeft.y + clampMargin;
        float yMax = cameraTopRight.y - clampMargin;

        bottomLimit = new Vector2(xMin, yMin);
        topLimit = new Vector2(xMax, yMin);

        // Ajustar la posición del jugador dentro de los límites
        float newX = Mathf.Clamp(transform.position.x, xMin, xMax);
        float newY = Mathf.Clamp(transform.position.y, yMin, yMax);

        transform.position = new Vector2(newX, newY);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;

        

        Gizmos.DrawLine(bottomLimit, topLimit);
    }

    public void SetSpeedMultiplier(float multiplier, float duration)
    {
        StopCoroutine(SpeedBoostCoroutine(multiplier, duration));
        StartCoroutine(SpeedBoostCoroutine(multiplier, duration));
    }

    private IEnumerator SpeedBoostCoroutine(float multiplier, float duration)
    {
        currentSpeed = baseSpeed * multiplier;
        visualFeedback.TriggerSpeedBoostColor(duration);
        yield return new WaitForSeconds(duration);
        currentSpeed = baseSpeed;
    }
}
