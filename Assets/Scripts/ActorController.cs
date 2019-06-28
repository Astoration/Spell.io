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
    public Transform shotPoint;
    [HideInInspector]
    public CharacterController characterController;
    [HideInInspector]
    public Animator animator;
    public Text nickname;
    public GameObject magicPrefab;
    public HealthContainer healthManager;
    public bool isEditor = false;
    public float speed = 5;
    public float maxHealth = 5f;
    private float health = 5f;
    [HideInInspector]
    public float currentCoolTime = 0f;
    public float coolTime = 0.3f;

    public float Health {
        get {
            return health;
        }

        set {
            health = value;
            if (maxHealth < value)
            {
                health = maxHealth;
            }
            if (health <= 0)
            {
                health = 0;
                OnPlayerDead();
            }
            healthManager.SetHealth(health);
        }
    }
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
            MoveInput = InputController.Instance.moveInput;
            ActionInput = InputController.Instance.actionInput;
        }
        nickname.text = gameObject.GetPhotonView().Owner.NickName ?? "Unknown Player";
    }
    #endregion

    // Update is called once per frame
    void Update()
    {
        if (hasController && 0 < Health)
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
        var moveInput = MoveInput.Direction;
#if UNITY_EDITOR
        if(isEditor)
            moveInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
#endif
        if (moveInput.magnitude == 0) return;
        Vector3 input = Vector3.zero;
        var vertical = moveInput.y;
        var horizontal = moveInput.x;
        input += transform.forward * vertical;
        input += transform.right * horizontal;
        input = ApplyGravity(input);
        characterController.Move(input * speed * Time.deltaTime);
        var characterDegree = Mathf.Atan2(moveInput.x,moveInput.y) * Mathf.Rad2Deg;
        chracterRoot.rotation = Quaternion.Euler(new Vector3(0,characterDegree,0));
    }

    public void ActionControl()
    {
        if (0 < currentCoolTime) return;
        var actDirection = ActionInput.Direction;
        if (actDirection.magnitude < 1) return;
        currentCoolTime = coolTime;
        var degree = Mathf.Atan2(ActionInput.Horizontal, ActionInput.Vertical) * Mathf.Rad2Deg;
        animator.SetTrigger("Action");
        var direction = Quaternion.Euler(new Vector3(0, degree, 0));
        chracterRoot.rotation = direction;
        gameObject.GetPhotonView().RPC("MakeProjectile", RpcTarget.All, magicPrefab.name, shotPoint.position, degree);
    }

    [PunRPC]
    public void MakeProjectile(string projectile, Vector3 position, float degree) {
        var direction = Quaternion.Euler(new Vector3(0, degree, 0));
        PhotonNetwork.Instantiate(projectile, position, direction);
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
        var moveInput = MoveInput.Direction;
#if UNITY_EDITOR
        if (isEditor)
            moveInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
#endif
        animator.SetFloat("Horizontal", Math.Abs(moveInput.x));
        animator.SetFloat("Vertical", Math.Abs(moveInput.y));
    }
    #endregion

    #region CHARACTER_UTIL
    public bool ApplyDamage(float damage) {
        if (Health <= 0) return false;
        Health -= damage;
        animator.SetTrigger("Damage");
        return Health <= 0;
    }
    #endregion

    #region CHARACTER_EVENT
    void OnPlayerDead() {
        animator.SetBool("Dead",true);
        if (hasController)
        {
            RespawnManager.Instance.Dead();
        }
    }

    [PunRPC]
    public void RespawnUser() {
        Destroy(this.gameObject);
    }
    #endregion
}
