using System;
using UnityEngine;

namespace Castle
{
    public class BattleLevelManager : MonoBehaviour
    {
        private int mCurrentBattleLevel;
        public static BattleLevelManager Instance;

        private void Awake()
        {
            Instance = this;
        }

        public void EnterBattleLevel(int levelIndex)
        {
            if (DataManager.power > 0) {
                --DataManager.power;
                if (levelIndex <= DataManager.battleLevel + 1)
                {
                    DataConfigure.BATTLING_LEVEL = levelIndex;
                    DataConfigure.BATTLING_LEVEL_WAVEINFO = SelectBattleLevelWaveInfo(levelIndex);
                    ScenesManager.Instance.LoadScene(DataConfigure.SCENE_ID_BATTLE_LEVEL_OFFSET_BASE + levelIndex);
                }
            }
        }

        private BattleLevelWaveInfo SelectBattleLevelWaveInfo(int levelIndex)
        {
            switch (levelIndex)
            {
                case 1:
                    return new BattleLevelWaveInfoOne();
                case 2:
                    return new BattleLevelWaveInfoTwo();
                case 3:
                    return new BattleLevelWaveInfoThree();
                default:
                    return new BattleLevelWaveInfoOne();
            }
        }
    }
}
