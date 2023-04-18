using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZG
{
    /// <summary>
    /// 
    /// </summary>
    public class WeaponSlotMgr : MonoBehaviour
    {
        WeaponHolderSlot leftHandSlot;
        WeaponHolderSlot rightHandSlot;

        DamageCollider leftHandDamageCollider;
        DamageCollider rightHandDamageCollider;

        private void Awake()
        {
            WeaponHolderSlot[] slots = GetComponentsInChildren<WeaponHolderSlot>();
            foreach(WeaponHolderSlot weaponSlot in slots)
            {
                if(weaponSlot.isLeftHandSlot)
                {
                    leftHandSlot = weaponSlot;
                }
                else if(weaponSlot.isRightHandSlot)
                {
                    rightHandSlot = weaponSlot;
                }
            }
        }

        /// <summary>
        /// ◊Û ÷”≈œ»≈–∂œ
        /// </summary>
        /// <param name="weaponItem"></param>
        /// <param name="isLeft"></param>
        public void LoadWeaponOnSlot(WeaponItem weaponItem,bool isLeft)
        {
            if(isLeft)
            {
                leftHandSlot.LoadWeaponModel(weaponItem);
                LoadLeftWeaponCollider();
            }
            else
            {
                rightHandSlot.LoadWeaponModel(weaponItem);
                LoadRightWeaponCollider();
            }
        }

        private void LoadLeftWeaponCollider()
        {
            leftHandDamageCollider=leftHandSlot.currentWeaponModel.GetComponentInChildren<DamageCollider>();
        }

        private void LoadRightWeaponCollider()
        {
            rightHandDamageCollider = rightHandSlot.currentWeaponModel.GetComponentInChildren<DamageCollider>();
        }

        #region Handle Weapon damage collider

        public void OpenRightDamageCollider()
        {
            rightHandDamageCollider.EnableDamageCollider();
           
        }

        public void OpenLeftDamageCollider()
        {
            leftHandDamageCollider.EnableDamageCollider();
        }

        public void CloseRightDamageCollider()
        {
            rightHandDamageCollider.DisableDamageCollider();
        }

        public void CloseLeftDamageCollider()
        {
            leftHandDamageCollider.DisableDamageCollider();
        }
        #endregion

    }
}
