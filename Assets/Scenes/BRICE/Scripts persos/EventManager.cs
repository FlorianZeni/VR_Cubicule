using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void btn_startClicked()
    {
        Debug.Log("Start clicked !");
    }

    public void btn_chooseConfigClicked()
    {
        Debug.Log("Switching to config selector...");
        GameObject camera = GameObject.Find("Main Camera");

        camera.GetComponent<CameraMovement>().SelectTransition(
            CameraMovement.Transition.MAINMENU_CONFIGSELECTOR
            );
    }
}
