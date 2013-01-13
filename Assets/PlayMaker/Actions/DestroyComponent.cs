// (c) Copyright HutongGames, LLC 2010-2011. All rights reserved.

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.GameObject)]
	[Tooltip("Destroys a Component of an Object.")]
	public class DestroyComponent : FsmStateAction
	{
		[RequiredField]
		public FsmOwnerDefault gameObject;
		[RequiredField]
		[UIHint(UIHint.ScriptComponent)]
		public FsmString component;
				
		Component aComponent;

		public override void Reset()
		{
			aComponent = null;
			gameObject = null;
			component = null;
		}

		public override void OnEnter()
		{
			DoDestroyComponent(gameObject.OwnerOption == OwnerDefaultOption.UseOwner ? Owner : gameObject.GameObject.Value);
			
			Finish();
		}

		
		void DoDestroyComponent(GameObject go)
		{
			aComponent = go.GetComponent(component.Value);

			if (aComponent == null)
			{
				LogError("No such component: " + component.Value);
			}
			else
			{
				Object.Destroy(aComponent);
			}
		}
	}
}