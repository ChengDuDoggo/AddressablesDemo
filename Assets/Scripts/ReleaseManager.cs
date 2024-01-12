using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.UI;

public class ReleaseManager : MonoBehaviour
{
    public Button LoadBtn;
    public Button DestroyBtn;
    public Button ReleaseBtn;
    private GameObject Cube;
    private void Start()
    {
        //异步初始化Addressables系统，初始化AA系统是必要的，可以自动正确高效的配置AA的系统文件
        //一般来说整个程序只需要调用一次，通常在程序启动阶段调用一次
        //请注意，如果你的应用程序有多个入口点（例如多个场景），并且每个入口点都需要使用Addressables系统，则需要
        //在每个入口点的脚本中都调用一次Addressables.InitializeAsync()函数
        Addressables.InitializeAsync();
        LoadBtn.onClick.AddListener(LoadGameObject);
        DestroyBtn.onClick.AddListener(OnClickDestroyObj);
        ReleaseBtn.onClick.AddListener(ReleaseGameObject);
    }
    /// <summary>
    /// 加载物体
    /// </summary>
    private void LoadGameObject()
    {
        Addressables.InstantiateAsync("Cube").Completed += (hal) =>
        {
            Cube = hal.Result;
        };
    }
    /// <summary>
    /// 释放物体
    /// </summary>
    private void ReleaseGameObject()
    {
        Addressables.Release(Cube);
    }
    /// <summary>
    /// 销毁物体
    /// </summary>
    private void OnClickDestroyObj()
    {
        Destroy(Cube);
        //释放资源并销毁物体，在实战中通常使用此函数，它会自动调用Destroy销毁物体
        //Addressables.ReleaseInstance(Cube);
    }
}
