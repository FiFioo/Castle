using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Castle
{
    public class GatlingTower : Tower
    {

        private Vector3[] mBulletOffsets = DataConfigure.Gatling.BULLET_OFFSET_POSS;
        private int mBulletOffsetNums = 1;
        private GameObject mFireEffect;

        private uint mAttackTimes = 0;

        protected override void Start()
        {
            base.Start();

            mAttackRate = DataConfigure.Gatling.ATTACK_RATE;
            mAttackInterval = CalculateIntervalBetweenAttack();
            mAttackTimeCount = mAttackInterval;
            mBulletSpeed = DataConfigure.Gatling.BULLET_SPEED;
            mBulletDamage = DataConfigure.Gatling.BULLET_DAMAGE;

            mBulletOffsetNums = mBulletOffsets.Length;
            mFireOffsetPos = CalculateFireRelativePos();
        }

        protected override void Update()
        {
            base.Update();

            if (0 == mEnemyList.Count)
            {
                if (null != mFireEffect)
                {
                    Destroy(mFireEffect);
                }
                if (mAudioSource.isPlaying)
                {
                    mAudioSource.Stop();
                }
            }
        }

        protected override void Shoot(GameObject enemy)
        {
            Vector3 bulletPos = CalculateBulletPos(mBulletOffsets[mAttackTimes % mBulletOffsetNums]);
            GameObject bullet = GameObject.Instantiate(mBulletPrefab, bulletPos, mBulletPrefab.transform.rotation);
            bullet.GetComponent<Bullet>().SetBulletData(enemy.transform, mBulletSpeed, mBulletDamage);
            ++mAttackTimes;
        }

        protected override void PlayAttackEffect()
        {
            if (null == mFireEffect)
            {
                mFireEffect = GameObject.Instantiate(mAttackEffectPrefab);
            }
            Vector3 firePos = CalculateFirePos(mFireOffsetPos);
            mFireEffect.transform.position = firePos;
            mFireEffect.transform.rotation = mTurret.rotation;
        }

        private Vector3 CalculateFireRelativePos()
        {
            Vector3 resultPos = Vector3.zero;
            for (int i = 0; i < mBulletOffsetNums; ++i)
            {
                resultPos += mBulletOffsets[i];
            }
            return resultPos /= mBulletOffsetNums;
        }

        protected override void PlayAttackAudio()
        {
            if (!mAudioSource.isPlaying)
            {
                mAudioSource.Play();
            }
        }
    }
}