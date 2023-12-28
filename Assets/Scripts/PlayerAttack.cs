using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{

   private Animator _anim;

   public Transform attackPoint;
   public float attackRange = 0.5f;

   public LayerMask enemyLayers;


   private void Start()
   {
      _anim = GetComponent<Animator>();
   }

   private void Update()
   {
      if (Input.GetMouseButtonDown(1))
      {
         Attack();
      }
   }

   private void Attack()
   {
      _anim.SetTrigger("Attack");

      Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

      foreach (Collider2D enemy in hitEnemies)
      {
         Debug.Log("A"+enemy.name);
      }
      
   }

   private void OnDrawGizmos()
   {
      if (attackPoint == null)
      {
         return;
      }
      Gizmos.DrawWireSphere(attackPoint.position, attackRange);
   }
}
