using System.Collections;
using UnityEngine;
using UnityEngine.Audio;
using Random = UnityEngine.Random;

namespace FlappyPlane
{
    public class AudioController : MonoBehaviour
    {
        [SerializeField] private AudioSource playerSource;
        [SerializeField] private AudioSource UISource;
        [SerializeField] private AudioSource EndSource;
        [SerializeField] private AudioSource GameSource;
        [SerializeField] private AudioSource MenuSource;
        [SerializeField] private AudioMixerSnapshot startSnapshot;
        [SerializeField] private AudioMixerSnapshot gameSnapshot;
        [SerializeField] private AudioMixerSnapshot endSnapshot;
        [SerializeField] private AudioMixer mainMixer;
        private const float endTransitionTime = 1f;

        [SerializeField] private AudioClip endRecord;
        [SerializeField] private AudioClip endMedal;
        [SerializeField] private AudioClip endLose;

        [SerializeField] private AudioClip[] hitRock;
        [SerializeField] private AudioClip[] hitGround;

        [SerializeField] private AudioClip buttonClick;
        [SerializeField] private AudioClip select;
        [SerializeField] private AudioClip star;
        [SerializeField] private AudioClip jump;

        private const float maxStr = 5f;

        private void Awake()
        {
            Player.OnPlayerHitSomething += PlayerHit;
            Player.OnPlayerDeath += PlayerDied;
            GameController.OnGameStart += GameStarted;
            startSnapshot.TransitionTo(.5f);
        }

        private void GameStarted()
        {
            gameSnapshot.TransitionTo(1);
            GameSource.Play();
            StartCoroutine(StopDelayed(MenuSource));
        }

        private void PlayerDied(DeathEventArgs e)
        {
            EndSource.clip = EndClipForPosition(e.Position);
            endSnapshot.TransitionTo(endTransitionTime);
            EndSource.Play();
            StartCoroutine(StopDelayed(GameSource));
        }

        private AudioClip EndClipForPosition(int position)
        {
            if (position <= 0 || position > 10)
                return endLose;
            else if (position <= 3)
                return endMedal;
            else
                return endRecord;
        }

        private void PlayerHit(CollisionArgs e)
        {
            AudioClip[] clipsToPlay;
            switch (e.Collision) {
                case CollisionObject.Ground:
                    clipsToPlay = hitGround;
                    break;

                case CollisionObject.Rock:
                    clipsToPlay = hitRock;
                    break;

                default:
                    return;
            }
            PlayRandom(playerSource, clipsToPlay, (e.Strength / maxStr).Capped());
        }

        private static void PlayRandom(AudioSource source, AudioClip[] clips, float volume = 1f)
        {
            source.PlayOneShot(clips[Random.Range(0, clips.Length)], volume);
        }

        private IEnumerator StopDelayed(AudioSource source, float delay = 1)
        {
            yield return new WaitForSeconds(delay);
            source.Stop();
        }

        private void OnDestroy()
        {
            Player.OnPlayerHitSomething -= PlayerHit;
            Player.OnPlayerDeath -= PlayerDied;
            GameController.OnGameStart -= GameStarted;
        }

        public void PlayButton()
        {
            UISource.PlayOneShot(buttonClick);
        }

        public void PlaySelect()
        {
            UISource.PlayOneShot(select);
        }

        public void PlayStar()
        {
            playerSource.PlayOneShot(star);
        }

        public void PlayJump()
        {
            playerSource.PlayOneShot(jump);
        }

        public void SetAttenuation(float volume, string mixerName)
        {
            float adjustedVolume = volume.Capped();
            // adjustedVolume (0 to 1) becomes attenuation (-80 if 0) (-30 to 20 if not)
            float attenuation = adjustedVolume == 0 ? -80 : adjustedVolume * 50f - 30f;
            mainMixer.SetFloat(mixerName, attenuation);
        }
    }
}