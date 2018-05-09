using UnityEngine;
using UnityEngine.UI;

namespace Castle
{
    public class Castle : Unit
    {
        private int mCastleOnlyGetDamagePerTime = 1;

        private Slider mHPSlider;

        private void Awake()
        {
            mMaxHP = DataConfigure.Castle.HP;
            mHP    = mMaxHP;
            InitHPSlider();
        }

        private void InitHPSlider()
        {
            mHPSlider = transform.Find(DataConfigure.PATH_HP_SLIDER).GetComponent<Slider>();
            mHPSlider.value = 1f;
        }

        protected override void SynchronizeHPSlider()
        {
            mHPSlider.value = (float)mHP / mMaxHP;
        }

        protected override int DamageHandler(int damage)
        {
            return mCastleOnlyGetDamagePerTime;
        }

        protected override void Attack() { }
    }
}
