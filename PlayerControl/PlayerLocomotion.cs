using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZG
{

    public class PlayerLocomotion : MonoBehaviour
    {
        PlayerManager playerManager;
        Transform cameraObject;
        InputHandler inputHandler;
        public Vector3 moveDirection;

        [HideInInspector]
        public Transform myTransform;
        [HideInInspector]
        public AnimatorHandler animatorHandler;

        public new Rigidbody rigidbody;

        public GameObject normalCamera;


        [Header("Movement Stats")]
        [SerializeField]
        float movementSpeed = 5f;
        [SerializeField]
        float sprintSpeed = 7f;
        [SerializeField]
        float rotationSpeed = 10f;
        [SerializeField]
        float fallingSpeed = 100f;

        [Header("ground & air detection")]
        [SerializeField]
        float groundDetectionRayStartPoint = 0.5f;
        [SerializeField]
        float minimumDistanceNeededToBeginFall = 1f;
        [SerializeField]
        float groundDirectionRayDistance = 0.3f;
        LayerMask ignoreForGroundCheck;
        public float inAirTimer;





        void Start()
        {
            rigidbody = GetComponent<Rigidbody>();
            inputHandler = GetComponent<InputHandler>();
            animatorHandler = GetComponentInChildren<AnimatorHandler>();
            playerManager=GetComponent<PlayerManager>();
            cameraObject = Camera.main.transform;
            myTransform = transform;
            animatorHandler.Initailzie();
            playerManager.isGrounded= true;
            ignoreForGroundCheck = ~(1<<8|1<<11);
        }

        #region Movement
        Vector3 normalVector;
        Vector3 targetPosition;

       
        private void HandleRotation(float delta)
        {
            Vector3 targetDir = Vector3.zero;
            float moveOverride = inputHandler.moveAmount;

            targetDir = cameraObject.forward * inputHandler.vertical;
            targetDir += cameraObject.right * inputHandler.horizontal;

            targetDir.Normalize();
            targetDir.y = 0;

            if (targetDir == Vector3.zero)
                targetDir = myTransform.forward;

            Quaternion tr = Quaternion.LookRotation(targetDir);
            Quaternion targetRotation = Quaternion.Slerp(myTransform.rotation, tr, rotationSpeed * delta);

            myTransform.rotation = targetRotation;

        }

        public void HandleMovement(float delta)
        {
            if (inputHandler.rollFlag)
                return;

            if(playerManager.isInteracting) 
                return;

            moveDirection = cameraObject.forward * inputHandler.vertical;
            moveDirection += cameraObject.right * inputHandler.horizontal;
            moveDirection.Normalize();
            moveDirection.y = 0;

            //Sprint加速处理
            float speed = movementSpeed;
            if(inputHandler.sprintflag&&moveDirection!=Vector3.zero)
            {
                speed = sprintSpeed;
                playerManager.isSprinting = true;
            }else
                playerManager.isSprinting = false;

            moveDirection *= speed;
            Vector3 projectedVelocity = Vector3.ProjectOnPlane(moveDirection, normalVector);
            rigidbody.velocity = moveDirection;
           

            animatorHandler.UpdateAnimatorValues(inputHandler.moveAmount, 0, playerManager.isSprinting);

            if (animatorHandler.canRotate)
            {
                HandleRotation(delta);
            }
        }

        public void HandleRollingAndSprinting(float delta)
        {
            if (animatorHandler.anim.GetBool("isInteracting"))
                return;
            //处理翻滚
            if(inputHandler.rollFlag)
            {
                animatorHandler.PlayerTargetAnimation("RollForward", true);
            }

        }



        //TO-DO:教程逻辑复杂，效果不好有BUG，不是重力下坠
        public void HandleFalling(float delta, Vector3 moveDirection)
        {
            playerManager.isGrounded = false;
            RaycastHit hit;

            Vector3 origin = myTransform.position;
            origin.y += groundDetectionRayStartPoint;

            if (Physics.Raycast(origin, myTransform.forward, out hit, 0.4f))
            {
                moveDirection = Vector3.zero;
            }

            if (playerManager.isInAir)
            {
                rigidbody.AddForce(-Vector3.up * fallingSpeed);
                rigidbody.AddForce(moveDirection * fallingSpeed / 5f);
            }

            //Vector3 dir = moveDirection;
            
            //dir.Normalize();
            origin = origin +(-myTransform.forward) * groundDirectionRayDistance;

            targetPosition = myTransform.position;

            Debug.DrawRay(origin, -Vector3.up * minimumDistanceNeededToBeginFall, Color.red, 2, true);
          
            if (Physics.Raycast(origin, -Vector3.up, out hit, minimumDistanceNeededToBeginFall, ignoreForGroundCheck))
            {
                normalVector = hit.normal;
                Vector3 tp = hit.point;
                playerManager.isGrounded = true;
                targetPosition.y = tp.y;
                if (playerManager.isInAir)
                {
                    if (inAirTimer > 0.5f)
                    {
                        Debug.Log("You were in the air for" + inAirTimer);
                        animatorHandler.PlayerTargetAnimation("Land", true);
                    }
                    else
                    {
                        animatorHandler.PlayerTargetAnimation("Empty", false);
                        inAirTimer = 0;
                    }
                    playerManager.isInAir = false;
                }
            }
            else
            {
                if (playerManager.isGrounded)
                {
                    playerManager.isGrounded = false;
                }

                if (playerManager.isInAir == false)
                {
                    if (playerManager.isInteracting == false)
                    {
                        animatorHandler.PlayerTargetAnimation("Falling", true);
                    }
                    Vector3 vel = rigidbody.velocity;
                    vel.Normalize();
                    rigidbody.velocity = vel * (fallingSpeed / 2);
                    playerManager.isInAir = true;
                }
            }

            if (playerManager.isGrounded)
            {
                if (playerManager.isInteracting || inputHandler.moveAmount > 0)
                {
                    myTransform.position = Vector3.Lerp(myTransform.position, targetPosition, Time.deltaTime);
                }
                else
                {
                    myTransform.position = targetPosition;
                }
            }
        }
        #endregion


    }
}