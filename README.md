======================================================================================================================================================================
1.可寻址系统概念介绍

官方：Unity Addressable Asset System提供了一个可以随着您项目增长的系统，您可以通过最少的代码来实现这些。
例如，您可以从一组可寻址资产开始，Unity将其作为一组加载。然后，当你添加跟多内容时，您可以将资产分成多个组，以便在给定时间仅加载您需要的那些。
随着团队规模的扩大，您可以创建单独的Unity项目来开发不同类型的资产，这些辅助项目可以生成它们自己的Addressables内容构建，您可以从主项目加载这些构建，同样只需要最少的代码

关键特性：Addressable系统的一个关键特性是您可以为资产分配地址，并使用这些地址在运行时加载它们。Addressables资源管理器在内容目录中查找地址以找出资产的存储位置

Addressable系统管理的相关概念：
①资产地址：标识可寻址资产的字符串ID，您可以使用地址作为加载资产的密匙

②资产引用：可用于支持将Addressable资产分配给Inspector窗口中的字段的类型，您可以使用AssetReference实例作为加载资产的键，AssetReference还提供了自己的加载方法

③标签：您可以分配给多个资产并用于将相关资产作为一个组一起加载的标签，您可以使用标签作为加载资产的键

④资产位置：描述如何加载资产及其依赖项的运行时对象，您可以使用位置对象作为加载资产的键

⑤Key：标识一个或多个Addressable的对象，键包括地址、标签、AssetReference实例和位置对象

⑥资产的加载和卸载：AddressableAPI提供了自己的函数来在运行时加载和释放资产

⑦依赖项：资产依赖项是一个资产被另一个资产使用，例如场景资产中使用的预制体或预制体资产中使用的材质

⑧依赖和资源管理：Addressables系统使用引用计数来跟踪正在使用的资产和AssetBundle，包括系统是否应该加载或卸载依赖项（其他引用资产）

⑨组：您将资产分配给编辑器中的组，组设置决定了Addressables如何将资产打包到AssetBundle中，以及它如何在运行时加载它们

⑩内容目录：Addressables使用目录将您的资产映射到包含它们的资源

⑪内容构建：使用Addressables时，您可以在构建播放器之前将内容构建用于整理和打包您的资产作为单独的步骤

⑫多平台支持：构建系统分离平台构建的内容并在运行时解析正确的路径

⑬Addressables工具：Addressables包含多个窗口和工具，用于组织、构建和优化您的内容，默认情况下，Addressables使用AssetBundles来打包您的资产，您还可以实现自己的IResourceProvider类来支持访问资产的其他方式
======================================================================================================================================================================
2.可寻址系统工具目录介绍

https://blog.csdn.net/Czhenya/article/details/126679181

导入插件：Window -> Package Manager -> UnityRegistry，搜索Addressables
Addressables和Addressables.CN(中国版)的区别是：中国版新增了打包加密功能，若需要可以点击查看

导入插件后点击Window -> Asset Management ->Addressables看到可寻址系统的几个工具目录：

①Groups：资源组，组列表显示项目中的可寻址组，展开列表中的组以显示其包含的资产，例如Sprite表，以显示它们包含的子对象

②Settings：可寻址系统的各种设置

③Profiles：配置文件，配置文件包含一组可寻址构建脚本使用的变量，这些变量定义了诸如保存构建文件的位置以及在运行时加载数据的位置等信息，您可以添加自定义配置文件变量以在您自己的构建脚本中使用

④Event Viewer：事件查看器，可寻址系统使用Event Viewer窗口来监控资产的内存管理，此窗口可以显示应用程序何时加载和卸载资产，并显示所有可寻址系统操作的引用计数，此窗口还显示了应用程序帧速率和分配的托管内存总量的大致视图，我们可以使用这些图表
来检测可寻址事件（例如加载和释放资产）如何影响应用程序性能，并检测您从未释放的资产

⑤Addressables Report：资源报告工具，用来生成和查看Addressables资源的报告的。它提供了关于资源的详细信息，包括资源的引用情况、大小、类型等，可以帮助开发者了解项目中的资源使用情况，包括哪些资源被加载、哪些资源未被使用、哪些资源已经过期等等。
这对于优化项目大小、减少未使用的资源、提高运行效率非常有帮助。

⑥Analyze：分析工具，分析工具是一种收集有关项目的可寻址布局信息的工具，在某些情况下，Analyze可能会采取适当的措施来清理您的项目状态

⑦Hosting：托管服务，托管服务提供了一个集成工具，用于使用可寻址资产配置数据从Unity编辑器中将打包内容提供给本地或网络连接的应用程序构建，托管服务可以在测试打包内容时提高迭代速度，还可以为本地和远程网络上的连接客户端提供内容
======================================================================================================================================================================
3.可寻址系统工具面板属性介绍

https://blog.csdn.net/Czhenya/article/details/126679205

①创建分组

工程在导入后就会在window -> Asset Management -> Addressables中看到可寻址系统的几个工具目录
选择Groups，就会弹出Addressables Groups面板
单击上面的创建按钮，即可开始使用可寻址系统

创建后，会在工程Assets目录下面生成一个"AddressableAssetsData"资源文件夹，其中存储设置文件和用于跟踪你的Addressables设置的其他资产
AddressableAssetSettings：可寻址资产设置
Default Local Group：资源组设置
Packed Assets：资源组模板
BuildScriptFastMode、BuildScriptPackedMode、BuildScriptPackedPlayMode、BuildScriptVirtualMode：打包默认设置
ProfileDataSourceSettings：配置文件数据源设置

其中最常修改的是AddressableAssetSettings-可寻址资产设置
接下来，我们详细了解一下AddressableAssetSettings下的各种属性介绍：

Ⅰ.Profile-配置文件：
配置文件设置
Profile In Use：可下拉选择可以使用的配置文件列表选择配置文件
点击Manager Profiles按钮可以跳转到，Addressables Profiles面板，进行设置配置文件内容
一般用于测试服和正式服的环境切换，这样正式服和测试服配置信息就各自独立

