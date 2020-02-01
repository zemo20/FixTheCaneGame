using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int maxHealth = 100;
    int currentHealth;
    bool hasDied = false;
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
    }

    public void GetHit(int damageTaken)
    {
        if (!hasDied)
        {
        DecreaseHealth(damageTaken);
        //KnockBack();
        //Glow();
        //canMoveAgain = Time.time + knockoutTime;
        Debug.Log(currentHealth);
        }
    }

    void DecreaseHealth(int damageTaken)
    {
        currentHealth -= damageTaken;
        if (currentHealth <= 0)
        {
            currentHealth = 0;
            Die();
        }
    }
    /*
    void KnockBack()
    {
        enemyRb.AddForce(new Vector2(knockBackForce * transform.localScale.x, knockBackForce / 2), ForceMode2D.Impulse);
    }

    void Glow()
    {
        if (!is_glowing && !hasDied)
        {
            StartCoroutine("ChangeColors");
        }
        else
        {
            stopGlowTime = Time.time + onHitGlowTime;
        }


    }

    IEnumerator ChangeColors()
    {
        is_glowing = true;
        stopGlowTime = Time.time + onHitGlowTime;
        while (stopGlowTime > Time.time)
        {
            yield return new WaitForSeconds(0.2f);
            if (spriteRenderer.color == glowColor1)
            {
                spriteRenderer.color = glowColor2;
            }
            else
                spriteRenderer.color = glowColor1;
        }
        is_glowing = false;
        spriteRenderer.color = glowColor1;
    }
    */

    void Die()
    {
        //hasDied = true;
        gameObject.SetActive(false);
        //GetComponent<Collider2D>().enabled = false;
        //this.enabled = false;
    }
}
