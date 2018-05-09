using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace Castle
{
    public class BattleLevelPanel : MonoBehaviour
    {
        private Sprite mLevelLockSprite;
        private Sprite mLevelBattleSprite;
        private Sprite mLevelCompleteSprite;

        private void Awake()
        {
            LoadResource();
            ConfigureLevelText();
        }

        private void OnEnable()
        {
            ConfigureLevelImages();
        }

        private void LoadResource()
        {
            mLevelLockSprite     = new Sprite();
            mLevelLockSprite     = Resources.Load<Sprite>(DataConfigure.PATH_RESOURCE_SPRITE_LEVEL_LOCK);
            mLevelBattleSprite     = new Sprite();
            mLevelBattleSprite     = Resources.Load<Sprite>(DataConfigure.PATH_RESOURCE_SPRITE_LEVEL_BATTLE);
            mLevelCompleteSprite = new Sprite();
            mLevelCompleteSprite = Resources.Load<Sprite>(DataConfigure.PATH_RESOURCE_SPRITE_LEVEL_COMPLETE);
        }

        private void ConfigureLevelImages()
        {
            Transform levelLayout = transform.Find(DataConfigure.PATH_LEVEL_LAYOUT);
            if (null != levelLayout) {
                int playerCompleteLevels = DataManager.battleLevel;
                for (int i = 0; i < levelLayout.childCount; ++i) {
                    if (playerCompleteLevels > i) {
                        levelLayout.GetChild(i).GetComponent<Image>().sprite = mLevelCompleteSprite;
                    }
                    else if (playerCompleteLevels == i) {
                        levelLayout.GetChild(i).GetComponent<Image>().sprite = mLevelBattleSprite;
                    }
                    else {
                        levelLayout.GetChild(i).GetComponent<Image>().sprite = mLevelLockSprite;
                    }                
                }
            }
        }

        private void ConfigureLevelText()
        {
            Transform levelLayout = transform.Find(DataConfigure.PATH_LEVEL_LAYOUT);
            if (null != levelLayout) {
                for (int i = 0; i < levelLayout.childCount; ++i) {
                    levelLayout.GetChild(i).Find(DataConfigure.PATH_TEXT_IN_OBJECT).GetComponent<Text>().text
                        += (i + 1).ToString();
                }
            }
        }
    }
}