Ⅱ.Catalog-目录：
可寻址目录相关的设置，此设置可将资产的地址映射到其物理地址
Player Version Override：覆盖用于指定远程目录名称的时间戳，若不设置，则使用默认时间戳
Compress Local Catalog：压缩本地目录，减少目录的存储大小，但增加了构建和加载目录的时间
Build Remote Catalog：此选项允许你从远程存储构建一个本地地址表，当你选择此选项时，Addressables会下载远程地址表到本地，然后你可以在本地编辑它，编辑后，你可以选择将更改推送到远程地址表或保存为本地副本
Only update catalogs manually：当你选中此选项时，Addressables不会自动从远程存储更新本地地址列表，只有当你手动点击更新按钮时，才会下载远程地址表

Ⅲ.Update a Previous Build-更新构建
控制远程内容构建和更新的设置
Check for Update Issues：选择是否将内容更新限制作为更新的一部分执行，以及如何处理结果
Content State Build Path：内容状态构建路径，可以设置默认构建脚本生成的内容状态文件路径

Ⅳ.Downloads-下载
下载设置，影响目录和AA包的下载处理设置
Custom certificate handler：自定义正式处理类
Max Concurrent Web Requests：最大并发Web请求，系统会将超出此限制的请求排队，建议同时下载2~4个以大到最佳下载速度
Catalog Download Timeout：目录下载超时时间，秒单位

Ⅴ.Build-构建
可修改所有构建相关设置
Build Addressables on Player Build：Unity构建是否自动构建AA包
---Use global Settings(stored in preferences)：使用全局设置（存储在首选项中），在打开Preferernces面板下的Addressables系统
---Build Addressables content on Player Build：在构建时始终构建AA包内容
---Do not Build Addressables content on Player Build：在构建时不自动构建AA包，选择此模式若需要更新，则需构建之前手动构建AA包
Ignore Invalid/Unsupported Files in Build：忽略构建中的无效/不受支持的文件，若启用则忽略无效和不支持文件，若不启用遇到无效文件则停止构建
Unique Bundle IDs：Bundles包唯一ID，是否在每个构建中为包生成唯一名称
Contiguous Bundles：连续Bundle包，生成一个更有效的Bundle布局
Non-Recursive Dependency Calculation：非递归依赖计算，当资源具有循环依赖关系时，启用此选项可缩短构建时间并减少运行时内存开销
Shader Bundle Naming Prefix：着色器包命名前缀，给Unity着色器生成的包添加前缀
MonoScript Bundle Naming Prefix：MonoScript Bundle 命名前缀，该Bundle包确保 Unity 在任何 MonoBehavior 可以引用它们之前加载所有 Monoscript。它还减少了重复或复杂的 Monoscript 依赖项的数量，从而减少了运行时内存开销
Strip Unity Version From AssetBundles：从AssetBundle中剥离Unity版本，是否从包头中删除Unity版本
Disable Visible Sub Asset Representations：禁用可见子资产表示，如果您不直接使用子对象（精灵、子网格等），启用此选项可缩短构建时间

Ⅵ.Build and Play Mode Scripts-构建和播放模式脚本
配置项目中可用的IDataBuilder脚本。如果您创建自定义构建或播放模式脚本，则必须先将其添加到此列表中，然后才能使用它
Addressables 包包含一些构建脚本，它们处理默认构建过程并提供在 Play 模式下访问数据的不同方式
您可以在 AddressableAssetData/DataBuilders文件夹中找到这些脚本
要添加自定义脚本，请单击+按钮并从文件面板中选择代表所需脚本的ScriptableObject资产

Ⅶ.Asset Group Templates-配置的组模板
定义可用于创建新组的模板列表。创建新模板时，必须先将其添加到此列表中，然后才能使用它
Addressables包包含一个模板，其中包含默认构建脚本使用的模式
您可以在AddressableAssetData/AssetGroupTemplates文件夹中找到该模板

Ⅷ.Initialization object list-初始化对象列表
为项目配置初始化的对象
这些对象都是ScriptableObject的对象，需要实现IObjectInitializationDataProvider接口
可以通过创建这些对象在Addressables初始化的时候传递数据

Ⅸ.Cloud Content Delivery-云内容交付
Enable Exerimental CCD Features：勾选则启用基本CCD功能

AddressableAssetGroup-可寻址资源组设置也是经常需要修改的设置
接下来，我们详细了解一下AddressableAssetGroup下的各种属性介绍：

Ⅰ.Content Update Restriction-内容更新限制
Update Restriction：更新的限制，可以选择：允许在发布后修改和不允许在发布后修改，一般是远程加载的资源组设置为允许更新，本地加载的设置为不允许更新

