using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.IO;

[System.Serializable]
public class highscore
{
   public int Highscore = 0;
}
public class PointCounter : MonoBehaviour {
	Text text;
	public int score = 0;
    highscore hs;
    int high;
	// Use this for initialization
	void Start () {
		text = gameObject.GetComponent<Text>();
		text.text = "Score: 0";
        string savestring = File.ReadAllText(Application.dataPath + "/highscore.txt");
        highscore hsobject = JsonUtility.FromJson<highscore>(savestring);
        high = hsobject.Highscore;
        Debug.Log(high);
    }
	
	// Update is called once per frame
	void Update () {
		text.text = "Score: " + score;
	}

	public void increment() {
		score++;
	}

	public void clear() {
     if(high<score)
        {
            hs = new highscore
            {
                Highscore = score,
            };
            string json = JsonUtility.ToJson(hs);
            File.WriteAllText(Application.dataPath + "/highscore.txt", json);
        }
    }
}
