using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

namespace ZG
{

    public class PlayerAttacker : MonoBehaviour
    {

        AnimatorHandler animatorHandler;
        InputHandler inputHandler;
        public string lastAttack;


        private void Awake()
        {
            animatorHandler = GetComponentInChildren<AnimatorHandler>();
            inputHandler=GetComponent<InputHandler>();

        }

        public void HandleWeaponCombo(WeaponItem weapon)
        {
            if (inputHandler.comboFlag)
            {
                animatorHandler.anim.SetBool("canDoCombo", false);
                if (lastAttack == weapon.oh_Light_ATK_1)
                {
                    animatorHandler.PlayerTargetAnimation(weapon.on_Light_ATK_2, true);
                }
            }
        }



        public void HandleLightATK(WeaponItem weapon)
        {
            animatorHandler.PlayerTargetAnimation(weapon.oh_Light_ATK_1,true);
            lastAttack=weapon.oh_Light_ATK_1; 
        }

        public void HandleHeaveyATK(WeaponItem weapon)
        {
            animatorHandler.PlayerTargetAnimation(weapon.oh_Heavey_ATK,true);
            lastAttack = weapon.oh_Heavey_ATK;
        }
    }
}
