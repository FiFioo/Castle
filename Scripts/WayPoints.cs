using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Castle
{
    public class WayPoints : MonoBehaviour
    {
        public static List<Transform> wayPointTransformList = new List<Transform>();
        
        private void OnEnable()
        {
            for (int i = 0; i < transform.childCount; ++i){
                wayPointTransformList.Add(transform.GetChild(i));
            }
        }

        private void OnDisable()
        {
            wayPointTransformList.Clear();
        }
    }
}