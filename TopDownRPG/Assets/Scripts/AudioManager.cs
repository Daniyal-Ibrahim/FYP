using System;
using UnityEngine;
namespace Dan
{
    [System.Serializable]
    public class Sound
    {
        public AudioClip clip;

        public string name;

        [Range(0f, 1f)]
        public float volume;
        [Range(0f, 1f)]
        public float pitch;

        public bool loop;

        [HideInInspector]
        public AudioSource source;
    }


    public class AudioManager : MonoBehaviour
    {
        public Sound[] sounds;
        public static AudioManager instance;
        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
            else
            {
                // if (instance != this)
                //{
                Destroy(gameObject);
                return;
                //}
            }

            DontDestroyOnLoad(gameObject);

            foreach (Sound s in sounds)
            {
                s.source = gameObject.AddComponent<AudioSource>();
                s.source.clip = s.clip;

                s.source.volume = s.volume;
                s.source.pitch = s.pitch;
                s.source.loop = s.loop;
            }
        }

        private void Start()
        {
            PlayAudio("Theme");
        }

        public void PlayAudio(string name)
        {
            Sound s = Array.Find(sounds, sound => sound.name == name);
            if (s == null)
            {
                Debug.Log("Sound: " + name + " not found");
                return;
            }
            s.source.Play();
            // copy code where u want to play a sound
            //FindObjectOfType<AudioManager>().PlayAudio("AudioName");
        }
    }
}
