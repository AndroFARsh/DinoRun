using System;
using System.Collections.Generic;
using CodeBase.Utils;
using Cysharp.Threading.Tasks;
using Infrastructure;
using Infrastructure.Utils;
using UnityEngine;

namespace Services
{
    public class EffectService : IEffectService, IDisposable
    {
        private readonly IAssetProvider _assetProvider;
        private readonly ISettingsService _settingsService;
        private readonly Dictionary<Channel, AudioSource> _audioSources = new ();
        
        public EffectService(IAssetProvider assetProvider,
            ISettingsService settingsService)
        {
            _assetProvider = assetProvider;
            _settingsService = settingsService;
        }

        public async UniTask Initialize()
        {
            var go = await _assetProvider.CreateGameObject(AssetKeys.EffectSource);
            GameObject.DontDestroyOnLoad(go);

            _audioSources.Add(Channel.Primary, go.GetComponentInChildrenByName<AudioSource>(Channel.Primary.GetName()));
            _audioSources.Add(Channel.Secondary, go.GetComponentInChildrenByName<AudioSource>(Channel.Secondary.GetName()));

            OnEffectVolumeChanged(_settingsService.EffectVolume);
            _settingsService.OnEffectVolumeChanged += OnEffectVolumeChanged;
        }

        private void OnEffectVolumeChanged(float newValue) =>
            _audioSources.Values.ForEach(source => source.volume = newValue);

        public async void PlayEffect(Effect effect, Channel channel = Channel.Primary)
        {
            if (_audioSources.TryGetValue(channel, out var audioSource))
            {
                audioSource.clip = await _assetProvider.Load<AudioClip>(effect);
                audioSource.loop = effect.Loop;
                audioSource.Play();
            }
        }

        public void StopEffect(Channel channel = Channel.Primary)
        {
            if (_audioSources.TryGetValue(channel, out var audioSource))
            {
                audioSource.Stop();
            }
        }

        public void Dispose()
        {
            _settingsService.OnEffectVolumeChanged -= OnEffectVolumeChanged;
        }
    }
}