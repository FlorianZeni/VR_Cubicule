using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateOnOffKey : MonoBehaviour
{
    public float speed = 5.0f;

    public KeyCode keyToRotate = KeyCode.Space;

    private bool isRotating = false;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(keyToRotate))
        {
            isRotating = !isRotating;
        }

        if(isRotating)
        {
            transform.Rotate(new Vector3(0.0f, speed, 0.0f));
        }
    }
}
