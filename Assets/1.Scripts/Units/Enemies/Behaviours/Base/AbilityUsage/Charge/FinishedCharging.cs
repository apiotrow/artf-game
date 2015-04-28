// Charge state of bully trunk, when enemy is out of attackRange it will charge BullRush

using UnityEngine;

public class FinishedCharging : ChargeBehaviour {
	// This will be called when the animator first transitions to this state.
	public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	}
	
	// This will be called once the animator has transitioned out of the state.
	public override void OnStateExit (Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	}
	
	public override void OnStateUpdate (Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
		if (this.charge.curCoolDown > 0) {
			animator.SetTrigger ("ChargeOffCD"); // Used here just to transition out after charge is done
		}
	}
}