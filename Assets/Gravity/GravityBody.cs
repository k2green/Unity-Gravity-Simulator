using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class GravityBody : MonoBehaviour {
	new private Rigidbody rigidbody;

	public Rigidbody Rigidbody => rigidbody;
	private Vector3 force;

	public float Mass => rigidbody.mass;
	public Vector3 Position => transform.position;

	public Vector3 InitialVelocity;
	public Vector3 Force {
		get => force;
		set {
			rigidbody.AddForce(value, ForceMode.Impulse);
			force = value;
		}
	}

	void Awake () {
		rigidbody = GetComponent<Rigidbody>();
		rigidbody.useGravity = false;
		//rigidbody.velocity = InitialVelocity;
	}
}
