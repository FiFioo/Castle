using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Castle
{
    using Config = DataConfigure.EnemyCactus;
    public class EnemyCactus : Enemy
    {
        protected override void CustomInit()
        {
            mDamage         = Config.ATTACK_DAMAGE;
            mSpeed          = Config.SPEED;
            mMaxHP          = Config.HP;
            mAttackDistance = Config.ATTACK_DISTANCE;
            mAttackRate     = Config.ATTACK_RATE;
        }
    }
}