Ⅱ.Context Packing & Loading-打包和加载
Bulid & Load Paths：设置此资源组的打包和加载路径，可选择使用配置文件的本地，远程，或自定义路径
Advanced Options：高级选项
-Asset Bundle Compression：压缩格式，可选格式：LZ4，LZMA和不压缩三种，通常使用LZ4格式进行压缩(WebGl平台不支持LZMA压缩格式)
-Include In Build：是否在构建中包含此组，默认勾选，不勾选则此组内资源打不进包中
-Force Unique Provider：是否为此组使用资源提供者类的唯一实例
-Use Asset Bundle Cache：是否缓存远程分发的Bundle包
-Asset Bundle CRC：是否加载之前验证包的完整性
--Disable：禁用，从不校验
--Enabled，Including Cached：启用，包括缓存；始终在加载前进行校验
--Enabled，Excluding Cached：启用，不包括缓存；仅在下次时校验
-Use UnityWebRequest for Local Asset Bundles：使用从该组加载本地AssetBundle 档案UnityWebRequestAssetBundle.GetAssetBundle代替AssetBundle.LoadFromFileAsync
-Request Timeout：下载远程包的超时间隔
-Use Http Chunked Transfer：下载捆绑包时是否使用 HTTP/1.1 分块传输编码方法。在 Unity 2019.3以及之后的版本都已弃用
-Http Redirect Limit：下载资源包时允许的重定向次数，-1 表示没有限制
-Retry Count：重试失败下载的次数
-Include Addresses in Catalog：是否将地址写进catalog，如果这个组内的资源不需要通过地址加载，可以取消该项来减少catalog的体积
-Include GUIDs in Catalog：是否将GUID写进catalog，如果使用了AssetReferences，则必须勾选该选项。如果没有使用AssetReferences的方式或者用GUID作为key去加载，则可以取消勾选来减少catalog的大小
-Include Labels in Catalog：是否将Label写进catalog
-Internal Asset Naming Mode：在catalog里如何为资源命名
--Full Path：用完整路径
--Filename：资源的文件名
--GUID：用GUID串
--Dynamic：可寻址系统根据组内的资产选择最小的内部命名
-Cache Clear Behavior：什么时候清理缓存
--Clear When Space Is Needed In Cache：空间不足时清理
--Clear When When New Version Loaded：是有新版本时清理
-Bundle Mode：打包模式
--Pack Together：组内资源打一个Bundle包
--Pack Separately：组内资源每一个创建一个Bundle包
--Pack Together By Label：组内资源根据设置标签打Bundle包
-Bundle Naming Mode：创建bundle的名字
--Filename：文件名是从组名派生的字符串
--Append Hash to Filename：文件名+哈希值
--Use Hash of AssetBundle：使用AssetBundle的哈希值
--Use Hash of Filename：使用文件名哈希值
-Asset Load Mode：资源加载模式
--Requested Asset and Dependencies：加载请求的资源和依赖（通常使用）
--All Packed Asset And Dependencies：加载所有打包的资源和依赖项
-Asset Provider：定义 Addressables 使用哪个 Provider 类从该组生成的 AssetBundle 加载资产,将此选项设置为来自 Bundles Provider 的 Assets，除非有自定义 Provider 实现来提供来自 AssetBundle 的资产
-Asset Bundle Provider ：定义 Addressables 使用哪个 Provider 类来加载从该组生成的 AssetBundle，将此选项设置为AssetBundle Provider，除非您有自定义 Provider 实现来提供 AssetBundle
======================================================================================================================================================================
4.可寻址系统工具面板操作流程介绍

https://blog.csdn.net/Czhenya/article/details/126782149

Ⅰ.Groups-资源组

①创建组：打开Addressables Group窗口，然后点击工具栏中的Create，即可打开Groups窗口
②创建新组：创建组成功后，会自带一个默认组（Default Local Group），在左上角New的下拉菜单中选择模板创建新组，或者在标签页右键空白处可选Create Group创建新组
③操作组：选择想要操作的组，右键可以看到可操作的选项有：移除组、简化名称、设为默认组、查看组设置面板、重命名、创建新组
④添加资源到组：在Project中选择需要添加的资源然后拖拽到组中即可
⑤操作组内资源：选中组内资源，右键可以看到操作选项有：移动到现有组、移动到新建组、简化名称、复制资源名称到剪切板、修改资源、创建新组
⑥标签：在组内资源的最后一栏中可以添加标签，要分配标签，请选中或取消选中所需标签的复选框。单击左上角的加号按钮，然后单击管理标签以添加、删除或重命名您的标签
标签的作用在于一个组内的分类，当我们打Bundel包时 ，一个Group会打成一个包，若我们将组内资源设置为不同的标签，在设置组的打包方式为Pack Together By Layer，可寻址系统就会以Label为颗粒细分成多个.bundle，使得组更加灵活
⑦工具栏：资源组的工具栏包括：打开系统设置、检测内容更新限制、窗口（配置文件、标签页、分析工具等），组视图可以设置组窗口显示选项
⑧加载模式：可通过设置在编辑进行模拟远程加载，三种方式分别为：Fast Mode -> 研发阶段、Virtual Mode -> 本地模拟、Packed Play Mode -> 正式打包
⑨构建脚本：选择执行构建命令：创建一个新的构建（打新资源包），更新以前的构建（热更资源包），清理构建生成文件

Ⅱ.Settings-设置

Settings：可寻址系统的各种的和单一的组的各种设置详细介绍都在上篇文章写了，请自行查看

Ⅲ.Profiles-配置文件

打开Profiles窗口（Window -> Asset Management -> Addressables -> Profiles）
Profiles其实就是配置文件打包和加载使用的路径的

①Local：为本地内容定义两个路径变量
	-Local.BuildPath：设置使用此本地打包资源的保存路径
	-Local.LoadPath：加载应用程序本地安装的资产位置
②Remote：为远程内容定义两个路径变量
	-Remote.BuildPath：设置使用此远程打包资源保存路径
	-Remote.LoadPath：从中下载远程内容和目录的URL
③BuildTarget：构建目标的名称，例如Android或IOS等
④新建配置：可以通过右键删除、修改名称，右侧面板可以通过选择Custom来自定义各个加载地址和构建目标
⑤更换配置文件的两种方式：
	-在资源组中选择需要的的配置文件
	-在设置面板中选择需要的配置文件

⑥配置变量的语法：
	-大括号{}：可寻址对象在运行时评估大括号包围的条目。可以使用运行时类的代码变量（例如，{UnityEngine.AddressableAssets.Addressables.RuntimePath}）
	-中括号[]：在构建时评估被方括号包围的条目。这些条目可以是其他配置脚本文件变量（例如，[BuildTarget]）
一个示例：
若你有很多的平台需要修改配置文件则可以这样写：
Remote.LoadPath：[BuidleEditor.RemoteLoadPath] -> 对应下面代码中的变量：

using UnityEditor;
public class BuidleEditor : Editor
{
#if UNITY_ANDROID
	public static string RemoteLoadPath = "https://android/Czhenya";
#else
	pubilc static string RemoteLoadPath = "https://blog.csdn.net/Czhenya";
#endif
}

