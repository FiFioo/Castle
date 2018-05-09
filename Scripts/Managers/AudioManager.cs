using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Castle
{
    public class AudioManager : MonoBehaviour
    {
        public Transform mAudioUI;

        private Toggle mAudioToggle;
        private Slider mAudioSlider;

        private void Start()
        {
            InitAudioSetting();
            InitAudioUI();
        }

        public void MuteAudio()
        {
            if (null != mAudioToggle) {
                AudioListener.pause             = !mAudioToggle.isOn;
                DataManager.audioPause          = AudioListener.pause;
            }
        }

        public void ChangeVolume()
        {
            if (null != mAudioSlider) {
                AudioListener.volume    = mAudioSlider.value;
                DataManager.audioVolume = AudioListener.volume;
            }
        }

        private void InitAudioSetting()
        {
            AudioListener.pause  = DataManager.audioPause;
            AudioListener.volume = DataManager.audioVolume;
        }

        private void InitAudioUI()
        {
            if (null != mAudioUI) {
                mAudioToggle = mAudioUI.Find(DataConfigure.PATH_AUDIO_UI_TOGGLE).GetComponent<Toggle>();
                mAudioSlider = mAudioUI.Find(DataConfigure.PATH_AUDIO_UI_SLIDER).GetComponent<Slider>();
            }

            if (null != mAudioToggle) { 
                mAudioToggle.isOn = !AudioListener.pause;
            }

            if (null != mAudioSlider) {
                mAudioSlider.value = AudioListener.volume ;
            }
        }
    }
}