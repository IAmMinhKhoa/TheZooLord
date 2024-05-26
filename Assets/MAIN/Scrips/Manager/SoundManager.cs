using System.Collections;
using System.Collections.Generic;
using com.cyborgAssets.inspectorButtonPro;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public enum AudioSingle
{
    Global
}
public enum SoundType
{
    ClickButton,
    Success,
    Failed, 
    PickUp,
    DropDownTrue,
    DropDownWrong,
    ClapWin,
    footSteps,
    hightEngery,
    Collect,
    hellicoper
    
}
public enum SoundBr
{
    MainMenu_1,
    MainMenu_2,
    MainMenu_3,
    MainMenu_4,

    dayForest,
    dayMeadow,

    nightForest,
    nightMeadow,

    miniGame_1,
    miniGame_2,
    miniGame_3,
    miniGame_4,
    miniGame_5,
}

[System.Serializable]
public class SoundFX
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
[System.Serializable]
public class SoundBR{
    public SoundBr name;
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

    public List<SoundFX> sounds;
    public List<SoundBR> soundsBRMainMenu;
    private Dictionary<SoundType, AudioSource> audioSources;//use for SFX
    private Dictionary<AudioSingle, AudioSource> audioSourcesSingle;//use for single 
    private Dictionary<SoundBr,AudioSource> audioSourcesBr;

    #region AUDIO SOURCE SINGLE
    [SerializeField] AudioSource AS_BackGround;
    [SerializeField] AudioSource AS_Global;




    #endregion



    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        audioSources = new Dictionary<SoundType, AudioSource>();
        foreach (SoundFX sound in sounds)
        {
            AudioSource source = gameObject.AddComponent<AudioSource>();
            sound.SetOriginalVolume(sound.volume);
            source.clip = sound.clip;
            source.volume = sound.volume;
            source.pitch = sound.pitch;
            source.loop = sound.loop;
            audioSources.Add(sound.name, source);
        }

        audioSourcesBr = new Dictionary<SoundBr, AudioSource>();
        foreach (SoundBR sound in soundsBRMainMenu)
        {
            AudioSource source = gameObject.AddComponent<AudioSource>();
            sound.SetOriginalVolume(sound.volume);
            source.clip = sound.clip;
            source.volume = sound.volume;
            source.pitch = sound.pitch;
            source.loop = sound.loop;
            audioSourcesBr.Add(sound.name, source);
        }
        audioSourcesSingle = new Dictionary<AudioSingle, AudioSource>();
        audioSourcesSingle.Add(AudioSingle.Global, AS_Global);
      

    }


    public void PlaySound(SoundBr soundType)
    {
        foreach (KeyValuePair<SoundBr, AudioSource> entry in audioSourcesBr)
        {
            entry.Value.Stop();
        }
        if (audioSourcesBr.ContainsKey(soundType))
        {
            audioSourcesBr[soundType].Play();
        }
    }
    [ProButton]
    public void PlayRandomSound_BR()
    {
  
        int randomeSoundBRMainMenu = Random.Range(1, 5);

        switch (randomeSoundBRMainMenu)
        {
            case 1:
                PlaySound(SoundBr.MainMenu_1);
                break;
            case 2:
                PlaySound(SoundBr.MainMenu_2);
                break;
            case 3:
                PlaySound(SoundBr.MainMenu_3);
                break;
            case 4:
                PlaySound(SoundBr.MainMenu_4);
                break;

        }
    }
    public void PlayRandomSound_MiniGame()
    {

        int randomeSoundBRMainMenu = Random.Range(1, 7);

        switch (randomeSoundBRMainMenu)
        {
            case 1:
                PlaySound(SoundBr.miniGame_1);
                break;
            case 2:
                PlaySound(SoundBr.miniGame_2);
                break;
            case 3:
                PlaySound(SoundBr.miniGame_3);
                break;
            case 4:
                PlaySound(SoundBr.miniGame_4);
                break;
            case 5:
                PlaySound(SoundBr.miniGame_5);
                break;
    

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
    public void StopSoundBG(SoundBr soundType)
    {
        if (audioSourcesBr.ContainsKey(soundType))
        {
            audioSourcesBr[soundType].Stop();
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
            SoundFX sound = sounds.Find(s => s.name == entry.Key);
            entry.Value.volume = sound.originalVolume;
        }
     
    }
    [ProButton]
    public void AdjustValueSFX(float value=0)
    {
        foreach (KeyValuePair<SoundType, AudioSource> entry in audioSources)
        {
            entry.Value.volume = value;
        }
    }
    [ProButton]
    public void AdjustValueBR(float value = 0)
    {
        foreach (KeyValuePair<SoundBr, AudioSource> entry in audioSourcesBr)
        {
            entry.Value.volume = value;
        }
    }
    [ProButton]
    public void AdjustValueMusic(float value = 0)
    {
        AS_BackGround.volume = value;
        AS_Global.volume = value;
    }
    #region AUDIO SINGLE USE TO VOICE DETAIL ANIMAL CAGE
    public void PlayAudioSingle(AudioClip src, AudioSingle type = AudioSingle.Global)
    {
        StopAllSoundsSingle();
        if (audioSourcesSingle.TryGetValue(type, out AudioSource audioSource))
        {
            audioSource.clip = src;
            audioSource.Play();
        }
        else
        {
            Debug.LogError($"PlayAudioSingle: Unknown AudioSingle type: {type}");
        }
    }
    public void StopAllSoundsSingle()
    {
        foreach (KeyValuePair<AudioSingle, AudioSource> kvp in audioSourcesSingle)
        {
            AudioSource audioSource = kvp.Value;
            audioSource.Stop();
        }
    }
    #endregion

    /*   [ProButton]
       public void playFX(SoundType type)
       {
           PlaySound(type);
       }*/
}
