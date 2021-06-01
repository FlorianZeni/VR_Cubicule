using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateOnClick : MonoBehaviour
{
    // Start is called before the first frame update
    private GameObject model;
    private GameObject copy;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            RaycastHit hit;
            // Does the ray intersect any objects excluding the player layer
            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity))
            {
                //Debug.Log("Did Hit");
                model = hit.collider.gameObject;
                Debug.Log("Copie");
            }
        }
        else if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            if (model != null)
            {
                copy = GameObject.Instantiate(model);
                copy.GetComponent<Renderer>().material.color = Color.blue;
                copy.transform.position = transform.position + transform.forward;
                copy.transform.rotation = transform.rotation;
                Debug.Log("Création");
            }
        }
    }
}
