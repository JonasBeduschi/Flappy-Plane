using UnityEngine;

namespace FlappyPlane
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class Star : MonoBehaviour
    {
        private SpriteRenderer myRenderer;
        private SpriteChanger changer;
        private AudioController audioController;
        private ParticleSystem particle;

        private void Awake()
        {
            myRenderer = GetComponent<SpriteRenderer>();
            myRenderer.sprite = null;
            changer = transform.parent.GetComponent<SpriteChanger>();
            audioController = FindObjectOfType<AudioController>();
            particle = GetComponent<ParticleSystem>();
        }

        public void Hit()
        {
            myRenderer.enabled = false;
            if (myRenderer.sprite == null)
                return;
            audioController.PlayStar();
            particle.Play();
            if (myRenderer.sprite == changer.GetSprites()[1])
                Score.AddPoints(1);
            else if (myRenderer.sprite == changer.GetSprites()[2])
                Score.AddPoints(2);
            else if (myRenderer.sprite == changer.GetSprites()[3])
                Score.AddPoints(5);
        }
    }
}