using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShipSelectionController : MonoBehaviour
{
    [Header("Ships")]
    [SerializeField] Image shipImg;
    [Space]
    [SerializeField] ShipDataSO ships;

    [Header("Image Buttons")]
    [SerializeField] Image previousBtn;
    [SerializeField] Image nextBtn;

    [Header("Visual feedback")]
    [SerializeField] Color selectedColor = Color.green;
    [SerializeField] float resetDelayInSeconds = 2f;
    [SerializeField] TextMeshProUGUI feedbackTMP;

    private int selectedShipIndex = 0;

    private void Start()
    {
        if (PlayerPrefs.HasKey(Constants.PLAYER_PREFS.SHIP_SELECTED))
        {
            selectedShipIndex = PlayerPrefs.GetInt(Constants.PLAYER_PREFS.SHIP_SELECTED);
        }

        UpdateShipSelection();
    }

    public void SelectNextShip()
    {
        selectedShipIndex = (selectedShipIndex + 1) % ships.shipSprites.Length;
        UpdateShipSelection();
    }

    public void SelectPreviousShip()
    {
        selectedShipIndex = (selectedShipIndex - 1 + ships.shipSprites.Length) % ships.shipSprites.Length;
        UpdateShipSelection();
    }

    public void ConfirmSelection()
    {
        PlayerPrefs.SetInt(Constants.PLAYER_PREFS.SHIP_SELECTED, selectedShipIndex);
        PlayerPrefs.Save();

        // Feedback visual de que se ha guardado
        feedbackTMP.text = "Nave seleccionada guardada!";
        feedbackTMP.color = selectedColor;
        feedbackTMP.gameObject.SetActive(true);

        // Puedes hacer que el texto vuelva al estado original después de un breve tiempo si lo deseas
        Invoke(nameof(ResetFeedback), resetDelayInSeconds);
    }

    private void UpdateShipSelection()
    {        
        shipImg.GetComponent<Image>().sprite = ships.shipSprites[selectedShipIndex].sprite;

        // Sprite nave anterior
        int previousIndex = (selectedShipIndex - 1 + ships.shipSprites.Length) % ships.shipSprites.Length;
        previousBtn.sprite = ships.shipSprites[previousIndex].GetComponent<SpriteRenderer>().sprite;

        // Sprite nave siguiente
        int nextIndex = (selectedShipIndex + 1) % ships.shipSprites.Length; 
        nextBtn.sprite = ships.shipSprites[nextIndex].GetComponent<SpriteRenderer>().sprite;

        // Opcional: Dar feedback visual de la selección
        // Puedes agregar un borde o cambiar el color de la nave seleccionada
        //shipIMG.color = selectedColor;
    }

    private void ResetFeedback()
    {
        // Resetear el mensaje y color de feedback
        feedbackTMP.color = Color.white;
        feedbackTMP.gameObject.SetActive(false);

        // Reseteamos la nave a su color original
        //shipImg.GetComponent<Image>().color = Color.white;
    }
}
