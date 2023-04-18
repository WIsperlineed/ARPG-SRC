using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;

namespace ZG
{
    /// <summary>
    /// 挂载到 人物的左右手weapon实例化，赋值以currentWeaponModel，装载WeaponItem
    /// </summary>
    public class WeaponHolderSlot : MonoBehaviour
    {
        public Transform parentOverride;
        public bool isLeftHandSlot;
        public bool isRightHandSlot;

        public GameObject currentWeaponModel;


        public void UnloadWeaponAndDestory()
        {
            if(currentWeaponModel!=null)
            {
                Destroy(currentWeaponModel);
            }
        }


        public void UnloadWeapon()
        {
            if(currentWeaponModel!=null)
            {
                currentWeaponModel.SetActive(false);
            }
        }


        public  void LoadWeaponModel(WeaponItem weaponItem)
        {
            UnloadWeaponAndDestory();

            if (weaponItem==null)
            {
                //unload weapon
                UnloadWeapon();
                return;
            }

            //GameObject model = Instantiate(weaponItem.modelPrefab)as GameObject;

            //if(model!=null)
            //{
            //    if(parentOverride!=null)
            //    {
            //        //model.transform.parent = parentOverride;
            //    }
            //    else
            //    {
            //        //model.transform.parent = transform;
            //    }
            //    model.transform.localPosition = Vector3.zero;
            //    model.transform.localRotation = Quaternion.identity;
            //    model.transform.localScale = Vector3.one;

            GameObject model = null;
            if(parentOverride != null)
            {
                model = Instantiate(weaponItem.modelPrefab, parentOverride)as GameObject;
            }
            else
            {
                model = Instantiate(weaponItem.modelPrefab, transform)as GameObject;
            }
            model.transform.localPosition = Vector3.zero;
            model.transform.localRotation = Quaternion.identity;
            model.transform.localScale = Vector3.one;


            currentWeaponModel = model;
         
        }
    }
}