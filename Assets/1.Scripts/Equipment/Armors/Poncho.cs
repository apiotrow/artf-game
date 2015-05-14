using UnityEngine;
using System.Collections;

public class Poncho : Armor {
	// Used for setting stats for each weapon piece
	protected override void setInitValues() {
		base.setInitValues();
		//armVal, goldVal, strength, coordination, health
		stats = new ArmorStats(1, 1, 10, 10, 50);
	}
}