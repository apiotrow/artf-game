﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Frenzy : Override {

	private float spd;
	private float str;
	private float def;
	int level;

	public Frenzy () {
		level = 0;
		name = "Frenzy";
		spd = 0f;
		str = 0f;
		def = 0f;
	}

	public void setSpeedBoost(float percent){
		spd = percent;
	}

	public void setDmgBoost(float percent){
		str = percent;
	}

	public void setDmgReduction(float percent){
		def = percent;
	}

	public void reset(){
		spd = 0;
		str = 0;
		def = 0;
	}

	protected override void bdEffects(BDData newData){
		base.bdEffects (newData);
		newData.unit.stats.dmgManip.setDamageReduction(0, def);
		newData.unit.stats.spdManip.setSpeedAmplification(spd);
		newData.unit.stats.dmgManip.setDamageAmplification(0, str);
	}

	protected override void removeEffects (BDData oldData, GameObject source) {
		base.removeEffects (oldData, source);
		oldData.unit.stats.dmgManip.removeDamageReduction(0, def);
		oldData.unit.stats.spdManip.removeSpeedAmplification(spd);
		oldData.unit.stats.dmgManip.removeDamageAmplification(0, str);
	}

	public override void purgeBD(Character unit, GameObject source) {
		base.purgeBD (unit, source);
	}
}
