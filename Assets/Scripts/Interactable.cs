using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    // Start is called before the first frame update
    private float glowTime = 0.2f;
    private float stopGlowTime = 0f;
    private SpriteRenderer spriteRenderer;
    private GameManager gameManager;
    private Color glowColor1;
    private Color glowColor2;
    bool is_glowing = false;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        glowColor1 = new Color(1f, 1f, 1f);
        glowColor2 = new Color(230f/255f, 230f/255f, 230f/255f);
    }

    // Update is called once per frame
    void Update()
    {
        if (stopGlowTime < Time.time)
            spriteRenderer.color = glowColor1;
    }

    public void Glow()
    {
        if (!is_glowing)
        {
            StartCoroutine("ChangeColors");
        }
        stopGlowTime = Time.time + glowTime;
    }

    IEnumerator ChangeColors()
    {
        is_glowing = true;
        gameManager.updateInstruction("Press E to Search");
        while (stopGlowTime > Time.time)
        {
            yield return new WaitForSeconds(0.3f);
            if (spriteRenderer.color == glowColor1)
                spriteRenderer.color = glowColor2;
            else spriteRenderer.color = glowColor1;
        }
        is_glowing = false;
        gameManager.removeInstruction();
    }
}
