using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr : MonoBehaviour {

    Transform target;
    GameObject player = GameObject.FindGameObjectWithTag("Player");
    float detectRange = 30;


    // Use this for initialization
    void Start () {
        //door = this.gameObject;
		
	}

    // Update is called once per frame
    void Update()
    {
        var tgtDirection = target.position - transform.position;
        var tgtDistance = tgtDirection.magnitude;
        if (tgtDistance <= detectRange)
        {
            this.gameObject.GetComponent<Animation>().Play("Default Take");
        }
    }
}
