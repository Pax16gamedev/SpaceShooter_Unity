using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    [SerializeField] float speed = 3f;
    [SerializeField] float imageWidth;
    [SerializeField] Vector2 direction = new Vector2(-1, 0);

    Vector2 initialPosition;

    void Start()
    {
        initialPosition = transform.position;
    }

    void Update()
    {
        // Resto: Cuanto me queda de recorrido para alcanzar un nuevo ciclo
        float resto = (speed * Time.time) % imageWidth;

        transform.position = initialPosition + resto * direction;
    }


}
