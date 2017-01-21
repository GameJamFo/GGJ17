using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour {
	
	void Update () {
		if(Input.GetKeyDown(KeyCode.Space))
        {
            LoadGame();
        }
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
	}

    void LoadGame()
    {
        SceneManager.LoadScene(1);
    }
}
