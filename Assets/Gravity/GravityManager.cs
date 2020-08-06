using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class GravityManager : MonoBehaviour {

	private GravityBody[] bodies;

	void Start () {
		bodies = FindObjectsOfType<GravityBody>();
	}

	// Update is called once per frame
	void FixedUpdate () {
		TreeBuilder builder = new TreeBuilder(bodies, InitialSizePower);
		var node = builder.BuildTree();

		CalculateForces(node, node);
	}

	private int LargestMagnitudeRounded => (int) Mathf.Round(GetLargestMagnitude());
	private int InitialSizePower => (int) (Mathf.Round(Mathf.Log((int) GetLargestMagnitude(), 2))) + 1;

	public void CalculateForces (GravityNode node, GravityNode root) {
		if (node.IsLeaf) {
			var leaf = (LeafNode) node;
			leaf.Body.Rigidbody.AddForce(CalculateTreeForce(node, root), ForceMode.Impulse);
		} else {
			foreach (var subNode in ((ParentNode) node).SubNodes)
				if (subNode != null)
					CalculateForces(subNode, root);
		}
	}

	private float GetLargestMagnitude () {
		float output = 0;

		foreach (var body in bodies) {
			var sqrMag = body.Position.sqrMagnitude;
			if (sqrMag > output)
				output = sqrMag;
		}

		return Mathf.Sqrt(output);
	}

	const float gravityRatio = 0.5f;
	const float G = 6.674e-2f;

	public static Vector3 CalculateTreeForce (GravityNode target, GravityNode tree) {
		if (tree.IsLeaf || ((ParentNode) tree).Size / Vector3.Distance(target.CentreOfMass, tree.CentreOfMass) < gravityRatio) {
			return CalculateForce(target, tree);
		} else {
			var force = Vector3.zero;

			foreach (var subNode in ((ParentNode) tree).SubNodes) {
				if (subNode != null) {
					force += CalculateTreeForce(target, subNode);
				}
			}

			return force;
		}
	}

	public static Vector3 CalculateForce (GravityNode target, GravityNode tree) {
		if (target == tree)
			return Vector3.zero;

		var difference = tree.CentreOfMass - target.CentreOfMass;
		var distance = difference.magnitude;

		return (difference * target.Mass * tree.Mass * G) / Mathf.Pow(distance, 3);
	}
}
