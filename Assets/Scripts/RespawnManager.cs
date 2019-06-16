using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;
using UnityEngine.UI;

public class RespawnManager : Singleton<RespawnManager>
{
    public float respawnTime = 5f;
    private float respawnWait = 5f;
    public Text spawnTime;
    public bool isDead = false;
    public GameObject respawnPanel;

    // Update is called once per frame
    void Update()
    {
        if (isDead)
        {
            spawnTime.text = Mathf.RoundToInt(respawnWait) + "";
            respawnWait -= Time.deltaTime;
            if (respawnWait < 0)
            {
                isDead = false;
                Respawn();
            }
        }
    }

    public void Dead() {
        isDead = true;
        respawnWait = respawnTime;
        respawnPanel.SetActive(true);
    }

    public void Respawn() {
        respawnPanel.SetActive(false);
        MainSceneController.player.GetPhotonView().RPC("RespawnUser", RpcTarget.All);
        MainSceneController.instance.SpawnPlayer();
    }
}
