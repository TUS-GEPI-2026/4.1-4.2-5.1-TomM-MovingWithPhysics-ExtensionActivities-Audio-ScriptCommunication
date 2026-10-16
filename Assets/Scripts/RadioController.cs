using UnityEngine;
using System.Collections.Generic;

public class RadioController : MonoBehaviour
{
    [Header("Radio Audio")]
    [Tooltip("The Audio Source used to play the radio's music")]
    [SerializeField] private AudioSource radioAudioSource;

    [Space(5)]

    [Header("Volume Settings")]
    [Tooltip("How much the radio volume changes each key press")]
    [SerializeField, Range(0.01f, 1f)] private float volumeStep = 0.1f;

    [Space(5)]

    [Header("Music Playlist")]
    [Tooltip("The songs that the radio can play")]
    [SerializeField] private List<AudioClip> playList = new List<AudioClip>();

    //stores the position of the next song in the playlist.
    private int lastSongIndex = -1;
    public void PlayNextSong()
    {
        //Selects a song from the playlist.
        int randomIndex = Random.Range(0, playList.Count);

        //Checks if the same song was selected twice.
        if (playList.Count > 1 && randomIndex == lastSongIndex)
        {
            //Moves to the next song instead to avoid repeating.
            randomIndex++;

            //Goes back to the first song if the index is too high.
            if (randomIndex >= playList.Count)
            {
                randomIndex = 0;
            }

            lastSongIndex = randomIndex;
            //Sets the Audio Source to the randomly selected song.
            radioAudioSource.clip = playList[randomIndex];
            radioAudioSource.Play();
            Debug.Log("Now playing: " + radioAudioSource.clip.name);
        }
    }

    public void StopPlayingSong()
    { 
        radioAudioSource.Stop();
        Debug.Log("The radio has stopped!");
    }

    public void TurnVolumeUp()
    { 
        radioAudioSource.volume += volumeStep;
        radioAudioSource.volume = Mathf.Clamp01(radioAudioSource.volume);
        Debug.Log("Radio volume: " + radioAudioSource.volume);
    }

    public void TurnVolumeDown()
    { 
        radioAudioSource.volume -= volumeStep;
        radioAudioSource.volume = Mathf.Clamp01(radioAudioSource.volume);
        Debug.Log("Radio volume: " + radioAudioSource.volume);
    }
}
