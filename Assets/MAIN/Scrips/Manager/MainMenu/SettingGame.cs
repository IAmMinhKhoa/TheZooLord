using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;


public class SettingGame : MonoBehaviour
{
    //---slider sound---
    [SerializeField] Slider _sliderSFX;
    [SerializeField] Slider _sliderMusic;
    //---toggle sound---
    [SerializeField] Toggle _toggleSFX;
    [SerializeField] Toggle _toggleMusic;
    //---Parentail Control---
    [SerializeField] ParentalControl _parentalControl;
    [SerializeField] GameObject _mainSetting;
    private void Start()
    {
        //------------EVENT SLIDER---------
        _sliderSFX.onValueChanged.AddListener((v) =>
        {
            SoundManager.instance.AdjustValueSFX(v);
            if(v>0) _toggleSFX.isOn=true;
        });
        _sliderMusic.onValueChanged.AddListener((v) =>
        {
            SoundManager.instance.AdjustValueMusic(v);
            if (v > 0) _toggleMusic.isOn = true;
        });

        //----------EVENT TOGGLE-----------
        _toggleSFX.onValueChanged.AddListener(toggleSFX);
        _toggleMusic.onValueChanged.AddListener(toggleMusic);
    }

    public void SelectSetting()//use this event to button setting in main screen
    {
        _parentalControl.OpenPanel();
        _parentalControl.afterCorrect += OpenMainSetting;
    }
    private void OpenMainSetting()
    {
        _mainSetting.SetActive(true);
    }
    public void CloseMainSetting()
    {
        _mainSetting.SetActive(false);
    }
    #region TOGGLE SOUND
    private void toggleSFX(bool isOn)
    {
        // Handle the toggle value change
        if (isOn)
        {
            _sliderSFX.value = 1f;
        }
        else
        {
            _sliderSFX.value = 0f;
        }
    }
    private void toggleMusic(bool isOn)
    {
        // Handle the toggle value change
        if (isOn)
        {
            _sliderMusic.value = 1f;
        }
        else
        {
            _sliderMusic.value = 0f;
        }
    }
    #endregion
}
