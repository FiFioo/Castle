using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Castle
{
    [System.Serializable]
    public struct TowerData
    {
        public GameObject tower;
    }

    public abstract class Tower : Unit
    {
        protected List<GameObject> mEnemyList = new List<GameObject>();

        public GameObject mBulletPrefab;
        public GameObject mAttackEffectPrefab;

        protected Transform mTurret;
        protected int mBulletSpeed;
        protected int mBulletDamage;
        protected float mAttackInterval;
        protected float mAttackTimeCount;
        protected Vector3 mFireOffsetPos;
        protected AudioSource mAudioSource;

        protected string mEnemyTag = DataConfigure.TAG_ENEMY;
        protected abstract void Shoot(GameObject enemy);
        protected abstract void PlayAttackEffect();

        protected abstract void PlayAttackAudio();

        protected virtual void Start()
        {
            Init();
        }

        protected virtual void Update()
        {
            if (mEnemyList.Count > 0) {
                if (null == mEnemyList[0]) {
                    UpdateEnemyInfo();
                }
                if (mEnemyList.Count > 0) {
                    TurretLookAtEnemy();
                    Attack();
                }
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.tag == mEnemyTag) {
                mEnemyList.Add(other.gameObject);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.tag == mEnemyTag) {
                // prevent enemy was removed in Update method
                if (mEnemyList.Contains(other.gameObject)) {
                    mEnemyList.Remove(other.gameObject);
                }
            }
        }

        protected virtual void ResetAttackTimeCount()
        {
            mAttackTimeCount = mAttackInterval;
        }

        protected virtual Vector3 CalculateBulletPos(Vector3 originalBulletOffset)
        {
            return CalculatePos(originalBulletOffset);
        }

        protected virtual Vector3 CalculateFirePos(Vector3 originalFireOffset)
        {
            return CalculatePos(originalFireOffset);
        }

        protected virtual Vector3 CalculatePos(Vector3 originalOffset)
        {
            return mTurret.TransformPoint(originalOffset);
        }

        protected override void Attack()
        {
            mAttackTimeCount -= Time.deltaTime;
            if (mAttackTimeCount < 0) {
                if (null != mEnemyList[0]) {
                    Attack(mEnemyList[0]);
                }
                ResetAttackTimeCount();
            }
        }

        private void Attack(GameObject enemy)
        {
            if (null != mBulletPrefab) {
                PlayAttackEffect();
                PlayAttackAudio();
                Shoot(enemy);
            }
        }

        private void TurretLookAtEnemy()
        {
            if (null != mEnemyList[0]) {
                CastleUtility.LookAtTarget(mTurret, mEnemyList[0].transform);
            }
        }

        private void UpdateEnemyInfo()
        {
            List<int> deadEnemys = new List<int>();

            for (int i = 0; mEnemyList.Count > i; ++i) {
                if (null == mEnemyList[i]) {
                    deadEnemys.Add(i);
                }
            }

            for (int i = 0; deadEnemys.Count > i; ++i) {
                mEnemyList.RemoveAt(deadEnemys[i] - i);
            }
        }

        private void Init()
        {
            mTurret = transform.Find(DataConfigure.PATH_TOWER_TURRET).transform;
            mAudioSource = transform.GetComponent<AudioSource>();

            CustomInit();

            mAttackInterval = CastleUtility.CalculateIntervalBetweenAttack(mAttackRate);
            mAttackTimeCount = mAttackInterval;
        }

        protected abstract void CustomInit();

        protected override void Die() { }

        protected override int DamageHandler(int damage) { return damage; }

        protected override void SynchronizeHPSlider() { }
    }
}