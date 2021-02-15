using System;
using UnityEngine;

namespace FlappyPlane
{
    public class DeathParticles : MonoBehaviour
    {
        [SerializeField] private ParticleSystem deathParticles;

        private void Awake()
        {
            EventSystem.OnPlayerDeath += PlayerDied;
        }

        public void PlayerDied(object sender, EventArgs args)
        {
            deathParticles.Play();
        }

        private void OnDestroy()
        {
            EventSystem.OnPlayerDeath -= PlayerDied;
        }
    }
}