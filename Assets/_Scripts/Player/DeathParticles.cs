using System;
using UnityEngine;

namespace FlappyPlane
{
    public class DeathParticles : MonoBehaviour
    {
        [SerializeField] private ParticleSystem deathParticles;

        private void Awake()
        {
            Player.OnPlayerDeath += PlayerDied;
        }

        public void PlayerDied(EventArgs args)
        {
            deathParticles.Play();
        }

        private void OnDestroy()
        {
            Player.OnPlayerDeath -= PlayerDied;
        }
    }
}