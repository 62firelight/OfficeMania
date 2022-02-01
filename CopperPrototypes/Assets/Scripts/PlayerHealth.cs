using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    int hearts;
    int maxHealth;
    int playerhealth;

    [SerializeField] private Image heartPrefab;
    [SerializeField] private Transform heartContainer;

    public Sprite fullHeart;
    public Sprite halfHeart;
    public Sprite emtpyHeart;

    private List<Image> heartDisplay = new List<Image>();
    // Start is called before the first frame update
    void Awake()
    {
        // Finding the number of hearts to spawn
        hearts = GameObject.Find("Player").GetComponent<PlayerDamage>().playerMaxHealth / 2;
        maxHealth = hearts * 2;

        for (int i = 0; i < hearts; i++) {
            heartDisplay.Add(Instantiate(heartPrefab.gameObject, heartContainer).GetComponent<Image>());
        }

        setHealthBarHealth();

    }

    // Update is called once per frame
    public void setHealthBarHealth()
    {
        playerhealth = GameObject.Find("Player").GetComponent<PlayerDamage>().playerHealth;

        for (int i = 0; i < maxHealth; i++) {

            int remainingHealth = Mathf.Clamp(playerhealth - (i * 2), 0, 2);

            switch (remainingHealth)
            {
                case 0:
                    heartDisplay[i].sprite = emtpyHeart;
                    break;
                case 1:
                    heartDisplay[i].sprite = halfHeart;
                    break;
                case 2:
                    heartDisplay[i].sprite = fullHeart;
                    break;
            }
        }
    }

}
