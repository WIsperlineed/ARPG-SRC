using System.Collections;
using System.Collections.Generic;
using UnityEngine;



namespace ZG
{
    /// <summary>
    /// ��� ����Item,����Item
    /// </summary>
    public class PlayerInventory : MonoBehaviour
    {
        WeaponSlotMgr WeaponSlotMgr;

        public WeaponItem rightWeapon;
        public WeaponItem leftWeapon;

        private void Awake()
        {
            WeaponSlotMgr = GetComponentInChildren<WeaponSlotMgr>();
        }

        private void Start()
        {
            WeaponSlotMgr.LoadWeaponOnSlot(rightWeapon,false);
            WeaponSlotMgr.LoadWeaponOnSlot(leftWeapon, false);
        }

    }

}
