using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

// attach to UI Text component (with the full text already there)

public class TypeWriter: MonoBehaviour 
{

    public float speed;

    public float fadeSpeed;

    public bool startFading = false;

    public string nextSceneName = "Level1";

    public AudioSource music;

    private Text txt;

	private string story;

	void Awake () 
	{
		txt = GetComponent<Text> ();
		story = txt.text;
		txt.text = "";

		// TODO: add optional delay when to start
		StartCoroutine ("PlayText");
	}

	IEnumerator PlayText()
	{
		foreach (char c in story) 
		{
			txt.text += c;
			yield return new WaitForSeconds (0.0625f);
		}

        startFading = true;
	}

    void Update()
    {
        if (Input.GetButtonDown("Jump"))
        {
            SceneManager.LoadScene(nextSceneName);
        }
        
        if (startFading)
        {
            txt.color -= new Color(0, 0, 0, fadeSpeed * Time.deltaTime);

            if (music != null) music.volume -= fadeSpeed * Time.deltaTime;

            if (txt.color.a < 0)
            {
                SceneManager.LoadScene(nextSceneName);
            }
        }
        transform.Translate(Vector3.up * speed * Time.deltaTime);
    }

}