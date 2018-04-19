using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Castle
{
    public class Enemy : Unit
    {

        private Transform mDestination;
        private Transform mPlayer;
        private Slider mHPSlider;

        public GameObject mExplosionEffectPrefab;

        public GameObject mAudioSource;

        void Start()
        {
            mSpeed  = DataConfigure.Enemy1.SPEED;
            mMaxHP  = DataConfigure.Enemy1.HP;
            mHP     = mMaxHP;

            mHPSlider = transform.Find(DataConfigure.PATH_HP_SLIDER).GetComponent<Slider>();
            InitHPSlider();

            mDestination    = GameObject.FindGameObjectWithTag(DataConfigure.TAG_CASTLE).transform;
            mPlayer         = GameObject.FindGameObjectWithTag(DataConfigure.TAG_PLAYER).transform;
        }

        void Update()
        {
            Walk();
        }

        protected virtual void Walk()
        {
            if (null != mPlayer)
            {

            }

            if (null != mDestination)
            {
                if (Vector3.Distance(transform.position, mDestination.position) > 0.5f)
                {
                    Vector3 direction = (mDestination.position - transform.position).normalized;
                    transform.Translate(direction * mSpeed * Time.deltaTime);
                }
            }
        }

        protected virtual void InitHPSlider()
        {
            mHPSlider.value = 1f;
            mHPSlider.transform.LookAt(Camera.main.transform);
            mHPSlider.transform.forward = transform.forward;
        }

        protected virtual void SynchronizeHPSlider()
        {
            mHPSlider.value = (float)mHP / mMaxHP;
        }

        protected override void Attack()
        {

        }

        protected override float CalculateIntervalBetweenAttack()
        {
            return 1f;
        }

        protected virtual void Die()
        {
            GameObject explosionEffect = GameObject.Instantiate(mExplosionEffectPrefab, transform.position, transform.rotation);
            Destroy(explosionEffect, 1f);

            GameObject explosionAudio = GameObject.Instantiate(mAudioSource, transform.position, transform.rotation);
            AudioSource audio = explosionAudio.GetComponent<AudioSource>();
            audio.PlayOneShot(audio.clip);
            Destroy(explosionAudio, 0.5f);

            Destroy(this.gameObject);
        }

        public override void GotHurt(int damage)
        {
            mHP -= damage;
            Mathf.Clamp(mHP, 0, mMaxHP);
            if (0 >= mHP)
            {
                // prevent reenter die method before destroy
                if (mIsAlive)
                {
                    mIsAlive = false;
                    Die();
                }
            }
            SynchronizeHPSlider();
        }
    }
}
