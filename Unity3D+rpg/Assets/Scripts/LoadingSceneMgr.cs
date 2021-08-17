using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadingSceneMgr : MonoBehaviour
{
    // DataBase
    GameObject dataBase;
    LoadGoogleSheet loadGoogleSheet;

    public static string nextScene;

    [SerializeField]
    Scrollbar loadingBar;

    public GameObject[] loadImage;

    public static void LoadScene(string sceneName)
    {
        nextScene = sceneName;
        SceneManager.LoadScene("LoadingScene");
    }

    // Start is called before the first frame update
    void Start()
    {
        dataBase = GameObject.Find("DataBase");
        loadGoogleSheet = dataBase.GetComponent<LoadGoogleSheet>();
        StartCoroutine(LoadScene());
    }

    public void RandomImageSet()
    {
        int maxImage = 4;

        int ImageValue = Random.Range(1, maxImage);

        for (int i = 0; i < maxImage - 1; i++)
        {
            loadImage[i].SetActive(false);
        }

        switch (ImageValue)
        {
            case 1:
                loadImage[0].SetActive(true);
                break;
            case 2:
                loadImage[1].SetActive(true);
                break;
            case 3:
                loadImage[2].SetActive(true);
                break;
        }

    }

    IEnumerator LoadScene()
    {
        yield return null;
        
        RandomImageSet();
        
        yield return new WaitForSeconds(3f);

        AsyncOperation op = SceneManager.LoadSceneAsync(nextScene);
        op.allowSceneActivation = false;

        float timer = 0.0f;
        while (!op.isDone)
        {
            int loadCount = loadGoogleSheet.LoadedCount;
            int listCount = loadGoogleSheet.ListCount;
            yield return new WaitUntil(() => loadGoogleSheet.LoadedCount == loadGoogleSheet.ListCount);

            timer += Time.deltaTime;
            if (op.progress < 0.9f) 
            {
                loadingBar.size = Mathf.Lerp(loadingBar.size, op.progress, timer); 
                if (loadingBar.size >= op.progress) 
                { 
                    timer = 0f; 
                } 
            }
            else
            {
                loadingBar.size = Mathf.Lerp(loadingBar.size, 1f, timer);
                if (loadingBar.size == 1.0f) { op.allowSceneActivation = true; yield break; }
            }
        }       
    }
}
