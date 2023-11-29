using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Audio
{
    public class AudioManager : Framework.MonoSingleton<AudioManager>
    {
        private AudioSource bgmSource;
        private AudioSource playerSource;
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
            playerSource = gameObject.AddComponent<AudioSource>();
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

        private void PlayPlayerSound(AudioClip audioClip)
        {
            playerSource.clip = audioClip;
            playerSource.Play();
        }

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
            PlayPlayerSound(clipsData.stageClearSFX);
        }

        public void PlayGameFailSFX()
        {
            PlayPlayerSound(clipsData.gameFailSFX);
        }

        public void PlayPlayerMoveSFX()
        {
            PlayPlayerSound(clipsData.playerMove);
        }

        public void PlayCollisionSFX()
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