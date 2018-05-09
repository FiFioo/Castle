namespace Castle
{
    using ENEMY_TYPE = DataConfigure.CustomDataType.ENEMY_TYPE;
    public class WaveInfo 
    {
        public int        numbersInWave { get; private set; }
        public float      spawnRate     { get; private set; }
        public ENEMY_TYPE enemyType     { get; private set; }

        public WaveInfo(int numbersInWave, float spawnRate, ENEMY_TYPE enemyType)
        {
            this.numbersInWave = numbersInWave > 0 ? numbersInWave : 0;
            this.spawnRate     = spawnRate > 0 ? spawnRate : 1;
            this.enemyType     = enemyType;
        }

        public float GetIntervalSpawnTime()
        {
            return 1 / spawnRate;
        }
    }  
}