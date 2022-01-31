using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class DynamicMusic : MonoBehaviour
{

    public AudioClip[] musicPhases;

    public AudioSource audioSource;

    public float maxVolume = 0.25f;

    public float fadeOutSpeed = 0.6f;

    public float fadeInSpeed = 1f;

    private int index;

    private bool fadeOut = false;
    
    private bool fadeIn = false;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        index = 0;

        // Initialize audio source
        if (musicPhases.Length > 0)
        {
            audioSource.clip = musicPhases[0];
            audioSource.volume = maxVolume;
            audioSource.loop = true;

            audioSource.Play();
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Fade out music
        if (fadeOut)
        {
            audioSource.volume -= fadeOutSpeed * Time.deltaTime;

            if (audioSource.volume <= 0)
            {
                // Switch to next music track
                audioSource.clip = musicPhases[++index];
                audioSource.Play();

                // Fade in new music track
                fadeOut = false;
                fadeIn = true;
            }
        }
        else if (fadeIn)
        {
            audioSource.volume += fadeInSpeed * Time.deltaTime;

            // Make sure volume doesn't exceed max volume
            if (audioSource.volume >= maxVolume)
            {
                audioSource.volume = maxVolume;
                fadeIn = false;
            }
        }

        // If N is pressed, transition to the next music track
        if (Input.GetKeyDown(KeyCode.N))
        {
            if (index < musicPhases.Length - 1)
            {
                TriggerFadeOut();

                if (index + 1 == musicPhases.Length - 1)
                {
                    audioSource.loop = false;
                }
            }
            else
            {
                audioSource.clip = null;
            }
        }
    }

    public void TriggerFadeOut()
    {
        // fade out music track
        fadeOut = true;
    }

    void OnGUI()
    {
        GUI.skin.label.fontSize = 72;

        TextAnchor originalAlignment = GUI.skin.label.alignment;
        GUI.skin.label.alignment = TextAnchor.LowerCenter;
        GUI.Label(new Rect(0, 0, Camera.main.pixelWidth, Camera.main.pixelHeight), "Press N to change to next song");
        GUI.skin.label.alignment = originalAlignment;
    }
}
