using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnchorMovement : MonoBehaviour
{

    public enum MenuRotation
    {
        MAINMENU = 0,
        SELECT_SCENE = -90,
        SETTINGS = 90,
        MUSIC = 180
    }

    [SerializeField]
    public float transitionDuration = 3.0f;

    [SerializeField]
    private float elapsedForNow;

    [SerializeField]
    private bool bInTransition = false;

    // Start is called before the first frame update
    void Start()
    {
        bInTransition = false;
        elapsedForNow = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {


    }

    public void SelectTransition(MenuRotation rotation)
    {
        StartCoroutine(SwitchPosition(rotation));
    }

    private IEnumerator SwitchPosition(MenuRotation rotation)
    {
        bInTransition = true;
        float currentRotation = transform.eulerAngles.y % 360;
        if (currentRotation > 180)
        {
            currentRotation -= 360;
        }
        int toRotate = (int)rotation;
        if(toRotate == 180 && Mathf.Abs((int)currentRotation) == 90)
        {

            toRotate *= (int)currentRotation / 90;
        }
        if ((int)currentRotation == 180 && Mathf.Abs(toRotate) == 90)
        {
            currentRotation *= (int)toRotate / 90;
        }

        if (toRotate == (int)currentRotation)
        {
            yield return new WaitForEndOfFrame();
        }
        else
        {
            float elapsed;
            while (elapsedForNow < transitionDuration)
            {
                elapsed = Time.deltaTime;
                elapsedForNow += elapsed;
                transform.Rotate(
                    new Vector3(
                        0,
                        ((float)toRotate - currentRotation) * elapsed / transitionDuration,
                        0)
                    );
                yield return new WaitForEndOfFrame();
            }
            bInTransition = false;
            transform.eulerAngles = new Vector3(0, (float)toRotate, 0);
            elapsedForNow = 0.0f;
        }
    }
}
