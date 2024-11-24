using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoader : MonoBehaviour
{
    [Header("UI Elements")]
    [SerializeField] GameObject loadingPanel;
    [SerializeField] TextMeshProUGUI loadingText;
    //[SerializeField] Slider progressBar;

    [Header("Delay")]
    [SerializeField] float waitTimeDelayInSeconds = 2.5f;

    private void Start()
    {
        loadingPanel.SetActive(false);
    }

    public void LoadSceneWithProgress(string sceneName)
    {
        loadingPanel.SetActive(true);

        StartCoroutine(LoadSceneAsync(sceneName));
    }

    private IEnumerator LoadSceneAsync(string sceneName)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);

        // No activar la escena hasta que la carga este completa
        operation.allowSceneActivation = false;

        while (!operation.isDone)
        {
            // Actualizamos la barra de progreso
            float progress = Mathf.Clamp01(operation.progress / 0.9f);
            //progressBar.value = progress;

            // Actualizamos el texto opcional de la carga
            loadingText.text = $"Cargando... {(int)(progress * 100)}%";

            // Una vez que llegamos al 90%, permitimos la activacion de la escena
            if (operation.progress >= 0.9f)
            {
                //progressBar.value = 1f;
                loadingText.text = $"Completado 100%";

                operation.allowSceneActivation = true;
            }

            yield return new WaitForSeconds(waitTimeDelayInSeconds);
        }
    }
}
