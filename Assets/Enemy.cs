using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    float nextAttackTime=0f;
    float coolDownTime = 1f;
    private float walkingSpeed = 4.0f;
    private float runningSpeed = 8.0f;
    public float knockoutTime = 0.5f;
    private float canMoveAgain;
    private Rigidbody2D enemyRb;
    private SpriteRenderer spriteRenderer;
    private float previousXLocation;
    private float lookDirection;
    private Animator animator;
    public float health = 100;
    public float knockBackForce = 20f;
    private float onHitGlowTime = 2f;
    private float stopGlowTime = 0f;
    private bool is_glowing = false;
    private bool hasDied;
    private Color glowColor1;
    private Color glowColor2;
    // Start is called before the first frame update
    void Start()
    {
        enemyRb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (nextAttackTime < Time.time)
        {
            GameObject collided = collision.gameObject;
            if (collided.CompareTag("Player"))
            {
                PlayerController playerScript = collided.GetComponent<PlayerController>();
                playerScript.GetHit(20f);
            }
            nextAttackTime = Time.time + coolDownTime;
        }
    }

    public void GetHit(float damageTaken)
    {
        if (!hasDied)
        {
            DecreaseHealth(damageTaken);
            KnockBack();
            Glow();
            canMoveAgain = Time.time + knockoutTime;
            Debug.Log(health);
        }
    }

    void DecreaseHealth(float damageTaken)
    {
        health -= damageTaken;
        if (health <= 0)
        {
            health = 0;
            Die();
        }
    }
    void KnockBack()
    {
        enemyRb.AddForce(new Vector2(knockBackForce * transform.localScale.x, knockBackForce / 2), ForceMode2D.Impulse);
    }
    void Glow()
    {
        if (!is_glowing&& !hasDied)
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


    void Die()
    {
        hasDied = true;
        gameObject.SetActive(false);
    }
}
