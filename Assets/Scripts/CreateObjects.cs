using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CreateObjects : MonoBehaviour
{
    [SerializeField] private int cantObjects;
    public GameObject[] Floor;
    List<NavMeshSurface> navMeshSurfaces = new List<NavMeshSurface>();
    [SerializeField] private List <GameObject> prefabsObstacles;
    [SerializeField] private List <GameObject> prefabsItem;
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
        posZ = 0;
    }
    // Start is called before the first frame update
    void Start()
    {
        cObstacles = prefabsObstacles.Count;
        cItem = prefabsItem.Count;
        for (int i = 0; i < cantObjects;)
        {
            typeObject = Random.Range(0, 2);
            if (typeObject == 0)
                prefabSelected = Random.Range(0, cObstacles);
            else if (typeObject == 1)
                prefabSelected = Random.Range(cObstacles, (cObstacles + cItem));
            newPosX = Random.Range(0, 2);
            GameObject newObj;
            //GameObject newObj = Instantiate(prefabsObjects[prefabSelected], new Vector3(posX, 0.5f, posZ), Quaternion.identity, this.transform);
            //newObj.AddComponent<NavMeshSurface>();
            //navMeshSurfaces.Add(newObj.GetComponent<NavMeshSurface>());
            switch (prefabSelected)
            {
                case 0:
                    newObj = Instantiate(prefabsObstacles[0], new Vector3(posX, 0.5f, posZ), Quaternion.identity, this.transform);
                    break;
                case 1:
                    newObj = Instantiate(prefabsObstacles[1], new Vector3(posX, 0.5f, posZ), Quaternion.identity, this.transform);
                    break;
                case 2:
                    newObj = Instantiate(prefabsObstacles[2], new Vector3(posX, 0.5f, posZ), Quaternion.identity, this.transform);
                    break;
                case 3:
                    newObj = Instantiate(prefabsObstacles[3], new Vector3(posX, 0.5f, posZ), Quaternion.identity, this.transform);
                    break;
                case 4:
                    newObj = Instantiate(prefabsObstacles[4], new Vector3(posX, 0.5f, posZ), Quaternion.identity, this.transform);
                    break;
                case 5:
                    newObj = Instantiate(prefabsItem[0], new Vector3(posX, 0.5f, posZ), Quaternion.identity, this.transform);
                    break;
                case 6:
                    newObj = Instantiate(prefabsItem[1], new Vector3(posX, 0.5f, posZ), Quaternion.identity, this.transform);
                    break;
                case 7:
                    newObj = Instantiate(prefabsItem[2], new Vector3(posX, 0.5f, posZ), Quaternion.identity, this.transform);
                    break;
                case 8:
                    newObj = Instantiate(prefabsItem[3], new Vector3(posX, 0.5f, posZ), Quaternion.identity, this.transform);
                    break;
                case 9:
                    newObj = Instantiate(prefabsItem[4], new Vector3(posX, 0.5f, posZ), Quaternion.identity, this.transform);
                    break;
            }
            /*
            if (prefabSelected == 0) {
                newObj = Instantiate(prefabsObjects[0], new Vector3(posX, 0.5f, posZ), Quaternion.identity, this.transform);
                newObj.AddComponent<NavMeshSurface>();
            } else if (prefabSelected == 1) {
                newObj = Instantiate(prefabsObjects[1], new Vector3(posX, 0.5f, posZ), Quaternion.identity, this.transform);
                newObj.AddComponent<NavMeshSurface>();
            } else if (prefabSelected == 2) {
                newObj = Instantiate(prefabsObjects[2], new Vector3(posX, 0.5f, posZ), Quaternion.identity, this.transform);
                newObj.AddComponent<NavMeshSurface>();
            } else if (prefabSelected == 3) {
                newObj = Instantiate(prefabsObjects[3], new Vector3(posX, 0.5f, posZ), Quaternion.identity, this.transform);
                newObj.AddComponent<NavMeshSurface>();
            } else if (prefabSelected == 4) {
                newObj = Instantiate(prefabsObjects[4], new Vector3(posX, 0.5f, posZ), Quaternion.identity, this.transform);
                newObj.AddComponent<NavMeshSurface>();
            } else if (prefabSelected == 5) {
                newObj = Instantiate(prefabsObjects[5], new Vector3(posX, 0.5f, posZ), Quaternion.identity, this.transform);
                newObj.AddComponent<NavMeshSurface>();
            } else if (prefabSelected == 6) {
                newObj = Instantiate(prefabsObjects[6], new Vector3(posX, 0.5f, posZ), Quaternion.identity, this.transform);
                newObj.AddComponent<NavMeshSurface>();
            } else if (prefabSelected == 7) {
                newObj = Instantiate(prefabsObjects[7], new Vector3(posX, 0.5f, posZ), Quaternion.identity, this.transform);
                newObj.AddComponent<NavMeshSurface>();
            } else if (prefabSelected == 8) {
                newObj = Instantiate(prefabsObjects[8], new Vector3(posX, 0.5f, posZ), Quaternion.identity, this.transform);
                newObj.AddComponent<NavMeshSurface>();
            } else if (prefabSelected == 9) {
                newObj = Instantiate(prefabsObjects[9], new Vector3(posX, 0.5f, posZ), Quaternion.identity, this.transform);
                newObj.AddComponent<NavMeshSurface>();
            }
            */
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
            i++;
        }

        bakeNavMesh();
    }
    
    private void bakeNavMesh() {
        foreach(GameObject _floor in Floor) {
            navMeshSurfaces.Add(_floor.GetComponent<NavMeshSurface>());
        }

        foreach (NavMeshSurface navMeshSurface in navMeshSurfaces) {
            navMeshSurface.BuildNavMesh();
        }
    }
}
