namespace FlappyPlane
{
    public static class Highscore
    {
        private static int[] highscoresValues;

        public static int[] HighscoresValues
        {
            get
            {
                if (highscoresValues == null)
                    highscoresValues = SaveSystem.LoadHighscoresValues();
                return highscoresValues;
            }
            set => highscoresValues = value;
        }

        private static string[] highscoresNames;

        public static string[] HighscoresNames
        {
            get
            {
                if (highscoresNames == null)
                    highscoresNames = SaveSystem.LoadHighscoresNames();
                return highscoresNames;
            }
            set => highscoresNames = value;
        }

        public static void InsertHighScore(int score, string name)
        {
            for (int i = 0; i < HighscoresValues.Length; i++) {
                if (score > HighscoresValues[i]) {
                    HighscoresValues.PushAt(i, score);
                    HighscoresNames.PushAt(i, name);
                    return;
                }
            }
            return;
        }

        public static int CheckHighscore(int score)
        {
            for (int i = 0; i < HighscoresValues.Length; i++)
                if (score > HighscoresValues[i])
                    return i + 1;
            return 0;
        }

        public static void LoadHighscore()
        {
            // Dummy arrays to force the loading
            string[] s = HighscoresNames;
            int[] i = HighscoresValues;
        }
    }
}