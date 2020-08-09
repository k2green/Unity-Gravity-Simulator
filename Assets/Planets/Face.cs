using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Face {
	Top, Bottom, North, East, South, West
}

public static class FaceExtensions {
	public static Vector3 GetUpVector(this Face face) {
		switch (face) {
			case Face.Top: return Vector3.up;
			case Face.Bottom: return Vector3.down;
			case Face.North: return Vector3.forward;
			case Face.East: return Vector3.right;
			case Face.South: return Vector3.back;
			case Face.West: return Vector3.left;
			default: return Vector3.zero;
		}
	}
}
