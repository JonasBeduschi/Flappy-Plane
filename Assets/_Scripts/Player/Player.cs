using System;
using System.Collections;
using UnityEngine;

namespace FlappyPlane
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(Collider2D))]
    public class Player : MonoBehaviour
    {
#if UNITY_EDITOR
        [SerializeField] private bool cheat;
#endif
        [SerializeField] private float force;
        private Rigidbody2D rb;
        private Vector2 forceVector;
        private bool dead = true;
        private InputActions input;
        private const float minPosX = -.8f;
        //private AudioController audioController;

        public static Action<DeathEventArgs> OnPlayerDeath;
        public static Action<CollisionArgs> OnPlayerHitSomething;

        private void Awake()
        {
            input = new InputActions();
            rb = GetComponent<Rigidbody2D>();
            forceVector = new Vector2(0, force);
            rb.simulated = false;
            GameController.OnGameStart += StartGame;
            Score.OnScoreChange += AdjustPosition;
            //audioController = FindObjectOfType<AudioController>();
        }

        private void OnEnable()
        {
            input.Enable();
            input.Player.Jump.performed += _ => TryToJump();
        }

        private void OnDisable()
        {
            input.Player.Jump.performed -= _ => TryToJump();
            input.Disable();
        }

        private void TryToJump()
        {
            if (!dead) {
                rb.velocity = forceVector;
                //audioController.PlayJump();
            }
        }

        private void StartGame()
        {
            dead = false;
            rb.simulated = true;
            TryToJump();
        }

        // Adjust x position based on score, down to minPosX
        private void AdjustPosition(int score)
        {
            Vector3 temp = transform.position;
            temp.x = minPosX * ((float)score / GameController.ScoreToMaxSpeed).Capped(0, 1);
            transform.position = temp;
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
#if UNITY_EDITOR
            if (cheat)
                return;
#endif
            // Find out what the player collided with
            CollisionObject co = collision.gameObject.tag switch
            {
                "Ground" => CollisionObject.Ground,
                "Rock" => CollisionObject.Rock,
                "Roof" => CollisionObject.Roof,
                _ => CollisionObject.Ground,
            };

            OnPlayerHitSomething?.Invoke(new CollisionArgs(co, collision.relativeVelocity.magnitude));
            if (!dead)
                Die(new DeathEventArgs(Score.CurrentScore, co));
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (dead)
                return;

            collision.enabled = false;
            StartCoroutine(ReEnableCollider(collision));

            if (collision.CompareTag("Hole"))
                Score.AddPoints(1);
            else if (collision.CompareTag("Star"))
                collision.GetComponent<Star>().Hit();
        }

        private IEnumerator ReEnableCollider(Collider2D collider)
        {
            yield return new WaitForSeconds(2f / GameController.Speed);
            collider.enabled = true;
        }

        private void Die(DeathEventArgs args)
        {
            dead = true;
            OnPlayerDeath?.Invoke(args);
        }

        private void OnDestroy()
        {
            GameController.OnGameStart -= StartGame;
            Score.OnScoreChange -= AdjustPosition;
        }
    }
}