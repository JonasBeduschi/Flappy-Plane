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
        [SerializeField] private AudioSource EndMusicSource;
        [SerializeField] private AudioSource GameMusicSource;
        [SerializeField] private AudioSource MenuMusicSource;
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
            GameController.OnGameStart += GameStarted;
            Player.OnPlayerHitSomething += PlayerHitSomething;
            Player.OnPlayerDeath += PlayerDied;
            startSnapshot.TransitionTo(.5f);
        }

        private void GameStarted()
        {
            gameSnapshot.TransitionTo(1);
            GameMusicSource.Play();
            StartCoroutine(StopDelayed(MenuMusicSource));
        }


        private void PlayerHitSomething(CollisionArgs e)
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

        private void PlayerDied(DeathEventArgs e)
        {
            EndMusicSource.clip = EndClipForResult(e.Result);
            endSnapshot.TransitionTo(endTransitionTime);
            EndMusicSource.Play();
            StartCoroutine(StopDelayed(GameMusicSource));
        }

        private AudioClip EndClipForResult(PlayerResult result)
        {
            return result switch
            {
                PlayerResult.None => endLose,
                PlayerResult.Highscore => endRecord,
                PlayerResult.Medal => endMedal,
                _ => endLose,
            };
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
            GameController.OnGameStart -= GameStarted;
            Player.OnPlayerHitSomething -= PlayerHitSomething;
            Player.OnPlayerDeath -= PlayerDied;
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