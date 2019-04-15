using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class UIManager : MonoBehaviour {

	

	void Start () {

	}

	// Update is called once per frame
	void Update () {
		
	}


	
public void togglePaused()
{
    if (Time.timeScale == 1)
    {
        GameObject.Find("Canvas").transform.Find("pausemenu").gameObject.SetActive(true);
        Time.timeScale = 0;
    }
}

    public void resume()
    {
        if (Time.timeScale == 0)
        {
            GameObject.Find("Canvas").transform.Find("pausemenu").gameObject.SetActive(false);
            Time.timeScale = 1;

        }
    }

public void play() {
		SceneManager.LoadScene("MainGame");
        Time.timeScale = 1;
	}

	public void mainMenu() {
		SceneManager.LoadScene("MainMenu");
	}

	public void quitGame() {
		Application.Quit();
	}
}
