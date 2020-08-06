using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GravityNode {

	public abstract bool IsLeaf { get; }
	public abstract float Mass { get; protected set; }
	public abstract Vector3 CentreOfMass { get; protected set; }
}
