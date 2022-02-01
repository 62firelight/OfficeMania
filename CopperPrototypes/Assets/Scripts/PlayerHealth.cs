using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    int hearts;
    int maxHealth;
    float playerhealth;

    private Image heartPrefab;
    private Transform heartContainer;

    public Sprite fullHeart;
    public Sprite halfHeart;
    public Sprite emptyHeart;

    public Image[] heartDisplay;
    // Start is called before the first frame update
    void Awake()
    {
        // Finding the number of hearts to spawn
        hearts = GameObject.Find("Player").GetComponent<PlayerDamage>().playerMaxHealth / 2;
        maxHealth = hearts * 2;


    }

    // Update is called once per frame
    void Update()
    {
        playerhealth = GameObject.Find("Player").GetComponent<PlayerDamage>().playerHealth;

        for(int i = 0; i < heartDisplay.Length; i++) {

            if(i < playerhealth/2) {
                if(i + 0.5 == playerhealth/2) {
                    heartDisplay[i].sprite = halfHeart;
                }
                else {
                    heartDisplay[i].sprite = fullHeart;
                }
            }
            else {
                heartDisplay[i].sprite = emptyHeart;
            }
        }
    }

}
