using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZG
{
    public class DamagePlayer : MonoBehaviour
    {
        public int damage = 25;

        private void OnTriggerStay(Collider other)
        {
           
        }

        private void OnTriggerEnter(Collider other)
        {
            PlayerStatus playerStatus = other.GetComponent<PlayerStatus>();
            if (playerStatus != null)
            {
                playerStatus.TakeDamage(damage);
            }
        }
    }

}