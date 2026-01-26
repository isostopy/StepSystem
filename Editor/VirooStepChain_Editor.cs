using UnityEngine;
using UnityEditor;

namespace Isostopy.StepSystem.Viroo.Editor
{

	[CustomEditor(typeof(VirooStepChain))]
	public class VirooStepChain_Editor : UnityEditor.Editor
	{
#if !VIROO
		public override void OnInspectorGUI()
		{
			base.OnInspectorGUI();

			EditorGUILayout.Space();
			EditorGUILayout.HelpBox(
				"Este componente necesita tener importado el paquete de Viroo y haber añadido el scripting define symbol \"VIROO\" en las player settings.\n" +
				"Asegurate de haber añadido el paquete de Viroo y pulsa el boton para añadir el define a las player settings.",
				MessageType.Info);
			if (GUILayout.Button("#define VIROO"))
			{
				PlayerSettings.SetScriptingDefineSymbols(UnityEditor.Build.NamedBuildTarget.Standalone, "VIROO");
			}
		}
#endif
	}
}
