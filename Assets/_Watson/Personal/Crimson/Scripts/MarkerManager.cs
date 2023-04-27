using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Sirenix.OdinInspector;

namespace Watson.Anchors
{
    public enum MarkerType
    {
        Drawing,
        Note,
        TimeStamp
    }
    public class MarkerManager : MonoBehaviour
    {
        public MarkerData data;
        public SpatialAnchorLoader loader;
        [SerializeField] List<Marker> markers = new List<Marker>();
        List<Marker> timeStamps => markers.Where(m => m.Type == MarkerType.TimeStamp).ToList();
        List<Marker> notes => markers.Where(m => m.Type == MarkerType.Note).ToList();
        List<Marker> drawings => markers.Where(m => m.Type == MarkerType.Drawing).ToList();

        public void AddMarker(Marker newMarker) => markers.Add(newMarker);
        public void RemoveMarker(Marker oldMarker) => markers.Remove(oldMarker);

        [Button, HideInEditorMode]
        public void SaveAll()
        {
            markers.ForEach(m => m.SaveAnchor());
            markers.ForEach(m => data.SaveMarkerData(m));
        }

        [Button, HideInEditorMode]
        public void LoadAll()
        {
            loader.LoadAnchorsByUuid();
        }

        [Button, HideInEditorMode]
        public void ClearAllAnchors()
        {
            markers.ForEach(m => m.DeleteAnchor());
        }
    }
}