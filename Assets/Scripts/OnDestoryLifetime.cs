using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnDestoryLifetime : MonoBehaviour
{
    public float lifeTime = 6f;
    // Start is called before the first frame update
    void Start()
    {
        Invoke("DestroySelf", lifeTime);
    }

    public void DestroySelf() {
        Destroy(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
