using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    public GameObject pooledObjPrefab;
    public int initPool = 20;
    public int maxGrowPool = 100;
    public List<GameObject> pooledObjList;

    // Start is called before the first frame update
    void Start()
    {
        CreatePooledObjects();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CreatePooledObjects()
    {
        pooledObjList = new List<GameObject>();

        for (int i = 0; i < initPool; i++)
        {
            GameObject temp = (GameObject)Instantiate(pooledObjPrefab);
            temp.SetActive(false);
            pooledObjList.Add(temp);
        }
    }

    public GameObject GetPooledObject(Transform parent)
    {
        for (int i = 0; i < pooledObjList.Count; i++)
        {
            if (!pooledObjList[i].activeInHierarchy)
            {
                pooledObjList[i].transform.localPosition = parent.position;
                return pooledObjList[i];
            }
        }

        if (pooledObjList.Count < maxGrowPool)
        {
            GameObject temp = (GameObject)Instantiate(pooledObjPrefab);
            pooledObjList.Add(temp);
            return temp;
        }
        return null;
    }

    public void DestroyPooledObject(GameObject objToDestroy)
    {
        objToDestroy.SetActive(false);
    }
}
