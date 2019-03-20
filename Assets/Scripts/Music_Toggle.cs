using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Music_Toggle : MonoBehaviour
{
    private bool isPlaying = true;
    public GameObject MUSIC_BUTTON_TEXT;

    public void Toggle()
    {
        isPlaying = !isPlaying;
        if (isPlaying)
        {
            GetComponent<AudioSource>().Play();
            MUSIC_BUTTON_TEXT.GetComponent<Text>().text = "Music : On";
        }
        else
        {
            GetComponent<AudioSource>().Stop();
            MUSIC_BUTTON_TEXT.GetComponent<Text>().text = "Music : Off";
        }
    }
}
