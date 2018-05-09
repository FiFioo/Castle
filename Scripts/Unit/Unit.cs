using System;
using UnityEngine;


namespace Castle
{
    public abstract class Unit : MonoBehaviour
    {
        private event EventHandler mDieEvent;

        protected int  mHP;
        protected int  mMaxHP;
        protected bool mIsAlive = true;

        protected float mSpeed;
        protected float mAttackDistance;
        protected int   mAttackRate;

        protected abstract void Attack();

        protected abstract int DamageHandler(int damage);

        protected abstract void SynchronizeHPSlider();

        public void GotHurt(int damage)
        {
            mHP -= DamageHandler(damage);
            Mathf.Clamp(mHP, 0, mMaxHP);
            if (0 >= mHP) {
                // prevent reenter die method before destroy 避免多次触发
                if (mIsAlive) {
                    mIsAlive = false;
                    Die();
                }
            }
            SynchronizeHPSlider();
        }

        protected virtual void Die()
        {
            if (null != mDieEvent) {
                mDieEvent(this, null);
            }
        }

        public virtual void RegisterDieEvent(EventHandler eventHandler)
        {
            mDieEvent += eventHandler;
        }
    }
}
