using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SheepCounting
{
    public class musiclooper : MonoBehaviour
    {
        public AudioSource source;
        public AudioClip initialClip;
        public AudioClip loopClip;

        // Start is called before the first frame update
        void Start()
        {
            source = this.gameObject.GetComponent<AudioSource>();
            source.loop = true;
            StartCoroutine(loop());   
        }

        // Update is called once per frame
        void Update()
        {

        }

        IEnumerator loop()
        {
            source.clip = initialClip;
            source.Play();
            yield return new WaitForSeconds(source.clip.length);
            source.clip = loopClip;
            source.Play();
        }
    }
}