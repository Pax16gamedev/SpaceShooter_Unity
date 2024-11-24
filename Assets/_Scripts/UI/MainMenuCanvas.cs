using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuCanvas : MonoBehaviour
{
    [Header("Panels")]
    [SerializeField] GameObject mainPanel;
    [SerializeField] GameObject ShipSelectionPanel;
    [SerializeField] GameObject creditsPanel;

    [Header("Scene Loader")]
    [SerializeField] SceneLoader sceneLoader; // Referencia al script SceneLoader

    public void PlayGame()
    {
        sceneLoader.LoadSceneWithProgress(Constants.SCENES.MAIN_LEVEL);
    }

    public void ShowMainPanel() => mainPanel.SetActive(true);
    public void HideMainPanel() => mainPanel.SetActive(false);

    public void ShowShipSelectionPanel() => ShipSelectionPanel.gameObject.SetActive(true);
    public void HideShipSelectionPanel() => ShipSelectionPanel.gameObject.SetActive(false);

    public void ShowCreditsPanel() => creditsPanel.SetActive(true);
    public void HideCreditsPanel() => creditsPanel.SetActive(false);
}
