using UnityEngine;
using UnityEngine.UI;
using System;

namespace FlappyPlane
{
    public class SettingsWindow : MonoBehaviour
    {
        private const string VolumeMaster = "VolumeMaster";
        private const string VolumeMusic = "VolumeMusic";
        private const string VolumeSFX = "VolumeSFX";

        private AudioController audioController;
        private Toggle[] toggles;
        private Slider[] sliders;
        private float[] values;
        public static Action<int> OnPlanePick;

        private void Awake()
        {
            audioController = FindObjectOfType<AudioController>();
            sliders = GetComponentsInChildren<Slider>();
            toggles = GetComponentsInChildren<Toggle>();
            // Load the saved settings
            values = SaveSystem.LoadSettings();
        }

        private void Start()
        {
            // Select the saved plane
            toggles[(int)values[0]].isOn = true;
            PickPlane((int)values[0]);

            // Set the proper value on the volume sliders (this is only visual)
            for (int i = 0; i < sliders.Length; i++)
                sliders[i].value = values[i + 1];
            // Set proper volumes
            SetMasterVolume(values[1]);
            SetMusicVolume(values[2]);
            SetEffectsVolume(values[3]);
        }

        public void SetMasterVolume(float volume) => audioController.SetAttenuation(volume, VolumeMaster);

        public void SetMusicVolume(float volume) => audioController.SetAttenuation(volume, VolumeMusic);

        public void SetEffectsVolume(float volume) => audioController.SetAttenuation(volume, VolumeSFX);

        public void PickPlane(int index)
        {
            OnPlanePick(index);
            values[0] = index;
        }

        public void SaveSettings()
        {
            for (int i = 0; i < sliders.Length; i++)
                values[i + 1] = sliders[i].value;
            SaveSystem.SaveSettings(values);
        }
    }
}