PS：尽量不要修改本地路径（Local.BuildPath和Local.LoadPath），可寻址系统会在打包时自动从Addressables.BuildPath复制到StreamingAssets文件夹。若修改了则需要手动复制后再重新打包

Ⅳ.Event Viewer-事件查看器

打开面板：Window -> Asset Management -> Addressables -> Event Viewer
开启方法：在可寻址系统的设置面板下的Diagnostics属性中，勾选Send Profiler Events选项，然后允许程序，即可在Event Viewer窗口看到如下现象：【图片】
可以用此窗口监控内存的使用情况，此窗口可以显示应用程序何时加载和卸载资产，并显示所有可寻址系统操作的引用计数

PS：我这里用代码加载了两个物体，加载代码如下：

using UnityEngine;
using UnityEngine.AddressableAssets;

public class LoadManager : MonoBehaviour
{
    private void Start()
    {
        LoadGameObject("Assets/Prefabs/Cube.prefab", Vector3.zero);
        LoadGameObject("Assets/Prefabs/Sphere.prefab", Vector3.zero);
    }
    private void LoadGameObject(string loadStr, Vector3 pos)
    {
        Addressables.LoadAssetAsync<GameObject>(loadStr).Completed += (obj) =>
        {
            GameObject go = obj.Result;
            Instantiate(go, pos, Quaternion.identity);
        };
    }
}

Ⅴ.Analyze-分析工具

分析工具是一种收集有关项目的可寻址布局信息的工具。在某些情况下，Analyze可能会采取适当措施来清理您的项目状态
Window -> Asset Manangement -> Addressables -> Analyze打开工具窗口

分析窗口显示分析规则列表包括以下操作：
	-分析选定的规则
	-修复选定的规则
	-清除选定的规则
Check Duplicate Bundle Dependencies（检查重复Bundle包依赖）：此规则会扫描所有组并计算资源组布局来检查可能重复的资源，这需要一个完整的打包过程进行检查
在打过一次资源包后，右键运行即可得到检测结果，由于我这里资源组少且没有依赖关系，所以显示的是：No issues found

Ⅵ.Hosting-托管服务

托管服务提供了一个集成工具，可以在本地模拟使用服务器功能的工具

新添加的服务出现在Addressables Hosting窗口的Hosting Services部分，使用服务名称字段输入服务的名称
新服务默认为禁用状态，选中Enable开启本地服务。要选择不同的端口，请在“端口”字段中分配特定的端口号，或单击“重置”按钮分配不同的随机端口号

使用示例：创建并开启本地服务后，在Profiles配置文件中使用本地服务为远程加载地址，最后设置资源包加载方式使用刚刚设置的路径即可实现模拟远程加载：【图片】
版本适配：2022.1之后的版本，默认情况下不允许HTTP下载。为了使默认HTTPHostingService设置正常，需要在Edit -> ProjectSetting -> Player -> Other Settings -> Allow downloads over HTTP中将Allow downloads over HTTP设置为Not allowed以外的其他选项
======================================================================================================================================================================
5.可寻址系统代码动态加载物体

https://blog.csdn.net/Czhenya/article/details/126873219

Ⅰ.可寻址系统代码加载

准备工作：创建几个预制体分别为：Cube、Capsule、Sphere，并将预制体设置为可寻址系统资源，然后将Cube的地址修改为“Cube”（资产栏的第一栏就是一个资产地址）

①三种回调加载物体形式：

using System;
using UnityEngine;
using UnityEngine.AddressableAssets;//引用命名空间
using UnityEngine.ResourceManagement.AsyncOperations;

public class LoadManager : MonoBehaviour
{
    private void Start()
    {
        //三种加载回调形式
        LoadGameObject();
        LoadGameObjectCallBack();
        InstantiateGameObject();
    }
    #region 加载物体的三种回调形式
    /// <summary>
    /// 加载物体1
    /// 逻辑简单且不需要复用，直接使用Lambda表达式的形式
    /// </summary>
    private void LoadGameObject()
    {
        Addressables.LoadAssetAsync<GameObject>("Cube").Completed += (obj) =>
        {
            GameObject go = obj.Result;
            Instantiate(go, Vector3.zero, Quaternion.identity);
        };
    }
    /// <summary>
    /// 加载物体2
    /// 采用手动编写一个回调函数
    /// </summary>
    private void LoadGameObjectCallBack()
    {
        Addressables.LoadAssetAsync<GameObject>("Assets/Prefabs/Sphere.prefab").Completed += LoadCallBack;
    }
    /// <summary>
    /// 回调函数
    /// </summary>
    /// <param name="handle">拿到加载的物体的相关信息</param>
    /// <exception cref="NotImplementedException"></exception>
    private void LoadCallBack(AsyncOperationHandle<GameObject> handle)
    {
        GameObject go = handle.Result;
        Instantiate(go, Vector3.right * 2, Quaternion.identity);
    }
    /// <summary>
    /// 加载物体3
    /// 直接加载并实例化物体
    /// </summary>
    private void InstantiateGameObject()
    {
        Addressables.InstantiateAsync("Assets/Prefabs/Capsule.prefab").Completed += (obj) =>
        {
            //已经实例化后的物体
            GameObject go = obj.Result;
            go.transform.position = Vector3.left * 2;
        };
    }
    #endregion
}

运行结果如下：成功加载三个预制体

②异步等待加载方式：

若你不习惯回调的形式，可寻址系统也给了我们加载异步等待的形式直接去加载物体，示例代码如下：

using System;
using UnityEngine;
using UnityEngine.AddressableAssets;//引用命名空间
using UnityEngine.ResourceManagement.AsyncOperations;

