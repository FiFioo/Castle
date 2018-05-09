namespace Castle
{
    using Config = DataConfigure.EnemyYellowBall;
    public class EnemyYellowBall : Enemy
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