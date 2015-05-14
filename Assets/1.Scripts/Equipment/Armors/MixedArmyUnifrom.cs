using UnityEngine;
using System.Collections;

public class MixedArmyUniform : Armor {
	// Used for setting stats for each weapon piece
	protected override void setInitValues() {
		base.setInitValues();
		//armVal, goldVal, strength, coordination, health
		stats = new ArmorStats(6, 1, 12, 20, 180);
	}
}