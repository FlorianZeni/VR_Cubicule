using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

using UnityEngine.UI;

public class EventManager : MonoBehaviour
{

    [SerializeField]
    public string sceneToLoad = ""; 
    
    private static EventManager instance;
    public static EventManager Instance { get; private set; }

    void Awake()
    {
        if (instance != null && instance != this)
            Destroy(gameObject);   

        instance = this;
        PlayerPrefs.SetString("sceneToLoad", "HouseCPY");
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void btn_startClicked()
    {
        Debug.Log("Start clicked !");
        Debug.Log(PlayerPrefs.GetString("sceneToLoad"));
        SceneManager.LoadScene(PlayerPrefs.GetString("sceneToLoad"));
    }

    public void btn_chooseConfigClicked()
    {
        Debug.Log("Switching to config selector...");
        GameObject anchor = GameObject.Find("Anchor");

        anchor.GetComponent<AnchorMovement>().SelectTransition(
            AnchorMovement.MenuRotation.SELECT_SCENE
            );
    }

    public void btn_mainClicked()
    {
        Debug.Log("Back to main menu...");
        GameObject anchor = GameObject.Find("Anchor");

        anchor.GetComponent<AnchorMovement>().SelectTransition(
            AnchorMovement.MenuRotation.MAINMENU
            );
    }

    public void btn_settingsClicked()
    {
        Debug.Log("Switching to settings menu...");
        GameObject anchor = GameObject.Find("Anchor");

        anchor.GetComponent<AnchorMovement>().SelectTransition(
            AnchorMovement.MenuRotation.SETTINGS
            );
    }

    public void btn_quitClicked()
    {
        Debug.Log("Switching to main menu...");
        SceneManager.LoadScene("Scene_MenuPrincipal");
    }

    public void btn_saveClicked()
    {
        Debug.Log("Saving");
        GameObject camera = GameObject.Find("Main Camera");
        camera.GetComponent<CubePlacer>().SaveNewObjects();
    }

    public void btn_placeCubeClicked()
    {
        GameObject camera = GameObject.Find("Main Camera");
        camera.GetComponent<CubePlacer>().StartPlacing();
    }

    public void btn_playClicked()
    {
        Dropdown music = GameObject.Find("Music Dropdown").GetComponent<Dropdown>();

        string text = music.options[music.value].text;
        GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioPlayer>().SetClip(text);

    }

    public void btn_stopClicked()
    {
        GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioPlayer>().StopMusic();
    }

    public void btn_musicClicked()
    {
        Debug.Log("Switching to music menu...");
        GameObject anchor = GameObject.Find("Anchor");

        anchor.GetComponent<AnchorMovement>().SelectTransition(
            AnchorMovement.MenuRotation.MUSIC
            );
    }

    public void btn_leaveClicked()
    {
        Application.Quit();
    }

    public void SetScene(string newScene)
    {
        Debug.Log("New Scene : " + newScene);
        PlayerPrefs.SetString("sceneToLoad", newScene);
    }


}
