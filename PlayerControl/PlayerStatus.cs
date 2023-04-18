using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZG
{
    public class PlayerStatus : MonoBehaviour
    {
        public int healthLevel = 10;
        public int maxHealth;
        public int currentHealth;


        public HealthBarControl healthBarControl;

        AnimatorHandler animHandler;

        private void Awake()
        {
            animHandler=GetComponentInChildren<AnimatorHandler>();
        }


        private void Start()
        {
            maxHealth = SetMaxHealthFromHealthLevel();
            currentHealth=maxHealth;
            healthBarControl.SetMaxHealth(maxHealth);
        }
        private int SetMaxHealthFromHealthLevel()
        {
            maxHealth = healthLevel*10;
            return maxHealth;
        }

        public void TakeDamage(int damage)
        {
            currentHealth=currentHealth-damage;
            healthBarControl.SetCurrentHealth(currentHealth);
            animHandler.PlayerTargetAnimation("Damage01", true);

            if(currentHealth<=0)
            {
                currentHealth = 0;
                animHandler.PlayerTargetAnimation("Death01",true);
            }
        }
    }
}
