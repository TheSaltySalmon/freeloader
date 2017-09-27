using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ObjectPoolService
{
    private Dictionary<string, List<object>> _gameObjectPool;

    public ObjectPoolService()
    {
        _gameObjectPool = new Dictionary<string, List<object>>();
    }

    public GameObject GetSingle(string gameObjectName)
    {
        return Get(gameObjectName, 1).First();
    }

    /// <summary>
    /// Get a number of GameObject type instances from a object pool. New instances are generated if needed.
    /// </summary>
    /// <param name="gameObjectName"></param>
    /// <param name="numberOfObjectsToGet"></param>
    /// <returns></returns>
    public List<GameObject> Get(string gameObjectName, int numberOfObjectsToGet)
    {
        var returnGameObjectList = new List<GameObject>(numberOfObjectsToGet);
        UnityEngine.Object gameObjectResourceToInstantiate = null;

        if (!_gameObjectPool.ContainsKey(gameObjectName))
        {
            _gameObjectPool.Add(gameObjectName, new List<object>());


            // Load resource, this is expensive on the CPU.
            gameObjectResourceToInstantiate = Resources.Load(gameObjectName);
        }
        else
        {
            foreach (GameObject gameObjectInPool in _gameObjectPool[gameObjectName])
            {
                // If we need more objects then return a game object that is not active in the scene
                if (returnGameObjectList.Count < numberOfObjectsToGet && !gameObjectInPool.activeInHierarchy)
                {
                    returnGameObjectList.Add(gameObjectInPool);
                }
            }

            // Avoid loading new resource to save CPU time, take first of type from pool.
            gameObjectResourceToInstantiate = (UnityEngine.Object)_gameObjectPool[gameObjectName][0];
        }

        // In case we are short of game objects to return
        int missingGameObjectCount = numberOfObjectsToGet - returnGameObjectList.Count;

        for (int i = 0; i < missingGameObjectCount; i++)
        {
            var newGameObject = MonoBehaviour.Instantiate(gameObjectResourceToInstantiate) as GameObject;

            newGameObject.SetActive(false);

            _gameObjectPool[gameObjectName].Add(newGameObject);
            returnGameObjectList.Add(newGameObject);
        }

        return returnGameObjectList;
    }

    #region Private methods

    #endregion
}
