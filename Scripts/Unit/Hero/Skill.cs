using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Castle
{
    public enum SKILL { ATTACK, ONE, TWO, THREE };
    public abstract class Skill
    {
        protected Animator  mHeroAnimator;
        protected Rigidbody mHeroRigidbody;

        protected List<GameObject> mTargetList = new List<GameObject>();

        protected float   mColdTime;
        protected float   mReleaseTime;
        protected int     mSkillTriggerID;

        public int   damage   { get; protected set; }
        public float distance { get; protected set; }

        public Vector3 effectOffsetOfHero { get; set; }
        public AudioClip skillWaveAudioClip { get; set; }
        public AudioClip skillAttackAudioClip { get; set; }
        public GameObject skillEffect { get; set; }

        public Skill(Animator hero, int skillTriggerID, float skillColdTime, int skillDamage, 
            float skillDistance, Vector3 effectOffsetOfHero,
            AudioClip skillWaveAudioClip, AudioClip skillAttackAudioClip, GameObject skillEffect)
        {
            mHeroAnimator         = hero;
            mSkillTriggerID       = skillTriggerID;
            mColdTime             = skillColdTime;
            damage                = skillDamage;
            distance              = skillDistance;

            this.effectOffsetOfHero     = effectOffsetOfHero;
            this.skillWaveAudioClip     = skillWaveAudioClip;
            this.skillAttackAudioClip   = skillAttackAudioClip;
            this.skillEffect            = skillEffect;

            mHeroRigidbody    = mHeroAnimator.transform.GetComponent<Rigidbody>();

            // make hero init also can use skill
            mReleaseTime        = Time.time - mColdTime;
        }

        public void LaunchSkill()
        {
            if (!IsCooling()) {
                if (null != mHeroAnimator) {
                    mHeroAnimator.SetTrigger(mSkillTriggerID);
                }
                mReleaseTime = Time.time;
            }
        }

        public float GetColdTime()
        {
            return mColdTime;
        }

        protected bool IsCooling()
        {
            return (Time.time - mReleaseTime) < mColdTime;
        }

        public void AddTarget(GameObject target)
        {
            mTargetList.Add(target);
        }

        protected void ClearTarget()
        {
            mTargetList.Clear();
        }

        public virtual void Hit()
        {
            for (int i = 0; i < mTargetList.Count; ++i) {
                mTargetList[i].GetComponent<Unit>().GotHurt(damage);
            }
            ClearTarget();
        }
    }

    public class Attack : Skill
    {
        public Attack(Animator hero, int skillTriggerID, float skillColdTime, int skillDamage,
            float skillDistance, Vector3 effectOffsetOfHero,
            AudioClip skillWaveAudioClip, AudioClip skillAttackAudioClip, GameObject skillEffect)
            : base(hero, skillTriggerID, skillColdTime, skillDamage, skillDistance,
                  effectOffsetOfHero, skillWaveAudioClip, skillAttackAudioClip, skillEffect)
        {}

        public override void Hit()
        {
            if (mTargetList.Count > 0) {
                if (null != mTargetList[0]) {
                    mTargetList[0].GetComponent<Unit>().GotHurt(damage);
                }
            }
            ClearTarget();
        }
    }

    public class SkillOne : Skill
    {
        public SkillOne(Animator hero, int skillTriggerID, float skillColdTime, int skillDamage,
            float skillDistance, Vector3 effectOffsetOfHero,
            AudioClip skillWaveAudioClip, AudioClip skillAttackAudioClip, GameObject skillEffect)
            : base(hero, skillTriggerID, skillColdTime, skillDamage, skillDistance,
                  effectOffsetOfHero, skillWaveAudioClip, skillAttackAudioClip, skillEffect)
        { }
    }

    public class SkillTwo : Skill
    {
        public SkillTwo(Animator hero, int skillTriggerID, float skillColdTime, int skillDamage,
            float skillDistance, Vector3 effectOffsetOfHero,
            AudioClip skillWaveAudioClip, AudioClip skillAttackAudioClip, GameObject skillEffect)
            : base(hero, skillTriggerID, skillColdTime, skillDamage, skillDistance,
                  effectOffsetOfHero, skillWaveAudioClip, skillAttackAudioClip, skillEffect)
        {}

        public override void Hit()
        {
            mHeroRigidbody.AddForce(mHeroRigidbody.transform.forward * DataConfigure.Hero.SPEED_FORCE, ForceMode.Impulse);
            base.Hit();
        }
    }

    public class SkillThree : Skill
    {
        public SkillThree(Animator hero, int skillTriggerID, float skillColdTime, int skillDamage,
            float skillDistance, Vector3 effectOffsetOfHero,
            AudioClip skillWaveAudioClip, AudioClip skillAttackAudioClip, GameObject skillEffect)
            : base(hero, skillTriggerID, skillColdTime, skillDamage, skillDistance,
                  effectOffsetOfHero, skillWaveAudioClip, skillAttackAudioClip, skillEffect)
        { }
    }
}
