using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplyExplosionDamage : MonoBehaviour
{
    public string owner = "";
    private void OnCollisionEnter(Collision collision) {
        ApplyDamage(collision.gameObject);
    }

    private void OnTriggerEnter(Collider other) {
        ApplyDamage(other.gameObject);
    }

    private void ApplyDamage(GameObject target) {
        var hasActor = target.GetComponent<ActorController>();
        if(hasActor != null && hasActor.hasController && !hasActor.IsDead)
        {
            bool kill = hasActor.ApplyDamage(0.5f);
            var targetView = target.GetPhotonView();
            Debug.Log(targetView.Owner.NickName + " : " + owner);

            if (kill && targetView.Owner.NickName != owner)
            {
                RankingManager.Instance.gameObject.GetPhotonView().RPC("IncrementKill", RpcTarget.All ,owner);
            }
        }
    }
}
