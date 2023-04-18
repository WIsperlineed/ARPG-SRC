using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using ZG;

public class PlayerManager : MonoBehaviour
{
    //默认是私有的
    Animator anim;

    [Header("Other Compo")]
    InputHandler inputHandler;
    CameraHandler cameraHandler;
    PlayerLocomotion playerLocomotion;

    
    [Header("palyer flags")]
    [Tooltip("是否在翻滚")]
    public bool isInteracting;
    public bool isSprinting;

    public bool isInAir;
    public bool isGrounded;

    public bool canDoCombo;


   

    private void Awake()
    {
        cameraHandler = CameraHandler.singleton;
    }



    void Start()
    {
        inputHandler = GetComponent<InputHandler>();    
        anim = GetComponentInChildren<Animator>();
        playerLocomotion=GetComponent<PlayerLocomotion>();    
    }


    void Update()
    {
        isInteracting = anim.GetBool("isInteracting");
        canDoCombo = anim.GetBool("canDoCombo");

        float delta = Time.deltaTime;
        inputHandler.TickInput(delta);
        playerLocomotion.HandleMovement(delta);
        playerLocomotion.HandleRollingAndSprinting(delta);
        playerLocomotion.HandleFalling(delta,playerLocomotion.moveDirection);

    }

    /// <summary>
    /// handle camera
    /// </summary>
    private void FixedUpdate()
    {
        float delta = Time.deltaTime;
        if (cameraHandler != null)
        {
            cameraHandler.FollowTarget(delta);
            cameraHandler.HandleCameraRotation(delta, inputHandler.mouseX, inputHandler.mouseY);
        }
    }


    private void LateUpdate()
    {
        inputHandler.rollFlag = false;
        inputHandler.sprintflag = false;
        inputHandler.rt_Input = false;
        inputHandler.rb_Input = false;

        if (isInAir) 
        {
            playerLocomotion.inAirTimer += Time.deltaTime;
        }

    }
}
