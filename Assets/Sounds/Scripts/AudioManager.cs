using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Audio
{
    public class AudioManager : Framework.MonoSingleton<AudioManager>
    {
        private AudioSource bgmSource;
        private AudioSource sfxSource;
        public ScriptableObj.AudioClipsData clipsData;

        public float minSfxPlayTime; //min sfxPlay Interval.
        private float lastSfxPlayTime; //sfxPlaying Timer.

        protected override void Awake()
        {
            base.Awake();
            DontDestroyOnLoad(gameObject);

            bgmSource = gameObject.AddComponent<AudioSource>();
            sfxSource = gameObject.AddComponent<AudioSource>();
        }

        private void Start()
        {
            PlayBgm();
        }

        //private void Update()
        //{
        //    //SfxSourceVolumnUpdate();
        //}

        #region PlayAudio

        /// <summary>
        /// To play bgm. Call On GameStart.
        /// </summary>
        private void PlayBgm()
        {
            bgmSource.clip = clipsData.bgm;
            bgmSource.loop = true;
            bgmSource.Play();
        }

        private void PlaySfxSound(AudioClip audioClip)
        {
            if (Time.time - lastSfxPlayTime < minSfxPlayTime) return;

            sfxSource.clip = audioClip;
            sfxSource.Play();
            lastSfxPlayTime = Time.time;
        }

        //private void SfxSourceVolumnUpdate()
        //{
        //    sfxSource.volume = Mathf.Lerp(0, 1, LerpSFXVolumn(Time.time - lastSfxPlayTime));
        //}

        //private float LerpSFXVolumn(float interval)
        //{
        //    if (interval > 0f && interval < 0.1f) return 10 * interval;
        //    else return 1f;
        //}

        public void PlayButtonHighlightedSFX()
        {
            PlaySfxSound(clipsData.buttonHighlightedSFX);
        }

        public void PlayButtonPressedSFX()
        {
            PlaySfxSound(clipsData.buttonPressedSFX);
        }

        public void PlayStageClearSFX()
        {
            PlaySfxSound(clipsData.stageClearSFX);
        }

        public void PlayGameFailSFX()
        {
            PlaySfxSound(clipsData.gameFailSFX);
        }

        public void PlayPlayerMoveSFX()
        {
            PlaySfxSound(clipsData.playerMove);
        }

        public void PlayMenuCollisionSFX()
        {
            PlaySfxSound(clipsData.menuCollisionSFX);
        }

        #endregion

        #region Other

        public bool IsSFXPlaying()
        {
            return sfxSource.isPlaying;
        }

        #endregion
    }
}