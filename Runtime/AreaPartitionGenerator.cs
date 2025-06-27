using System;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;

[assembly: InternalsVisibleTo("Narazaka.VRChat.AreaPartition.Editor")]

namespace Narazaka.VRChat.AreaPartition.Runtime
{
    internal class AreaPartitionGenerator : MonoBehaviour
#if VRC_SDK_VRCSDK3
        , VRC.SDKBase.IEditorOnly
#endif
    {
        [SerializeField] internal bool AutoGenerate;
        [SerializeField] internal Transform Root;
        [SerializeField] internal RoomSetting[] RoomSettings;
        [SerializeField] internal Vector3 Bounds = new Vector3(20, 20, 20);
        [SerializeField] internal float BoundWallThickness = 2;
        [SerializeField] internal int RoomColCount = 10;
        [SerializeField] internal bool Centering = true;

        internal Transform EffectiveRoot => Root == null ? transform : Root;

        internal void SetRoomTransforms(Transform room, int index)
        {
            room.transform.localPosition = RoomPosition(index);
            room.transform.localRotation = Quaternion.identity;
            room.transform.localScale = Vector3.one;

            // bounds
            var top = room.transform.Find("Bounds/Top");
            var bottom = room.transform.Find("Bounds/Bottom");
            var left = room.transform.Find("Bounds/Left");
            var right = room.transform.Find("Bounds/Right");
            var front = room.transform.Find("Bounds/Front");
            var back = room.transform.Find("Bounds/Back");
            top.localScale = bottom.localScale = new Vector3(Bounds.x, BoundWallThickness, Bounds.z);
            left.localScale = right.localScale = new Vector3(BoundWallThickness, Bounds.y, Bounds.z);
            front.localScale = back.localScale = new Vector3(Bounds.x, Bounds.y, BoundWallThickness);
            top.localPosition = new Vector3(0, Bounds.y / 2, 0);
            bottom.localPosition = new Vector3(0, -Bounds.y / 2, 0);
            left.localPosition = new Vector3(-Bounds.x / 2, 0, 0);
            right.localPosition = new Vector3(Bounds.x / 2, 0, 0);
            front.localPosition = new Vector3(0, 0, -Bounds.z / 2);
            back.localPosition = new Vector3(0, 0, Bounds.z / 2);
            top.localRotation = bottom.localRotation = left.localRotation = right.localRotation = front.localRotation = back.localRotation = Quaternion.identity;
        }

        Vector3 RoomPosition(int index)
        {
            var totalCount = RoomSettings.Sum(setting => setting.RoomCount);
            var colCount = Mathf.Min(totalCount, RoomColCount);
            var rowCount = Mathf.CeilToInt((float)totalCount / RoomColCount);
            int col = index % RoomColCount;
            int row = index / RoomColCount;
            var pos = new Vector3(col * Bounds.x, 0, row * Bounds.z);
            if (Centering)
            {
                var center = new Vector3(-(colCount - 1) / 2f * Bounds.x, 0, -(rowCount - 1) / 2f * Bounds.z);
                pos += center;
            }
            return pos;
        }

        internal void SetOcclusionMeshVisible(Transform room, bool visible)
        {
            var top = room.transform.Find("Bounds/Top/Occlusion");
            var bottom = room.transform.Find("Bounds/Bottom/Occlusion");
            var left = room.transform.Find("Bounds/Left/Occlusion");
            var right = room.transform.Find("Bounds/Right/Occlusion");
            var front = room.transform.Find("Bounds/Front/Occlusion");
            var back = room.transform.Find("Bounds/Back/Occlusion");
            top.gameObject.SetActive(visible);
            bottom.gameObject.SetActive(visible);
            left.gameObject.SetActive(visible);
            right.gameObject.SetActive(visible);
            front.gameObject.SetActive(visible);
            back.gameObject.SetActive(visible);
        }

        [Serializable]
        internal class RoomSetting
        {
            [SerializeField] internal GameObject RoomPrefab;
            [SerializeField] internal int RoomCount = 10;
        }
    }
}
