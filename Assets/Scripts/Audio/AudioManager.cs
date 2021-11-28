using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : Singleton<AudioManager>
{
  [SerializeField] private AudioSource musicSource;
  [SerializeField] private AudioSource SFXSource;
  [SerializeField] private AudioMixer mixer;
  [SerializeField] private float multiplier = 30f;
  [SerializeField] private Float masterVolume;
  [SerializeField] private Float musicVolume;
  [SerializeField] private Float SFXVolume;

  protected override void Awake()
  {
    base.Awake();
    // musicSource.Play();
  }

  protected void Start()
  {
    SetVolume("MasterVolume", masterVolume.Value);
    SetVolume("MusicVolume", musicVolume.Value);
    SetVolume("SFXVolume", SFXVolume.Value);
  }

  public void SetVolume(string volumeParameter, float value)
  {
    mixer.SetFloat(volumeParameter, Mathf.Log10(value) * multiplier);
  }

  public void PlaySound(string name)
  {
    AudioClip clip = SoundLibrary.Instance.GetClipFromName(name);
    if (clip != null)
    {
      SFXSource.PlayOneShot(clip);
    }
  }
}
