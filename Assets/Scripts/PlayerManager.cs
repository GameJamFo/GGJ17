using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour {

    private int health = 100;
    public int Health {
        get
        {
            return health;
        }
        set
        {
            health = value;
            if (health > 100)
                health = 100;
            if (health <= 0)
            {
                health = 0;
                StartCoroutine(GameObject.Find("GameManager").GetComponent<GameManager>().FadeOut("Shit! sorry.", false));
            }
        }
    }

	void Update () {
        GameObject.Find("HealthText").GetComponent<Text>().text = string.Format("Health: {0}%", Health);
	}
	
	public void Hit() {
        Health -= Random.Range(5, 30);
	}
}
