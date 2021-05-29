using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{

    public enum Transition
    {
        MAINMENU_CONFIGSELECTOR
    }

    [SerializeField]
    public List<Vector3> cameraPositions;

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

    public void SelectTransition(Transition transition)
    {
        switch(transition)
        {
            case Transition.MAINMENU_CONFIGSELECTOR:
                StartCoroutine(
                    SwitchPosition(cameraPositions[0], cameraPositions[1])
                    );
                break;

        }
    }

    private IEnumerator SwitchPosition(Vector3 startPosition, Vector3 endPosition)
    {
        transform.position = startPosition;
        bInTransition = true;
        Debug.Log(transform.position);
        while(elapsedForNow < transitionDuration)
        {
            elapsedForNow += Time.deltaTime;
            transform.position = startPosition + 
                (endPosition - startPosition) * elapsedForNow/transitionDuration;
            yield return new WaitForEndOfFrame();
        }
        transform.position = endPosition;
        bInTransition = false;
        elapsedForNow = 0.0f;
    }
}
