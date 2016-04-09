using UnityEngine;
using System.Collections;

public class SelfDestroy : MonoBehaviour {

    [Range(0f, 10f)]
    public float destroyAfter = 0.8f;

    ParticleSystem pSystem;
    AudioSource aSource;

	// Use this for initialization
	void Start () {
        pSystem = GetComponentInChildren<ParticleSystem>();
        pSystem.transform.localScale = transform.localScale;
        aSource = GetComponent<AudioSource>();
        aSource.pitch = Random.Range(0.7f, 1.3f);
        transform.Rotate(0f, Random.Range(-360f, 360f), 0f);
        Destroy(gameObject, destroyAfter);
    }

    // Update is called once per frame
    void Update () {
	
	}
}
