using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour {
	
	// Update is called once per frame
	private void Update () {
		if( SceneManager.GetActiveScene() == SceneManager.GetSceneByName("Start") ){
			if ( Input.anyKeyDown ){
				SceneManager.LoadScene ("Game");
			}
		}

		if (Input.GetKeyDown (KeyCode.Escape))
			Quit ();
	}

	public void LoadLevel(string name){
		SceneManager.LoadScene (name);
	}

	public void LoadNextLevel(){
		int loadedSceneIndex =  SceneManager.GetActiveScene ().buildIndex;
		SceneManager.LoadScene (loadedSceneIndex + 1);
	}

	public void Quit(){
		Application.Quit ();
	}
}
