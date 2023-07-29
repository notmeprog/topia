﻿using UnityEngine;
using UnityEditor;

namespace Deform
{
	[CustomEditor (typeof (DeformerComponentManager))]
	public class DeformerComponentManagerEditor : Editor
	{
		private const string TINY_INDENT = @"  ";
		private const string SMALL_INDENT = @"    ";
		private const string LARGE_INDENT = @"      ";

		private static bool showDebug = false;

		public override void OnInspectorGUI ()
		{
			var manager = target as DeformerComponentManager;

			DrawUpdateModeGUI (manager);
			DrawNormalsCalculationGUI (manager);
			DrawDeformerOrderUpdateGUI (manager);
			DrawMeshColliderGUI (manager);
			DrawDebugGUI (manager);

			Repaint ();
		}

		private void DrawUpdateModeGUI (DeformerComponentManager manager)
		{
			EditorGUI.BeginChangeCheck ();
			var label = new GUIContent ("Update Mode", "UpdateInstant: Performs deformation calculations to the entire mesh every frame.\n\nUpdateAsync: Performs all calculations on another thread. Has great performance, but update-rate may seem a little slow in some scenarios.\n\nPause: Maintains the current mesh.\n\nStop: Removes all deformation and shows the original mesh.");
			var updateMode = manager.updateMode;
			updateMode = (UpdateMode)EditorGUILayout.EnumPopup (label, updateMode);
			if (EditorGUI.EndChangeCheck ())
			{
				Undo.RecordObject (manager, "Update Mode");
				manager.updateMode = updateMode;
			}

			if (updateMode == UpdateMode.UpdateAsync && !Application.isPlaying)
			{
				EditorGUILayout.HelpBox ("UpdateAsync only works in Play-Mode, UpdateInstant will be used in the editor", MessageType.Info);
			}
		}

		private void DrawNormalsCalculationGUI (DeformerComponentManager manager)
		{
			EditorGUI.BeginChangeCheck ();
			var label = new GUIContent ("Normals Calculation", "Unity - Pretty Fast. Uses Unity's runtime normal recalculation\n\nSmooth - Very Slow. Looks much better than Unity's method.\n\nMaintain - Fastest. Keeps the current normals.\n\nOriginal - Very Fast. Applies the normals of the original, unmodified mesh.");
			var normalsCalculation = manager.normalsCalculation;
			normalsCalculation = (NormalsCalculationMode)EditorGUILayout.EnumPopup (label, normalsCalculation);
			if (EditorGUI.EndChangeCheck ())
			{
				Undo.RecordObject (manager, "Normals Calculation");
				manager.normalsCalculation = normalsCalculation;
			}
		}

		private void DrawDeformerOrderUpdateGUI (DeformerComponentManager manager)
		{
			EditorGUI.BeginChangeCheck ();
			var label = new GUIContent ("Auto Update Deformer Order (Runtime)", "Always auto updates outside playmode.");
			var autoUpdate = manager.autoUpdateDeformerOrder;
			autoUpdate = EditorGUILayout.Toggle (label, autoUpdate);
			if (EditorGUI.EndChangeCheck ())
			{
				Undo.RecordObject (manager, "Auto Update Deformer Order");
				manager.autoUpdateDeformerOrder = autoUpdate;
			}
		}

		private void DrawMeshColliderGUI (DeformerComponentManager manager)
		{
			EditorGUI.BeginChangeCheck ();
			var label = new GUIContent ("Mesh Collider", "Optional.");
			var meshCollider = manager.MeshCollider;
			meshCollider = (MeshCollider)EditorGUILayout.ObjectField (label, meshCollider, typeof (MeshCollider), true);
			if (EditorGUI.EndChangeCheck ())
			{
				Undo.RecordObject (manager, "Normals Calculation");
				manager.MeshCollider = meshCollider;
			}
		}

		private void DrawDebugGUI (DeformerComponentManager manager)
		{
			EditorGUILayout.BeginHorizontal (GUILayout.MaxWidth (5));
			EditorGUILayout.LabelField ("Debug Info", GUILayout.MaxWidth (70));
			showDebug = GUILayout.Toggle (showDebug, string.Empty);
			EditorGUILayout.EndHorizontal ();
			if (!showDebug)
				return;

			if (GUILayout.Button (new GUIContent ("Update Deformer Order", "Sets the execution order of the deformers to the order they appear in the inspector."), GUILayout.Width (200)))
				manager.RefreshDeformerOrder ();
			if (GUILayout.Button (new GUIContent ("Save Mesh", "Saves the current mesh to your Assets folder"), GUILayout.Width (200)))
				EditorMeshUtil.Save (manager.Target.sharedMesh, manager.transform.name);
			EditorGUILayout.LabelField (string.Format ("{0}Vertex Count: {1}", TINY_INDENT, manager.VertexCount));
			EditorGUILayout.LabelField (string.Format ("Time: {0}", manager.SyncedTime));
			EditorGUILayout.LabelField (string.Format ("Delta Time: {0}", manager.SyncedDeltaTime));
			EditorGUILayout.LabelField (TINY_INDENT + "Deformers:");
			var deformers = manager.GetDeformers ();
			foreach (var deformer in deformers)
			{
				var indent = deformer.update ? LARGE_INDENT : SMALL_INDENT;
				EditorGUI.BeginDisabledGroup (!deformer.update);
				EditorGUILayout.LabelField (indent + deformer.GetType ().Name);
				EditorGUI.EndDisabledGroup ();
			}
		}
	}
}