using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Watson.Anchors
{

    [RequireComponent(typeof(OVRSpatialAnchor))]
    public class Marker : MonoBehaviour
    {
        [SerializeField] MarkerManager manager;
        [SerializeField] OVRSpatialAnchor anchor;
        public OVRSpatialAnchor Anchor => anchor;
        public void SetAnchor(OVRSpatialAnchor newAnchor) => anchor = newAnchor;

        [SerializeField] MarkerType type;
        public MarkerType Type => type;
        public void SetType(MarkerType newType) => type = newType;


        private void Start()
        {
            manager = FindObjectOfType<MarkerManager>();
            manager.AddMarker(this);

            anchor = GetComponent<OVRSpatialAnchor>();

        }



        public void Remove()
        {
            DeleteAnchor();
            Destroy(gameObject);
        }

        public void Save()
        {
            anchor.Save();
        }
        public void DeleteAnchor()
        {
            anchor.Erase();
        }
    }
}
