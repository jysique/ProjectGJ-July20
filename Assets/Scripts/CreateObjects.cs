using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CreateObjects : MonoBehaviour
{
    [SerializeField] private int cantObjects;
    public GameObject[] Floor;
    List<NavMeshSurface> navMeshSurfaces = new List<NavMeshSurface>();
    [SerializeField] private GameObject[] prefabsObstacles;
    [SerializeField] private GameObject[] prefabsItem;
    private int prefabSelected;
    private int posX, posZ;
    private int newPosX;
    private int newPosZ;
    private int typeObject;
    private int cObstacles;
    private int cItem;
    private void Awake()
    {
        posX = 0;
        posZ = 10;
    }
    // Start is called before the first frame update
    void Start()
    {
        cObstacles = prefabsObstacles.Length;
        cItem = prefabsItem.Length;
        for (int i = 0; i < cantObjects;i++)
        {
            typeObject = Random.Range(0, 2);
            if (typeObject == 0)
                prefabSelected = Random.Range(0, cObstacles);
            else if (typeObject == 1)
                prefabSelected = Random.Range(0, cItem);
            newPosX = Random.Range(0, 2);
            switch (typeObject) {
                case 0:
                    GameObject newObj1 = Instantiate(prefabsObstacles[prefabSelected], new Vector3(posX, 0 , posZ), Quaternion.identity, this.transform);
                    break;
                case 1:
                    GameObject newObj2 = Instantiate(prefabsItem[prefabSelected], new Vector3(posX, 0.5f, posZ), Quaternion.identity, this.transform);
                    break;
                default:
                    break;
            }
         
            //
            if (newPosX == 0 && posX + 3 > 4)
                posX -= 3;
            else if (newPosX == 0 && posX + 3 < 4)
                posX += 3;
            //
            if (newPosX == 1 && posX - 2 < -4)
                posX += 3;
            else if (newPosX == 1 && posX - 3 > -4)
                posX -= 3;
            //
            posZ += 5;

        }

        bakeNavMesh();
    }
    
    private void bakeNavMesh() {
        foreach(GameObject _floor in Floor) {
            //navMeshSurfaces.Add(_floor.GetComponent<NavMeshSurface>());
            _floor.GetComponent<NavMeshSurface>().BuildNavMesh();
        }
        /*
        if (navMeshSurfaces.Count > 0) {
            foreach (NavMeshSurface navMeshSurface in navMeshSurfaces) {
                navMeshSurface.BuildNavMesh();
            }
        }
        */
    }
}