public class LoadManager : MonoBehaviour
{
    private void Start()
    {
        AsyncLoadCube();
        AsyncInstantiateCube();
    }
    #region 异步形式
    /// <summary>
    /// 此段代码演示了异步加载资源，使用await关键字等待加载完成
    /// 通过这种方式，可以在不阻塞主线程的情况下加载资源，提高游戏性能
    /// </summary>
    private async void AsyncLoadCube()
    {
        //async和await是绑定的
        //await和Task是绑定的
        //虽然这里使用了Task，但并没有使用多线程
        //Task表示一个异步操作，可以通过await关键字等待它加载完成
        //只有等待加载完成了才会返回加载结果，这就是异步加载资源
        GameObject prefabObj = await Addressables.LoadAssetAsync<GameObject>("Cube").Task;
        //实例化
        GameObject cubeObj = Instantiate(prefabObj);
        cubeObj.transform.position = Vector3.zero;
    }
    private async void AsyncInstantiateCube()
    {
        GameObject cubeObj = await Addressables.InstantiateAsync("Cube").Task;
        cubeObj.transform.position = Vector3.right * 2;
    }
    #endregion
}

异步加载关键字：async-->await(等待加载完成)-->Task(异步返回值)

③同步加载

在1.17.4版本之后的可寻址系统，通过AsyncOperationHandle的WaitForCompletion方法来实现同步加载，WaitForCompletion的作用会让系统阻拦代码的执行，直到资源加载完成
同步加载GameObject的基本用法，代码如下：

using Unity.VisualScripting;
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

多数情况下同步加载的性能和异步加载基本一致
WaitForCompletion运算必须在引擎的任务队列中依次完成，如果小型运算的前面有一些大型运算，则只有前面完成后系统才能完成队列后方的运算，这种情况会出现加载慢的现象

添加通用路径不存在的异常校验：

/// <summary>
/// 可寻址资源加载
///     强制同步加载GameObject的基本用法
/// </summary>
/// <param name="pathKey">路径key</param>
/// <typeparam name="T"></typeparam>
/// <returns></returns>
public static T Load<T>(string pathKey)
{
    var op = Addressables.LoadAssetAsync<T>(pathKey);
    if (op.Status == AsyncOperationStatus.Failed)
    {
        Debug.LogError($"加载资源失败，路径：{pathKey}，异常 {op.OperationException}");
        return default(T);
    }
    T go = op.WaitForCompletion();
    return go;
}

④面板拖拽赋值方式

在代码中声明AssetReference资产引用类型的变量，将上面准备好的Cube拖拽上来进行赋值，若此时拖拽上来的没有加入Addressables系统的资源，它会自动变成一个可寻址系统资产

示例代码如下：

using UnityEngine;
using UnityEngine.AddressableAssets;

public class LoadManager : MonoBehaviour
{
    public AssetReference cubeRef;
    private void Start()
    {
        RefLoadCube();
    }
    #region 面板拖拽方式加载
    private void RefLoadCube()
    {
        cubeRef.LoadAssetAsync<GameObject>().Completed += (obj) =>
        {
            //加载完成结果
            GameObject cubePrefab = obj.Result;
            //实例化
            GameObject cubeObj = Instantiate(cubePrefab);
            //修改位置
            cubeObj.transform.position = Vector3.zero;
        };
    }
    #endregion
}

PS：如果我们声明的不是AssetReference类型，而是我们常用的GameObject类型，那么场景就直接依赖了Cube预制体，打包时Cube预制体就会被打到场景中。现在，这里使用的是AssetReference类型，它是一个弱引用，不会真正依赖Cube预制体

总结四种加载资产方式：
①通过资产位置地址进行回调加载
②通过资产位置地址进行异步加载(不会阻塞主线程)-----------------------异步加载适用于加载一些不重要的装饰场景时，即便没有加载完毕，玩家仍然可以移动，不会影响主线程
③通过资产位置地址进行同步加载(必须排队前面的程序队列运行完毕)------同步加载适用于一些重要的资源加载，例如游戏的进度条，进度条没有加载完毕，主线程就不会继续向前，玩家也就不能移动
④通过资产地址引用进行拖拽加载

Ⅱ.可寻址系统分标签加载

①场景搭建

创建一个RawImage和一个Button摆放位置如下图：【图片】
找两张图片将其放入到工程中，并将其设置为可寻址资源，然后将地址都修改为Logo，最后分别为其创建不同的标签Animal，Plant设置如下：【图片】

创建如下代码并将其挂载到上面创建的RawImage物体上，并初始化如下：【图片】
下面的代码，分别使用了AssetLabelReferebce面板赋值的形式和寻址+标签两种形式进行了分标签加载代码的示例，根据实际情况决定使用哪一种形式即可

代码示例：

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
        loadPlantLogoBtn.onClick.AddListener(() =>
        {
            LoadTextureByKeyLabel("Logo", "Plant");
        });
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

根据寻址和标签加载的代码中"Addressables.MergeMode"：用于合并请求结果。可以理解为，可寻址系统依次从key和label加载两组结果，合并结果有四个枚举值：若查询结果分别为：A,B,C和B,C,D
①None和UseFirst都会取第一组结果：A,B,C
②Union取两组结果的并集：A,B,C,D
③Intersection取两组结果的交集：B,C

Ⅲ.代码加载可寻址的解释

代码加载逻辑基本上就这几个，写法也都在上面。逻辑和使用AB包加载逻辑基本一致，只是写法略有不同，所以稍微有经验的同学，应该可以很快上手，只需要熟悉写法即可
上面的代码加载用的都是key加载的，我们也可以使用它的绝对路径加载，比如上面的"Cube"，可以这样写：

Addressables.LoadAssetAsyn<GameObject>("Cube")或Addressables.LoadAssetAsyn<GameObject>("Assets/Prefab/Cube.prefab")

使用key的方式加载的好处在于不管我们之后如何修改路径或者修改预制体名称，都不会影响到上面代码的加载逻辑，这就是可寻址
只要key不修改就不用修改代码，若AddressablesGroups中存在相同的key，则代码会返回列表中靠上的资源
若资源不在本地而在服务器上，可寻址系统会帮助我们先下载等待下载完成在执行后面的加载逻辑，所以代码上也不需要有任何的修改！

除了我们常用的资源类型（预制体、贴图、音频、配置文件）这些可以使用可寻址代码加载，场景也可以使用可寻址代码加载
代码如下：

