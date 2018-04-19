using UnityEngine;
using UnityEngine.SceneManagement;


namespace Castle
{
    public class GameManager : MonoBehaviour
    {

        public Transform mEnemySpawnPoint;
        public GameObject mEnemyPrefab;

        private Vector3 mEnemySpawnPos;
        private float mEnemySpawnIntervalTime = 2f;
        private float mSpawnTimeCount = 0f;

        private void Start()
        {
            mEnemySpawnPos = mEnemySpawnPoint.position;
        }

        private void Update()
        {
            mSpawnTimeCount -= Time.deltaTime;
            if (mSpawnTimeCount <= 0f)
            {
                SpawnEnemy();
                mSpawnTimeCount = mEnemySpawnIntervalTime;
            }
        }

        private void SpawnEnemy()
        {
            Instantiate(mEnemyPrefab, mEnemySpawnPos, mEnemyPrefab.transform.rotation);
        }
    }
}