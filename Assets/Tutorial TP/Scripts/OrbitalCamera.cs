using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbitalCamera : MonoBehaviour
{
    public Transform origin;

    public float speed = 1.0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.DownArrow))
        {
            transform.position = Quaternion.Euler(-1, 0, 0) * transform.position;
            transform.LookAt(origin);
        }
        if (Input.GetKey(KeyCode.UpArrow))
        {
            transform.position = Quaternion.Euler(1, 0, 0) * transform.position;
            transform.LookAt(origin);
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.position = Quaternion.Euler(0, 1, 0) * transform.position;
            transform.LookAt(origin);
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.position = Quaternion.Euler(0, -1, 0) * transform.position;
            transform.LookAt(origin);
        }
        if (Input.GetKey(KeyCode.Z))
        {
            transform.position = Vector3.MoveTowards(transform.position, origin.position, speed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.position = Vector3.MoveTowards(transform.position, origin.position, -1.0f * speed * Time.deltaTime);
        }
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            sphere.transform.position = transform.position + transform.forward * 2.0f;
        }
    }
}
