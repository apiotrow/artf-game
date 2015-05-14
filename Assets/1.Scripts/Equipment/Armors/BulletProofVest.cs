using UnityEngine;
using System.Collections;

public class BulletProofVest : Armor {
	// Used for setting stats for each weapon piece
	protected override void setInitValues() {
		base.setInitValues();
		//armVal, goldVal, strength, coordination, health
		stats = new ArmorStats(6, 1, 14, 10, 150);
	}
}