Addressables.LoadSceneAsync("Assets/Scenes/Game.unity");

在后面介绍资源更新的文章中会介绍一个检测更新的脚本，用以游戏开始前下载我们需要的资源，这样就不会再等到使用时现下载，也就解决了资源过大或者网络稳定的情况下，下载卡顿的问题
======================================================================================================================================================================
6.可寻址系统资源的加载和资源的释放

https://blog.csdn.net/Czhenya/article/details/128219265

Ⅰ.资源加载

①同步异步对比：

同步：是指一个进程在执行某个请求的时候，如果该请求需要一段时间才能返回信息，那么这个进程会一直等待下去，直到收到返回信息才继续执行下去
异步：是指进程不需要一直等待下去，而是继续执行下面的操作，不管其他进程的状态，当有信息返回的时候会通知进程进行处理

举个例子：你打游戏口渴了想喝水

同步：暂停游戏 -> 下楼买水 -> 找附近操作 -> 付款喝水 -> 回来继续游戏
异步：游戏继续 -> 美团下单 -> 继续游戏 -> 货到喝水 -> 继续游戏

②实战中同步异步的对比：

在一般加载中我们通常使用的：Instantiate来实例化预制

同步实例化问题：
	-会等到实例化结束才继续运行
	-大量加载容易造成卡顿

使用AA系统时，使用InstantiateAsyns替换Instantiate：

异步实例化：
	-系统不会等待
	-调用完成时回来继续执行
	-大量实例化不会卡住系统

③资源的三种加载模式

Addressables资源加载模式有三个
如下图，默认情况下是Use Asset Database(fastest)
	-Use Asset Database(fastest)：快速，直接加载文件而不打包，一般在开发时使用此模式
	-Simulate Groups(advanced)：在不打包的情况下模拟AssetBundle的操作
	-Use Exising Build：实际上是从AssetBundle打包和加载。需要先通过Build打包，才能使用，也可以加载本地和远程Bundle

三种模式的使用时机：
	-Fast Mode -> 研发阶段
	-Virtual Mode -> 本地模拟
	-Packed Play Mode -> 正式打包

Ⅱ.资源释放

释放资源 != 销毁资源
Destroy != Release

①基础概念：
	-资源释放不影响场景中实例化出的对象，但是会影响非实例化的资源（材质、音效等）
	-释放后的资源，再次使用需要重新加载
	-资源释放后，AssetReference的资源引用(Asset)会清空，但AsyncOperationHandle类的资源引用(Result)不为空

②实例演示：

在Window -> Asset Management -> Addressables -> Event Viewer打开工具面板
找到Addressables的设置面板，开启Send Profiler Events，即可在Event Viewer面板上看到资源的使用情况了
此工具可以直观的看到，程序运行时的资源引用

可寻址资源组还是用的前面文章中创建的：此处用到一个Cube，一个Logo，若你没看过之前的问题，新建一下这两个即可

③实例演示一：

使用InstantiateAsync实例化物体：
搭建测试场景，创建三个按钮分别为：实例化资源(加载资源)、删除实例化(销毁资源)、释放资源

实例代码：

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

打开Event Viewer工具实时查看内存使用情况我们可以看到：
当加载一个Cube时，内存中多了一个预制体占用内存情况
当Destroy销毁这个Cube时，场景中的Cube虽然不见了，但是Cube仍然在内存中占用着内存！说明Cube的内存引用并没有被销毁
当Release释放这个Cube时，内存中的Cube预制体的引用占用也被清空了！Nice！Baby！

在实战中，如果想要销毁对象并同时释放对象内存，通常使用Addressables.ReleaseInstance(Cube)函数来释放并销毁实例化的物体，此函数通常用于实例化到场景中的GameObject对象

总结：想要真正销毁一个实例化对象的内存引用，从内存中将实例化对象的引用消除需要使用Release释放资源，而不是单单使用Destroy只是删除场景中的对象，对象的依赖还仍然保留在内存中

④实例演示二：

使用LoadAssetAsync加载图片资源：
创建一个Button用来触发加载，一个RawImage用来接收显示加载的图片，还有一个名Manager空物体用来挂载脚本：

代码如下：

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

通过Event Viewer我们可以看到，在加载了图片后我们立即释放掉它的内存，图片并没有任何变化，消失，但是图片的引用在内存中被立即清空了，提高了性能而没有影响功能！
Release不会影响到AA系统加载的资源的Result，只会清空加载的资源的引用

Ⅲ.注意事项

AA系统中的资源释放有两个方法，一个是Release，另一个是ReleaseInstance。两个方法分别用于不同的情况：
	-Release释放不需要实例化在场景中的GameObject资源
	-ReleaseInstance释放需要实例化在场景中的GameObject资源

一个错误的使用示范：

使用LoadAssetAsync加载然后Instantiate实例化的，是不能通过ReleaseInstance来进行释放资源的，因为Instantiate不是AA系统的函数，只有由AA系统的函数加载实例化的对象能够通过AA系统的释放函数来释放掉
而通过Release来释放也只能释放由AA系统的LoadAssetAsync函数加载的handle对象，而不能释放Instantiate函数加载的Cube对象

Addressables.LoadAssetAsync<GameObject>("Cube").Completed += (hal) =>
{
    Cube = Instantiate(hal.Result);
    handle = hal;
};

// 这么写释放不掉
//Addressables.ReleaseInstance(Cube);//Cube不是AA系统加载的
        
// 这么写会报错
Addressables.Release(Cube);//Cube不是AA系统加载的

// 正确释放方式
Addressables.Release(handle);//handle(hal)是AA系统加载的
Destroy(Cube);

