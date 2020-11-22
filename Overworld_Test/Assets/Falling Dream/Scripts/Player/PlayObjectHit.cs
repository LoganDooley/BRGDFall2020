using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FallingDream.Player
{
    public class PlayObjectHit : MonoBehaviour
    {

        public AudioSource source;
        public AudioClip nextClip;

        void Update() {
            if (!source.isPlaying && nextClip != null) {
                source.clip = nextClip;
                source.Play();
                nextClip = null;
            }
        }

        public void Play(AudioClip clip) {
            nextClip = clip;
        }

    }
}