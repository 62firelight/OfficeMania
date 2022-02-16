using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHealth : MonoBehaviour
{
    float baseHealth = 0;
    float stagehealth;
    float bossMultipler;
    float bossHealth;
    bool bossStatus;

    private Image heartPrefab;
    private Transform heartContainer;

    public Sprite fullHeartGreen;
    public Sprite halfHeartGreen;

    public Sprite fullHeartYellow;
    public Sprite halfHeartYellow;
    

    public Sprite fullHeartRed;
    public Sprite halfHeartRed;

    public Sprite emptyHeart;

    public Image[] heartDisplay;
    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        bossStatus = GameObject.Find("Boss").GetComponent<Enemy>().isBoss;

        if(bossStatus == true && baseHealth == 0)
        {
            baseHealth = GameObject.Find("Boss").GetComponent<Enemy>().health;
        }


        stagehealth = GameObject.Find("Boss").GetComponent<Enemy>().health;
        bossMultipler = GameObject.Find("Boss").GetComponent<Enemy>().bossHealth;
        if(bossMultipler > 1) {
            bossHealth = baseHealth * (bossMultipler-1) + stagehealth;
            Debug.Log(baseHealth);
        }else {
            bossHealth = stagehealth * bossMultipler;
        }



        for(int i = 0; i < heartDisplay.Length; i++) {

            if(i < bossHealth/2f) {
                if(i + 0.5 == bossHealth/2f) {
                    if (bossMultipler == 3)
                    {
                        heartDisplay[i].sprite = halfHeartGreen;
                    }
                    else if (bossMultipler == 2)
                    {
                        heartDisplay[i].sprite = halfHeartYellow;
                    }
                    else {
                        heartDisplay[i].sprite = halfHeartRed;
                    }
                }
                else {
                    if (bossMultipler == 3)
                    {
                        heartDisplay[i].sprite = fullHeartGreen;
                    }
                    else if (bossMultipler == 2)
                    {
                        heartDisplay[i].sprite = fullHeartYellow;
                    }
                    else
                    {
                        heartDisplay[i].sprite = fullHeartRed;
                    }
                }
            }
            else {
                heartDisplay[i].sprite = emptyHeart;
            }
        }
    }

}
