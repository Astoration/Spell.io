using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RankingManager : MonoBehaviour,IPunObservable {
    public static RankingManager Instance {
        get {
            return instance;
        }
    }
    public static RankingManager instance;
    public Dictionary<string, int> killRank = new Dictionary<string, int>();
    public Text rankingText;

    private void Awake() {
        if (instance == null)
            instance = this;
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info) {
        if (stream.IsWriting)
        {
            stream.SendNext(killRank);
        }else if (stream.IsReading)
        {
            var prevCount = killRank.Count;
            killRank = (Dictionary<string,int>)stream.ReceiveNext();
            if(prevCount != killRank.Count)
            {
                UpdateRankingPanel();
            }
        }
    }

    void UpdateRankingPanel() {
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
        } else
        {
            killRank.Add(nickname, 1);
        }
        UpdateRankingPanel();
    }

    [PunRPC]
    void AddUser(string nickname) {
        if (!killRank.ContainsKey(nickname))
        {
            killRank.Add(nickname, 0);
        }
        UpdateRankingPanel();
    }

    [PunRPC]
    void RemoveUser(string nickname) {
        if (killRank.ContainsKey(nickname))
        {
            killRank.Remove(nickname);
        }
        UpdateRankingPanel();
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
