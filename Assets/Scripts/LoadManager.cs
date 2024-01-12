using UnityEngine;
using UnityEngine.AddressableAssets;

public class LoadManager : MonoBehaviour
{
    private void Start()
    {
        Addressables.InitializeAsync();//初始化线程
        InstantiatePrefab();
    }
    #region 同步加载
    private void InstantiatePrefab()
    {
        //实例化加载到的游戏物体
        Instantiate(LoadPrefab(), transform);
    }
    private GameObject LoadPrefab()
    {
        var op = Addressables.LoadAssetAsync<GameObject>("Cube");
        GameObject go = op.WaitForCompletion();
        return go;
    }
    #endregion
}
