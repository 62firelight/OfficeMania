using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Dialog : MonoBehaviour
{
    public TextMeshProUGUI textDisplay;
    public string[] sentences;
    public int index;
    public float typingSpeed;
    public float sentenceWaitTime = 0;

    public Transform[] speakers;
    public int speakerIndex;

    private void Start()
    {
        // Position text above speakers
        textDisplay.alignment = TextAlignmentOptions.Midline;
        speakerIndex = 0;
        textDisplay.rectTransform.position = Camera.main.WorldToScreenPoint(speakers[speakerIndex].position + new Vector3(0, 1, 0));

        StartCoroutine(Type());
    }

    private void Update()
    {
        if (sentenceWaitTime > 0) 
        {
            sentenceWaitTime -= Time.deltaTime;

            // If enough time has passed, go to the next sentence
            if (sentenceWaitTime <= 0)
            {
                NextSentence();
            }
        }
    }

    IEnumerator Type()
    {

        foreach (char letter in sentences[index].ToCharArray())
        {
            textDisplay.text += letter;
            yield return new WaitForSeconds(0.02f);

            {

            }
        }

        sentenceWaitTime = 3;
    }

    public void NextSentence()
    {
        if (index < sentences.Length - 1)
        {
            index++;
            textDisplay.text = "";
            StartCoroutine(Type());
        }
        else
        {
            textDisplay.text = "";

        }

        // Increase speaker index
        speakerIndex++;
        if (speakerIndex >= speakers.Length)
        {
            // Cycle to start of speaker array
            speakerIndex = 0;
        }

        // Position text on next speaker
        textDisplay.rectTransform.position = Camera.main.WorldToScreenPoint(speakers[speakerIndex].position  + new Vector3(0, 1, 0));
    }
}