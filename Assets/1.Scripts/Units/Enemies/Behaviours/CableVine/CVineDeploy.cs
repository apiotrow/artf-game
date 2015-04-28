using UnityEngine;

public class CVineDeploy : EnemyBehaviour {

	public Blink blink;

	public override void OnStateEnter (Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
		base.OnStateEnter (animator, stateInfo, layerIndex);
		unit = (NewCableVine)unit;

	}

	// This will be called once the animator has transitioned out of the state.
	public override void OnStateExit (Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
		
	}
	
	
	public override void OnStateUpdate (Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
		unit.facing = unit.target.transform.position - unit.transform.position;
		unit.facing.y = 0.0f;
		float wait = 1.5f;
		
		if (blink.curCoolDown <= 0) {
			do {
				/*
				this.facing.x =  Random.value * (this.facing.x == 0 ? (Random.value - 0.5f) : Mathf.Sign);
				this.facing.z = Random.value * (this.facing.z == 0 ? (Random.value - 0.5f) : Mathf.Sign);*/
				
				unit.facing.x += Mathf.Sign (unit.facing.x) * Random.value * 10;
				unit.facing.z += Mathf.Sign (unit.facing.z) * Random.value * 10;
				
				unit.facing.Normalize ();
			} while (unit.facing == Vector3.zero);
			
			blink.useItem ();
		}
	}
}