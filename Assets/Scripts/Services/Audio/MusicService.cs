using System;
using Cysharp.Threading.Tasks;
using Infrastructure.Utils;
using UnityEngine;

namespace Services
{
    public class MusicService : IMusicService, IDisposable
    {
        private readonly IAssetProvider _assetProvider;
        private readonly ISettingsService _settingsService;
        private AudioSource _audioSource;
        
        MusicService(IAssetProvider assetProvider, ISettingsService settingsService)
        {
            _assetProvider = assetProvider;
            _settingsService = settingsService;
        }

        public async UniTask Initialize()
        {
            var go = await _assetProvider.CreateGameObject(AssetKeys.MusicSource);
            GameObject.DontDestroyOnLoad(go);

            _audioSource = go.GetComponent<AudioSource>();
            
            OnMusicVolumeChanged(_settingsService.MusicVolume);
            _settingsService.OnMusicVolumeChanged += OnMusicVolumeChanged;
        }
        
        private void OnMusicVolumeChanged(float newValue) => _audioSource.volume = newValue;

        public async void PlayMusic(Music music)
        {
            _audioSource.clip = await _assetProvider.Load<AudioClip>(music);
            _audioSource.loop = true;
            _audioSource.Play();
        } 

        public void StopMusic() => _audioSource.Stop();

        public void Dispose()
        {
            _settingsService.OnMusicVolumeChanged -= OnMusicVolumeChanged;
        }
    }
}