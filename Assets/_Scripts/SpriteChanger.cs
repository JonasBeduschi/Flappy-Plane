using UnityEngine;

namespace FlappyPlane
{
    [RequireComponent(typeof(Translator))]
    public class SpriteChanger : MonoBehaviour
    {
        [SerializeField] private Sprite[] spritesToChange;
        [SerializeField] private int[] scoreToChangeSprite;
        private int currentSprite = 0;

        public Sprite[] GetSprites() => spritesToChange;

        private void Awake()
        {
            if (spritesToChange.Length <= 0 || scoreToChangeSprite.Length != spritesToChange.Length)
                Debug.LogError("Mismatch on sprites");
            GetComponent<Translator>().OnSpriteResetPosition += ResetSprite;
        }

        private void ResetSprite(GameObject obj)
        {
            SpriteRenderer[] spriteRenderers = obj.GetComponentsInChildren<SpriteRenderer>(true);
            for (int i = 0; i < spriteRenderers.Length; i++) {
                spriteRenderers[i].sprite = GetCurrentSprite();
                spriteRenderers[i].enabled = true;
            }
        }

        private Sprite GetCurrentSprite()
        {
            // Change to spritesToChange[X] when reaching scoreToChangeSprite[X] points
            // But make sure that each sprite is used at least once
            if (currentSprite < spritesToChange.Length - 1 && Score.CurrentScore >= scoreToChangeSprite[currentSprite + 1])
                currentSprite++;
            return spritesToChange[currentSprite];
        }

        private void OnDestroy()
        {
            GetComponent<Translator>().OnSpriteResetPosition -= ResetSprite;
        }
    }
}