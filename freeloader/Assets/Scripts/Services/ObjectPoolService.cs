using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ObjectPoolService
{
    private Dictionary<string, List<object>> gameObjectPool;

    public ObjectPoolService()
    {
        gameObjectPool = new Dictionary<string, List<object>>();
    }

    public GameObject GetSingle(string gameObjectName)
    {
        return Get(gameObjectName, 1).First();
    }

    /// <summary>
    /// Get a number of GameObject type instances from a object pool. New instances are generated if needed.
    /// </summary>
    /// <typeparam name="T">GameObject child class type</typeparam>
    /// <param name="numberOfObjectsToGet">The number of objects to get or generate</param>
    /// <returns>List of objects with GameObject as base class</returns>
    public List<GameObject> Get(string gameObjectName, int numberOfObjectsToGet)
    {
        var returnGameObjectList = new List<GameObject>(numberOfObjectsToGet);
        UnityEngine.Object gameObjectResourceToInstantiate = null;

        if (!gameObjectPool.ContainsKey(gameObjectName))
        {
            gameObjectPool.Add(gameObjectName, new List<object>());


            // Load resource, this is expensive on the CPU.
            gameObjectResourceToInstantiate = Resources.Load(gameObjectName);
        }
        else
        {
            foreach (GameObject gameObjectInPool in gameObjectPool[gameObjectName])
            {
                // If we need more objects then return a game object that is not active in the scene
                if (returnGameObjectList.Count < numberOfObjectsToGet && !gameObjectInPool.activeInHierarchy)
                {
                    returnGameObjectList.Add(gameObjectInPool);
                }
            }
        }

        // In case we are short of game objects to return
        int missingGameObjectCount = numberOfObjectsToGet - returnGameObjectList.Count;

        for (int i = 0; i < missingGameObjectCount; i++)
        {
            var newGameObject = MonoBehaviour.Instantiate(gameObjectResourceToInstantiate) as GameObject;

            newGameObject.SetActive(false);

            gameObjectPool[gameObjectName].Add(newGameObject);
            returnGameObjectList.Add(newGameObject);
        }

        return returnGameObjectList;
    }

    #region Private methods

    #endregion
}
