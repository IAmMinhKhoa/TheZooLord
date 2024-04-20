using System.Collections;
using System.Collections.Generic;
using com.cyborgAssets.inspectorButtonPro;
using UnityEngine;

public enum AudioSingle
{
    BackGround,
    Cage,
    Question
}
public enum SoundType
{
    ClickButton,
    Success,
    Failed,
    PickUp,
    DropDownTrue,
    DropDownWrong,
    ClapWin

}

[System.Serializable]
public class Sound
{
    public SoundType name;
    public AudioClip clip;
    [Range(0f, 1f)]
    public float volume = 1f;
    [Range(0.1f, 3f)]
    public float pitch = 1f;
    public bool loop = false;

    [HideInInspector]
    public AudioSource source;
    [HideInInspector]
    public float originalVolume;

    public void SetOriginalVolume(float value)
    {
        originalVolume = value;
    }
}

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    public List<Sound> sounds;

    private Dictionary<SoundType, AudioSource> audioSources;//use for SFX

    #region AUDIO SOURCE SINGLE
    [SerializeField] AudioSource AS_BackGround;
    [SerializeField] AudioSource AS_Cage;
    [SerializeField] AudioSource AS_Question;



    #endregion



    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        audioSources = new Dictionary<SoundType, AudioSource>();
        foreach (Sound sound in sounds)
        {
            AudioSource source = gameObject.AddComponent<AudioSource>();
            sound.SetOriginalVolume(sound.volume);
            source.clip = sound.clip;
            source.volume = sound.volume;
            source.pitch = sound.pitch;
            source.loop = sound.loop;
            audioSources.Add(sound.name, source);
        }
       
    }

    public void PlaySound(SoundType soundType)
    {
        if (audioSources.ContainsKey(soundType))
        {
            audioSources[soundType].Play();
        }
    }

    public void StopSound(SoundType soundType)
    {
        if (audioSources.ContainsKey(soundType))
        {
            audioSources[soundType].Stop();
        }
    }


    public void PauseSound()
    {
        foreach (KeyValuePair<SoundType, AudioSource> entry in audioSources)
        {
            entry.Value.volume = 0f;
        }

    }
    public void SetAllVolumesToOriginal()
    {
        foreach (KeyValuePair<SoundType, AudioSource> entry in audioSources)
        {
            Sound sound = sounds.Find(s => s.name == entry.Key);
            entry.Value.volume = sound.originalVolume;
        }
     
    }
    #region AUDIO SINGLE
    public void PlayAudioSingle(AudioSingle type,AudioClip src)
    {
        Dictionary<AudioSingle, AudioSource> audioSourceMap = new Dictionary<AudioSingle, AudioSource>()
        {
            { AudioSingle.BackGround, AS_BackGround },
            { AudioSingle.Cage, AS_Cage },
            { AudioSingle.Question, AS_Question }
        };
        // Play the audio based on the type and handle potential missing entries
        if (audioSourceMap.TryGetValue(type, out AudioSource audioSource))
        {
            audioSource.clip = src;
            audioSource.Play();
        }
        else
        {
            Debug.LogError($"PlayAudioSingle: Unknown AudioSingle type: {type}");
        }
    }
    #endregion

    [ProButton]
    public void playFX(SoundType type)
    {
        PlaySound(type);
    }
}
