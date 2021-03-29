using UnityEngine;

namespace FlappyPlane
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class Star : MonoBehaviour
    {
        private const int PointsForStarA = 1;
        private const int PointsForStarB = 2;
        private const int PointsForStarC = 5;

        private ParticleSystem particle;
        private SpriteRenderer myRenderer;
        private SpriteChanger changer;
        private AudioController audioController;

        private void Awake()
        {
            particle = GetComponent<ParticleSystem>();
            myRenderer = GetComponent<SpriteRenderer>();
            myRenderer.sprite = null;

            changer = transform.parent.GetComponent<SpriteChanger>();
            audioController = FindObjectOfType<AudioController>();
        }

        public void Hit()
        {
            myRenderer.enabled = false;
            if (myRenderer.sprite == null)
                return;

            audioController.PlayStar();
            particle.Play();

            if (myRenderer.sprite == changer.GetSprites()[1])
                Score.AddPoints(PointsForStarA);
            else if (myRenderer.sprite == changer.GetSprites()[2])
                Score.AddPoints(PointsForStarB);
            else if (myRenderer.sprite == changer.GetSprites()[3])
                Score.AddPoints(PointsForStarC);
        }
    }
}