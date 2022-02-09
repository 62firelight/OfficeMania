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

        // StartCoroutine(FadeIntoTrack(lightsOffMusic));
    }

    public void TriggerLightsOnMusic()
    {
        // musicSource.clip = lightsOnMusic;
        // musicSource.Play();

        StartCoroutine(FadeIntoTrack(lightsOnMusic));
    }

    public void TriggerLightsOffMusicMid()
    {
        // musicSource.clip = lightsOffMusic;
        // musicSource.time = 70;
        // musicSource.Play();

        StartCoroutine(FadeIntoTrack(lightsOffMusic, 70));
    }

    public void TriggerBattleMusic()
    {
        // musicSource.clip = battleMusic;
        // musicSource.Play();

        StartCoroutine(FadeIntoTrack(battleMusic));
    }

    public void TriggerBigBattleMusic()
    {
        // musicSource.clip = bigBattleMusic;
        // musicSource.Play();

        StartCoroutine(FadeIntoTrack(bigBattleMusic));
    }

    IEnumerator FadeIntoTrack(AudioClip clip, int time = 0)
    {
        while (musicSource.volume > 0)
        {
            musicSource.volume -= 2f * Time.deltaTime;
            yield return null;
        }

        if (musicSource.volume <= 0)
        {
            musicSource.clip = clip;
            musicSource.time = time;
            musicSource.Play();
            yield return null;
        }

        while (musicSource.volume < 1)
        {
            musicSource.volume += 2f * Time.deltaTime;
            yield return null;
        }
            
    }
}
