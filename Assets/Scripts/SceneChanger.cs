using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{

    public static SceneChanger instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(instance);
        }
        DontDestroyOnLoad(this.gameObject);
    }
    public void ChangeScene(string sceneName)
    {
        Debug.Log("Changing Scenes");
        SceneManager.LoadScene(sceneName);
    }

    public void LeaveScene()
    {
        SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene().buildIndex);
    }
}
