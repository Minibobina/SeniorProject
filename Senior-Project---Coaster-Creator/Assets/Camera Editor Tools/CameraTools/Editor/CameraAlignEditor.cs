using System;
using UnityEditor;
using UnityEngine;

namespace Area730.CameraTools
{
	[CustomEditor(typeof(CameraAlign))]
	public class CameraAlignEditor  : Editor
	{

#if UNITY_EDITOR

		private static bool 	_isInitedFromEditorToGame;
		private static bool 	_isInitedFromGameToEditor;
		private static bool 	_isSubscribed;
		private static Camera 	_activeCameraFromEditorToGame;
		private static Camera   _activeCameraFromGameToEditor;

	#region Buttons titles

		private const string ENABLE_FROM_EDIT_TO_GAME 		= "View From Editor To Game";
		private const string DISNABLE_FROM_EDIT_TO_GAME 	= "Disable";
		private const string ENABLE_FROM_GAME_TO_EDIT 		= "View From Game To Editor";
		private const string DISNABLE_FROM_GAME_TO_EDIT 	= "Disable";

	#endregion

		void OnEnable()
		{
			if(!_isSubscribed)
			{
				_isSubscribed 					= true;
				SceneView.onSceneGUIDelegate 	+= this.cameraUpdate;
			}
		}

		private void cameraUpdate(SceneView sceneView)
		{
			if(_isInitedFromEditorToGame)
			{
				_activeCameraFromEditorToGame.transform.position = sceneView.camera.transform.position;
				_activeCameraFromEditorToGame.transform.rotation = sceneView.camera.transform.rotation;
			}

			if(_isInitedFromGameToEditor)
			{
				sceneView.orthographic = _activeCameraFromGameToEditor.orthographic;
				sceneView.LookAtDirect(_activeCameraFromGameToEditor.transform.position, _activeCameraFromGameToEditor.transform.rotation, 0);
			}
		}


		public override void OnInspectorGUI()
		{
			CameraAlign myScript = (CameraAlign)target;

			updateEnableButton(myScript);
			updateEditorCameraFromGame(myScript);

		}

		private void updateEnableButton(CameraAlign myScript)
		{
			Color oldColor = GUI.color;

			if(!myScript.IsActiveFromEditorToGame)
			{
				GUI.color = Color.green;
				if(GUILayout.Button(ENABLE_FROM_EDIT_TO_GAME))
				{
					disableFromEditorToGame();

					_activeCameraFromEditorToGame 		= myScript.gameObject.GetComponent<Camera>();
					_isInitedFromEditorToGame 			= true;
					myScript.IsActiveFromEditorToGame 	= true;
				}
			}
			else
			{
				GUI.color = Color.red;
				if(GUILayout.Button(DISNABLE_FROM_EDIT_TO_GAME))
				{
					_activeCameraFromEditorToGame 		= null;
					_isInitedFromEditorToGame 			= false;
					myScript.IsActiveFromEditorToGame 	= false;
				}
			}
			
			GUI.color = oldColor;
			EditorUtility.SetDirty(myScript.gameObject);

		}


		private void updateEditorCameraFromGame(CameraAlign myScript)
		{
			Color oldColor = GUI.color;

			if(!myScript.IsActiveFromGameToEditor)
			{
				GUI.color = Color.cyan;

				if(GUILayout.Button(ENABLE_FROM_GAME_TO_EDIT))
				{
					disableFromGameToEditor();
					_activeCameraFromGameToEditor		= myScript.gameObject.GetComponent<Camera>();
					myScript.IsActiveFromGameToEditor 	= true;
					_isInitedFromGameToEditor 			= true;
				}
			}
			else
			{
				GUI.color = Color.gray;

				if(GUILayout.Button(DISNABLE_FROM_GAME_TO_EDIT))
				{
					myScript.IsActiveFromGameToEditor 	= false;
					_isInitedFromGameToEditor			= false;
					_activeCameraFromGameToEditor 		= null;
				}
			}

			GUI.color = oldColor;
			EditorUtility.SetDirty(myScript.gameObject);
		}


		#region toggles

		private void disableFromGameToEditor()
		{
			CameraAlign [] attachedCameras = GameObject.FindObjectsOfType<CameraAlign>();
			
			for(int i = 0; i < attachedCameras.Length; ++i)
			{
				attachedCameras[i].IsActiveFromGameToEditor = false;
			}

		}

		private void disableFromEditorToGame()
		{
			CameraAlign [] attachedCameras = GameObject.FindObjectsOfType<CameraAlign>();

			for(int i = 0; i < attachedCameras.Length; ++i)
			{
				attachedCameras[i].IsActiveFromEditorToGame = false;
			}


		}

		#endregion

#endif
	}


}