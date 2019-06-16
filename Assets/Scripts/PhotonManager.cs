using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PhotonManager : Singleton<PhotonManager>
{
    public readonly string version = "1.0.0";

    public void Awake() {
        PhotonNetwork.AutomaticallySyncScene = true;
    }

    public void Start() {
        PhotonConnect();
    }

    private void PhotonConnect() {
        PhotonNetwork.GameVersion = version;
        PhotonNetwork.ConnectUsingSettings();
    }
}
