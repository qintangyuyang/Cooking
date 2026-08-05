using System;
using System.Collections;
using System.Collections.Generic;
using Cooking.Model;
using UnityEngine;

namespace Cooking.Manager
{
    /// <summary>
    /// 音频管理器
    /// </summary>
    public class AudioManager : MonoBehaviour
    {
        private static AudioManager _instance;

        public static AudioManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = FindObjectOfType<AudioManager>();
                    if (_instance == null)
                    {
                        //自动挂载到UIRoot上
                        var UIRoot = GameObject.Find("UIRoot");
                        if (UIRoot != null)
                        {
                            _instance = UIRoot.AddComponent<AudioManager>();
                        }
                        else
                        {
                            Debug.LogError("No AudioManager found");
                        }
                    }
                }

                return _instance;
            }
        }

        /// <summary>
        /// BGM专用播放器，常驻
        /// </summary>
        private AudioSource _bgmSource;

        /// <summary>
        /// 环境音播放器，常驻
        /// </summary>
        private AudioSource _ambientSource;

        /// <summary>
        /// SFX播放器池
        /// </summary>
        private List<AudioSource> _sfxSourcePool = new List<AudioSource>();
        private int _poolSize = 8;
        
        private SettingData _settingData;

        private void Awake()
        {
            if (_instance == null)
            {
                _instance = this;
                Init();
            }
            else
            {
                Destroy(this);
            }
        }

        private void Init()
        {
            DontDestroyOnLoad(this.gameObject);
            _settingData = PlayerDataManager.Instance.GetSettingData();
            
            //创建BGM播放器
            _bgmSource = CreateAudioSource(gameObject,"BGM");
            _bgmSource.loop = true;
            _bgmSource.playOnAwake = false;
            _bgmSource.volume = _settingData.masterVolume;
            
            //环境音播放器
            _ambientSource = CreateAudioSource(gameObject,"Ambient");
            _ambientSource.loop = true;
            _ambientSource.playOnAwake = false;
            _ambientSource.volume = _settingData.AmbientSound;
            
            //创建SFX播放器池
            for (int i = 0; i < _poolSize; i++)
            {
                var src = CreateAudioSource(gameObject, "SFX_" + i);
                src.loop = false;
                src.playOnAwake = false;
                _sfxSourcePool.Add(src);
            }

            EventManager.RegisterEvent(EventType.SettingChanged, (Action)OnSettingChanged);
        }

        private AudioSource CreateAudioSource(GameObject parent,string name)
        {
            var go = new GameObject(name);
            go.transform.SetParent(parent.transform);
            return go.AddComponent<AudioSource>();
        }

        /// <summary>
        /// 播放循环背景音乐
        /// </summary>
        /// <param name="clip"></param>
        public void PlaBGM(AudioClip clip)
        {
            if(_bgmSource == null)
                return;
            _bgmSource.clip = clip;
            _bgmSource.Play();
        }

        public void PlayAmbientSound(AudioClip clip)
        {
            if (clip == null) return;
            _ambientSource.clip = clip;
            _ambientSource.Play();
        }
        
        /// <summary>
        /// 播放一次性音效
        /// </summary>
        /// <param name="clip"></param>
        public void PlaySFX(AudioClip clip)
        {
            if (clip == null)
                return;
            foreach (var src in _sfxSourcePool)
            {
                if (!src.isPlaying)
                {
                    src.clip = clip;
                    src.volume = _settingData.sfxVolume;
                    src.Play();
                    return;
                }
            }
            
            //如果全部都忙直接复用第一个
            _sfxSourcePool[0].clip = clip;
            _sfxSourcePool[0].volume = _settingData.sfxVolume;
            _sfxSourcePool[0].Play();
        }
        
        public void ApplyVolumes()
        {
            _bgmSource.volume = _settingData.masterVolume;
            _ambientSource.volume = _settingData.AmbientSound;
            foreach (var src in _sfxSourcePool)
            {
                if (src.isPlaying)
                    src.volume = _settingData.sfxVolume;
            }
        }
        
        private void OnSettingChanged()
        {
            ApplyVolumes();
        }
        
        private void OnDestroy()
        {
            EventManager.UnregisterEvent(EventType.SettingChanged,(Action)OnSettingChanged);
        }
    }
}
