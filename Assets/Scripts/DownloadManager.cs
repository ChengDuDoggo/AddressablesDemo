using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.UI;
//检测更新并下载资源
public class DownloadManager : MonoBehaviour
{
    /// <summary>
    /// 显示下载状态和进度
    /// </summary>
    public Text updateText;

    /// <summary>
    /// 重试按钮
    /// </summary>
    public Button retryBtn;

    private AsyncOperationHandle downloadDependencies;

    /// <summary>
    /// 下载的文件的key
    /// </summary>
    //private string downLoadKey = "Logo";

    /// <summary>
    /// 下载多个文件列表
    /// PS：一个组内填写一个资源Key即可，下载时会按照资源组进行下载
    /// </summary>
    private List<string> downLoadKeyList = new List<string>()
    {
        "Cube","Logo"
    };

    //当前下载文件索引
    private int downLoadIndex = 0;
    //下载完成文件个数
    private int downLoadCompleteCount = 0;
    //下载每组资源大小
    private List<long> downLoadSizeList = new List<long>();
    //下载资源总大小
    private long downLoadTotalSize = 0;
    //当前下载大小
    private float curDownLoadSize = 0;
    //下载完成
    private bool isDownLoadFinished = false;

    private void Start()
    {
        downLoadIndex = 0;
        //重试
        retryBtn.onClick.AddListener(() =>
        {
            retryBtn.gameObject.SetActive(false);
            StartCoroutine(StartPreload());
        });
        //开始预下载
        StartCoroutine(StartPreload());
    }
    private void Update()
    {
        //实时监测下载是否有效,点击开始下载后不断执行更新下载信息,一有问题立即停止
        //downloadDependencies.IsValid():判断当前异步句柄是否还有效,例如此异步操作已经完成或者被异常取消终止,则该句柄不再有效
        if (isDownLoadFinished && downloadDependencies.IsValid())
        {
            curDownLoadSize = 0;
            for (int i = 0; i < downLoadSizeList.Count; i++)
            {
                if (i < downLoadCompleteCount)
                {
                    curDownLoadSize += downLoadSizeList[i];
                }
            }
            //如果已经下载的组的数目小于整个需要下载的组的数目就说明还没有下载完成
            if (downLoadCompleteCount < downLoadSizeList.Count - 1)
            {
                curDownLoadSize += downloadDependencies.GetDownloadStatus().Percent;
            }
            float percent = curDownLoadSize * 1.0f / downLoadTotalSize;
            //Debug.Log($"共{downLoadKeyList.Count}个文件，下载到第{downLoadCompleteCount}个文件，当前文件下载进度{downloadDependencies.GetDownloadStatus().Percent}，总下载进度{percent}。");
            if (percent < 1)
            {
                updateText.text = "正在下载：" + (percent * 100).ToString("F1") + "%";
            }
            else if (downloadDependencies.IsDone)
            {
                isDownLoadFinished = false;
                updateText.text = "下载完成";
                Debug.Log("下载完成 释放句柄");
                //下载完成释放句柄
                Addressables.Release(downloadDependencies);
            }
        }
    }
    /// <summary>
    /// 预下载
    /// </summary>
    /// <returns></returns>
    public IEnumerator StartPreload()
    {
        Debug.Log("开始下载");
        //初始化加载远端配置文件
        yield return Addressables.InitializeAsync();
        //清理缓存
        Caching.ClearCache();
        for (int i = 0; i < downLoadKeyList.Count; i++)
        {
            //获取下载内容大小,AsyncOperationHandle是异步操作句柄,是AA系统异步操作的通常返回值
            AsyncOperationHandle<long> size = Addressables.GetDownloadSizeAsync(downLoadKeyList[i]);
            Debug.Log("获取下载内容大小：" + size.Result);
            downLoadSizeList.Add(size.Result);
            downLoadTotalSize += size.Result;//下载的总大小
        }
        if (downLoadTotalSize <= 0)
        {
            Debug.LogError("无可预下载内容");
            yield break;
        }
        isDownLoadFinished = true;
        for (int i = downLoadIndex; i < downLoadKeyList.Count; i++)
        {
            downloadDependencies = Addressables.DownloadDependenciesAsync(downLoadKeyList[i]);//核心函数：根据资产Key开始异步下载资产
            yield return downloadDependencies;
            //如果下载的异步句柄状态为失败
            if (downloadDependencies.Status == AsyncOperationStatus.Failed)
            {
                downLoadIndex = i;//拿到下载失败的那个组的索引，点击重新预下载按钮时继续从这个组开始下载，就不必重新从0开始
                isDownLoadFinished = false;
                updateText.text = "下载失败,请重试...";
                retryBtn.gameObject.SetActive(true);
                yield break;//下载失败退出预下载协程
            }
            else
            {
                downLoadCompleteCount = i + 1;//下载成功的资产组数+1
            }
        }
        Debug.Log("下载完成");
    }
}
