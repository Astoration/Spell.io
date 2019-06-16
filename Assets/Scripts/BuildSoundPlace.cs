using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildSoundPlace : MonoBehaviour
{
    public GameObject audioSfx;
    // Start is called before the first frame update
    void Start()
    {
        Instantiate(audioSfx,transform.position,Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
