using UnityEngine;
using System.Collections;

public class CarbonFibSuit : Armor {
	// Used for setting stats for each weapon piece
	protected override void setInitValues() {
		base.setInitValues();
		//armVal, goldVal, strength, coordination, health
		stats = new ArmorStats(10, 1, 30, 18, 250);
	}
}