using UnityEngine;
using System.Collections;

public class lazerBeam : MonoBehaviour
{

    public Transform gun;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        Ray ray = new Ray(gun.position, gun.forward);
        Debug.DrawRay(ray.origin, ray.direction * 10f, Color.red, 0.01f);
    }
}
