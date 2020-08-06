using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class GravityBody : MonoBehaviour {
	new private Rigidbody rigidbody;

	public Rigidbody Rigidbody => rigidbody;

	public float Mass => rigidbody.mass;
	public Vector3 Position => transform.position;

	void Awake () {
		rigidbody = GetComponent<Rigidbody>();
		rigidbody.useGravity = false;
	}

	
}
