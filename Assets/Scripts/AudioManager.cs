using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance; // reference to the audio manager

    public AudioSource[] music;
    public AudioSource[] sfx;
    public int levelMusicToPlay;
    // private int currentTrack;

    private void Awake()
    {
        instance = this; // set the instance to this audio manager
    }

    // Start is called before the first frame update
    void Start()
    {
        PlayMusic(levelMusicToPlay);
    }

    // Update is called once per frame
    void Update()
    {
        // if (Input.GetKeyDown(KeyCode.M))
        // {
        //     // currentTrack++;
        //     // PlayMusic(currentTrack);
        //     // PlaySFX(5);
        // }
    }

    public void PlayMusic(int musicToPlay)
    {
        for (int i = 0; i < music.Length; i++)
        {
            music[i].Stop();
        }
        music[musicToPlay].Play();
    }

    public void PlaySFX(int sfxToPlay)
    {
        sfx[sfxToPlay].Play();
    }
}
