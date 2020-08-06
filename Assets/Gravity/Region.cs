using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Region {
	FrontUpperLeft, FrontUpperRight, FrontLowerLeft, FrontLowerRight,
	RearUpperLeft, RearUpperRight, RearLowerLeft, RearLowerRight
}

public static class RegionExtensions {
	public static Vector3Int GetDirection(this Region region) {
		switch (region) {
			case Region.FrontUpperLeft:
				return new Vector3Int(-1, 1, 1);
			case Region.FrontUpperRight:
				return new Vector3Int(1, 1, 1);
			case Region.FrontLowerLeft:
				return new Vector3Int(-1, -1, 1);
			case Region.FrontLowerRight:
				return new Vector3Int(1, -1, 1);
			case Region.RearUpperLeft:
				return new Vector3Int(-1, 1, -1);
			case Region.RearUpperRight:
				return new Vector3Int(1, 1, -1);
			case Region.RearLowerLeft:
				return new Vector3Int(-1, -1, -1);
			case Region.RearLowerRight:
				return new Vector3Int(1, -1, -1);
			default: return Vector3Int.zero;
		}
	}
}