总结：
①同步和异步的区别
②使用AA系统时，使用InstantiateAsyns替换Instantiate函数来实现异步加载实例
③AA系统的三种资源加载模式
④释放资源的概念：Release将一个对象的内存引用释放掉，并不影响已经加载的对象，优化内存
⑤Destroy只是删除了场景中的物体，并没有销毁内存中的引用
⑥Release和ReleaseInstance的区别：Release是单纯释放对象的引用，通常用于非在场景中实例化的GameObj。ReleaseInstance会先Destroy场景中的物体再释放对象的引用，通常用于需要在场景中实例化的GameObj
⑦只有通过AA系统函数加载出来的对象（InstantiateAsyns、LoadAssetAsync等）可以直接使用AA系统的Release或ReleaseInstance来释放资源
======================================================================================================================================================================
7.可寻址系统资源远程加载和资源预下载

实现方式：可寻址系统（AA）+云资源分发（CCD）的形式实现
!：CCD已经修改为UOS！

Ⅰ.Unity云资源分发(CCD)使用介绍

①概述：

在Hub界面的游戏云选项，可以看到官方介绍入口（https://unity.cn/product/cloud-content-delivery）
CCD全称Cloud Content Delivery，译为：云端资源分发
Unity 推出首个用于实时游戏更新的端到端服务：专为游戏开发打造的内容分发网络 (CDN) 和后端即服务 (BAAS)
Unity最新的在线资源更新服务，结合革新性的Addressable Assets资源管理系统帮助开发者借助云端强大的资源管理和内容分发能力，轻松制作和发布游戏更新

②后台准备工作：

使用前需要在Unity后台创建项目并开启相应服务，流程如下：

--!!!!!!!--：由于是老教程，如今的Unity似乎将CCD服务封装进了UOS（Unity一站式游戏云服务）教程中的页面、操作和步骤等等似乎全部失效了...

只有通过我自己的探索：

通过我自己的探索得出结论：Unity已经将CCD云资源分发替换为了UOS一站式游戏云服务中

我们可以根据官方文档进行CDN服务或其他服务进行详细了解学习：https://uos.u3dcloud.cn/doc/cdn

登陆UOS服务后，我们可以直接创建一个CDN服务，创建后我们可以得到一个CDN服务器用来存储上传远程资源

在创建了CDN服务器后我们可以点击“设置”可以看到自己的CDN的App ID和App Secret用来与Unity的官方UOS插件进行连接绑定

点击CDN选项卡下的“Bucket”桶，可以创建一个“桶”是专门用来存储上传的Bundle资产文件的

在创建的“Bucket”桶中可以点击拖拽上传文件和创建新的发布版本分支“Release”

这样我们基本就完成了远程资产服务端（CDN）的基本准备工作

③UOS CDN的使用

UOS CDN是Unity项目连接CDN服务器的可视化面板工具，可以非常方便的完成前端和后端的连接，直接在Unity端完成资源上传，桶的创建等等

安装包：https://uos.u3dcloud.cn/doc/cdn/package

将插件导入到Unity中后，首先点击Edit -> Project Settings -> Unity Online Service进入后将自己创建的CDN服务器中的App ID和App Service Secret填入字段进行Unity和CDN服务器的连接

然后点击Window -> Unity Online Service -> CDN -> Manager就可以打开UOS CDN插件的管理窗口进行使用

CDN的主要功能分为如图所示的四个部分：Bucket、Entry、Release、Badge

a.存储桶(Bucket)：
	-按钮New，用于新建Bucket，点击后在弹窗中填写Bucket Description和Bucket Description，单击Create完成创建
	-按钮Load，用于加载已经存在的Bucket
	-按钮Delete，用于删除选中的Bucket
b.条目(Entry)：
	-按钮Choose用于选择路径，先选好Bucket后，点击选择要上传的文件所在的路径
	-按钮Sync用于上传文件，点击后可在弹窗中看到上传文件的数量、大小以及上传速度。Sync包含了新建、更新和删除操作(删除需要点击Window -> Unity Online Service -> CDN -> Settings里面勾选sync with delete)
	-按钮Load，用于加载Entry，点击后会加载当前Bucket中的所有Entry
	-按钮Upload，用于上传单个文件，点击选择需要上传的文件，不受所选路径的影响
c.发布版本(Release)：
	-按钮New，用于新建Release，Entry上传成功后，点击完成创建
	-按钮Load，用于加载Release，点击后加载目前所有的Release，选中的Release可在Badge部分为其创建Badge
	-按钮Promote，可将当前Bucket中选中的Release投放到其他Bucket中
d.标识(Badge)：
	-按钮New，用于新建Badge，点击后在弹窗中填写Badge Name，单击Create为选中的Release创建一个Badge
	-按钮Update，用于更新指向，可将当前Badge指向选中的Release
	-按钮Load Url(Badge)可用于配置下载路径，显示基于当前选中的Badge所构建的Url
锁定使用latest badge构建Url，可在Window -> Unity Online Service -> CDN -> Settings里面勾选use latest badge

注意事项：Badge是可以切换标记Release的，若用户通过Badge来访问资源，则可以通过Badge的切换来实现访问不同的资源，这时访问资源的URL是不需要更换的

不同的版本Release代表着不同的资源，可以自定义切换需要的资源包，配合Badge标识使用，可以根据标识Badge来加载指定的版本Release来实现自定义加载需要的资源包

在使用release的promote功能时，务必使两个bucket对应的addressable的Settings（Window -> Asset Managment -> Addressables -> Settings）中Catlog选项下的Player Version Override的值保持一致且不为空！
因为Catalog的配置文件名称是和这个版本对应的，所以不能对不上

④Set Addressables Profile按钮

a.Set Addressables Profile 按钮的作用是什么?

会将 Addressables Profile 里面的 Active Profile 的 Remote Load Path 值设置为 {UOS.cdn_url}/?path=
会在 Assets/AddressableAssetsData 目录下生成一个 cdnurl.cs 文件，内容如下
class UOS { public static string cdn_url = '...'; }
通过这种方式，可以更新 cdnurl.cs 文件来改变 Addressables 的资源加载地址，从而减少 Addressables 资源打包的次数

b.什么时候需要点 Set Addressables Profile 按钮?

