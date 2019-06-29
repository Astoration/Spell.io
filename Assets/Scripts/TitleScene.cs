using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TitleScene : MonoBehaviour
{
    public Animator titleScene;
    public GameObject inputField;
    public InputField nicknameField;
    public GameObject characterSelector;
    public bool isStarted = false;
    public string selectedCharacter = "1";
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
#if UNITY_EDITOR || UNITY_STANDALONE
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

    public void OnSelectCharacter(GameObject item)
    {
        if (item == null) return;
        selectedCharacter = item.name;
    }


    public void EnableSelector() {
        characterSelector.SetActive(true);
    }

    public void EnterGame() {
        PlayerStatusManager.Instance.selectedCharacter = selectedCharacter;
        PlayerStatusManager.Instance.nickname = nicknameField.text;
        SceneManager.LoadScene("MainScene");
    }
}
