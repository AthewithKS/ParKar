using UnityEngine;


public enum SoundType
{
    EnginStart,
    EnginIdel,
    E_lowRPM,
    E_highRPM,
    V_Drift,
    Breakint,
    VehicleHit
}
[RequireComponent(typeof(AudioSource))]
public class SoundManager : MonoBehaviour
{
    [SerializeField] private AudioClip[] soundList;
    private static SoundManager instance;
    private AudioSource audioSource;

    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public static void PlaySound(SoundType sound, float volume = 1)
    {
        instance.audioSource.PlayOneShot(instance.soundList[(int)sound], volume);
    }
    public static void PlayLoopingSound(SoundType sound, float volume = 0.2f)
    {
        if (instance != null && instance.audioSource != null)
        {
            instance.audioSource.clip = instance.soundList[(int)sound];
            instance.audioSource.volume = volume;
            instance.audioSource.loop = true;
            instance.audioSource.Play();
        }
    }
    public static void StopLoopingSound()
    {
        if (instance.audioSource.isPlaying && instance.audioSource.loop)
        {
            instance.audioSource.Stop();
        }
    }
    public static void SetPitch(float pitch)
    {
        if (instance != null && instance.audioSource != null)
        {
            instance.audioSource.pitch = Mathf.Clamp(pitch, 0.5f, 3f); // Set pitch, clamped between 0.5 and 3
        }
    }
}
