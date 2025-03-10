using UnityEngine;
using Zenject;

namespace Tirlim.Gun
{
    public class GunAudioController : MonoBehaviour, IShootNotifier, IUnloadNotifier, IStartReloadNotifier
    {
        [SerializeField] private AudioSource audioSource;
        [Inject] private AudioGunLibrary _audioGunLibrary;
        
        public void OnShootMessage()
        {
            PlayClip(_audioGunLibrary.Shot);
        }

        public void OnUnloadMessage()
        {
            PlayClip(_audioGunLibrary.Unload);
        }

        public void OnStartReloadMessage()
        {
            PlayClip(_audioGunLibrary.Reload);
        }

        private void PlayClip(AudioClip clip)
        {
            if(clip == null)
                return;
            audioSource.clip = clip;
            audioSource.Play();
        }
    }
}