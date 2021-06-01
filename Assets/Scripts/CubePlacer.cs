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

    private Dictionary<GameObject, int> placedObjects;

    JSONScene thisScene;

    ListaScenes scenesList;

    [SerializeField]
    public List<GameObject> prefabs;

    [SerializeField]
    private int index;

    public enum State
    {
        INITIAL_STATE,
        TYPE_STATE,
        POSITION_STATE,
        ROTATION_STATE
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Awake()
    {
        index = 0;
        LoadOldObjects(SceneManager.GetActiveScene().name);
    }

    // Update is called once per frame
    void Update()
    {
        UpdateCube();
    }

    private void UpdateCube()
    {
        SelectInput();
        if(cube != null)
        {
            cube.transform.position = transform.position + currentPlacementDistance * transform.forward;
            cube.transform.eulerAngles = new Vector3(0, currentPlacementRotation, 0);

        }
    }

    public void StartPlacing()
    {
        state = State.POSITION_STATE;
        cube = GameObject.Instantiate(prefabs[index]);
        currentPlacementDistance = defaultPlacementDistance;
        currentPlacementRotation = defaultPlacementRotation;
    }

    private void SaveCube()
    {
        placedObjects.Add(GameObject.Instantiate(cube), index);
        Destroy(cube);
    }

    private void UpdateObjectType()
    {
        Transform tr = cube.transform;
        Destroy(cube);
        cube = GameObject.Instantiate(prefabs[index]);
        cube.transform.position = tr.position;
        cube.transform.rotation = tr.rotation;
    }

    private void SelectInput()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            gameObject.transform.Find("IG_UI").gameObject.SetActive(
                !gameObject.transform.Find("IG_UI").gameObject.activeInHierarchy
                );
            Cursor.visible = gameObject.transform.Find("IG_UI").gameObject.activeInHierarchy;
            if (Cursor.visible)
                Cursor.lockState = CursorLockMode.Confined;
            else
                Cursor.lockState = CursorLockMode.None;
            return;
        }

        if(cube == null)
        {
            return;
        }

        if(state == State.INITIAL_STATE)
        {
            return;
        }

        if (state == State.TYPE_STATE)
        {
            if(Input.mouseScrollDelta.y > 0)
            {
                index += 1;
                index = index % prefabs.Count;
                UpdateObjectType();
            }
            else if (Input.mouseScrollDelta.y < 0)
            {
                index -= 1;
                index = (index + prefabs.Count) % prefabs.Count;
                UpdateObjectType();
            }
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Destroy(cube);
                state = State.INITIAL_STATE;
                return;
            }
            if (Input.GetKeyDown(KeyCode.Return))
            {
                SaveCube();
                state = State.INITIAL_STATE;
                return;
            }
            if (Input.GetKeyDown(KeyCode.E))
            {
                state = State.POSITION_STATE;
                return;
            }
        }

        if (state == State.POSITION_STATE)
        {
            currentPlacementDistance += Input.mouseScrollDelta.y;

            currentPlacementDistance = Mathf.Max(currentPlacementDistance, minPlacementDistance);
            currentPlacementDistance = Mathf.Min(currentPlacementDistance, maxPlacementDistance);
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Destroy(cube);
                state = State.INITIAL_STATE;
                return;
            }
            if (Input.GetKeyDown(KeyCode.Return))
            {
                SaveCube();
                state = State.INITIAL_STATE;
                return;
            }
            if (Input.GetKeyDown(KeyCode.E))
            {
                state = State.ROTATION_STATE;
                return;
            }
            return;
        }

        if (state == State.ROTATION_STATE)
        {
            currentPlacementRotation += Input.mouseScrollDelta.y;

            currentPlacementRotation = Mathf.Max(currentPlacementRotation, -180.0f);
            currentPlacementRotation = Mathf.Min(currentPlacementRotation, 180.0f);
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Destroy(cube);
                state = State.INITIAL_STATE;
                return;
            }
            if (Input.GetKeyDown(KeyCode.Return))
            {
                SaveCube();
                state = State.INITIAL_STATE;
                return;
            }
            if (Input.GetKeyDown(KeyCode.E))
            {
                state = State.TYPE_STATE;
                return;
            }
        }
    }

    public void LoadOldObjects(string sceneName)
    {
        Debug.Log("COUCOU");
        state = State.INITIAL_STATE;
        currentSwitch = switchCooldown;
        cube = null;
        placedObjects = new Dictionary<GameObject, int>();

        string path = Application.dataPath + "/scenes.json";
        string json = File.ReadAllText(path);

        scenesList = JsonUtility.FromJson<ListaScenes>(json);
        foreach(JSONScene scene in scenesList.scenes)
        {
            if(scene.name == sceneName)
            {
                thisScene = scene;
                foreach(JSONPrefab jsonPrefab in scene.prefabs)
                {
                    GameObject cubeToLoad = GameObject.Instantiate(prefabs[jsonPrefab.prefabIndex]);
                    cubeToLoad.transform.position = new Vector3(
                        jsonPrefab.locationX,
                        jsonPrefab.locationY,
                        jsonPrefab.locationZ
                        );

                    cubeToLoad.transform.Rotate(new Vector3(
                        0.0f,
                        jsonPrefab.rotation,
                        0.0f));

                    placedObjects.Add(cubeToLoad, jsonPrefab.prefabIndex);
                }
            }
        }
    }

    public void SaveNewObjects()
    {
        List<JSONPrefab> prefabsToSave = new List<JSONPrefab>();
        Debug.Log(placedObjects.Count);

        foreach(GameObject cubeToSave in placedObjects.Keys)
        {
            JSONPrefab prefabData = new JSONPrefab();
            prefabData.prefabIndex = placedObjects[cubeToSave];
            prefabData.locationX = cubeToSave.transform.position.x;
            prefabData.locationY = cubeToSave.transform.position.y;
            prefabData.locationZ = cubeToSave.transform.position.z;
            prefabData.rotation = cubeToSave.transform.eulerAngles.y;
            prefabsToSave.Add(prefabData);
        }

        thisScene.prefabs = prefabsToSave;
        Debug.Log(JsonUtility.ToJson(scenesList));

        File.WriteAllText(Application.dataPath + "/scenes.json", JsonUtility.ToJson(scenesList, true));

    }

    [System.Serializable]
    public class JSONPrefab
    {
        public int prefabIndex;
        public float locationX;
        public float locationY;
        public float locationZ;
        public float rotation;
    }

    [System.Serializable]
    public class JSONScene
    {
        public string name;
        public List<JSONPrefab> prefabs;
    }

    [System.Serializable]
    public class ListaScenes
    {
        public List<JSONScene> scenes;
    }
}
