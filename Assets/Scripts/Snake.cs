using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using System.Linq;

public class Snake : MonoBehaviour {
	// current movement direction, default is right
	Vector2 dir = Vector2.right;
    public float f;
	// keep track of tail
	List<Transform> tail = new List<Transform>();
	// tail prefab
	public GameObject tailPrefab;
	// did we eat?
	bool ate = false;
	// are we alive?
	bool alive = true;
	// count our points
	public PointCounter pointCounter;

	// Use this for initialization
	void Start () {
		//clear score
		pointCounter.clear();
        InvokeRepeating("Move", 0.3f, f);

    }
	
	// Update is called once per frame
	void Update () {
		// key presses
		// movement?
		if (Input.GetKeyDown(KeyCode.UpArrow) && dir != Vector2.down)
			dir = Vector2.up;
		else if (Input.GetKeyDown(KeyCode.DownArrow) && dir != Vector2.up)
			dir = Vector2.down;
		else if (Input.GetKeyDown(KeyCode.LeftArrow) && dir != Vector2.right)
			dir = Vector2.left;
		else if (Input.GetKeyDown(KeyCode.RightArrow) && dir != Vector2.left)
			dir = Vector2.right;
	}

	// Do movement stuff
	void Move () {
		// current position
		Vector2 oldPosition = transform.position;
		// move in the new direction
		transform.Translate(dir);

		// did we eat something?
		if (ate) {
			// load prefab to extend tail
			GameObject newTail = (GameObject) Instantiate(tailPrefab, oldPosition, Quaternion.identity);
            newTail.GetComponent<SpriteRenderer>().color = Color.green;
            // add new tail to our existing tail (at the front)
            tail.Insert(0, newTail.transform);

			//reset ate
			ate = false;
		}
		// do we have a tail?
		else if (tail.Count > 0) {
			
			// move last element to where head was
			tail.Last ().position = oldPosition;

			// move last element to front of list
			tail.Insert (0, tail.Last ());
			tail.RemoveAt (tail.Count - 1);
		}
	}

	// Check collisions
	void OnTriggerEnter2D(Collider2D collider) {
		// food?
		if (collider.name.StartsWith("FoodPrefab")) {
			ate = true;
			pointCounter.increment();
			Destroy(collider.gameObject);

			GameObject.Find("Main Camera").GetComponent<SpawnFood>().Spawn();
		}
		// if not food, must be border or self...
		else {
			alive = false;
            pointCounter.clear();
            Time.timeScale = 0;
            GameObject.Find("Canvas").transform.Find("Gameover").gameObject.SetActive(true);
        }
	}

	public bool isAlive() {
		return alive;
	}

   public void restart()
    {
        pointCounter.score = 0;
            for (int i = tail.Count; i > 0; i--)
            {
                Destroy(GameObject.FindGameObjectsWithTag("tail")[i], 0f);
            }
            tail.Clear();
            transform.position = new Vector3(0, 0, 0);
        check1();
        check2();
           
         Time.timeScale = 1;
    }

    void check1()
    {
        if (GameObject.Find("Canvas").transform.Find("Gameover") == true)
        {
            GameObject.Find("Canvas").transform.Find("Gameover").gameObject.SetActive(false);
        }

    }
    void check2()
    {
        if (GameObject.Find("Canvas").transform.Find("pausemenu") == true)
        {
            GameObject.Find("Canvas").transform.Find("pausemenu").gameObject.SetActive(false);
        }
    }

}



