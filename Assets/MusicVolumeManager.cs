using UnityEngine;
using UnityEngine.Audio;

public class MusicVolumeManager : MonoBehaviour
{
    public static float volumePercent => PlayerPrefs.GetFloat("MusicVolume", 1f);

    void Awake()
    {
        AudioListener.volume = volumePercent;
    }

    public static void SetVolume(float percent)
    {
        percent = Mathf.Clamp01(percent);
        PlayerPrefs.SetFloat("MusicVolume", percent);
        PlayerPrefs.Save();
        AudioListener.volume = percent;
    }
}
