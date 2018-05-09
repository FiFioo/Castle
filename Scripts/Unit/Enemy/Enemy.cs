using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Castle
{
    using ENEMY_STATE = DataConfigure.CustomDataType.ENEMY_STATE;
    public abstract class Enemy : Unit
    {
        private Transform  mCastle;
        public  Transform  mHero;
        private Slider     mHPSlider;
        private Animator   mAnimator;
        public  GameObject mExplosionEffectPrefab;
        public  GameObject mAudioSource;

        private List<Transform> mWayPointsTransformList;
        private int             mNextWayPointIndex;

        protected int mDamage;
        private float mAttackInterval;
        private float mAttackedTime;
        
        protected ENEMY_STATE mEnemyState;

        protected virtual void Start()
        {
            Init();
        }

        protected virtual void FixedUpdate()
        {
            switch (mEnemyState)
            {
                case ENEMY_STATE.IDLE:
                    StateIdleWork();            break;
                case ENEMY_STATE.MOVE_TO_CASTLE:
                    StateMoveToCastleWork();    break;
                case ENEMY_STATE.DISCOVER_HERO:
                    StateDiscoverHeroWork();    break;
                case ENEMY_STATE.ATTACK:
                    StateAttackWork();          break;
            }
        }

        private void Init()
        {
            CustomInit();
            
            mHP = mMaxHP;
            InitHPSlider();

            mAttackInterval  = CastleUtility.CalculateIntervalBetweenAttack(mAttackRate);
            mAttackedTime    = Time.time - mAttackInterval;

            mAnimator = transform.GetComponent<Animator>();

            mWayPointsTransformList = WayPoints.wayPointTransformList;
            mNextWayPointIndex      = 0;
            mCastle   = GetCastle();
        }

        protected abstract void CustomInit();

        private void InitHPSlider()
        {
            mHPSlider = transform.Find(DataConfigure.PATH_HP_SLIDER).GetComponent<Slider>();
            mHPSlider.value = 1f;
            mHPSlider.transform.LookAt(Camera.main.transform);
            mHPSlider.transform.forward = transform.forward;
        }

        protected override void SynchronizeHPSlider()
        {
            mHPSlider.value = (float)mHP / mMaxHP;
        }

        protected override void Attack()
        {
            // have past time last attack
            if (Time.time - mAttackedTime > mAttackInterval)
            {
                mAnimator.SetTrigger(DataConfigure.TRIGGER_ATTACK_ID);
                mAttackedTime = Time.time;
                mHero.GetComponent<Unit>().GotHurt(mDamage);
            }
        }

        protected override void Die()
        {
            base.Die();

            GameObject explosionEffect = Instantiate(mExplosionEffectPrefab, transform.position, transform.rotation);
            Destroy(explosionEffect, 1f);

            GameObject explosionAudio = Instantiate(mAudioSource, transform.position, transform.rotation);
            AudioSource audio = explosionAudio.GetComponent<AudioSource>();
            audio.PlayOneShot(audio.clip);
            Destroy(explosionAudio, 0.5f);

            Destroy(transform.parent.gameObject);
        }

        protected override int DamageHandler(int damage)
        {
            return damage;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (DataConfigure.TAG_HERO == other.tag) {
                mHero       = other.transform;
                mEnemyState = ENEMY_STATE.DISCOVER_HERO;
            }
            else if (DataConfigure.TAG_CASTLE == other.tag) {
                Die();
                mCastle.GetComponent<Unit>().GotHurt(mDamage);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (DataConfigure.TAG_HERO == other.tag) {
                mHero       = null;
                mEnemyState = ENEMY_STATE.IDLE;
            }
        }

        private Transform GetCastle()
        {
            if (null != GameObject.FindGameObjectWithTag(DataConfigure.TAG_CASTLE)) {
                mEnemyState = ENEMY_STATE.MOVE_TO_CASTLE;
                return GameObject.FindGameObjectWithTag(DataConfigure.TAG_CASTLE).transform;
            }
            return null;
        }

        private void StateIdleWork()
        {
            mAnimator.SetBool(DataConfigure.IS_RUN_ID, false);
            if (IsTargetAlive(mCastle)) {
                mEnemyState = ENEMY_STATE.MOVE_TO_CASTLE;
            }
        }

        private void StateMoveToCastleWork()
        {
            mAnimator.SetBool(DataConfigure.IS_RUN_ID, true);

            if (mWayPointsTransformList.Count <= mNextWayPointIndex) {
                mEnemyState = ENEMY_STATE.IDLE;
            }
            else {
                Vector3 nextWayPointPosition            = mWayPointsTransformList[mNextWayPointIndex].position;
                Vector3 nextWayPointPositionIgnoreYAxis = new Vector3(nextWayPointPosition.x, 0, nextWayPointPosition.z);
                Vector3 currentPositionIgnoreYAxis      = new Vector3(transform.position.x, 0, transform.position.z);
                float   distanceOfNextPoint             = Vector3.Distance(currentPositionIgnoreYAxis, nextWayPointPositionIgnoreYAxis);
                if (distanceOfNextPoint < 0.1f) {
                    ++mNextWayPointIndex;
                }

                CastleUtility.LookAtTarget(transform, mWayPointsTransformList[mNextWayPointIndex]);
                transform.Translate(Vector3.forward * mSpeed * Time.deltaTime);
            }
        }
        
        private void StateDiscoverHeroWork()
        {
            if (CastleUtility.IsTargetInAttackRange(transform, mHero, mAttackDistance)) {
                mAnimator.SetBool(DataConfigure.IS_RUN_ID, false);
                mEnemyState = ENEMY_STATE.ATTACK;
            }
            else {
                mAnimator.SetBool(DataConfigure.IS_RUN_ID, true);
                // move to hero 
                MoveToTarget();
            }
        }

        private void MoveToTarget()
        {
            if (IsTargetAlive(mHero)) {
                CastleUtility.LookAtTarget(transform, mHero);
                transform.Translate(Vector3.forward * mSpeed * Time.fixedDeltaTime);
            }
            else {
                mEnemyState = ENEMY_STATE.IDLE;
            }
        }

        private void StateAttackWork()
        {
            if (IsTargetAlive(mHero)) {
                if (!CastleUtility.IsTargetInAttackRange(transform, mHero, mAttackDistance)) {
                    mEnemyState = ENEMY_STATE.DISCOVER_HERO;
                }
                Attack();
            }
            else {
                mEnemyState = ENEMY_STATE.IDLE;
            }
        }

        private bool IsTargetAlive(Transform target)
        {
            return null == target ? false : true;
        }
    }
}
