// (c) Copyright HutongGames, LLC 2010-2013. All rights reserved.

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Physics)]
	[Tooltip("Sets the Velocity of a Game Object. To leave any axis unchanged, set variable to 'None'. NOTE: Game object must have a rigidbody.")]
	public class SetVelocity : FsmStateAction
	{
		[RequiredField]
		[CheckForComponent(typeof(Rigidbody))]
		public FsmOwnerDefault gameObject;
		
		[UIHint(UIHint.Variable)]
		public FsmVector3 vector;
		
		public FsmFloat x;
		public FsmFloat y;
		public FsmFloat z;
		
		public Space space;
		
		public bool everyFrame;

		public override void Reset()
		{
			gameObject = null;
			vector = null;
			// default axis to variable dropdown with None selected.
			x = new FsmFloat { UseVariable = true };
			y = new FsmFloat { UseVariable = true };
			z = new FsmFloat { UseVariable = true };
			space = Space.Self;
			everyFrame = false;
		}

        public override void Awake()
        {
            Fsm.HandleFixedUpdate = true;
        }		

		// TODO: test this works in OnEnter!
		public override void OnEnter()
		{
			DoSetVelocity();
			
			if (!everyFrame)
			{
				Finish();
			}		
		}

		public override void OnFixedUpdate()
		{
			DoSetVelocity();
			
			if (!everyFrame)
				Finish();
		}

		void DoSetVelocity()
		{
			var go = Fsm.GetOwnerDefaultTarget(gameObject);
			if (go == null || go.GetComponent<Rigidbody>() == null)
			{
				return;
			}
			
			// init position
			
			Vector3 velocity;

			if (vector.IsNone)
			{
				velocity = space == Space.World ?
					go.GetComponent<Rigidbody>().velocity : 
					go.transform.InverseTransformDirection(go.GetComponent<Rigidbody>().velocity);
			}
			else
			{
				velocity = vector.Value;
			}
			
			// override any axis

			if (!x.IsNone) velocity.x = x.Value;
			if (!y.IsNone) velocity.y = y.Value;
			if (!z.IsNone) velocity.z = z.Value;

			// apply
			
			go.GetComponent<Rigidbody>().velocity = space == Space.World ? velocity : go.transform.TransformDirection(velocity);
		}
	}
}