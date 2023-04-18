using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace SA
{
    public class Helper : MonoBehaviour
    {
        [Range(0,1)]
        public float vertical;

        public string[] oh_attacks;
        public string[] th_attacks;
        public bool playAnim;
        
        public bool twohanded;
        public bool enableRM;


        Animator anim;



        void Start()
        {
            anim = GetComponent<Animator>();
        }

        
        void Update()
        {

            //enableRM = !anim.GetBool("canMove");
            //anim.applyRootMotion = enableRM;
            //if(enableRM)
            //    return;

            anim.SetBool("two_handed", twohanded);
            if(playAnim)
            {
                string targetAnim;

                if(!twohanded)
                {
                    int r = Random.Range(0, oh_attacks.Length );
                    targetAnim = oh_attacks[r];
                }else
                {
                    int r = Random.Range(0, th_attacks.Length  );
                    targetAnim = th_attacks[r];
                }
                vertical = 0;

                //切换状态到targetAnim，产生渐变效果
                
                anim.CrossFade(targetAnim, 0.2f);
                //anim.SetBool("canMove", false);
                //enableRM = true;
                playAnim = false;
            }

            anim.SetFloat("vertical", vertical);
        }
    }
}