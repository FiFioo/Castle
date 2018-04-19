using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Castle
{
    public class Bullet : MonoBehaviour
    {

        public GameObject mExplosionEffectPrefab;

        private int mSpeed;
        private int mBulletDamage;
        private Transform mTarget;
        private string mEnemyTag = DataConfigure.TAG_ENEMY;

        void Start()
        {

        }
        void Update()
        {

            //if (Vector3.Distance(transform.position, mTarget.position) < )
            if (mTarget != null)
            {
                transform.LookAt(mTarget.position);
                transform.Translate(Vector3.forward * mSpeed * Time.deltaTime);
            }
            else
            {
                Die();
            }

        }

        public void SetBulletData(Transform target, int bulletSpeed, int bulletDamage)
        {
            mTarget = target;
            mSpeed = bulletSpeed;
            mBulletDamage = bulletDamage;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.tag == mEnemyTag)
            {
                Die();
                other.GetComponent<Enemy>().GotHurt(mBulletDamage);
            }
        }

        private void Die()
        {
            GameObject explosionEffect = GameObject.Instantiate(mExplosionEffectPrefab, transform.position, transform.rotation);
            Destroy(explosionEffect, 0.5f);
            Destroy(gameObject);
        }
    }
}
