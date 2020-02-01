using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public float  damage = 20f;
    PlayerController player;

    private void Start()
    {
        player = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        Debug.Log(collision.gameObject.name);
        if (player.isAttacking)
        {
            GameObject collided = collision.gameObject;
            if (collided.CompareTag("Enemy"))
            {
                Debug.Log("enemy is getting hit");
                Enemy enemy = collided.GetComponent<Enemy>();
                enemy.GetHit(20f);
                player.isAttacking = false;
            }
    }
}
  
  
}
