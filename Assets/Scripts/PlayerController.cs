using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    private float runningSpeed = 4.0f;
    private Rigidbody2D playerRb;
    private float previousXLocation;
    private float lookDirection;
    private Animator animator;
    public float health = 100;
    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        
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
            transform.Translate(Vector2.right * runningSpeed * horizontalInput * Time.deltaTime);
            animator.SetFloat("walkSpeed", runningSpeed);
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
}
