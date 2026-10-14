using UnityEngine;
using System.Collections.Generic;
using Unity.VisualScripting;
using System;

public class RadioController : MonoBehaviour
{
    [Header("Radio Audio")]
    [Tooltip("The Audio Source used to play the radio's music")]
    [SerializeField] private AudioSource radioAudioSource;

    [Header("Music Playlist")]
    [Tooltip("The songs that the radio can play")]
    [SerializeField] private List<AudioClip> playList = new List<AudioClip>();

    //stores the position of the next song in the playlist.
    private int songIndex = 0;
    public void PlayNextSong()
    {
        //Selects the song from the playlist using the current songIndex.
        radioAudioSource.clip = playList[songIndex];
        radioAudioSource.Play();
        songIndex++;

        if (songIndex >= playList.Count)
        { 
            songIndex = 0;
        }
    }

    public void StopPlayingSong()
    { 
        radioAudioSource.Stop();

        Debug.Log("The radio has stopped!");
    }
}
