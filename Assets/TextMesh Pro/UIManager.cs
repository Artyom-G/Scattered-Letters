using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public GameObject finalLoveLetter;
    public GameObject canvas;
    public string nextScene = "Level";

    void Update()
    {
        //Universal Inputs
        if(Input.GetButtonDown("Submit")){
            canvas.SetActive(false);
            if(finalLoveLetter == null){
                SceneManager.LoadScene(nextScene);
            }
        }
    }
}
