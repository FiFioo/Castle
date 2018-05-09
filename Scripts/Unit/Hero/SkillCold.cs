using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Castle
{
    public class SkillCold : MonoBehaviour
    {

        private float mSkillColdTime;

        public Transform mPlayer;

        private Image mImgCold;
        private Text  mTextCold;
        private float mColdTime;

        void Start()
        {
            mImgCold = transform.Find(DataConfigure.PATH_IMG_COLD).GetComponent<Image>();
            mTextCold = transform.Find(DataConfigure.PATH_TEXT_COLD).GetComponent<Text>();
        }

        void Update()
        {
            if (mColdTime > 0) {
                mImgCold.fillAmount = mColdTime / mSkillColdTime;
                mColdTime -= Time.deltaTime;

                int intColdTime = (int)mColdTime;
                mTextCold.text = intColdTime.ToString();
            }
            else {
                mTextCold.text = "";
            }
        }

        private void OnSkill(SKILL skillIndex)
        {
            mSkillColdTime = mPlayer.GetComponent<Hero>().GetSkillColdTime(skillIndex);
            if (mColdTime <= 0) {
                mColdTime = mSkillColdTime;
            }
        }

        public void OnSkillOne()
        {
            OnSkill(SKILL.ONE);
        }

        public void OnSkillTwo()
        {
            OnSkill(SKILL.TWO);
        }

        public void OnSkillThree()
        {
            OnSkill(SKILL.THREE);
        }
    }
}
