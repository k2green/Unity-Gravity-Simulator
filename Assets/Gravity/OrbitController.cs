using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof (GravityBody))]
public class OrbitController : MonoBehaviour {

	[SerializeField]
	private GravityBody orbitTarget;

	[SerializeField]
	private Vector3 orbitDirection;
	public Vector3 OrbitDirection => orbitDirection.normalized;

	[SerializeField]
	private float scaleFactor;

	private GravityBody satelliteBody;

	void Start () {
		satelliteBody = GetComponent<GravityBody>();

		satelliteBody.Rigidbody.velocity = OrbitDirection * CalculateOrbitVelocity() * scaleFactor;
	}

	private float CalculateOrbitVelocity() {
		float distance = Vector3.Distance(orbitTarget.Position, satelliteBody.Position);
		return Mathf.Sqrt((GravityManager.Instance.GConstant * orbitTarget.Mass) / distance);
	}

	// Update is called once per frame
	void Update () {

	}
}
