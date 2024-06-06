using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
public class LoadContent : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
      
    }
    public void cc()
    {
        StartCoroutine(DownloadScenes());   
    }
    private IEnumerator DownloadScenes()
    {
        AsyncOperationHandle downloadHandle = Addressables.DownloadDependenciesAsync("Scene", true);

        while (!downloadHandle.IsDone)
        {
            float progress = downloadHandle.PercentComplete;

            // progressBar.fillAmount = progress;
            Debug.Log("loading:" + progress);

            yield return null;
        }

        // Download completed, get bundle version
        yield return downloadHandle;
        // Get bundle version
        string bundleVersion = Addressables.GetDownloadSizeAsync("Scene").Result.ToString();
        yield return downloadHandle.IsDone;
        Debug.Log("concac:" + bundleVersion);
        // Common.LoadSceneAsync(GameScenes.MainMenu);
        Addressables.LoadSceneAsync("MainMenu");
       
    }

}
