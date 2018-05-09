namespace Castle
{
    using Config = DataConfigure.EnemyBlueBall;
    public class EnemyBlueBall : Enemy
    {
        protected override void CustomInit()
        {
            mDamage = Config.ATTACK_DAMAGE;
            mSpeed = Config.SPEED;
            mMaxHP = Config.HP;
            mAttackDistance = Config.ATTACK_DISTANCE;
            mAttackRate = Config.ATTACK_RATE;
        }
    }
}

