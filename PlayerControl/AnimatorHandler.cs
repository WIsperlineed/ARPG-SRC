using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace ZG
{
    public class AnimatorHandler : MonoBehaviour
    {

        PlayerManager playerManager;

        public Animator anim;
        int vertical;
        int horizontal;
        public bool canRotate;

        InputHandler inputHandler;
        PlayerLocomotion playerLocomotion;


        public void Initailzie()
        {
            anim = GetComponent<Animator>();
            vertical = Animator.StringToHash("vertical");
            horizontal = Animator.StringToHash("horizontal");
            inputHandler = GetComponentInParent<InputHandler>();
            playerLocomotion = GetComponentInParent<PlayerLocomotion>();
            playerManager = GetComponentInParent<PlayerManager>();
        }

        /// <summary>
        /// 处理，运动参数
        /// </summary>
        /// <param name="verticalMovement"></param>
        /// <param name="horizontalMovement"></param>
        /// <param name="_isSprinting"></param>
        public void UpdateAnimatorValues(float verticalMovement, float horizontalMovement, bool _isSprinting)
        {
            #region Vertical
            float v = 0f;

            if (verticalMovement > 0 && verticalMovement < 0.55f)//(0,0.55)
            {
                v = 0.5f;
            }
            else if (verticalMovement > 0.55f)  //(0.55,+00)
            {
                v = 1f;
            }
            else if (verticalMovement < 0 && verticalMovement > -0.55f)  //(-0.55,0)
            {
                v = -0.5f;
            }
            else if (verticalMovement < -0.55f)//(-00,-0.55)
            {
                v = -1f;
            }
            else
                v = 0f;

            #endregion
            #region horizontal
            float h = 0;

            if (horizontalMovement > 0 && horizontalMovement < 0.55f)
            {
                h = 0.5f;
            }
            else if (horizontalMovement > 0.55f)
            {
                h = 1;
            }
            else if (horizontalMovement < 0 && horizontalMovement > -0.55f)
            {
                h = -0.5f;
            }
            else if (horizontalMovement < -0.55f)
            {
                h = -1;
            }
            else
                h = 0;

            #endregion

            if (_isSprinting)
            {
                v = 2;
                h = horizontalMovement;
            }

            anim.SetFloat(vertical, v, 0.1f, Time.deltaTime);
            anim.SetFloat(horizontal, h, 0.1f, Time.deltaTime);
        }

        public void PlayerTargetAnimation(string targetAnim, bool isInteracting)
        {
            anim.applyRootMotion = isInteracting;
            anim.SetBool("isInteracting", isInteracting);
            anim.CrossFade(targetAnim, 0.2f);
        }

        public void CanRotate()
        {
            canRotate = true;
        }

        public void StopRotate()
        {
            canRotate = false;
        }

        /// <summary>
        /// 动画后归位
        /// </summary>  
        private void OnAnimatorMove()
        {
            if (playerManager.isInteracting == false)
                return;

            //动画移动后，将玩家移动到 该游戏对象的中心

            float delta = Time.deltaTime;
            playerLocomotion.rigidbody.drag = 0;
            Vector3 deltaPosition = anim.deltaPosition;
            deltaPosition.y = 0;
            Vector3 velocity = deltaPosition / delta;
            playerLocomotion.rigidbody.velocity = velocity;

        }
        public void FootR()
        {
        }


        public void EnableCombo()
        {
            anim.SetBool("canDoCombo",true);
        }

        public void DisableCombo()
        {
            anim.SetBool("canDoCombo", false);
        }


    }
}
