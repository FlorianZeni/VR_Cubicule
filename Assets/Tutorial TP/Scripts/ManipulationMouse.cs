using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManipulationMouse : MonoBehaviour
{

    private Vector3 lastMousePos = Vector3.zero;

    private bool isClicked = false;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(isClicked && Input.GetKey(KeyCode.Mouse0))
        {
            Debug.Log("Clicked");
            Vector3 newMousePos = Input.mousePosition;
            Vector3 delta = newMousePos - lastMousePos;
            transform.position += delta * 0.01f;
            lastMousePos = newMousePos;
        }
    }

    private void OnMouseDown()
    {
        lastMousePos = Input.mousePosition;
        isClicked = true;
    }

    private void OnMouseExit()
    {
        isClicked = false;
    }
}
