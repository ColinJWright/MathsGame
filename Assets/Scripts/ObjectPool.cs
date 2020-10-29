using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    // array of all obstacle types
    public GameObject[] objectPrefabs;

    // number of objects to have in pool
    public int poolSize = 20;

    // list of all objects in the pool
    private List<GameObject> pooledObjects = new List<GameObject>();

    void Start ()
    {
        // create the pool's objects
        CreatePool();
    }

    // instantiates all the pool objects
    void CreatePool ()
    {
        for(int i = 0; i < poolSize; i++)
        {
            // instantiate the object
            GameObject objectToSpawn = Instantiate(objectPrefabs[i % objectPrefabs.Length]);

            // deactivate it
            objectToSpawn.SetActive(false);

            // add to the pooled objects list
            pooledObjects.Add(objectToSpawn);
        }
    }

    // returns a pooled object, ready for use
    public GameObject GetPooledObject ()
    {
        GameObject objectToSend = pooledObjects.Find(x => !x.activeInHierarchy);

        // is there no object to send?
        if(!objectToSend)
            Debug.LogError("No more available objects in the pool! Increase its size.");
        else
            objectToSend.SetActive(true);

        // return the object
        return objectToSend;
    }
}