using DevelopEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Universal.Audio
{
    public enum AudioType { bgm, effect, speech }
    public class AudioPlayer : MonoSingleton<AudioPlayer>
    {

        public static string SpeechStart = "SpeechStart";
        public static string SpeechEnd = "SpeechEnd";

        AudioSource bgm;
        AudioSource effect;
        AudioSource speech;

        private Coroutine speechCoroutine = null;

        public List<AudioClip> sounds;

        private void Awake()
        {
            bgm = gameObject.AddComponent<AudioSource>();
            bgm.loop = true;
            bgm.volume = 0.08f;

            effect = gameObject.AddComponent<AudioSource>();
            effect.loop = false;

            speech = gameObject.AddComponent<AudioSource>();
            speech.loop = false;
            speech.volume = 1f;

            //sounds = new List<AudioClip>();
            //sounds.Add(Resources.Load("Sounds/Effect/MatchPos") as AudioClip); //0
            //sounds.Add(Resources.Load("Sounds/Speech/测试") as AudioClip); //1

            //PlayAudio(AudioType.bgm, 16);
        }

        public void PlayAudio(AudioType type, int index)
        {
            switch (type)
            {
                case AudioType.bgm:
                case AudioType.effect:
                    AudioClip clip = GetSound(index);
                    AudioSource source = GetAudioSource(type);
                    if (clip != null && source != null)
                    {
                        if (source.isPlaying && source.clip.name.Equals(clip.name))
                        {
                            return;
                        }
                        source.clip = clip;
                        source.Play();
                    }
                    break;

                case AudioType.speech:
                    StopSpeech();
                    speechCoroutine = StartCoroutine(SpeechPlay(index));
                    break;
            }
        }

        //public string GetAudioNameByIndex(int index)
        //{
        //    if (sounds.Count > 0 && index >= 0 && index < sounds.Count)
        //        return sounds[index].ToString();
        //    return null;
        //}

        public IEnumerator SpeechPlay(int index, Action onPlayFinish = null)
        {
            AudioClip clip = GetSound(index);
            AudioSource source = GetAudioSource(AudioType.speech);
            if (clip != null && source != null)
            {
                source.clip = clip;
                source.Play();
                //ManagerEvent.Send(SpeechStart, index);
                while (source.isPlaying)
                {
                    yield return null;
                }
                StopSpeech();
                if (onPlayFinish != null)
                    onPlayFinish();
                //ManagerEvent.Send(SpeechEnd, index);
            }
        }


        public IEnumerator SpeechPlay(string name, Action onPlayFinish = null)
        {
            AudioClip clip = GetSound(name);
            AudioSource source = GetAudioSource(AudioType.speech);
            if (clip != null && source != null)
            {
                source.clip = clip;
                source.Play();
                //ManagerEvent.Send(SpeechStart, index);
                while (source.isPlaying)
                {
                    yield return null;
                }
                StopSpeech();
                if (onPlayFinish != null)
                    onPlayFinish();
                //ManagerEvent.Send(SpeechEnd, index);
            }
        }


        public IEnumerator SpeechPlay(AudioClip clip, Action onPlayFinish = null)
        {
            //AudioClip clip = GetSound(index);
            AudioSource source = GetAudioSource(AudioType.speech);
            if (clip != null && source != null)
            {
                source.clip = clip;
                source.Play();
                //ManagerEvent.Send(SpeechStart, index);
                while (source.isPlaying)
                {
                    yield return null;
                }
                StopSpeech();
                if (onPlayFinish != null)
                    onPlayFinish();
                //ManagerEvent.Send(SpeechEnd, index);
            }
        }
        public void StopSpeech()
        {
            //if (speechCoroutine != null)
            {
                AudioSource source = GetAudioSource(AudioType.speech);
                if (source.clip != null)
                {
                    source.Stop();
                }
                if (speechCoroutine != null)
                {
                    StopCoroutine(speechCoroutine);
                    speechCoroutine = null;
                }
            }
        }
        //public void StopSpeech()
        //{
        //    if (speechCoroutine != null)
        //    {
        //        AudioSource source = GetAudioSource(AudioType.speech);
        //        source.Stop();
        //        StopCoroutine(speechCoroutine);
        //        speechCoroutine = null;
        //    }
        //}

        public void StopAudio(AudioType type)
        {
            switch (type)
            {
                case AudioType.bgm:
                case AudioType.effect:
                    AudioSource source = GetAudioSource(type);
                    if (source != null)
                    {
                        source.Stop();
                    }
                    break;

                case AudioType.speech:
                    StopSpeech();
                    break;
            }
        }

        public float AudioLen(int index)
        {
            AudioSource source = GetAudioSource(AudioType.speech);
            return source.clip.length;
        }
        private AudioSource GetAudioSource(AudioType type)
        {
            switch (type)
            {
                case AudioType.bgm: return bgm;
                case AudioType.effect: return effect;
                case AudioType.speech: return speech;
            }
            return null;
        }

        private AudioClip GetSound(int index)
        {
            AudioClip clip = null;
            if (sounds != null && sounds.Count - 1 >= index && index >= 0)
            {
                return sounds[index];
            }
            return clip;
        }


        private AudioClip GetSound(string name)
        {
            AudioClip clip = null;
            clip = sounds.Find(p => p.name.Equals(name));
            return clip;
        }


        /// <summary>
        /// 通过下标获取音频长度
        /// </summary>
        /// <param name="index">音频下标</param>
        /// <returns></returns>
        public float GetLength(int index)
        {
            return sounds[index].length;
        }

    }
}

