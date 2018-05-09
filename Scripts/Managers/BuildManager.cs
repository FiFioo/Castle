using UnityEngine;

namespace Castle
{
    using TOWER_TYPE = DataConfigure.CustomDataType.TOWER_TYPE;
    public class BuildManager : MonoBehaviour
    {
        // public UIManager mUIManager;

        //public TowerData mAcidTowerPrefab;
        //public TowerData mGatlingTowerPrefab;

        //private TowerData mSelectedTower;
        private static float mTowerInstanceOffsetY = DataConfigure.TOWER_INSTANCE_OFFSET_Y;

        //private void BuildTower(Tower selectTower, Vector3 pos)
        //{
        //    Vector3 towerPos = pos;
        //    towerPos.y = pos.y - DataConfigure.TOWER_SWITCH_OFFSET_Y + mTowerInstanceOffsetY;
        //    Instantiate(mSelectedTower.tower, towerPos, mSelectedTower.tower.transform.rotation);
        //}

        public GameObject AcidTowerPrefab;
        public GameObject GatlingTowerPrefab;
        private static GameObject mAcidTowerPrefab;
        private static GameObject mGatlingTowerPrefab;

        private void Awake()
        {
            mAcidTowerPrefab = AcidTowerPrefab;
            mGatlingTowerPrefab = GatlingTowerPrefab;
        }

        public static void BuildTower(TOWER_TYPE towerType, Vector3 pos)
        {
            Vector3 towerPos = pos;
            towerPos.y = pos.y - DataConfigure.TOWER_SWITCH_OFFSET_Y + mTowerInstanceOffsetY;
            GameObject selectTower = SelectTowerPrefab(towerType);
            Instantiate(selectTower, towerPos, selectTower.transform.rotation);
        }

        private static GameObject SelectTowerPrefab(TOWER_TYPE towerType)
        {
            switch (towerType)
            {
                case TOWER_TYPE.ACID:
                    return mAcidTowerPrefab;
                case TOWER_TYPE.GATLING:
                    return mGatlingTowerPrefab;
                default:
                    return null;
            }
        }

        //public void OnAcid()
        //{
        //    mUIManager.InActiveTowerSwitch();
        //    mSelectedTower = mAcidTowerPrefab;
        //    AcidTower acidTower = null;
        //    BuildTower(acidTower, mUIManager.GetTowerSwitchPos());
        //}

        //public void OnGatling()
        //{
        //    mUIManager.InActiveTowerSwitch();
        //    mSelectedTower = mGatlingTowerPrefab;
        //    GatlingTower gatlingTower = null;
        //    BuildTower(gatlingTower, mUIManager.GetTowerSwitchPos());
        //}
    }
}