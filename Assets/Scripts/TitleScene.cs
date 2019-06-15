using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleScene : MonoBehaviour
{
    public Animator titleScene;
    public GameObject inputField;
    public bool isStarted = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!isStarted)
        {
            UpdateStart();
        }
    }

    private void UpdateStart() 
    {
        bool isTouched = false;
#if UNITY_EDITOR
        isTouched = Input.GetMouseButton(0);
#else
        isTouched = Input.touchCount != 0;
#endif
        if (isTouched)
        {
            TouchStart();
        }
    }

    public void TouchStart() {
        titleScene.enabled = true;
        titleScene.Play("InputStart");
        inputField.SetActive(true);
    }
}
