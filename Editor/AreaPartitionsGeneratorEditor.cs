using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Narazaka.VRChat.AreaPartition.Runtime;

namespace Narazaka.VRChat.AreaPartition.Editor
{
    [CustomEditor(typeof(AreaPartitionGenerator))]
    public class AreaPartitionsGeneratorEditor : UnityEditor.Editor
    {
        bool occlusionEnabled = false;

        SerializedProperty AutoGenerate;
        SerializedProperty Root;
        SerializedProperty RoomSettings;
        SerializedProperty Bounds;
        SerializedProperty BoundWallThickness;
        SerializedProperty RoomColCount;
        SerializedProperty Centering;
        void OnEnable()
        {
            AutoGenerate = serializedObject.FindProperty(nameof(AreaPartitionGenerator.AutoGenerate));
            Root = serializedObject.FindProperty(nameof(AreaPartitionGenerator.Root));
            RoomSettings = serializedObject.FindProperty(nameof(AreaPartitionGenerator.RoomSettings));
            Bounds = serializedObject.FindProperty(nameof(AreaPartitionGenerator.Bounds));
            BoundWallThickness = serializedObject.FindProperty(nameof(AreaPartitionGenerator.BoundWallThickness));
            RoomColCount = serializedObject.FindProperty(nameof(AreaPartitionGenerator.RoomColCount));
            Centering = serializedObject.FindProperty(nameof(AreaPartitionGenerator.Centering));
        }

        public override void OnInspectorGUI()
        {
            serializedObject.UpdateIfRequiredOrScript();
            EditorGUILayout.PropertyField(AutoGenerate);
            EditorGUILayout.PropertyField(Root);
            EditorGUILayout.PropertyField(RoomSettings, true);
            EditorGUILayout.PropertyField(Bounds);
            EditorGUILayout.PropertyField(BoundWallThickness);
            EditorGUILayout.PropertyField(RoomColCount);
            EditorGUILayout.PropertyField(Centering);

            EditorGUI.BeginChangeCheck();
            occlusionEnabled = EditorGUILayout.Toggle("Occlusion Enabled", occlusionEnabled);
            if (EditorGUI.EndChangeCheck())
            {
                ChangeOcclusion(target as AreaPartitionGenerator, occlusionEnabled);
            }

            var changed = serializedObject.hasModifiedProperties;
            serializedObject.ApplyModifiedProperties();
            if (GUILayout.Button("Regenerate Rooms") || (changed && AutoGenerate.boolValue))
            {
                GenerateRooms(target as AreaPartitionGenerator);
                ChangeOcclusion(target as AreaPartitionGenerator, occlusionEnabled);
            }
        }

        static void GenerateRooms(AreaPartitionGenerator gen)
        {
            var toDeletes = new List<GameObject>();
            // delete all children
            foreach (Transform child in gen.EffectiveRoot)
            {
                toDeletes.Add(child.gameObject);
            }
            foreach (var obj in toDeletes)
            {
                Undo.DestroyObjectImmediate(obj);
            }
            // generate rooms
            var count = 0;
            for (var i = 0; i < gen.RoomSettings.Length; i++)
            {
                var roomSetting = gen.RoomSettings[i];
                if (roomSetting == null || roomSetting.RoomPrefab == null || roomSetting.RoomCount == 0) continue;
                for (int j = 0; j < roomSetting.RoomCount; j++)
                {
                    var room = PrefabUtility.InstantiatePrefab(roomSetting.RoomPrefab, gen.EffectiveRoot) as GameObject;
                    room.name = $"{roomSetting.RoomPrefab.name}_{j}";
                    gen.SetRoomTransforms(room.transform, count);
                    Undo.RegisterCreatedObjectUndo(room, "[AreaPartitionGenerator] Create Room");
                    count++;
                }
            }
        }

        static void ChangeOcclusion(AreaPartitionGenerator gen, bool occlusionEnabled)
        {
            foreach (Transform room in gen.EffectiveRoot)
            {
                gen.SetOcclusionMeshVisible(room, occlusionEnabled);
            }
        }
    }
}
