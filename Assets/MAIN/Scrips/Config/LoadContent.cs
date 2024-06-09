using System.Collections;
using System.Collections.Generic;
using com.cyborgAssets.inspectorButtonPro;
using TMPro;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.UI;

public class LoadContent : MonoBehaviour
{
    // Start is called before the first frame update
    public Slider sliderLoad;
    public TMP_Text textCountPercent;
    void Start()
    {
       StartDownloadRSC();
    }
    [ProButton]
    public void ClearCache()
    {
        Caching.ClearCache();
    }
    [ProButton]
    public void StartDownloadRSC()
    {
        StartCoroutine(DownloadScenes());   
    }
    private IEnumerator DownloadScenes()
    {
        AsyncOperationHandle downloadHandle = Addressables.DownloadDependenciesAsync("MainMenu", true);
        Debug.Log("khoa 2:" + downloadHandle.IsDone);
        while (!downloadHandle.IsDone)
        {
            float progress = downloadHandle.PercentComplete*100f;
            string tempPercent = progress.ToString("F0") + "%";
            Debug.Log("loading:" + progress.ToString("F0") + "%");
            textCountPercent.text = "Loading " + tempPercent;
            sliderLoad.value = progress ;
            yield return null;
        }
        Debug.Log("khoa 1:" + downloadHandle.IsDone);

        yield return downloadHandle;

        string bundleVersion = Addressables.GetDownloadSizeAsync("MainMenu").Result.ToString();
        yield return downloadHandle.IsDone;
        Addressables.LoadSceneAsync("MainMenu");
       
    }

}
