using UnityEngine;
using UnityEngine.UI;

public class MainMenuCanvas : MonoBehaviour
{
    [Header("Panels")]
    [SerializeField] GameObject mainPanel;
    [SerializeField] GameObject ShipSelectionPanel;
    [SerializeField] GameObject creditsPanel;

    [Header("Scene Loader")]
    [SerializeField] SceneLoader sceneLoader;

    [Header("Mute button")]
    [SerializeField] Button muteBtn;
    [SerializeField] Sprite muteImg;
    [SerializeField] Sprite unmuteImg;

    bool isMuted;

    private void Start()
    {
        if (PlayerPrefs.HasKey(Constants.PLAYER_PREFS.IS_MUTED))
        {
            isMuted = PlayerPrefs.GetInt(Constants.PLAYER_PREFS.IS_MUTED) == 1;
            AudioListener.volume = isMuted ? 0 : 1;
            muteBtn.image.sprite = isMuted ? muteImg : unmuteImg;
        }
    }

    public void PlayGame() => sceneLoader.LoadSceneWithProgress(Constants.SCENES.MAIN_LEVEL);

    public void ShowMainPanel() => mainPanel.SetActive(true);
    public void HideMainPanel() => mainPanel.SetActive(false);

    public void ShowShipSelectionPanel() => ShipSelectionPanel.gameObject.SetActive(true);
    public void HideShipSelectionPanel() => ShipSelectionPanel.gameObject.SetActive(false);

    public void ShowCreditsPanel() => creditsPanel.SetActive(true);
    public void HideCreditsPanel() => creditsPanel.SetActive(false);

    public void ToggleMute()
    {
        isMuted = !isMuted;
        AudioListener.volume = isMuted ? 0 : 1;
        muteBtn.image.sprite = isMuted ? muteImg : unmuteImg;

        PlayerPrefs.SetInt(Constants.PLAYER_PREFS.IS_MUTED, isMuted ? 1 : 0);
        PlayerPrefs.Save();
    }
}
