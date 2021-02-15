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
            string temp;
            for (int i = 0; i < Highscore.HighscoresNames.Length; i++) {
                temp = Highscore.HighscoresNames[i];
                while (temp.Length < 3)
                    temp += " ";
                builder.Append(temp);
                builder.Append(" ");
                builder.AppendLine(Highscore.HighscoresValues[i].ToString());
            }
            return builder.ToString();
        }
    }
}