using System;
using UnityEngine;


namespace Castle
{
    public class GameManager : MonoBehaviour
    {
        public GameObject mVictoryBanner;
        public GameObject mLoseBanner;

        public static GameManager Instance;

        //private event EventHandler mGameWinEvent;

        private void Awake()
        {
            Instance = this;
        }

        //public void GameWinEventRegister(EventHandler eventHandler)
        //{
        //    mGameWinEvent += eventHandler;
        //}

        public void BattleFinish(bool win)
        {
            if (win) {
                GameWin();
            }
            else {
                GameLose();
            }
        }

        private void GameWin()
        {
            //mGameWinEvent(this, null);

            DataManager.battleLevel = DataConfigure.BATTLING_LEVEL;
            mVictoryBanner.SetActive(true);
        }

        private void GameLose()
        {
            mLoseBanner.SetActive(true);
        }
    }
}