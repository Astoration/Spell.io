using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RankingManager : Singleton<RankingManager>,IPunObservable {
    public Dictionary<string, int> killRank = new Dictionary<string, int>();
    public Text rankingText;
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info) {
        if (stream.IsWriting)
        {
            stream.SendNext(killRank);
        }else if (stream.IsReading)
        {
            var killRank = stream.ReceiveNext();
        }
    }

    void UpdateRankingPnael() {
        rankingText.text = "";
        var ranking = killRank.GetEnumerator();
        var i = 1;
        while (ranking.MoveNext())
        {
            var item = ranking.Current;
            rankingText.text += (i++ + ". <color=\"#f00\">" + item.Key + "</color> - " + item.Value + " kill\n");
        }
    }

    [PunRPC]
    void IncrementKill(string nickname) {
        if (killRank.ContainsKey(nickname))
        {
            killRank[nickname] += 1;
        }
        UpdateRankingPnael();
    }

    [PunRPC]
    void AddUser(string nickname) {
        if (!killRank.ContainsKey(nickname))
        {
            killRank.Add(nickname, 0);
        }
        UpdateRankingPnael();
    }

    [PunRPC]
    void RemoveUser(string nickname) {
        if (killRank.ContainsKey(nickname))
        {
            killRank.Remove(nickname);
        }
        UpdateRankingPnael();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
