﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour {

	// Opens the game scene
	public void StartGame ()
    {
        SceneManager.LoadScene("Scene02");
	}
	
    //Sends you back to the menu
    public void RestartGame()
    {
        SceneManager.LoadScene(0);
    }
}
