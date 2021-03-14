using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void OnClickLoadScene(int sceneId)
    {

        SceneManager.LoadScene(sceneId);
        //SceneManager.LoadSceneAsync(1, LoadSceneMode.Additive);
    }
    void Update()
    {
        
    }
}
