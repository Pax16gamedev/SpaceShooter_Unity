using UnityEngine;

public class PlayerAudio : MonoBehaviour
{
    [Header("SFX")]
    [SerializeField] private AudioClip damageSfx;
    [SerializeField] private AudioClip shootSfx;

    public void PlayDamageSfx()
    {
        AudioManager.Instance.PlaySFX(damageSfx);
    }

    public void PlayShootSfx()
    {
        AudioManager.Instance.PlaySFX(shootSfx);
    }
}
