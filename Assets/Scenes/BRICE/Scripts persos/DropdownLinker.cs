using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DropdownLinker : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        EventManager manager = GameObject.Find("EventManager").GetComponent<EventManager>();
        Dropdown dropdownComponent = GetComponent<Dropdown>();

        dropdownComponent.onValueChanged.AddListener(delegate
        {
            manager.SetScene(dropdownComponent.options[dropdownComponent.value].text);
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
