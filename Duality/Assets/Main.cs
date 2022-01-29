using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Main : MonoBehaviour
{
    
    void Start()
    {
        
    }

    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            //Application.Quit();
        }
    }
    private void FixedUpdate()
    {
        
    }

    public void StartLevel(int lvl)
    {
        SceneManager.LoadScene(lvl);
    }
    
}
