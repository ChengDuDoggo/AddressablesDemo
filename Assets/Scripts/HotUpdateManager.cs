using UnityEngine;
using UnityEngine.AddressableAssets;

public class HotUpdateManager : MonoBehaviour
{
    private void Start()
    {
        InstantiateGameObject();
    }
    private void InstantiateGameObject()
    {
        Addressables.InstantiateAsync("HotUpdateCube").Completed += (obj) =>
        {
            //已经实例化后的物体
            GameObject go = obj.Result;
            go.transform.position = Vector3.zero;
            go.transform.localEulerAngles = Vector3.up * 180;
            go.transform.localScale = Vector3.one * 2;
        };
    }
}
