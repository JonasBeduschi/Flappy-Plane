using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

namespace FlappyPlane
{
    public static class SaveSystem
    {
        private static BinaryFormatter bf;
        private static string path;
        private static FileStream stream;

        public static void SaveHighscores()
        {
            bf = new BinaryFormatter();
            path = GetPathHighscoreValues();
            stream = new FileStream(path, FileMode.Create);
            bf.Serialize(stream, Highscore.HighscoresValues);
            stream.Close();

            bf = new BinaryFormatter();
            path = GetPathHighscoreNames();
            stream = new FileStream(path, FileMode.Create);
            bf.Serialize(stream, Highscore.HighscoresNames);
            stream.Close();
        }

        public static void SaveSettings(float[] values)
        {
            bf = new BinaryFormatter();
            path = GetPathSettings();
            stream = new FileStream(path, FileMode.Create);
            Debug.Log("Saving settings: " + values.ToStringLong());
            bf.Serialize(stream, values);
            stream.Close();
        }

        public static int[] LoadHighscoresValues()
        {
            path = GetPathHighscoreValues();
            if (File.Exists(path)) {
                bf = new BinaryFormatter();
                stream = new FileStream(path, FileMode.Open);
                int[] result = bf.Deserialize(stream) as int[];
                stream.Close();
                return result;
            }
            else {
                Debug.Log($"Nothing found at {GetPathHighscoreValues()}, creating standard file");
                return new int[] {
            500,
            300,
            200,
            150,
            120,
            100,
            80,
            60,
            40,
            20
            };
            }
        }

        public static string[] LoadHighscoresNames()
        {
            path = GetPathHighscoreNames();
            if (File.Exists(path)) {
                bf = new BinaryFormatter();
                stream = new FileStream(path, FileMode.Open);
                string[] result = bf.Deserialize(stream) as string[];
                stream.Close();
                return result;
            }
            else {
                Debug.Log($"Nothing found at {GetPathHighscoreNames()}, creating standard file");
                return new string[] {
            "AAA",
            "BBB",
            "CCC",
            "DDD",
            "EEE",
            "FFF",
            "GGG",
            "HHH",
            "III",
            "JJJ"
            };
            }
        }

        public static float[] LoadSettings()
        {
            path = GetPathSettings();
            if (File.Exists(path)) {
                bf = new BinaryFormatter();
                stream = new FileStream(path, FileMode.Open);
                float[] result = bf.Deserialize(stream) as float[];
                stream.Close();
                return result;
            }
            else {
                Debug.Log($"Nothing found at {GetPathSettings()}, creating standard file");
                return new float[] {
            0,
            .6f,
            .6f,
            .4f
            };
            }
        }

        private static string GetPathHighscoreValues() => $"{Application.persistentDataPath}/highscoresValues.dat";

        private static string GetPathHighscoreNames() => $"{Application.persistentDataPath}/highscoresNames.dat";

        private static string GetPathSettings() => $"{Application.persistentDataPath}/settings.dat";
    }
}