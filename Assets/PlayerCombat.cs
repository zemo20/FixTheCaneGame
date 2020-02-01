using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    public Transform attackPoint;
    public float attackrange = 0.5f;
    public int attackDamage = 40;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.LeftControl))
            Attack();
    }

    void Attack(){
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackrange);
        foreach (Collider2D enemy in hitEnemies)
            if(enemy.CompareTag("Enemy"))
                enemy.GetComponent<Enemy>().GetHit(attackDamage);
    }
}
