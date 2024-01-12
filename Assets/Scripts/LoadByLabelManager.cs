using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.UI;

public class LoadByLabelManager : MonoBehaviour
{
    //可寻址系统标签的引用
    public AssetLabelReference prefabsLabel;
    public Button loadPlantLogoBtn;
    private RawImage _rawImage;
    private void Start()
    {
        _rawImage = GetComponent<RawImage>();
/*        loadPlantLogoBtn.onClick.AddListener(() =>
        {
            LoadTextureByKeyLabel("Logo", "Plant");
        });*/
        LoadGameObjectByLabel();//根据标签加载资源
    }
    /// <summary>
    /// 直接根据资产标签加载资源
    /// </summary>
    private void LoadGameObjectByLabel()
    {
        //这里是LoadAssetsAsync不是LoadAssetAsync，LoadAssetsAsync加了s，代表一次性可以加载许多资源的函数
        //LoadAssetsAsync中可以放数组参数直接加载许多资源，也可以只放一个参数只加载一个资源
        Addressables.LoadAssetsAsync<Texture2D>(prefabsLabel, (texture) =>
        {
            //每加载完成一个资源，就回调一次，标签下有几个就会执行几次
            Debug.Log("加载完成一个资源：" + texture.name);
            _rawImage.texture = texture;
            _rawImage.SetNativeSize();
        });
    }
    /// <summary>
    /// 根据资产地址和标签加载资源
    /// </summary>
    /// <param name="key">可寻址资产地址(ID)</param>
    /// <param name="label">资产标签</param>
    private void LoadTextureByKeyLabel(string key, string label)
    {
        Addressables.LoadAssetsAsync<Texture2D>(new List<string> { key, label }, null, Addressables.MergeMode.Intersection).Completed += TextureLoaded;
    }
    private void TextureLoaded(AsyncOperationHandle<IList<Texture2D>> texture)
    {
        _rawImage.texture = texture.Result[0];//这里List中只有一个值，所以直接用索引0
        _rawImage.SetNativeSize();
    }
}
