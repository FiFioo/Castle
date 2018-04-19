using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Castle
{
    public class Player : Unit {

        //private float mAttackInterval;
        //private float mAttackTimeCount;

        public List<AudioClip> mPlayerAudioClips;
        public List<GameObject> mPlayerEffectPrefab; 

        private AudioSource mPlayerAudioSource;
        private Animator mPlayerAnimator;
        private Rigidbody mPlayerRigidbody;
        private Transform mStone;
        private Vector3[] mStoneHitOffset = new Vector3[4];

        private ScrollCircle mJoystick;
        private float mSpeedForce;
        private int mIsRunID;

        private Skill mSkill1;
        private Skill mSkill2;
        private Skill mSkill3;
        private Skill mAttack;
        private Skill mSelectSkill;

        enum SKILL { ATTACK, ONE, TWO, THREE };

        // Use this for initialization
        void Start () {
            mMaxHP              = DataConfigure.Hero.HP;
            mHP                 = mMaxHP;
            mAttackRate         = DataConfigure.Hero.ATTACK_RATE;
            mSpeed              = DataConfigure.Hero.SPEED;
            mSpeedForce = DataConfigure.Hero.SPEED_FORCE;
            //mAttackInterval     = CaculateIntervalBetweenAttack();
            //mAttackTimeCount    = mAttackInterval;

            mPlayerAnimator     = transform.GetComponent<Animator>();
            mPlayerAudioSource  = transform.GetComponent<AudioSource>();
            mPlayerRigidbody    = transform.GetComponent<Rigidbody>();

            mJoystick           = transform.Find(DataConfigure.PATH_PLAYER_JOYSTICK).GetComponent<ScrollCircle>();

            mIsRunID            = DataConfigure.IS_RUN_ID;

            mStone              = transform.Find("Stone");
            mStoneHitOffset[0]  = DataConfigure.Hero.OFFSET_ATTACK_HIT;
            mStoneHitOffset[1]  = DataConfigure.Hero.OFFSET_SKILL1_HIT;
            mStoneHitOffset[2]  = DataConfigure.Hero.OFFSET_SKILL2_HIT;
            mStoneHitOffset[3]  = DataConfigure.Hero.OFFSET_SKILL3_HIT;

            mAttack = new Skill(mPlayerAnimator, "AttackTrigger", 1f);
            mSkill1 = new Skill(mPlayerAnimator, "Skill1Trigger", 4f);
            mSkill2 = new Skill(mPlayerAnimator, "Skill2Trigger", 6f);
            mSkill3 = new Skill(mPlayerAnimator, "Skill3Trigger", 12f);
        }

	    void Update () {

	    }

        private void FixedUpdate()
        {
            MovePlayer();
        }

        public override void GotHurt(int damage)
        {

        }

        protected override void Attack() {}

        protected override float CalculateIntervalBetweenAttack()
        {
            return 1f;
        }


        public void OnSkill(int skillIndex)
        {
            SelectSkill(skillIndex);
        
            if (null != mSelectSkill)
            {
                OnSkill(mSelectSkill);
            }
        }

        private void OnSkill(Skill skill)
        { 
            skill.LaunchSkill();   
        }

        private void SelectSkill(int skillIndex)
        {
            switch ((SKILL)skillIndex)
            {
                case SKILL.ATTACK:
                    mSelectSkill = mAttack;
                    break;
                case SKILL.ONE:
                    mSelectSkill = mSkill1;
                    break;
                case SKILL.TWO:
                    mSelectSkill = mSkill2;
                    break;
                case SKILL.THREE:
                    mSelectSkill = mSkill3;
                    break;
                default:
                    mSelectSkill = null;
                    break;
            }
        }

        public float GetSkillColdTime(int skillIndex)
        {
            SelectSkill(skillIndex);

            if (null != mSelectSkill)
            {
                return mSelectSkill.GetColdTime();
            }
            else
            {
                return 1f;
            }
        }

        void AttackHitEvent(int skillID)
        {
            int audioID = (skillID << 1) + 1;
            if (mPlayerAudioClips.Count > audioID)
            {
                mPlayerAudioSource.PlayOneShot(mPlayerAudioClips[audioID]);
            }
            
            if (mPlayerEffectPrefab.Count > skillID)
            {
                if (null != mStone)
                {
                    // calculate
                    float offsetLenth = mStoneHitOffset[skillID].magnitude;
                    Vector3 localToWorld = transform.TransformVector(mStoneHitOffset[skillID]);
                    Vector3 stoneHitPos = mStone.position + localToWorld.normalized * offsetLenth;
                    GameObject skillEffect = GameObject.Instantiate(mPlayerEffectPrefab[skillID], stoneHitPos, mStone.rotation);
                    Destroy(skillEffect, 0.5f);
                }
            }
            
        }

        void AttackWaveEvent(int skillID)
        {
            int audioID = skillID << 1;
            if (mPlayerAudioClips.Count > audioID)
            {
                mPlayerAudioSource.PlayOneShot(mPlayerAudioClips[audioID]);
            }
        }

        private void MovePlayer()
        {
            if (mPlayerAnimator != null)
            {
                Vector2 moveDirection = mJoystick.GetJoystickDirection();
                if (moveDirection != Vector2.zero)
                {
                    mPlayerAnimator.SetBool(mIsRunID, true);

                    Vector3 rotateVector = new Vector3(moveDirection.x, 0, moveDirection.y);
                    mPlayerAnimator.transform.rotation = Quaternion.LookRotation(rotateVector);
                    mPlayerRigidbody.AddForce(transform.forward * mSpeedForce);
                }
                else
                {
                    mPlayerAnimator.SetBool(mIsRunID, false);
                }
            }
        }
    }
}
