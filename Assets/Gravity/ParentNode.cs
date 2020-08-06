using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class ParentNode : GravityNode {



	public override bool IsLeaf => false;
	public override float Mass { get; protected set; }
	public override Vector3 CentreOfMass { get; protected set; }
	public GravityNode[] SubNodes { get; }
	public float Size { get; }

	public ParentNode (GravityNode[] subNodes, float size) {
		SubNodes = subNodes;
		Size = size;
		Mass = 0;
		CentreOfMass = Vector3.zero;

		CalculateValues();
	}

	public void CalculateValues () {
		foreach (var node in SubNodes) {
			if (node != null) {
				Mass += node.Mass;
				CentreOfMass += node.CentreOfMass * node.Mass;
			}
		}

		CentreOfMass /= Mass;
	}
}
