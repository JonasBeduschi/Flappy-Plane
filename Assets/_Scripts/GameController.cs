using System;
using UnityEngine;

namespace FlappyPlane
{
    public class GameController : MonoBehaviour
    {
        public static GameController Instance;
        public static float Speed { get => speed; private set => speed = value; }
        private static float speed = 1f;
        [SerializeField] private GameObject tap;
        public const float MaxSpeed = 4f;
        public const int ScoreToMaxSpeed = 400;

        public static event Action OnGameStart;

        private void Awake()
        {
            if (Instance != null && Instance != this) {
                Destroy(this);
            }
            else {
                Instance = this;
                Score.OnScoreChange += ChangeSpeed;
                Highscore.LoadHighscore();
            }
        }

        public void StartGame()
        {
            tap.SetActive(false);
            OnGameStart?.Invoke();
        }

        private static void ChangeSpeed(int score)
        {
            if (score >= ScoreToMaxSpeed)
                speed = MaxSpeed;
            else
                speed = 1f + (score / (float)ScoreToMaxSpeed) * (MaxSpeed - 1);
        }

        public void ResetGame()
        {
            SaveSystem.SaveHighscores();
            Score.ResetScore();
            UnityEngine.SceneManagement.SceneManager.LoadScene(0);
        }

        private void OnDestroy()
        {
            Score.OnScoreChange -= ChangeSpeed;
        }
    }
}