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

    //[System.Serializable]
    //public struct BulletData
    //{
    //    public GameObject bullet;
    //}

    public abstract class Tower : Unit
    {

        protected int mPrice;


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
            mTurret = transform.Find(DataConfigure.PATH_TURRET).transform;
            mAudioSource = transform.GetComponent<AudioSource>();
        }

        protected virtual void Update()
        {
            if (mEnemyList.Count > 0)
            {
                if (null == mEnemyList[0])
                {
                    UpdateEnemyInfo();
                }
                if (mEnemyList.Count > 0)
                {
                    TurretLookAtEnemy();
                    Attack();
                }
            }
        }

        protected virtual void OnTriggerEnter(Collider other)
        {
            if (other.tag == mEnemyTag)
            {
                mEnemyList.Add(other.gameObject);
            }
        }

        protected virtual void OnTriggerExit(Collider other)
        {
            if (other.tag == mEnemyTag)
            {
                // prevent enemy was removed in Update method
                if (mEnemyList.Contains(other.gameObject))
                {
                    mEnemyList.Remove(other.gameObject);
                }
            }
        }

        protected override float CalculateIntervalBetweenAttack()
        {
            float attackInterval = 1f;
            try
            {
                attackInterval = (float)1 / mAttackRate;
            }
            catch (System.DivideByZeroException e)
            {
                attackInterval = 1f;
                print(e.ToString());
            }
            return attackInterval;
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
            // Caculate the object need to instance position ,  although i don't know how it works
            float turretRotateAngle = Quaternion.Angle(transform.rotation, mTurret.rotation);
            Quaternion qWeight = Quaternion.AngleAxis(turretRotateAngle, Vector3.up);
            return qWeight * originalOffset + transform.position;
        }

        protected override void Attack()
        {
            mAttackTimeCount -= Time.deltaTime;
            if (mAttackTimeCount < 0)
            {
                if (null != mEnemyList[0])
                {
                    Attack(mEnemyList[0]);
                }
                ResetAttackTimeCount();
            }
        }

        protected virtual void Attack(GameObject enemy)
        {
            if (null != mBulletPrefab)
            {
                PlayAttackEffect();
                PlayAttackAudio();
                Shoot(enemy);
            }
        }

        protected virtual void TurretLookAtEnemy()
        {
            if (null != mEnemyList[0])
            {
                Vector3 targetPos = mEnemyList[0].transform.position;
                targetPos.y = mTurret.position.y;
                mTurret.LookAt(targetPos);
            }
        }

        protected virtual void UpdateEnemyInfo()
        {
            List<int> deadEnemys = new List<int>();

            for (int i = 0; mEnemyList.Count > i; ++i)
            {
                if (null == mEnemyList[i])
                {
                    deadEnemys.Add(i);
                }
            }

            for (int i = 0; deadEnemys.Count > i; ++i)
            {
                mEnemyList.RemoveAt(deadEnemys[i] - i);
            }
        }

        public override void GotHurt(int damage)
        {

        }
    }
}