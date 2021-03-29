using UnityEngine;
using UnityEngine.UI;

namespace FlappyPlane
{
    public class NewHighscoreScreen : EndingWindow
    {
        [SerializeField] private Sprite[] medals;
        [SerializeField] private Image medalImage;
        private InputField inputField;

        protected override void HandlePlayerDeath(DeathEventArgs e)
        {
            if (e.Result == PlayerResult.Highscore || e.Result == PlayerResult.Medal)
                Show(e);
        }

        protected override void Show(DeathEventArgs e)
        {
            base.Show(e);

            if (e.Result == PlayerResult.Medal) {
                medalImage.enabled = true;
                medalImage.sprite = medals[e.Position - 1];
            }
            else
                medalImage.enabled = false;
        }

        public void AddScore()
        {
            inputField = target.GetComponentInChildren<InputField>();
            Highscore.InsertHighScore(Score.CurrentScore, inputField.text.ToUpper());
        }
    }
}