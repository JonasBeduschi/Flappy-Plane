using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace FlappyPlane
{
    public class HighscoreVisualizer : MonoBehaviour
    {
        [SerializeField] private Text myText;

        private void OnEnable()
        {
            myText.text = GetFormatedHighscores();
        }

        private static string GetFormatedHighscores()
        {
            StringBuilder builder = new StringBuilder();
            string highscorerName;
            for (int i = 0; i < Highscore.HighscoresNames.Length; i++) {
                highscorerName = Highscore.HighscoresNames[i];
                while (highscorerName.Length < 3)
                    highscorerName += " ";
                builder.Append(highscorerName);
                builder.Append(" ");
                builder.AppendLine(Highscore.HighscoresValues[i].ToString());
            }
            return builder.ToString();
        }
    }
}