using UnityEngine.Audio;
using UnityEngine;
using UnityEngine.UI;

public class SoundScript : MonoBehaviour
{
    public AudioSource backgroundmusic;
    public Slider volume_music;

    public void VolumeControl(Slider slider)
    {
        PlayerPrefs.SetFloat("VolumeSetting", slider.value);
    }

    void OnMouseDown()
    {
        backgroundmusic.volume = volume_music.value;        
    }
}
