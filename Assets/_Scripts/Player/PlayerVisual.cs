using UnityEngine;

namespace FlappyPlane
{
    public class PlayerVisual : MonoBehaviour
    {
        [SerializeField] private float maxRotation;
        [SerializeField] private float rotationSpeed;
        private SpriteRenderer spriteRenderer;
        private Rigidbody2D rb;
        private bool gameStarted = false;
        private bool playerAlive = true;

        [SerializeField] private float timePerFrame;
        [SerializeField] private PlaneArray[] planes;

        [System.Serializable]
        private struct PlaneArray
        {
            public Sprite[] frames;
        }

        private int planeColor = 0;
        private int currentAnimation = 0;
        private float timer = 0;

        private void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
            spriteRenderer = GetComponentInChildren<SpriteRenderer>();

            GameController.OnGameStart += GameStarted;
            SettingsWindow.OnPlanePick += PlanePicked;
            Player.OnPlayerDeath += PlayerDied;
        }

        private void Update()
        {
            if (gameStarted && playerAlive)
                UpdateRotation();
            UpdateSprite();
        }

        private void GameStarted()
        {
            gameStarted = true;
        }

        private void PlayerDied(DeathEventArgs e)
        {
            playerAlive = false;
        }

        private void UpdateSprite()
        {
            timer += Time.deltaTime;
            if (timer < timePerFrame)
                return;

            timer -= timePerFrame;
            currentAnimation += 1;
            // Show the updated frame
            spriteRenderer.sprite = planes[planeColor].frames[currentAnimation.PingPong(planes[planeColor].frames.Length - 1)];
        }

        private void PlanePicked(int index)
        {
            planeColor = index;
        }

        private void UpdateRotation()
        {
            transform.rotation = Quaternion.Euler(0, 0, (rb.velocity.y * rotationSpeed).Capped(-maxRotation, maxRotation));
        }
    }
}