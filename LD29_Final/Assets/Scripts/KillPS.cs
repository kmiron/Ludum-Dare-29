﻿using UnityEngine;
using System.Collections;

public class KillPS : MonoBehaviour {

    public float lifeTime;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        lifeTime -= Time.deltaTime;

        if (lifeTime <= 0)
        {
            Destroy(gameObject);
        }
    
	}
}
