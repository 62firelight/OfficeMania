using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelMaster : MonoBehaviour
{

    public AudioClip lightsOnMusic;

    public AudioClip lightsOffMusic;

    public AudioClip battleMusic;

    public AudioClip bigBattleMusic;

    private AudioSource musicSource;

    // Start is called before the first frame update
    void Start()
    {
        musicSource = GetComponent<AudioSource>();

        if (musicSource == null)
        {
            Debug.Log("Audio source on " + gameObject.name + " was not found!");
        }

        musicSource.clip = lightsOnMusic;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TriggerLightsOffMusic()
    {
        musicSource.clip = lightsOffMusic;
        musicSource.Play();
    }

    public void TriggerLightsOnMusic()
    {
        musicSource.clip = lightsOnMusic;
        musicSource.Play();
    }

    public void TriggerBattleMusic()
    {
        musicSource.clip = battleMusic;
        musicSource.Play();
    }

    public void TriggerBigBattleMusic()
    {
        musicSource.clip = bigBattleMusic;
        musicSource.Play();
    }
}
