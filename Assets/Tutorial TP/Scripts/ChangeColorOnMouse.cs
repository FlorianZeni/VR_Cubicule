using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeColorOnMouse : MonoBehaviour
{

    public Color newMaterialColor = Color.blue;

    private Color oldMaterialColor;

    // Start is called before the first frame update
    void Start()
    {
        oldMaterialColor = GetComponent<Renderer>().material.color;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnMouseOver()
    {
        GetComponent<Renderer>().material.color = newMaterialColor;
    }

    void OnMouseExit()
    {
        GetComponent<Renderer>().material.color = oldMaterialColor;
    }
}
