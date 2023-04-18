using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace ZG
{
    public class DamageCollider : MonoBehaviour
    {
        Collider damageCollider;
        public int currentWeaponDamage = 25;

        private void Awake()
        {
            damageCollider=GetComponent<Collider>();
            damageCollider.gameObject.SetActive(true);
            damageCollider.isTrigger=true;
            damageCollider.enabled=false;
        }


        public void EnableDamageCollider()
        {
            damageCollider.enabled=true;
        }

        public void DisableDamageCollider()
        {
            damageCollider.enabled=false ;
        }


        private void OnTriggerEnter(Collider other)
        {
            if(other.tag=="Player")
            {
                PlayerStatus playerStatus = other.GetComponent<PlayerStatus>();
                if(playerStatus!=null)
                {
                    playerStatus.TakeDamage(currentWeaponDamage); 
                }
            }
            
            if(other.tag=="Enemy")
            {
               EnemyStatus enemyStatus= other.GetComponent<EnemyStatus>();  

               if(enemyStatus!=null)
                {
                    enemyStatus.TakeDamage(currentWeaponDamage);
                }
            }
        }
    }
}