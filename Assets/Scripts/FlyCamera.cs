using UnityEngine;
using System.Collections;

public class FlyCamera : MonoBehaviour
{
    float mainSpeed = 100.0f; //regular speed
    float shiftAdd = 200.0f; //multiplied by how long shift is held.  Basically running
    float maxShift = 500.0f; //Maximum speed when holdin gshift
    float camSens = 0.25f; //How sensitive it with mouse
    private Vector3 lastMouse = new Vector3(255, 255, 255); //kind of in the middle of the screen, rather than at the top (play)
    private float totalRun = 1.0f;

    void Update()
    {

        //move camera with mouse
        if(!Cursor.visible)
        {
            lastMouse = Input.mousePosition - lastMouse;
            lastMouse = new Vector3(-lastMouse.y * camSens, lastMouse.x * camSens, 0);
            lastMouse = new Vector3(transform.eulerAngles.x + lastMouse.x, transform.eulerAngles.y + lastMouse.y, 0);
            transform.eulerAngles = lastMouse;
            lastMouse = Input.mousePosition;
        }

        //end of move camera with mouse

        //Mouse  camera angle done.  

        //Keyboard commands
        Vector3 p = GetBaseInput();

        p = p * Time.deltaTime * mainSpeed;
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.velocity = p;

    }

    private Vector3 GetBaseInput()
    { //returns the basic values, if it's 0 than it's not active.
        Vector3 p_Velocity = new Vector3();
        if (Input.GetKey(KeyCode.Z))
        {
            p_Velocity += transform.forward.normalized;
            Debug.Log(p_Velocity);
        }
        if (Input.GetKey(KeyCode.S))
        {
            p_Velocity += transform.forward.normalized * -1;
            Debug.Log(p_Velocity);
        }
        if (Input.GetKey(KeyCode.Q))
        {
            p_Velocity += transform.forward.normalized;
            float temp = p_Velocity.x;
            p_Velocity.x = p_Velocity.z * -1.0f;
            p_Velocity.z = temp;
        }
        if (Input.GetKey(KeyCode.D))
        {
            p_Velocity += transform.forward.normalized;
            float temp = p_Velocity.x;
            p_Velocity.x = p_Velocity.z;
            p_Velocity.z = temp * -1.0f;
        }
        return p_Velocity;
    }
}