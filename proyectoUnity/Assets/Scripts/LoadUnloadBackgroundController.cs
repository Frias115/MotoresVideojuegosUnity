using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadUnloadBackgroundController : MonoBehaviour {

    public bool loadNext = false;
    public bool unloadPrevious = false;
    private bool firstTime = true;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "UpBound")
        {
            //First time nothing
            if (!firstTime)
            {
                //Second time load next
                loadNext = true;
            }
            firstTime = false;
        }

        if (other.gameObject.tag == "DownBound")
        {
            //First time unload previous
            unloadPrevious = true;
        }

    }
}