初次使用时，要在选择好 bucket 之后，进行 Addressables 资源打包之前点击
切换 bucket 或者 badge 之后，需要点击按钮，以更新 cdnurl.cs 文件
切换 Addressables 的 Active Profile 之后，需要点击按钮，以更新 Active Profile 的 Remote Load Path 值

Ⅲ.AA+CDN资源更新-实例练习

①打包设置资源地址

打包地址分为：本地路径和远端路径，这两个路径都在Addressables Profies面板显示
这个地方设置完成之后，就可以分别对每个组进行设置是远程组还是本地组了
将Build Load Path设置为Remote，就已将本组设置为远程组了，此时再次打资源包，此组的Bundle包就会在远程组设置的Build路径下显示了，而不会打到安装包里，当游戏中使用该组中的资产时，也是远程进行加载的
修改完成后就可以打资源包了：在Groups面板找到Build选项，进行打资源包
等待进度条执行完成，即可看到打包成功日志
然后就可以进行下一步骤将Bundle包上传到CDN服务器了

②预下载获取下载进度

基础如下：

总大小：获取所有下载组的大小
当前大小：获取当前下载进度 + 已下载组大小
进度：当前大小 - 总大小

需要使用Addressables系统的"Download"相关API

下载场景搭建：
一个下载管理器
一个下载进度按钮和文字

预下载逻辑实现DownloadManager.cs：

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
======================================================================================================================================================================
8.可寻址系统资源热更新

Unity可寻址系统的资源热更是我遇到过最简单的热更方式了。只需修改资源组然后发布资源热更即可。本篇文章就来为讲解AA的资源热更，并通过CCD实现资源热更完整流程

Ⅰ.本地实现资源热更

①概念：

Addressables 将资源的引用和打包分开处理，可加快运行模式下和运行版本的项目迭代。系统将资源合并为一个个的 Asset Bundles（资源包），一种可在运行时分发、加载资源的 Unity 专有文件结构，然后生成一个内容目录来辅助运行时的内容加载与资源跟踪

Addressable的资源热更，特别适合那种边下边玩的游戏，因为它不是在游戏刚进去时更新完所有资源

我们把下载AB的实现交给了Addressable，它的实现是当你在加载资源时找到这个资源的ab包，然后通过UnityWebRequestAssetBundle判断该AB包是不是已经下载如果下载那么直接从缓存目录加载，不然就下载到缓存目录再加载。所以我们要先加载资源才会去下载ab包

若需要进入游戏就下载所有资源可以参考我上一篇文章写的下载工具类

②具体实现步骤：

a.准备两个预制体
b.创建一个资源组，将一个预制放到组中
c.编写代码加载资源组，将其生成到场景中
b.资源地址设置，打资源包
e.发布查看是否正常加载运行
f.将资源组中的预制替换，发布本地资源热更
g.再次运行查看是否更换

③本地资源热更操作：

a.准备两个预制体，我这里准备了两个不同材质的Cube

b.创建一个资源组，资源组命名为：HotUpdate Group，将一个预制体放到组中，资源的Key命名为：HotUpdateCube

c.编写代码加载资源组中的资源，将其生成到场景中，创建一个空物体挂载代码，运行查看效果
代码如下：

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

d.资源地址设置，打资源包，将本地地址都修改为自定义，注意地址要在根目录下开始写（Windows就在盘符开始）
选中创建的资源组将其Inspector面板上设置为Local
打资源包Build -> New Build -> Default Build Script
此时运行项目就可以看到我们的资产加载到场景中了

e.将资源组中的预制替换，发布本地资源热更
将需要替换的预制也放到刚刚的资源组中，然后Key修改的和被替换的资源一样
移除被替换的资源（注意，这里的资源和2步骤中的Key保持一致，因为代码是根据这个Key来加载的）
点击发布资源热更：Groups -> Build ->Update a Previous Build
热更完成后，可以在刚刚的文件夹下看到两个前缀为hotupdategroup的bundle文件了
再次运行，可以看到白色的Cube变为了黑色的

这样我们就实现了本地资源包的资源热更新！

其实，联网热更也是一样的道理，只需要将上面的配置加载资源地址修改为在网络加载，再将打包出来的资源上传到CDN上即可

Ⅱ.AA+CDN实现资源热更

a.创建Bucket桶
在后台创建一个新的桶来实现资源热更：我这里命名为：AA+CND-HotUpdate

b.打包设置
点击Set Pre-defined Remote Load Path设置远程加载路径

c.打包资源
设置完成后，进行资源打包Build -> New Build -> Default Build Script（注意，将资源组的打包加载路径设置为Remote）

d.上传资源
选择刚刚创建的AA+CND-HotUpdate桶，选中打包的资源路径文件夹，点击Sync上传

e.新建版本
点击New新建一个版本然后Load同步到后台
上传完成后可以在后台看到刚刚上传的版本

此时运行加载你可以看到可以加载完成刚刚上传到远端的黑色Cube资源

f.实现热更
和热更本地资源一样，新修改资源组内容，将白色Cube把黑色Cube进行替换（注意！Key一定不能变更！）

g.生成热更补丁
点击Build -> Update a Previous Build和热更本地资源一样，此时你会发现一个新的bundle文件生成到了打包路径

h.上传热更补丁
和上传资源一模一样，选中打包路径文件见 -> 点击Sync上传文件 -> 新建版本 -> 发布资源

i.点击运行，发现资源Cube资源热更为了白色Cube

Addressables系统的热更新核心流程：
本地：创建组 -> 拖入资源 -> 设置打包路径 -> 打包 -> 拖入新的需要替换的热更资源 -> 替换原有资源(核心：Key不能改变，一定要一模一样) -> 热更(Update a Previous Build)
远端：创建组 -> 拖入资源 -> 设置打包路径 -> 打包 -> 创建远端Bucket桶、版本、标签等 -> 设置打包路径到远端 -> 上传资源包 -> 拖入新的需要替换的热更资源 -> 替换原有资源(核心：Key不能改变，一定要一模一样) -> 热更(Update a Previous Build) -> 上传新资源包
======================================================================================================================================================================
