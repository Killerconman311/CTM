using System.Collections;
using System.Collections.Generic;
using Unity.XR.CoreUtils;
using UnityEngine;
using System.Linq;

public class objPoolManager : MonoBehaviour
{
    public static List<PooledObjectInfo> ObjectPools = new List<PooledObjectInfo>();

    private GameObject _objectPoolEmptyHolder;
    private static GameObject _bulletEmpty;
    private static GameObject _spellsEmpty;
    private static GameObject _FXEmpty;
    private static GameObject _cloudsEmpty;
    public enum PoolType
    {
        bullets,
        Spells,
        FX,
        Clouds,
        None
    }
    
 
    private void Awake()
    {
        SetupEmpties();
    }

    private void SetupEmpties()
    {
        _objectPoolEmptyHolder = new GameObject("Object Pool");

        _bulletEmpty = new GameObject("Enemy Spells");
        _bulletEmpty.transform.SetParent(_objectPoolEmptyHolder.transform);

        _spellsEmpty = new GameObject("Spells");
        _spellsEmpty.transform.SetParent(_objectPoolEmptyHolder.transform);

        _FXEmpty = new GameObject("FX");
        _FXEmpty.transform.SetParent(_objectPoolEmptyHolder.transform);

        _cloudsEmpty = new GameObject("Clouds");
        _cloudsEmpty.transform.SetParent(_objectPoolEmptyHolder.transform);     
    }


    public static GameObject SpawnObject(GameObject objectToSpawn, Vector3 spawnPosition, Quaternion spawnRotation, PoolType poolType = PoolType.None)
    {

        PooledObjectInfo pool = ObjectPools.Find(p => p.LookupString == objectToSpawn.name);

        
        if (pool == null)
        {
            pool = new PooledObjectInfo() { LookupString = objectToSpawn.name };
            ObjectPools.Add(pool);
        }


        // Check if there are any inactive objects in the pool
        GameObject spawnableObj = pool.InactiveObjects.FirstOrDefault();
      

        if (spawnableObj == null)
        {
            //find the parent of empty object
            GameObject parentObject = SetParentObject(poolType);

            //instantiate new object 
            spawnableObj = Instantiate(objectToSpawn, spawnPosition, spawnRotation);
            Debug.Log("Instantiating new object");  

            if (parentObject != null)
            {
                spawnableObj.transform.SetParent(parentObject.transform);
            }
        }

        else
        {
            Debug.Log("Reactivating object from pool: " + spawnableObj.name);
            spawnableObj.transform.position = spawnPosition;
            spawnableObj.transform.rotation = spawnRotation;
            pool.InactiveObjects.Remove(spawnableObj);
            spawnableObj.SetActive(true);
        }
        return spawnableObj;

    }

    public static void ReturnObjectToPool(GameObject obj)
    {
        string goName = obj.name.Replace("(Clone)", ""); // remove (Clone) from the name
        PooledObjectInfo pool = ObjectPools.Find(p => p.LookupString == goName);

        if (pool == null)
        {
            Debug.LogError("Trying to release an object that is not pooled: " + goName);
        }

        else
        {
            obj.SetActive(false);
            pool.InactiveObjects.Add(obj);
        }
    }
    private static GameObject SetParentObject(PoolType poolType)
    {
        switch (poolType)
        {
            case PoolType.bullets:
                return _bulletEmpty;
            case PoolType.Spells:
                return _spellsEmpty;
            case PoolType.FX:
                return _FXEmpty;
            case PoolType.Clouds:
                return _cloudsEmpty;
            case PoolType.None:
                return null;
            default:
                return null;

            
        }
        
    }
}
public class PooledObjectInfo
{
    public string LookupString;
    public List<GameObject> InactiveObjects = new List<GameObject>();
}