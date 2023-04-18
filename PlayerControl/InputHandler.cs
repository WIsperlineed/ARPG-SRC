using System.Collections;
using System.Collections.Generic;

using System.Net.Sockets;
using TreeEditor;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

namespace ZG
{
    public class InputHandler : MonoBehaviour
    {
        [Header("输入量")]
        public float horizontal;
        public float vertical;
        public float moveAmount;
        public float mouseX;
        public float mouseY;

      
        public bool roll_Input;
        public bool sprint_Input;


        public bool rollFlag;
        public bool sprintflag;
        public bool comboFlag;


        public bool rb_Input;
        public bool rt_Input;



        [Header("重力模拟和跳跃")]
        public Transform checkPos;
        public bool isGrounded;
        public bool jumpFlag;
        public bool isUp;
        public bool isDown;


        [Header("Other Scripts")]
        PlayerControls inputActions;
        PlayerAttacker playerAttacker;
        PlayerInventory playerInventory;
        PlayerManager playerManager;
        //CameraHandler cameraHandler;

        Vector2 movementInput;
        Vector2 cameraInput;


        private void Awake()
        {
            playerAttacker= GetComponent<PlayerAttacker>();
            playerInventory=GetComponent<PlayerInventory>();
            playerManager= GetComponent<PlayerManager>();   
        }


        /// <summary>
        /// get ready for inputActions get input value into x,y,mx,my
        /// </summary>
        public void OnEnable()
        {
            if(inputActions==null)
            {
                inputActions = new PlayerControls();
                inputActions.PlayerMovement.Movements.performed += inputActions => movementInput = inputActions.ReadValue<Vector2>();
                inputActions.PlayerMovement.Camera.performed += i=> cameraInput=i.ReadValue<Vector2>();
              
            }

            inputActions.Enable();  
        }

    

        public void OnDisable()
        {
            inputActions.Disable();
        }

        /// <summary>
        /// get input data into Inputhandler ,framely check
        /// </summary>
        /// <param name="delta"></param>
        public void TickInput(float delta)
        {
            MoveInput(delta);   
            HandleRollInput(delta);
            HandleJumpInnput(delta);
            HandleAttackInput(delta);
        }

        private void MoveInput(float delta)
        {
            horizontal = movementInput.x;
            vertical=movementInput.y;
            moveAmount = Mathf.Clamp01(Mathf.Abs(horizontal)+Mathf.Abs(vertical));
            mouseX = cameraInput.x;
            mouseY = cameraInput.y;
        }

        private void HandleRollInput(float delta)
        {
            roll_Input = (inputActions.PlayerActions.Roll.phase == UnityEngine.InputSystem.InputActionPhase.Performed);
            sprint_Input = (inputActions.PlayerActions.Sprint.phase == UnityEngine.InputSystem.InputActionPhase.Performed);

            //冗余逻辑


            if (sprint_Input)
                sprintflag = true;
            else
                sprintflag = false;

            if (roll_Input)
                rollFlag = true;
            else
                rollFlag = false;

        }

        private void HandleJumpInnput(float delta)
        {
            //默认 在地面上，只有检测到
            
            //if (!isGrounded)
            //{
            //    isGrounded = Physics.Raycast(checkPos.transform.position, Vector3.down, 0.5f);
            //}
            //else
            //{
            //    checkPos = transform.Find("checkGoundPos");
               
            //    jumpFlag = (inputActions.PlayerActions.Jump.phase == UnityEngine.InputSystem.InputActionPhase.Performed);
            //}

            ////isUp=

        }


        private void HandleAttackInput(float delta)
        {
            inputActions.PlayerActions.RB.performed += i => rb_Input = true;
            inputActions.PlayerActions.RT.performed += i => rt_Input = true;

            if(rb_Input)
            { 
                
                if (playerManager.canDoCombo)
                {
                    comboFlag = true;
                    playerAttacker.HandleWeaponCombo(playerInventory.rightWeapon);
                    comboFlag = false;
                }
                else
                {
                    if(playerManager.canDoCombo)
                        return;
                    if(playerManager.isInteracting)
                        return;
                    playerAttacker.HandleLightATK(playerInventory.rightWeapon);
                }

            }

            if (rt_Input)
            {
                playerAttacker.HandleHeaveyATK(playerInventory.rightWeapon);
            }
        }   
    }
}
