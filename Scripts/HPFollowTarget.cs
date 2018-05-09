using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Castle
{
    public class HPFollowTarget : MonoBehaviour
    {
        public Transform mTarget;
        private Vector3 mFollowOffset;

        void Start()
        {
            mFollowOffset = transform.position - mTarget.position;
        }

        void Update()
        {
            transform.position = mTarget.position + mFollowOffset;
        }
    }
}
