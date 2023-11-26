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

        /// <summary>
        /// To play bgm. Call On GameStart.
        /// </summary>
        private void PlayBgm()
        {
            bgmSource.clip = clipsData.bgm;
            bgmSource.loop = true;
            bgmSource.Play();
        }

        private void PlaySound(AudioClip audioClip)
        {
            sfxSource.clip = audioClip;
            sfxSource.Play();
        }

        public void PlayButtonHighlightedSFX()
        {
            PlaySound(clipsData.buttonHighlightedSFX);
        }

        public void PlayButtonPressedSFX()
        {
            PlaySound(clipsData.buttonPressedSFX);
        }

        public void PlayStageClearSFX()
        {
            PlaySound(clipsData.stageClearSFX);
        }

        public void PlayGameFailSFX()
        {
            PlaySound(clipsData.gameFailSFX);
        }

        public void PlayPlayerMoveSFX()
        {
            PlaySound(clipsData.playerMove);
        }
    }
}