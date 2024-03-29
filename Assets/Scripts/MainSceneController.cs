﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;

public class MainSceneController : MonoBehaviourPunCallbacks {
    public static MainSceneController instance;
    public Transform[] spawnSpot;
    public static GameObject player;
    public GameObject loadingPanel;
    public readonly string version = "1.0.0";

    #region MonoBehavior LifeCycle
    public void Awake() {
        PhotonNetwork.AddCallbackTarget(this);
        PhotonNetwork.AutomaticallySyncScene = true;
        instance = this;
    }

    public void Start() {
        if (PhotonNetwork.IsConnected)
        {
            PhotonNetwork.JoinRandomRoom();
        } else
        {
            PhotonConnect();
        }
    }

    private void OnDestroy() {
        PhotonNetwork.RemoveCallbackTarget(this);
    }
    #endregion

    public void SpawnPlayer() {
        var spawnIndex = UnityEngine.Random.Range(0, spawnSpot.Length);
        player = PhotonNetwork.Instantiate("Player"+PlayerStatusManager.Instance.selectedCharacter, spawnSpot[spawnIndex].position, Quaternion.identity);
    }

    public void LeftGame() {
        PhotonNetwork.LeaveRoom();
    }

    #region Photon LifeCycle
    private void PhotonConnect() {
        PhotonNetwork.GameVersion = version;
        PhotonNetwork.ConnectUsingSettings();
        PhotonNetwork.NickName = PlayerStatusManager.Instance.nickname;
    }

    public override void OnConnectedToMaster() {
        base.OnConnectedToMaster();
        PhotonNetwork.JoinRandomRoom();
    }

    public override void OnJoinRandomFailed(short returnCode, string message) {
        PhotonNetwork.CreateRoom(null);
    }

    public override void OnJoinedRoom() {
        if (player == null)
        {
            SpawnPlayer();
            loadingPanel.SetActive(false);
            RankingManager.Instance.gameObject.GetPhotonView().RPC("AddUser", RpcTarget.All, PhotonNetwork.NickName ?? "Unknown Player");
        }
    }

    public override void OnLeftRoom() {
        player.GetPhotonView().RPC("RespawnUser", RpcTarget.All);
        RankingManager.Instance.gameObject.GetPhotonView().RPC("RemoveUser", RpcTarget.All, PhotonNetwork.NickName ?? "Unknown Player");
        base.OnLeftRoom();
        SceneManager.LoadScene("TitleScene");
    }
    #endregion
}
