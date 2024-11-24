using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerVisual : MonoBehaviour
{
    [SerializeField] ShipDataSO shipData;
    
    SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Start()
    {
        int selectedShipIndex = PlayerPrefs.GetInt(Constants.PLAYER_PREFS.SHIP_SELECTED, 0);

        // Cambiar el sprite de la nave según la selección
        if (selectedShipIndex >= 0 && selectedShipIndex < shipData.shipSprites.Length)
        {
            spriteRenderer.sprite = shipData.shipSprites[selectedShipIndex].sprite;
        }
        else
        {
            Debug.LogWarning("Índice de nave inválido, usando la nave por defecto.");
        }
    }
}
