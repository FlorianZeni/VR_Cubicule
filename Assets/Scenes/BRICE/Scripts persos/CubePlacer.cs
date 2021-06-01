using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.IO;
using UnityEngine.SceneManagement;

public class CubePlacer : MonoBehaviour
{

    #region Position
    [SerializeField]
    public float maxPlacementDistance = 10.0f;

    [SerializeField]
    public float minPlacementDistance = 0.5f;

    [SerializeField]
    public float defaultPlacementDistance = 5.0f;

    [SerializeField]
    private float currentPlacementDistance;
    #endregion

    #region Rotation

    [SerializeField]
    public float defaultPlacementRotation = 0.0f;

    [SerializeField]
    private float currentPlacementRotation;

    #endregion

    [SerializeField]
    private float switchCooldown = 0.5f;

    private float currentSwitch;

    [SerializeField]
    private State state;

    GameObject cube;

    private List<GameObject> placedObjects;

    JSONScene thisScene;

    ListaScenes scenesList;

    public enum State
    {
        INITIAL_STATE,
        POSITION_STATE,
        ROTATION_STATE
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Awake()
    {
        LoadOldObjects(SceneManager.GetActiveScene().name);
    }

    // Update is called once per frame
    void Update()
    {
        currentSwitch += Time.deltaTime;
        if(cube != null)
        {
            UpdateCube();
        }
    }

    private void UpdateCube()
    {
        SelectInput();
        cube.transform.position = transform.position + currentPlacementDistance * transform.forward;
        cube.transform.eulerAngles = new Vector3(0, currentPlacementRotation, 0);
    }

    public void StartPlacing()
    {
        state = State.POSITION_STATE;
        cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        currentPlacementDistance = defaultPlacementDistance;
        currentPlacementRotation = defaultPlacementRotation;
    }

    private void SaveCube()
    {
        placedObjects.Add(GameObject.Instantiate(cube));
        Destroy(cube);
    }

    private void SelectInput()
    {
        if(state == State.INITIAL_STATE)
        {
            return;
        }

        if(state == State.POSITION_STATE)
        {
            currentPlacementDistance += Input.mouseScrollDelta.y;

            currentPlacementDistance = Mathf.Max(currentPlacementDistance, minPlacementDistance);
            currentPlacementDistance = Mathf.Min(currentPlacementDistance, maxPlacementDistance);
            if (Input.GetKey(KeyCode.Escape))
            {
                Destroy(cube);
                state = State.INITIAL_STATE;
                return;
            }
            if (Input.GetKey(KeyCode.Return))
            {
                SaveCube();
                state = State.INITIAL_STATE;
                return;
            }
            if (Input.GetKey(KeyCode.E) && currentSwitch >= switchCooldown)
            {
                state = State.ROTATION_STATE;
                currentSwitch = 0.0f;
            }
            return;
        }

        if (state == State.ROTATION_STATE)
        {
            currentPlacementRotation += Input.mouseScrollDelta.y;

            currentPlacementRotation = Mathf.Max(currentPlacementRotation, -180.0f);
            currentPlacementRotation = Mathf.Min(currentPlacementRotation, 180.0f);
            if (Input.GetKey(KeyCode.Escape))
            {
                Destroy(cube);
                state = State.INITIAL_STATE;
                return;
            }
            if (Input.GetKey(KeyCode.Return))
            {
                SaveCube();
                state = State.INITIAL_STATE;
                return;
            }
            if (Input.GetKey(KeyCode.E) && currentSwitch >= switchCooldown)
            {
                state = State.POSITION_STATE;
                currentSwitch = 0.0f;
            }
        }
    }

    public void LoadOldObjects(string sceneName)
    {
        Debug.Log("COUCOU");
        state = State.INITIAL_STATE;
        currentSwitch = switchCooldown;
        cube = null;
        placedObjects = new List<GameObject>();

        string path = Application.dataPath + "/scenes.json";
        string json = File.ReadAllText(path);

        scenesList = JsonUtility.FromJson<ListaScenes>(json);
        foreach(JSONScene scene in scenesList.scenes)
        {
            if(scene.name == sceneName)
            {
                thisScene = scene;
                foreach(JSONCube jsonCube in scene.cubes)
                {
                    GameObject cubeToLoad = GameObject.CreatePrimitive(PrimitiveType.Cube);
                    cubeToLoad.transform.position = new Vector3(
                        jsonCube.locationX,
                        jsonCube.locationY,
                        jsonCube.locationZ
                        );

                    cubeToLoad.transform.Rotate(new Vector3(
                        0.0f,
                        jsonCube.rotation,
                        0.0f));

                }
            }
        }
    }

    public void SaveNewObjects()
    {
        List<JSONCube> cubesToSave = new List<JSONCube>();
        Debug.Log(placedObjects.Count);

        foreach(GameObject cubeToSave in placedObjects)
        {
            JSONCube cubeData = new JSONCube();
            cubeData.locationX = cubeToSave.transform.position.x;
            cubeData.locationY = cubeToSave.transform.position.y;
            cubeData.locationZ = cubeToSave.transform.position.z;
            cubeData.rotation = cubeToSave.transform.eulerAngles.y;
            cubesToSave.Add(cubeData);
        }

        thisScene.cubes = cubesToSave;
        Debug.Log(JsonUtility.ToJson(scenesList));

        File.WriteAllText(Application.dataPath + "/scenes.json", JsonUtility.ToJson(scenesList));

    }

    [System.Serializable]
    public class JSONCube
    {
        public float locationX;
        public float locationY;
        public float locationZ;
        public float rotation;
    }

    [System.Serializable]
    public class JSONScene
    {
        public string name;
        public List<JSONCube> cubes;
    }

    [System.Serializable]
    public class ListaScenes
    {
        public List<JSONScene> scenes;
    }
}
