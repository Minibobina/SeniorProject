using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif
namespace Area730.CameraTools
{

	public class CameraAlign : MonoBehaviour {


	#if UNITY_EDITOR
			private bool _isActiveFromEditorToGame;
			public 	bool IsActiveFromEditorToGame{get{return _isActiveFromEditorToGame;} set{_isActiveFromEditorToGame = value;}}
			
			private bool _isActiveFromGameToEditor;
			public	bool IsActiveFromGameToEditor{get{return _isActiveFromGameToEditor;} set{_isActiveFromGameToEditor = value;}}

	#endif

		}
}
