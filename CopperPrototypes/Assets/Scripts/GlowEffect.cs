using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlowEffect : MonoBehaviour
{

    public Color glowColor = new Color(1, 1, 0.5f, 1);

    public float glowSpeed = 2f;

    public bool isGlowing = true;

    private SpriteRenderer sr;

    private Interactable interactable;

    private Color defaultColor;

    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        defaultColor = sr.color;

        interactable = GetComponent<Interactable>();
    }

    // Update is called once per frame
    void Update()
    {
        if (interactable.pickedUp)
        {
            sr.color = defaultColor;
        }

        if (isGlowing)
        {
            Color newColor = Color.Lerp(sr.color, glowColor, glowSpeed * Time.deltaTime);

            sr.color = new Color(newColor.r, newColor.g, newColor.b, 1);

            if (sr.color.b <= 0.1f)
            {
                isGlowing = false;
            }
        }
        else
        {
            Color newColor = Color.Lerp(sr.color, defaultColor, glowSpeed * Time.deltaTime);

            sr.color = new Color(newColor.r, newColor.g, newColor.b, 1);

            if (sr.color.b >= 0.9f)
            {
                isGlowing = true;
            }
        }
    }
}
