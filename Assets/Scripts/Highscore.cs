using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;

public class Highscore : MonoBehaviour
{
    Text text;
    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<Text>();
        string savestring = File.ReadAllText(Application.dataPath + "/highscore.txt");
        highscore hs = JsonUtility.FromJson<highscore>(savestring);
        
        text.text ="HighScore : "+hs.Highscore ;
    }
    
}
