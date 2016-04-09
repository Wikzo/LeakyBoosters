using UnityEngine;
using System.Collections;

public class AngularVelocityTest : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        transform.Rotate(0f, 30f, 0f);
	}
}
