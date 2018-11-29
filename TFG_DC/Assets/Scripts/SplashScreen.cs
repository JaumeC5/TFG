using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SplashScreen : MonoBehaviour {

    public int counter;
    public string ScenetoLoad;
    private float Timer;

	void Start () {
        Timer = Time.time + counter;
	}
	
	void Update () {
		if(Timer < Time.time)
        {
            SceneManager.LoadScene(ScenetoLoad);
        }
	}
}
