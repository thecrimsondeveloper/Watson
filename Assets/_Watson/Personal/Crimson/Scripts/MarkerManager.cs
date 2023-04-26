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

        [Button]
        public void SaveAll()
        {
            // markers.ForEach(m => m.Save());
            markers.ForEach(m => data.SaveMarkerData(m));
        }


        [Button]
        public void LoadAll()
        {
            loader.LoadAnchorsByUuid();
        }
    }
}