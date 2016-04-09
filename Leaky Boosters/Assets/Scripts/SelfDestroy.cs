using UnityEngine;
using System.Collections;

public class SelfDestroy : MonoBehaviour {

    [Range(0f, 10f)]
    public float destroyAfter = 0.8f;

    ParticleSystem pSystem;

	// Use this for initialization
	void Start () {
        pSystem = GetComponentInChildren<ParticleSystem>();
        pSystem.transform.localScale = transform.localScale;
        Destroy(gameObject, destroyAfter);
    }

    // Update is called once per frame
    void Update () {
	
	}
}
