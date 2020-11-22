using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SongManage : MonoBehaviour
{
    public AudioSource music;
    public float delay;
    // Start is called before the first frame update
    void Start()
    {
        //Invoke("PlaySong", delay);
        
    }

    // Update is called once per frame
    public void PlaySong()
    {
        this.music.Play();
    }
}
