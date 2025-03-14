// (c) Copyright HutongGames, LLC 2010-2013. All rights reserved.

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Material)]
	[Tooltip("Sets a named color value in a game object's material.")]
	public class SetMaterialColor : FsmStateAction
	{
		[Tooltip("The GameObject that the material is applied to.")]
		[CheckForComponent(typeof(Renderer))]
		public FsmOwnerDefault gameObject;

		[Tooltip("GameObjects can have multiple materials. Specify an index to target a specific material.")]
		public FsmInt materialIndex;

		[Tooltip("Alternatively specify a Material instead of a GameObject and Index.")]
		public FsmMaterial material;

		[UIHint(UIHint.NamedColor)]
		[Tooltip("A named color parameter in the shader.")]
		public FsmString namedColor;
		
		[RequiredField]
		[Tooltip("Set the parameter value.")]
		public FsmColor color;

		[Tooltip("Repeat every frame. Useful if the value is animated.")]
		public bool everyFrame;

		public override void Reset()
		{
			gameObject = null;
			materialIndex = 0;
			material = null;
			namedColor = "_Color";
			color = Color.black;
			everyFrame = false;
		}

		public override void OnEnter()
		{
			DoSetMaterialColor();
			
			if (!everyFrame)
				Finish();
		}
		
		public override void OnUpdate()
		{
			DoSetMaterialColor();
		}

		void DoSetMaterialColor()
		{
			if (color.IsNone)
			{
				return;
			}

			var colorName = namedColor.Value;
			if (colorName == "") colorName = "_Color";

			if (material.Value != null)
			{
				material.Value.SetColor(colorName, color.Value);
				return;
			}
			
			var go = Fsm.GetOwnerDefaultTarget(gameObject);
			if (go == null) return;

			if (go.GetComponent<Renderer>() == null)
			{
				LogError("Missing Renderer!");
				return;
			}
			
			if (go.GetComponent<Renderer>().material == null)
			{
				LogError("Missing Material!");
				return;
			}		
			
			if (materialIndex.Value == 0)
			{
				go.GetComponent<Renderer>().material.SetColor(colorName, color.Value);
			}
			else if (go.GetComponent<Renderer>().materials.Length > materialIndex.Value)
			{
				var materials = go.GetComponent<Renderer>().materials;
				materials[materialIndex.Value].SetColor(colorName, color.Value);
				go.GetComponent<Renderer>().materials = materials;			
			}		
		}
	}
}