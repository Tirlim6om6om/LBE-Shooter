using UnityEngine;

namespace Tirlim.Gun
{
    [CreateAssetMenu(fileName = "AudioGunLibrary", menuName = "ScriptableObjects/AudioGunLibrary")]
    public class AudioGunLibrary : ScriptableObject
    {
        public AudioClip Shot { get { return shot; } }
        public AudioClip Reload { get { return reload; } }
        public AudioClip Unload { get { return unload; } }
        
        [SerializeField] private AudioClip shot;
        [SerializeField] private AudioClip reload;
        [SerializeField] private AudioClip unload;
    }
}