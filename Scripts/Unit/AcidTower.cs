using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Castle
{
    public class AcidTower : Tower
    {

        private Vector3 mBulletOffset = DataConfigure.Acid.BULLET_OFFSET_POS;

        protected override void Start()
        {
            base.Start();

            mAttackRate = DataConfigure.Acid.ATTACK_RATE;
            mAttackInterval = CalculateIntervalBetweenAttack();
            mAttackTimeCount = mAttackInterval;
            mBulletSpeed = DataConfigure.Acid.BULLET_SPEED;
            mBulletDamage = DataConfigure.Acid.BULLET_DAMAGE;

            mFireOffsetPos = mBulletOffset;
        }

        protected override void Update()
        {
            base.Update();
        }



        protected override void Shoot(GameObject enemy)
        {
            Vector3 bulletPos = CalculateBulletPos(mBulletOffset);
            GameObject bullet = GameObject.Instantiate(mBulletPrefab, bulletPos, mBulletPrefab.transform.rotation);
            bullet.GetComponent<Bullet>().SetBulletData(enemy.transform, mBulletSpeed, mBulletDamage);
        }

        protected override void PlayAttackEffect()
        {
            Vector3 effectPos = CalculateFirePos(mFireOffsetPos);
            GameObject attackEffect = GameObject.Instantiate(mAttackEffectPrefab, effectPos, transform.rotation);
            Destroy(attackEffect, 1f);
        }

        protected override void PlayAttackAudio()
        {
            mAudioSource.PlayOneShot(mAudioSource.clip);
        }
    }
}
