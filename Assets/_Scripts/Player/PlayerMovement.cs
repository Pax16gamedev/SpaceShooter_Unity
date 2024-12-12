using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement settings")]
    [SerializeField] float speed = 20f;

    [Header("Map Limits")]
    [SerializeField] float xLimit = 20f;
    [SerializeField] float yLimit = 9.5f;

    const string horizontalInput = "Horizontal";
    const string verticalInput = "Vertical";

    float horizontal;
    float vertical;

    float currentSpeed;
    float baseSpeed;

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
        float newX = Mathf.Clamp(transform.position.x, -xLimit, xLimit);
        float newY = Mathf.Clamp(transform.position.y, -yLimit, yLimit);

        Vector2 newTransform = new Vector2(newX, newY);
        transform.position = newTransform;
    }

    public void SetSpeedMultiplier(float multiplier, float duration)
    {
        StopCoroutine(SpeedBoostCoroutine(multiplier, duration));
        StartCoroutine(SpeedBoostCoroutine(multiplier, duration));
    }

    private IEnumerator SpeedBoostCoroutine(float multiplier, float duration)
    {
        currentSpeed = baseSpeed * multiplier;
        yield return new WaitForSeconds(duration);
        currentSpeed = baseSpeed;
    }
}
