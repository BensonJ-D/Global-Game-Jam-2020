using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomMusicPlayer : MonoBehaviour
{
    public AudioClip[] Songs;
    int songChoice;
    // Start is called before the first frame update
    void Start()
    {
        songChoice = Random.Range(0, Songs.Length);
        gameObject.GetComponent<AudioSource>().clip = Songs[songChoice];
        gameObject.GetComponent<AudioSource>().Play();
    }

}
