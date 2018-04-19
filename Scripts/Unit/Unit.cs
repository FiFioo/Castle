using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Castle
{
    public abstract class Unit : MonoBehaviour
    {

        protected int mHP;
        protected int mMaxHP;
        protected bool mIsAlive = true;

        protected float mSpeed;
        protected int mAttackRate;

        protected abstract void Attack();

        protected abstract float CalculateIntervalBetweenAttack();

        public abstract void GotHurt(int damage);
    }
}
