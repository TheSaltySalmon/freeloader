using System;
namespace FreeLoader.Services
{
    public interface IObjectPool
    {
        System.Collections.Generic.List<UnityEngine.GameObject> Get(string gameObjectName, int numberOfObjectsToGet);
        UnityEngine.GameObject GetSingle(string gameObjectName);
    }
}
