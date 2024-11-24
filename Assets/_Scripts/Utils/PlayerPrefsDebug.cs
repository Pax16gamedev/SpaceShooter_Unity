using UnityEngine;

public class PlayerPrefsDebug : MonoBehaviour
{
    void Start()
    {
        foreach (var key in PlayerPrefs.GetString("keys").Split(';'))
        {
            print($"{key}: {PlayerPrefs.GetString(key)}");
        }
    }
}
