using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCombat : MonoBehaviour
{
    public Transform attackPoint;
    public float attackrange = 0.5f;
    public int attackDamage = 10;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightControl))
            Attack();
    }

    void Attack()
    {
        Collider2D[] hitPlayer = Physics2D.OverlapCircleAll(attackPoint.position, attackrange);
        foreach (Collider2D player in hitPlayer)
            if (player.CompareTag("Player"))
                player.GetComponent<PlayerController>().GetHit(attackDamage);
    }
}
