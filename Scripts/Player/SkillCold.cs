using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Castle
{
    public class SkillCold : MonoBehaviour
    {

        private float mSkillColdTime;

        public Transform mPlayer;
        public int mSkillIndex;

        private Image mImgCold;
        private float mColdTime;

        void Start()
        {
            mImgCold = transform.Find(DataConfigure.IMG_SKILL_COLD).GetComponent<Image>();
        }

        void Update()
        {
            if (mColdTime > 0)
            {
                mImgCold.fillAmount = mColdTime / mSkillColdTime;
                mColdTime -= Time.deltaTime;
            }
        }

        public void OnSkill()
        {
            mSkillColdTime = mPlayer.GetComponent<Player>().GetSkillColdTime(mSkillIndex);
            if (mColdTime <= 0)
            {
                mColdTime = mSkillColdTime;
            }
        }
    }
}
