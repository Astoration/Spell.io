using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplyExplosionDamage : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision) {
        ApplyDamage(collision.gameObject);
    }

    private void OnTriggerEnter(Collider other) {
        ApplyDamage(other.gameObject);
    }

    private void ApplyDamage(GameObject target) {
        var hasActor = target.GetComponent<ActorController>();
        if(hasActor != null)
        {
            hasActor.ApplyDamage(0.5f);
        }
    }
}
