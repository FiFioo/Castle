using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Castle
{
    public class EnemySpawner : MonoBehaviour
    {
        public Transform mEnemySpawnPoint;
        public List<GameObject> mEnemyPrefabList;

        private Vector3 mEnemySpawnPos;

        private Transform  mCastle;
        private Transform  mHero;

        private bool mBattleLose = false;

        private static int mEnemyAliveCount = 0;

        private List<WaveInfo> mWaveInfoList;

        private event EventHandler mEnemySpawnEvent;
        private event EventHandler mEnemyDieEvent;
        public delegate void mEnemyGetCountDelegate(int nums);
        private event mEnemyGetCountDelegate mEnemyGetCountEvent;
        public delegate void mTickCountDelegate(int tickCount);
        private event mTickCountDelegate mTickCountEvent;

        public void RegisterEnemySpawnEvent(EventHandler e)
        {
            mEnemySpawnEvent += e;
        }

        public void RegisterEnemyGetCountEvent(mEnemyGetCountDelegate e)
        {
            mEnemyGetCountEvent += e;
        }

        public void RegisterTickCountEvent(mTickCountDelegate e)
        {
            mTickCountEvent += e;
        }

        public void RegisterEnemyDieEvent(EventHandler e)
        {
            mEnemyDieEvent += e;
        }

        IEnumerator SpawnEnemyCoroutine()
        {
            foreach (WaveInfo waveInfo in mWaveInfoList)
            {
                if (null != mTickCountEvent) {
                    mTickCountEvent(Mathf.FloorToInt(DataConfigure.SecondsBetweenWave));
                }
                yield return new WaitForSeconds(DataConfigure.SecondsBetweenWave);

                GameObject enemyPrefab = SelectEnemyPrefab(waveInfo.enemyType);
                for (int i = 0; i < waveInfo.numbersInWave; ++i) {
                    GameObject enemy = Instantiate(enemyPrefab, mEnemySpawnPos, enemyPrefab.transform.rotation);
                    enemy.GetComponentInChildren<Unit>().RegisterDieEvent(mEnemyDieEvent);
                    if (null != mEnemySpawnEvent) {
                        mEnemySpawnEvent(this, null);
                    }
                    mEnemyAliveCount++;
                    yield return new WaitForSeconds(waveInfo.GetIntervalSpawnTime());
                }
            }

            while (mEnemyAliveCount > 0) {
                yield return null;
            }

            if (mBattleLose) {
                GameManager.Instance.BattleFinish(false);
            }
            else {
                GameManager.Instance.BattleFinish(true);
            }
        }

        private void EnemyDie(object sender, EventArgs e)
        {
            mEnemyAliveCount--;
        }

        private void GameLose(object sender, EventArgs e)
        {
            mBattleLose = true;
            GameManager.Instance.BattleFinish(false);
        }

        private GameObject SelectEnemyPrefab(DataConfigure.CustomDataType.ENEMY_TYPE enemyType)
        {
            return mEnemyPrefabList[(int)enemyType];
        }


        // have static field , so initial should be in OnEnable
        private void OnEnable()
        {
            if (null != GameObject.FindWithTag(DataConfigure.TAG_CASTLE)) {
                mCastle = GameObject.FindWithTag(DataConfigure.TAG_CASTLE).transform;
                mCastle.GetComponent<Unit>().RegisterDieEvent(GameLose);
            }

            if (null != GameObject.FindWithTag(DataConfigure.TAG_HERO)) {
                mHero = GameObject.FindWithTag(DataConfigure.TAG_HERO).transform;
                mHero.GetComponent<Unit>().RegisterDieEvent(GameLose);
            }

            mWaveInfoList = DataConfigure.BATTLING_LEVEL_WAVEINFO.GetWaveInfo();
            
            if (null != mEnemyGetCountEvent)  {
                int enemyCount = 0;
                foreach (WaveInfo waveInfo in mWaveInfoList) {
                    enemyCount += waveInfo.numbersInWave;
                }
                mEnemyGetCountEvent(enemyCount);
            }

            RegisterEnemyDieEvent(EnemyDie);

            mEnemySpawnPos = mEnemySpawnPoint.position;
            StartCoroutine(SpawnEnemyCoroutine());
        }

        private void OnDisable()
        {
            mEnemyAliveCount = 0;
            mBattleLose = false;
            mWaveInfoList.Clear();
            mEnemySpawnEvent = null;
            mEnemyGetCountEvent = null;
            mTickCountEvent = null;
            mEnemyDieEvent = null;
        }
    }
}