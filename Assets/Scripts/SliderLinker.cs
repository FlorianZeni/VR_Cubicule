using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderLinker : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Slider sliderComponent = GetComponent<Slider>();

        sliderComponent.onValueChanged.AddListener(delegate
        {
            GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioSource>().volume = 
            sliderComponent.value;
        });

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
