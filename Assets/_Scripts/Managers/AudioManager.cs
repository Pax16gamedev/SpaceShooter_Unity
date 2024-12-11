using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [Header("Audio Sources")]
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource sfxSource;

    [Header("Audio Clips")]
    [SerializeField] private AudioClip backgroundMusic;

    [Header("UI Clips")]
    [SerializeField] private AudioClip buttonClickSfx;
    [SerializeField] private AudioClip confirmClickSfx;
    [SerializeField] private AudioClip swapShipClickSfx;
    [SerializeField][Range(0f, 1f)] float uiSfxVolume;

    float musicVolumeDefault;
    float sfxVolumeDefault;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        PlayMusic(backgroundMusic);

        musicVolumeDefault = musicSource.volume;
        sfxVolumeDefault = sfxSource.volume;
    }

    public void PlayMusic(AudioClip clip)
    {
        if (musicSource.isPlaying)
            musicSource.Stop();

        musicSource.clip = clip;
        musicSource.loop = true;
        musicSource.Play();
    }

    public void PlaySFX(AudioClip clip, float volume = 1f)
    {
        sfxSource.PlayOneShot(clip, volume);
    }

    public void SetMusicVolume(float volume)
    {
        musicSource.volume = volume;
    }

    public void SetSFXVolume(float volume)
    {
        sfxSource.volume = volume;
    }

    public void MuteAllMusic()
    {
        SetMusicVolume(0);
        SetSFXVolume(0);
    }

    public void ResumeAllMusic()
    {
        SetMusicVolume(musicVolumeDefault);
        SetSFXVolume(sfxVolumeDefault);
    }

    #region UI

    public void PlayButtonSound()
    {
        PlaySFX(buttonClickSfx, uiSfxVolume);
    }

    public void PlayConfirmSound()
    {
        PlaySFX(confirmClickSfx, uiSfxVolume);
    }

    public void PlaySwapShipSound()
    {
        PlaySFX(swapShipClickSfx, uiSfxVolume);
    }

    #endregion
}
