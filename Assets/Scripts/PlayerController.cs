using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour,IHittable
{
    private float walkingSpeed = 4.0f;
    private float runningSpeed = 8.0f;
    private Rigidbody2D playerRb;
    private SpriteRenderer spriteRenderer;
    private float previousXLocation;
    private float lookDirection;
    private Animator animator;
    public float health = 100;
    public float knockBackForce = 20f;
    private float onHitGlowTime = 2f;
    private float stopGlowTime;
    private bool is_glowing;
    private Color glowColor1;
    private Color glowColor2;
    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        glowColor1 = new Color(1f, 1f, 1f);
        glowColor2 = new Color(255f / 255f, 180f / 255f, 180f / 255f);

    }


    // Update is called once per frame
    void Update()
    {
        checkDirection();
        moveCharacter();
        checkInteractables();
    }


    void checkInteractables()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, lookDirection *Vector2.up,200f);
        if (hit.collider.gameObject.CompareTag("Interactable"))
        {
            if (hit.collider.gameObject.GetComponent<Interactable>() != null)
            {
                Interactable SearchButton = hit.collider.gameObject.GetComponent<Interactable>();
                SearchButton.Glow();
            }
        }
    }

    void moveCharacter()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        if (horizontalInput != 0)
        {
            float speed;
            if (Input.GetKey(KeyCode.LeftShift))
                speed = runningSpeed;
            else speed = walkingSpeed;
            Debug.Log(speed);
            transform.Translate(Vector2.right * speed * horizontalInput * Time.deltaTime);
            animator.SetFloat("walkSpeed", speed);
        }
        else 
            animator.SetFloat("walkSpeed", 0);
    }
    void checkDirection()
    {
        if (previousXLocation > transform.position.x)
            lookDirection = -1;
        else if (previousXLocation < transform.position.x)
            lookDirection = 1;
 
        transform.localScale = new Vector3(lookDirection, 1, 1);
        previousXLocation = transform.position.x;
    }
    private void OnMouseDown()
    {
        GetHit(20f);
    }

    public void GetHit(float damageTaken)
    {
        DecreaseHealth(damageTaken);
        KnockBack();
        Glow();
    }

    void DecreaseHealth(float damageTaken)
    {
        health -= damageTaken;
        if (health <= 0)
        {
            health = 0;
            Die();
        }
        Debug.Log(health);
    }
    void KnockBack()
    {
        playerRb.AddForce(new Vector2(knockBackForce * -lookDirection,knockBackForce),ForceMode2D.Impulse);
    }
    void Glow()
    {
        if (!is_glowing)
        {
            StartCoroutine("ChangeColors");
        }
        stopGlowTime = Time.time + onHitGlowTime;
    }

    IEnumerator ChangeColors()
    {
        is_glowing = true;
        while (stopGlowTime > Time.time)
        {
            yield return new WaitForSeconds(0.3f);
            if (spriteRenderer.color == glowColor1)
                spriteRenderer.color = glowColor2;
            else spriteRenderer.color = glowColor1;
        }
        is_glowing = false;
        spriteRenderer.color = glowColor1;
    }


    void Die()
    {
    
    }
}
