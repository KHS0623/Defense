using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{
    public SceneLoader SceneLoader;

    // Start is called before the first frame update
    void Start()
    {
        SceneLoader = GetComponent<SceneLoader>();
    }

    // Update is called once per frame
    void Update()
    {

    }


    public void ExitButton()
    {
        Application.Quit();
    }

    public void StartButton()
    {
        //¼Ò¸®

        SceneLoader.LoadScene("StageListScene");
    }

    public void SettingButton()
    {

    }

    public void HomeButton()
    {
        SceneLoader.LoadScene("MainScene");
    }
}
