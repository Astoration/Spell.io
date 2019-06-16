using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class ActorController : MonoBehaviour
{
    #region PROPERTIES
    private readonly float gravity = -9.8f;
    public bool hasController = false;
    public Camera playerCamera;
    public VariableJoystick MoveInput;
    public VariableJoystick ActionInput;
    public Transform chracterRoot;
    [HideInInspector]
    public CharacterController characterController;
    [HideInInspector]
    public Animator animator;
    public Text nickname;
    public GameObject magicPrefab;

    public float speed = 5;
    [HideInInspector]
    public float currentCoolTime = 0f;
    public float coolTime = 0.3f;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        InitalComponent();
    }

    #region INITAL
    private void InitalComponent() {
        hasController =  this.gameObject.GetPhotonView().IsMine;
        characterController = GetComponent<CharacterController>();
        animator = GetComponentInChildren<Animator>();
        if (!hasController)
        {
            Destroy(playerCamera.gameObject);
        }
        if (hasController)
        {
            nickname.text = PlayerStatusManager.Instance.nickname ?? "Unknown Player";
        }
    }
    #endregion

    // Update is called once per frame
    void Update()
    {
        if (hasController)
        {
            StatusUpdate();
            MoveControl();
            ActionControl();
            UpdateAnimation();
        }
    }

    #region CHARACTOR_UPDATE
    private void StatusUpdate() 
    {
        if (0 < currentCoolTime)
        {
            currentCoolTime -= Time.deltaTime;
        } else
        {
            currentCoolTime = 0;
        }
    }

    private void MoveControl()
    {
        if (MoveInput.Direction.magnitude == 0) return;
        Vector3 input = Vector3.zero;
        input += transform.forward * MoveInput.Vertical;
        input += transform.right * MoveInput.Horizontal;
        input = ApplyGravity(input);
        characterController.Move(input * speed * Time.deltaTime);
        var characterDegree = Mathf.Atan2(MoveInput.Horizontal,MoveInput.Vertical) * Mathf.Rad2Deg;
        chracterRoot.rotation = Quaternion.Euler(new Vector3(0,characterDegree,0));
    }

    public void ActionControl()
    {
        if (0 < currentCoolTime) return;
        var actDirection = ActionInput.Direction;
        if (actDirection.magnitude < 1) return;
        currentCoolTime = coolTime;
        var degree = Mathf.Atan2(ActionInput.Horizontal, ActionInput.Vertical) * Mathf.Rad2Deg;
        var direction = Quaternion.Euler(new Vector3(0, degree, 0));
        animator.SetTrigger("Action");
        chracterRoot.rotation = direction;
        Instantiate(magicPrefab, transform.position + Vector3.up * 0.5f,direction);
    }

    private Vector3 ApplyGravity(Vector3 input)
    {
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
