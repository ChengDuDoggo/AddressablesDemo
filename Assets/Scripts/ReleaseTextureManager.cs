using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.UI;

public class ReleaseTextureManager : MonoBehaviour
{
    public RawImage RawImg_Context;
    public Button Btn_LoadTexture;
    private void Start()
    {
        Addressables.InitializeAsync();
        Btn_LoadTexture.onClick.AddListener(LoadTexture);
    }
    private void LoadTexture()
    {
        Addressables.LoadAssetAsync<Texture2D>("Logo").Completed += (hel) =>
        {
            //赋值
            RawImg_Context.texture = hel.Result;
            //还原大小
            RawImg_Context.SetNativeSize();
            //释放资源(内存)
            Addressables.Release(hel);
        };
    }
}
