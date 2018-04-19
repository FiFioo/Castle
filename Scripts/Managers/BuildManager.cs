using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Castle
{
    public class BuildManager : MonoBehaviour
    {

        public UIManager mUIManager;

        public TowerData mAcidTowerPrefab;
        public TowerData mGatlingTowerPrefab;

        private TowerData mSelectedTower;
        private float mTowerInstanceOffsetY = DataConfigure.TOWER_INSTANCE_OFFSET_Y;

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        private void BuildTower(Tower selectTower, Vector3 pos)
        {
            Vector3 towerPos = pos;
            towerPos.y = pos.y - DataConfigure.TOWER_SWITCH_OFFSET_Y + mTowerInstanceOffsetY;
            GameObject.Instantiate(mSelectedTower.tower, towerPos, mSelectedTower.tower.transform.rotation);
        }

        public void OnAcid()
        {
            //Debug.Log(mUIManager.GetTowerSwitchPos());
            mUIManager.InActiveTowerSwitch();
            mSelectedTower = mAcidTowerPrefab;
            //AcidTower acidTower = new AcidTower();
            AcidTower acidTower = null;
            BuildTower(acidTower, mUIManager.GetTowerSwitchPos());
        }

        public void OnGatling()
        {
            mUIManager.InActiveTowerSwitch();
            mSelectedTower = mGatlingTowerPrefab;
            //GatlingTower gatlingTower = new GatlingTower();
            GatlingTower gatlingTower = null;
            BuildTower(gatlingTower, mUIManager.GetTowerSwitchPos());
        }
    }
}