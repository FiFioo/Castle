using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Castle
{
    public class Skill
    {

        private Animator mPlayerAnimator;

        private float mColdTime;
        private float mReleaseTime;
        private int mSkillTriggerID;

        public Skill(Animator player, string skillTriggerName, float skillColdTime)
        {
            mPlayerAnimator = player;
            mSkillTriggerID = Animator.StringToHash(skillTriggerName);
            mColdTime = skillColdTime;

            // make hero init also can use skill
            mReleaseTime = Time.time - mColdTime;
        }

        public void LaunchSkill()
        {
            if (Time.time - mReleaseTime >= mColdTime)
            {
                if (mPlayerAnimator != null)
                {
                    mPlayerAnimator.SetTrigger(mSkillTriggerID);
                }
                mReleaseTime = Time.time;
            }
        }

        public float GetColdTime()
        {
            return mColdTime;
        }
    }
}
