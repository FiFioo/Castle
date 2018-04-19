using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Castle
{
    public class CameraFollowPlayer : MonoBehaviour
    {


        private string mPlayerTag = DataConfigure.TAG_PLAYER;
        private GameObject mPlayer;
        private Vector3 mFollowOffset;

        void Start()
        {
            mPlayer = GameObject.FindGameObjectWithTag(mPlayerTag);
            mFollowOffset = mPlayer.transform.position - transform.position;
        }

        void Update()
        {
            transform.position = Vector3.Lerp(transform.position, mPlayer.transform.position - mFollowOffset, Time.deltaTime);
        }
    }
}
