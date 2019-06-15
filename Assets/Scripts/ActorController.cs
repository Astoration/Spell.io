using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActorController : MonoBehaviour
{
    #region PROPERTIES
    private readonly float gravity = -9.8f;
    public bool hasController = true;
    public Camera playerCamera;
    public VariableJoystick MoveInput;
    public VariableJoystick ActionInput;
    public Transform chracterRoot;
    [HideInInspector]
    public CharacterController characterController;
    [HideInInspector]
    public Animator animator;

    public float speed = 5;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        InitalComponent();
    }

    #region INITAL
    private void InitalComponent() {
        characterController = GetComponent<CharacterController>();
        animator = GetComponentInChildren<Animator>();
        if (!hasController)
        {
            Destroy(playerCamera.gameObject);
        }
    }
    #endregion

    // Update is called once per frame
    void Update()
    {
        MoveControl();
        UpdateAnimation();
    }

    #region CHARACTOR_UPDATE
    private void MoveControl() {
        Debug.Log("Move:");
        Debug.Log(MoveInput.Direction);
        Vector3 input = Vector3.zero;
        input += transform.forward * MoveInput.Vertical;
        input += transform.right * MoveInput.Horizontal;
        input = ApplyGravity(input);
        characterController.Move(input * speed * Time.deltaTime);
        var characterDegree = Mathf.Atan2(MoveInput.Horizontal,MoveInput.Vertical) * Mathf.Rad2Deg;
        chracterRoot.rotation = Quaternion.Euler(new Vector3(0,characterDegree,0));
    }

    private Vector3 ApplyGravity(Vector3 input) {
        var source = input;
        if (!characterController.isGrounded)
        {
            source += new Vector3(0, gravity, 0);
        }
        return source;
    }

    private void UpdateAnimation() 
    {
        animator.SetFloat("Horizontal", Math.Abs(MoveInput.Horizontal));
        animator.SetFloat("Vertical", Math.Abs(MoveInput.Vertical));
    }
    #endregion
}
