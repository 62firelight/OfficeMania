using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WalkAway : MonoBehaviour
{
    public RoomMaster tutorialRoom;

    public Animator transition;

    public float transitionTime = 1f;

    public AudioClip levelFinishSound;

    private DialogueManager dialogueManager;

    private bool canWalkAway = false;

    void Start()
    {
        dialogueManager = tutorialRoom.dialogueManager.GetComponent<DialogueManager>();
    }

    void Update()
    {
        if (tutorialRoom.GetRoomClear() == true)
        {
            canWalkAway = false;

            GetComponent<SpriteRenderer>().enabled = true;
            // GetComponent<Collider2D>().isTrigger = false;
        }
        else if (dialogueManager.index >= 3)
        {
            GetComponent<SpriteRenderer>().enabled = false;
            // GetComponent<Collider2D>().isTrigger = true;

            canWalkAway = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        if (collision.gameObject.tag == "Player" && canWalkAway == true)
        {
            if (levelFinishSound != null) AudioSource.PlayClipAtPoint(levelFinishSound, transform.position);

            // SceneManager.LoadScene("WalkAway");

            // AIMaster.currentLevel++;

            int walkAwayLevel = 9;
            // //Debug.Log(currentLevel);
            StartCoroutine(LoadLevel(walkAwayLevel));

            // // Disable player movement
            collision.gameObject.GetComponent<PlayerMovement>().enabled = false;
        }
    }

    IEnumerator LoadLevel(int levelIndex) {

        transition.SetTrigger("Start");

        // yield return new WaitForSeconds(transitionTime);

        // Wait for fade out to complete
        while (transition.gameObject.GetComponentInChildren<CanvasGroup>().alpha < 1)
        {
            // Debug.Log(transition.gameObject.GetComponentInChildren<CanvasGroup>().alpha);
            yield return null;
        }

        SceneManager.LoadScene(levelIndex);
    }
}
