using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZG
{
    [CreateAssetMenu(menuName ="Items/Weapon Item")]
    public class WeaponItem : Item
    {
        public GameObject modelPrefab;
        public bool isUnarmed;

        [Header("1 handed Atk Anim")]
        public string oh_Light_ATK_1;
        public string on_Light_ATK_2;

        public string oh_Heavey_ATK;

    
    }
}