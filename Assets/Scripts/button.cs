using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.SceneManagement;

public class button : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
   
    }
    public void Proximolevel()
    {
        SceneManager.LoadScene("LevelDois");
    }
}
