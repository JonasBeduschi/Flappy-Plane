using UnityEngine;
using UnityEngine.UI;

namespace FlappyPlane
{
    public class SettingsWindow : MonoBehaviour
    {
        private AudioController audioController;
        private Toggle[] toggles;
        private Slider[] sliders;
        private float[] values;

        private void Awake()
        {
            audioController = FindObjectOfType<AudioController>();
            sliders = GetComponentsInChildren<Slider>();
            toggles = GetComponentsInChildren<Toggle>();
            values = SaveSystem.LoadSettings();
        }

        private void Start()
        {
            toggles[(int)values[0]].isOn = true;
            PickPlane((int)values[0]);

            for (int i = 0; i < sliders.Length; i++)
                sliders[i].value = values[i + 1];
            SetMasterVolume(values[1]);
            SetMusicVolume(values[2]);
            SetEffectsVolume(values[3]);
        }

        public void SetMasterVolume(float volume) => audioController.SetAttenuation(volume, "VolumeMaster");

        public void SetMusicVolume(float volume) => audioController.SetAttenuation(volume, "VolumeMusic");

        public void SetEffectsVolume(float volume) => audioController.SetAttenuation(volume, "VolumeSFX");

        public void PickPlane(int index)
        {
            EventSystem.FireEvent(this, new PlanePickArgs(index));
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