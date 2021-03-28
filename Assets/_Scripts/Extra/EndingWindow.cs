using UnityEngine;
using UnityEngine.UI;

namespace FlappyPlane
{
    public abstract class EndingWindow : MonoBehaviour
    {
        [SerializeField] protected GameObject target;
        [SerializeField] protected Text playerScore;

        private void Awake()
        {
            Player.OnPlayerDeath += HandlePlayerDeath;
        }

        protected abstract void HandlePlayerDeath(DeathEventArgs e);

        protected virtual void Show(DeathEventArgs e)
        {
            target.GetComponent<Animator>().SetTrigger("Show");
            playerScore.text = e.PlayerScore.ToString();
        }

        private void OnDestroy()
        {
            Player.OnPlayerDeath -= HandlePlayerDeath;
        }
    }
}