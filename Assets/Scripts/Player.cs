using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.MagicLeap;

public class Player : MonoBehaviour
{
    CharacterController controller;
    Animator anim;
    Vector3 moveDirection = Vector3.zero;

    public float gravity = 20.0f;
    public float jumpForce = 10.0f;
    public float speed = 5.0f;
    public float turnSpeed = 5.0f;


    public GameObject runFaceVar;
    public GameObject jumpFaceVar;
    public GameObject idleFaceVar;

    private MLInputController _controller;

    void Awake()
    {
        _controller = MLInput.GetController(MLInput.Hand.Left);
    }

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        anim = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(controller.isGrounded && _controller.Touch1PosAndForce.z > 0.0f)
        {
            anim.SetInteger("AnimPar", 1);
            // TODO figure out how to replace vertical and horizontal with ML input stuff
            moveDirection = transform.forward * _controller.Touch1PosAndForce.y * speed;
            float turn = _controller.Touch1PosAndForce.x;
            transform.Rotate(0, turn * turnSpeed * Time.deltaTime, 0);

            runFaceVar.SetActive(true);
            idleFaceVar.SetActive(false);
            jumpFaceVar.SetActive(false);

        } else if (controller.isGrounded)
        {
            anim.SetInteger("AnimPar", 0);
            moveDirection = transform.forward * _controller.Touch1PosAndForce.y * 0;
            float turn = _controller.Touch1PosAndForce.x;
            transform.Rotate(0, turn * turnSpeed * Time.deltaTime, 0);

            runFaceVar.SetActive(false);
            idleFaceVar.SetActive(true);
            jumpFaceVar.SetActive(false);
        }

        if(controller.isGrounded && _controller.TriggerValue > 0.2f)
        {
            anim.SetInteger("AnimPar", 2);
            moveDirection.y = jumpForce;

            runFaceVar.SetActive(false);
            idleFaceVar.SetActive(false);
            jumpFaceVar.SetActive(true);
        }

        controller.Move(moveDirection * Time.deltaTime);
        moveDirection.y -= gravity * Time.deltaTime; 
        
    }
    
}
