using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Castle
{
    using Config = DataConfigure.Hero;

    public class Hero : Unit {
        public List<AudioClip>  mHeroAudioClips;
        public List<GameObject> mHeroEffectPrefab; 

        public List<GameObject> mEnemyList = new List<GameObject>();

        private AudioSource mHeroAudioSource;
        private Animator    mHeroAnimator;
        private Rigidbody   mHeroRigidbody;
        private Slider      mHPSlider;

        private float mSpeedForce;
        private int   mIsRunID;

        private Attack      mAttack;
        private SkillOne    mSkill1;
        private SkillTwo    mSkill2;
        private SkillThree  mSkill3;
        private Skill       mSelectSkill;

        public enum HERO_STATE { NORMAL, MOVE_TO_TARGET, AUTO_ATTACK };
        public HERO_STATE mHeroState;

        private void Start () 
        {
            mMaxHP              = Config.HP;
            mHP                 = mMaxHP;
            mAttackDistance     = Config.DISTANCE_ATTACK;
            mSpeed              = Config.SPEED;
            mSpeedForce         = Config.SPEED_FORCE;

            mHeroAnimator     = transform.GetComponent<Animator>();
            mHeroAudioSource  = transform.GetComponent<AudioSource>();
            mHeroRigidbody    = transform.GetComponent<Rigidbody>();
            mHPSlider         = transform.Find(DataConfigure.PATH_HP_SLIDER).GetComponent<Slider>();
            mHPSlider.value   = 1f;

            mIsRunID   = DataConfigure.IS_RUN_ID;

            mHeroState = HERO_STATE.NORMAL;

            InitPlayerSkills();
        }

        private void Update()
        {
            if (mEnemyList.Count > 0) {
                UpdateEnemyInfo();
            }

            switch (mHeroState) {
                case HERO_STATE.MOVE_TO_TARGET: StateMoveToTargetWorkInUpdate(); break;
                case HERO_STATE.AUTO_ATTACK:    StateAutoAttackWorkInUpdate();   break;
            } 
        }

        private void FixedUpdate()
        {
            // consider auto-move and control move condition
            switch (mHeroState) {
                case HERO_STATE.MOVE_TO_TARGET: StateMoveToTargetWorkInFixedUpdate(); break;
            }
        }

        private void InitPlayerSkills()
        {
            mAttack = new Attack(mHeroAnimator, Config.TRIGGER_ATTACK_ID, Config.COLDTIME_ATTACK, 
                Config.DAMAGE_ATTACK, Config.DISTANCE_ATTACK, Config.OFFSET_ATTACK_HIT,
                mHeroAudioClips[0], mHeroAudioClips[1], mHeroEffectPrefab[0]);

            mSkill1 = new SkillOne(mHeroAnimator, Config.TRIGGER_SKILL_ONE_ID, Config.COLDTIME_SKILL_ONE,
                Config.DAMAGE_SKILL_ONE, Config.DISTANCE_SKILL_ONE, Config.OFFSET_SKILL1_HIT, 
                mHeroAudioClips[2], mHeroAudioClips[3], mHeroEffectPrefab[1]);

            mSkill2 = new SkillTwo(mHeroAnimator, Config.TRIGGER_SKILL_TWO_ID, Config.COLDTIME_SKILL_TWO,
                Config.DAMAGE_SKILL_TWO, Config.DISTANCE_SKILL_TWO, Config.OFFSET_SKILL2_HIT,
                mHeroAudioClips[4], mHeroAudioClips[5], mHeroEffectPrefab[2]);

            mSkill3 = new SkillThree(mHeroAnimator, Config.TRIGGER_SKILL_THREE_ID, Config.COLDTIME_SKILL_THREE,
                Config.DAMAGE_SKILL_THREE, Config.DISTANCE_SKILL_THREE, Config.OFFSET_SKILL3_HIT,
                mHeroAudioClips[6], mHeroAudioClips[7], mHeroEffectPrefab[3]);
        }

        public void OnAttack()
        {
            // make sure attack effect generate correct
            mSelectSkill = mAttack;
            Attack();
        }

        protected override void Attack() 
        {
            GameObject attackTarget = GetAttackTarget();
            if (null != attackTarget) {
                CastleUtility.LookAtTarget(transform, attackTarget.transform);

                if (CastleUtility.IsTargetInAttackRange(transform, attackTarget.transform, mAttackDistance)) { 
                    mAttack.AddTarget(attackTarget);
                    mAttack.LaunchSkill();
                    mHeroState = HERO_STATE.AUTO_ATTACK;
                }
                else {
                    // change hero state to  move to target
                    mHeroState = HERO_STATE.MOVE_TO_TARGET;
                }
            }
            else {
                mAttack.LaunchSkill();
            }
        }

        public void OnSkill(SKILL skillIndex)
        {
            SelectSkill(skillIndex);
        
            if (null != mSelectSkill) {
                mSelectSkill.LaunchSkill();
            }
        }

        private void SelectSkill(SKILL skillIndex)
        {
            switch (skillIndex) {
                case SKILL.ATTACK: mSelectSkill = mAttack; break;
                case SKILL.ONE:    mSelectSkill = mSkill1; break;
                case SKILL.TWO:    mSelectSkill = mSkill2; break;
                case SKILL.THREE:  mSelectSkill = mSkill3; break;
                default:           mSelectSkill = null;    break;
            }

            if (SKILL.ATTACK != skillIndex) {
                mHeroState = HERO_STATE.NORMAL;
            }
        }

        public float GetSkillColdTime(SKILL skillIndex)
        {
            SelectSkill(skillIndex);

            if (null != mSelectSkill) {
                return mSelectSkill.GetColdTime();
            }
            else {
                return 1f;
            }
        }

        private void AttackHitEvent()
        {
            mHeroAudioSource.PlayOneShot(mSelectSkill.skillAttackAudioClip);

            // calculate effect pos
            float offsetLenth = mSelectSkill.effectOffsetOfHero.magnitude;
            Vector3 localToWorld = transform.TransformVector(mSelectSkill.effectOffsetOfHero);

            Vector3 stoneHitPos = transform.position + localToWorld.normalized * offsetLenth;
            GameObject skillEffect = GameObject.Instantiate(mSelectSkill.skillEffect, stoneHitPos, transform.rotation);
            Destroy(skillEffect, 0.5f);

            if (mSelectSkill != mAttack) {
                DetectSkillRangeEnemies();
            }

            mSelectSkill.Hit();
        }

        private void AttackWaveEvent()
        {
            mHeroAudioSource.PlayOneShot(mSelectSkill.skillWaveAudioClip);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (DataConfigure.TAG_ENEMY == other.tag) {
                mEnemyList.Add(other.gameObject);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (DataConfigure.TAG_ENEMY == other.tag) {
                if (mEnemyList.Contains(other.gameObject)) {
                    mEnemyList.Remove(other.gameObject);
                }
            }
        }

        private void UpdateEnemyInfo()
        {
            List<int> deadEnemyIndex = new List<int>();
            for (int i = 0; i < mEnemyList.Count; ++i) {
                if (null == mEnemyList[i]) {
                    deadEnemyIndex.Add(i);
                }
            }

            for (int i = 0; i < deadEnemyIndex.Count; ++i) {
                mEnemyList.RemoveAt(deadEnemyIndex[i] -i);
            }
        }

        private GameObject GetAttackTarget()
        {
            UpdateEnemyInfo();
            return mEnemyList.Count > 0 ? mEnemyList[0] : null;
        }

        public void OnMove(Vector2 direction)
        {
            mHeroState = HERO_STATE.NORMAL;
            Move(direction);
        }

        public void NoMove()
        {
            Move(Vector2.zero);
        }

        private void Move(Vector2 direction)
        {
            if (mHeroAnimator != null) {
                if (direction != Vector2.zero) {
                    SetHeroToRunState();
                    Vector3 rotateVector = new Vector3(direction.x, 0, direction.y);
                    mHeroAnimator.transform.rotation = Quaternion.LookRotation(rotateVector);
                    mHeroRigidbody.AddForce(transform.forward * mSpeedForce);
                }
                else {
                    CancelHeroRunState();
                }
            }
        }

        private void SetHeroToRunState()
        {
            mHeroAnimator.SetBool(mIsRunID, true);
        }

        private void CancelHeroRunState()
        {
            mHeroAnimator.SetBool(mIsRunID, false);
        }

        private void AutoMoveToTarget()
        {
            GameObject attackTarget = GetAttackTarget();
            if (null != attackTarget) {
                if (CastleUtility.IsTargetInAttackRange(transform, attackTarget.transform, mAttackDistance)) {
                    mHeroState = HERO_STATE.AUTO_ATTACK;
                }
                else {
                    // move to target
                    Vector3 direction = attackTarget.transform.position - transform.position;
                    Vector2 correctDirection = new Vector2(direction.x, direction.z);
                    Move(correctDirection);
                }
            }
        }

        private void StateMoveToTargetWorkInUpdate()
        {
            GameObject attackTarget = GetAttackTarget();
            if (null != attackTarget) {
                SetHeroToRunState();
            }
            else {
                mHeroState = HERO_STATE.NORMAL;
                CancelHeroRunState();
            }
        }

        private void StateMoveToTargetWorkInFixedUpdate() {
            AutoMoveToTarget();
        }

        private void StateAutoAttackWorkInUpdate()
        {
            if (null == GetAttackTarget()) {
                mHeroState = HERO_STATE.NORMAL;
            }
            else {
                OnAttack();
            }
        }

        private void DetectSkillRangeEnemies()
        {
            for (int i = 0; i < mEnemyList.Count; ++i) {
                if (CastleUtility.IsTargetInAttackRange(transform, mEnemyList[i].transform, mSelectSkill.distance)) {
                    if (mSkill3 == mSelectSkill) {
                        mSelectSkill.AddTarget(mEnemyList[i]);
                    }
                    else {
                        Vector3 inversePos = transform.InverseTransformPoint(mEnemyList[i].transform.position);
                        if (0 <= inversePos.z) {
                            mSelectSkill.AddTarget(mEnemyList[i]);
                        }
                    }
                }
            }
        }

        protected override int DamageHandler(int damage)
        {
            return damage;
        }

        protected override void SynchronizeHPSlider()
        {
            mHPSlider.value = (float)mHP / mMaxHP;
        }
    }
}
