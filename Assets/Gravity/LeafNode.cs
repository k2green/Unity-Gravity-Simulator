using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeafNode : GravityNode {
	public override bool IsLeaf => true;

	public GravityBody Body { get; }
	public override float Mass { get => Body.Mass; protected set => throw new System.NotImplementedException(); }
	public override Vector3 CentreOfMass { get => Body.Position; protected set => throw new System.NotImplementedException(); }

	public LeafNode(GravityBody body) {
		Body = body;
	}
}
