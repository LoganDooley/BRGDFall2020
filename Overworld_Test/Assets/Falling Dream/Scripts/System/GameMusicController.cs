using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FallingDream.System {

    public class GameMusicController : MonoBehaviour
    {
       
        public AudioClip startClip;
        public AudioClip loopClip;

        AudioSource source;

        void Start() {
            source = GetComponent<AudioSource>();
            source.clip = startClip;
            source.Play();
        }

        void Update() {
            if (!source.isPlaying) {
                source.clip = loopClip;
                source.Play();
            }
        }

    }
}