// Sprint item

using UnityEngine;
using System.Collections;

public class Sprint : ToggleItem {

	[Range(2, 4)]
	public int sprintSpeed;
	private int baseSpeed;

	// Use this for initialization
	protected override void Start () {
		base.Start();
	}

	protected override void setInitValues() {
		base.setInitValues();

		cooldown = 15.0f;
		maxDuration = 10;
	}

	// Update is called once per frame
	protected override void Update () {
		base.Update();
	}
	
	// Called when character with an this item selected uses their item key
	public override void useItem() {
		base.useItem();

		// player.animator.SetTrigger("Sprint"); Set speed var in animator once we have the animation
	}

	protected override IEnumerator bgnEffect() {
		baseSpeed = player.stats.speed;
		player.stats.speed *= sprintSpeed;
		return base.bgnEffect();
	}

	public override void deactivateItem() {
		base.deactivateItem();
	}

	protected override void atvDeactivation() {
		player.stats.speed = baseSpeed;

		base.atvDeactivation();
	}

	protected override void animDone() {
		base.animDone ();
	}
}