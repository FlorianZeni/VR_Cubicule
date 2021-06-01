using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateWhenKeyPressed : MonoBehaviour
{
    public float speed = 5.0f;

    public KeyCode keyToRotate = KeyCode.Space;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(keyToRotate))
        {
            Debug.Log("Silence, ça tourne !");
            transform.Rotate(new Vector3(0.0f, speed, 0.0f));
        }
    }
}
