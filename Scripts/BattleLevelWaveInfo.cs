using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Castle
{
    using ENEMY_TYPE = DataConfigure.CustomDataType.ENEMY_TYPE;
    public abstract class BattleLevelWaveInfo
    { 
        private List<WaveInfo> mWaves = new List<WaveInfo>();

        public BattleLevelWaveInfo()
        {
            ConfigureEnemyWaves();
        }

        protected abstract void ConfigureEnemyWaves();

        protected void AddWave(int numbersInWave, float spawnRate, ENEMY_TYPE enemyType) 
        {
            mWaves.Add(new WaveInfo(numbersInWave, spawnRate, enemyType));
        }

        public List<WaveInfo> GetWaveInfo()
        {
            return mWaves;
        }
    }

    public class BattleLevelWaveInfoOne : BattleLevelWaveInfo
    {
        protected override void ConfigureEnemyWaves()
        {
            AddWave(1, 1, ENEMY_TYPE.WHITE_BALL);
            AddWave(2, 1, ENEMY_TYPE.YELLOW_BALL);
            AddWave(2, 1, ENEMY_TYPE.BULE_BALL);
        }
    }

    public class BattleLevelWaveInfoTwo : BattleLevelWaveInfo
    {
        protected override void ConfigureEnemyWaves()
        {
            AddWave(1, 1, ENEMY_TYPE.FLYING_GOLEM);
            AddWave(2, 1, ENEMY_TYPE.YELLOW_BALL);
            AddWave(2, 1, ENEMY_TYPE.BULE_BALL);
        }
    }

    public class BattleLevelWaveInfoThree : BattleLevelWaveInfo
    {
        protected override void ConfigureEnemyWaves()
        {
            AddWave(1, 1, ENEMY_TYPE.WHITE_BALL);
            AddWave(2, 1, ENEMY_TYPE.YELLOW_BALL);
            AddWave(2, 1, ENEMY_TYPE.BULE_BALL);
        }
    }
}