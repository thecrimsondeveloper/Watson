using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Watson.Anchors
{
    public class GenericMarkerSpawner : MonoBehaviour
    {

        [SerializeField] GameObject markerPrefab;
        [SerializeField] MarkerType typesOfMarkers;

        public void Spawn()
        {
            Debug.Log("Debug: Spawning marker");
            GameObject markerObj = Instantiate(markerPrefab, transform.position, transform.rotation);
            Marker marker = markerObj.GetComponent<Marker>();
            if (!marker) return;


            if (typesOfMarkers == MarkerType.TimeStamp)
            {
                marker.SetupNewTimeStamp();
            }
            else if (typesOfMarkers == MarkerType.Marker)
            {
                marker.SetupNewPositionMarker();
            }
        }



    }

}
