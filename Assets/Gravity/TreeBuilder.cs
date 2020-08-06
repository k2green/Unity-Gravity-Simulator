using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeBuilder {

	public GravityBody[] Bodies { get; }
	public int InitialPower { get; }

	public TreeBuilder (GravityBody[] bodies, int initialPower) {
		Bodies = bodies;
		InitialPower = initialPower;
	}

	public GravityNode BuildTree() {
		return BuildTree(Bodies, Vector3.zero, InitialPower);
	}

	private GravityNode BuildTree (GravityBody[] bodies, Vector3 origin, int power) {
		if (bodies.Length == 1) {
			return new LeafNode(bodies[0]);
		}

		var seperatedBodies = new List<GravityBody>[8];

		foreach (var body in bodies) {
			foreach (Region region in Enum.GetValues(typeof(Region))) {
				if (IsWithinRegion(origin, body.Position, region, power - 1)) {
					if (seperatedBodies[(int) region] == null)
						seperatedBodies[(int) region] = new List<GravityBody>();

					seperatedBodies[(int) region].Add(body);
					break;
				}
			}
		}

		var nodes = new GravityNode[8];
		foreach (Region region in Enum.GetValues(typeof(Region))) {
			var newOrigin = origin + ((Vector3) region.GetDirection()) * Mathf.Pow(2, power - 2);

			if (seperatedBodies[(int) region] != null)
				nodes[(int) region] = BuildTree(seperatedBodies[(int) region].ToArray(), newOrigin, power - 1);
		}

		return new ParentNode(nodes, Mathf.Pow(2, power));
	}

	private bool IsWithinRegion (Vector3 origin, Vector3 position, Region region, int power) {
		var bound = region.GetDirection() * (int) Mathf.Pow(2, power);

		bool xCheck = InRange(position.x, origin.x, origin.x + bound.x);
		bool yCheck = InRange(position.y, origin.y, origin.y + bound.y);
		bool zCheck = InRange(position.z, origin.z, origin.z + bound.z);

		return xCheck && yCheck && zCheck;
	}

	private bool InRange(float point, float bound1, float bound2) {
		float min = bound1 < bound2 ? bound1 : bound2;
		float max = bound1 < bound2 ? bound2 : bound1;

		return point >= min && point <= max;
	}
}
