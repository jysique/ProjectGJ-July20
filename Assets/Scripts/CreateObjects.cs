using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateObjects : MonoBehaviour
{
    [SerializeField] private int cantObjects;
    [SerializeField] private List <GameObject> prefabsObjects;
    private int prefabSelected;
    private int posX, posZ;
    private int newPosX;
    private int newPosZ;
    private void Awake()
    {
        posX = 0;
        posZ = 0;
    }
    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < cantObjects; i++)
        {
            prefabSelected = Random.Range(0, prefabsObjects.Count);
            newPosX = Random.Range(0, 2);
            if (prefabSelected == 0)
                Instantiate(prefabsObjects[0], new Vector3(posX, 0.5f, posZ), Quaternion.identity, this.transform);
            else if (prefabSelected == 1)
                Instantiate(prefabsObjects[1], new Vector3(posX, 0.5f, posZ), Quaternion.identity, this.transform);
            else if (prefabSelected == 2)
                Instantiate(prefabsObjects[2], new Vector3(posX, 0.5f, posZ), Quaternion.identity, this.transform);
            else if (prefabSelected == 3)
                Instantiate(prefabsObjects[3], new Vector3(posX, 0.5f, posZ), Quaternion.identity, this.transform);
            else if (prefabSelected == 4)
                Instantiate(prefabsObjects[4], new Vector3(posX, 0.5f, posZ), Quaternion.identity, this.transform);
            else if (prefabSelected == 5)
                Instantiate(prefabsObjects[5], new Vector3(posX, 0.5f, posZ), Quaternion.identity, this.transform);
            else if (prefabSelected == 6)
                Instantiate(prefabsObjects[6], new Vector3(posX, 0.5f, posZ), Quaternion.identity, this.transform);
            else if (prefabSelected == 7)
                Instantiate(prefabsObjects[7], new Vector3(posX, 0.5f, posZ), Quaternion.identity, this.transform);
            else if (prefabSelected == 8)
                Instantiate(prefabsObjects[8], new Vector3(posX, 0.5f, posZ), Quaternion.identity, this.transform);
            else if (prefabSelected == 9)
                Instantiate(prefabsObjects[9], new Vector3(posX, 0.5f, posZ), Quaternion.identity, this.transform);
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
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
