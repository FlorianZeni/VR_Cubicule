using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EventManager : MonoBehaviour
{

    [SerializeField]
    private string sceneToLoad = "";

    // Start is called before the first frame update
    void Start()
    {
        sceneToLoad = "HouseCPY";
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void btn_startClicked()
    {
        Debug.Log("Start clicked !");
        SceneManager.LoadScene(sceneToLoad);
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
        GameObject anchor = GameObject.Find("Anchor");
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

    public void SetScene(string newScene)
    {
        Debug.Log("New Scene : " + newScene);
        sceneToLoad = newScene;
    }


}
