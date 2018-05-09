using System.Collections;
using System;
using UnityEngine;
using UnityEngine.UI;


namespace Castle
{
    using TOWER_TYPE = DataConfigure.CustomDataType.TOWER_TYPE;
    public class UIManager : MonoBehaviour
    {
        private Transform  mSelectTowerBase;
        public GameObject  mTowerSwitchCanvas;
        public CanvasGroup mSettingCG;

        public EnemySpawner mEnemySpawner;
        public Text         mSpawnEnemyText;
        public Text         mEnemyCountText;
        public Text         mTickCountText;
        public Text         mScoreText;
        public Text         mMoneyText;
        public Text         mPowerLeftText;
        public Text         mPowerTotalText;

        private int   mReactiveCount     = 0;
        private float mFadeTime          = 1f;
        private float mFadeInAlpha       = 1f;
        private float mFadeOutAlpha      = 0;
        private int   mScore             = 0;
        private int   mMoney             = DataConfigure.InitialMoney;

        // prevent click setting too quick to happen something wrong
        private bool  mSettingCGIsActive = false;

        private void Awake()
        {
            if (null != mPowerTotalText && null != mPowerLeftText) {
                mPowerTotalText.text = DataConfigure.DefaultData.POWER_TOTAL.ToString();
                mPowerLeftText.text = DataManager.power.ToString();
                TaskManager.RegisterTaskFinishEvent(UpdatePowerDisplay);
            }

            if (null != mEnemySpawner) {
                mEnemySpawner.RegisterEnemySpawnEvent(DisplaySpawnEnemyNumbers);
                mEnemySpawner.RegisterEnemyGetCountEvent(DisplayEnemyCount);
                mEnemySpawner.RegisterTickCountEvent(TickCountEvent);
                mEnemySpawner.RegisterEnemyDieEvent(EnemyDie);
                DisplayMoneyText();
            }
        }

        public IEnumerator ActiveTowerSwitch(Transform towerBase, Quaternion rotation, float activeTime)
        {
            bool towerBaseHadBuildTower = towerBase.GetComponent<TowerBase>().hadBuildTower;
            if (!towerBaseHadBuildTower) {
                if (mTowerSwitchCanvas != null) {
                    Vector3 towerSwitchPos = towerBase.position;
                    towerSwitchPos.y += DataConfigure.TOWER_SWITCH_OFFSET_Y;
                    mTowerSwitchCanvas.transform.position = towerSwitchPos;
                    mTowerSwitchCanvas.transform.rotation = rotation;
                    if (mTowerSwitchCanvas.activeSelf) {
                        ++mReactiveCount;
                    }
                    mSelectTowerBase = towerBase;
                    mTowerSwitchCanvas.SetActive(true);
                    yield return new WaitForSeconds(activeTime);
                    if (mReactiveCount == 0) {
                        mTowerSwitchCanvas.SetActive(false);
                    }
                    else {
                        --mReactiveCount;
                    }
                }
            }
        }

        public void OnAcid()
        {
            if (mMoney >= DataConfigure.Acid.PRICE) {
                mMoney -= DataConfigure.Acid.PRICE;
                DisplayMoneyText();
                mSelectTowerBase.GetComponent<TowerBase>().hadBuildTower = true;
                InActiveTowerSwitch();
                BuildManager.BuildTower(TOWER_TYPE.ACID, GetTowerSwitchPos());
            }
            else {
                StartCoroutine(ChangeMoneyTextColorForAWhile(DataConfigure.TIME_COLOR_CHANGE));
            }
        }

        public void OnGatling()
        {
            if (mMoney >= DataConfigure.Gatling.PRICE) {
                mMoney -= DataConfigure.Gatling.PRICE;
                DisplayMoneyText();
                mSelectTowerBase.GetComponent<TowerBase>().hadBuildTower = true;
                InActiveTowerSwitch();
                BuildManager.BuildTower(TOWER_TYPE.GATLING, GetTowerSwitchPos());
            }
            else {
                StartCoroutine(ChangeMoneyTextColorForAWhile(DataConfigure.TIME_COLOR_CHANGE));
            }
        }

        public void InActiveTowerSwitch()
        {
            mReactiveCount = 0;
            mTowerSwitchCanvas.SetActive(false);
        }

        public Vector3 GetTowerSwitchPos()
        {
            return mTowerSwitchCanvas.transform.position;
        }

        public static IEnumerator ChangeColorForAWhile(Material material, Color newColor, float changeTime)
        {
            Color oldColor = material.color;
            material.color = newColor;
            yield return new WaitForSeconds(changeTime);
            material.color = oldColor;
        }

        public void OnSettingCilck()
        {
            if (null != mSettingCG) {
                if (mSettingCGIsActive) {
                    mSettingCGIsActive = false;
                    CanvasGroupFadeOut();
                }
                else {
                    mSettingCGIsActive = true;
                    CanvasGroupFadeIn();
                }
            }
        }

        public void OnCloseClick(GameObject closeGameObject)
        {
            closeGameObject.SetActive(false);
        }

        public void OnActiveClick(GameObject panel)
        {
            panel.SetActive(true);
        }

        public void OnActiveTaskPanelClick(GameObject panel)
        {
            panel.SetActive(true);
        }

        public void OnEnterBattleLevel(int levelIndex)
        {
            BattleLevelManager.Instance.EnterBattleLevel(levelIndex);
        }

        public void LoadScene(int sceneIndex)
        {
            ScenesManager.Instance.LoadScene(sceneIndex);
        }

        public void OnClearDataClick()
        {
            DataManager.ClearData();
            UpdatePowerDisplay(null, null);
        }

        private void CanvasGroupFadeIn()
        {
            mSettingCG.gameObject.SetActive(true);
            StopAllCoroutines();
            StartCoroutine(FadeCanvasGroup(mSettingCG, mSettingCG.alpha, mFadeInAlpha, mFadeTime));
        }

        private void CanvasGroupFadeOut()
        {
            StopAllCoroutines();
            StartCoroutine(FadeCanvasGroup(mSettingCG, mSettingCG.alpha, mFadeOutAlpha, mFadeTime));
            StartCoroutine(FadeOutDisactive(mFadeTime));
        }

        private IEnumerator FadeCanvasGroup(CanvasGroup cg, float start, float end, float fadeTime)
        {
            fadeTime = Mathf.Abs(fadeTime);
            float startTime = Time.time;
            while (true) {
                float percentage = (Time.time - startTime) / fadeTime;
                cg.alpha = start - (start - end) * percentage;
                if (percentage >= 1) {
                    break;
                }
                yield return new WaitForFixedUpdate();
            }
        }

        private IEnumerator FadeOutDisactive(float delayTime)
        {
            yield return new WaitForSeconds(delayTime);
            mSettingCG.gameObject.SetActive(false);
        }

        private void DisplaySpawnEnemyNumbers(object sender, EventArgs e)
        {
            int currentNum = int.Parse(mSpawnEnemyText.text);
            mSpawnEnemyText.text = ((currentNum + 1).ToString());
        }

        private void DisplayEnemyCount(int numbers)
        {
            mEnemyCountText.text = numbers.ToString();
        }

        private void TickCountEvent(int tickCount)
        {
            StartCoroutine(DisplayTickCount(tickCount));
        }

        private IEnumerator DisplayTickCount(int tickCount)
        {
            for (int i = tickCount - 1; i >= 0; --i) {
                mTickCountText.text = i.ToString();
                yield return new WaitForSeconds(1);
            }
        }

        private void EnemyDie(object sender, EventArgs e)
        {
            mScore += DataConfigure.PerEnemyScoreValue;
            mScoreText.text = mScore.ToString();
            mMoney += DataConfigure.EnemyMoneyValue;
            DisplayMoneyText();
        }

        private void DisplayMoneyText()
        {
            mMoneyText.text = mMoney.ToString();
        }

        private IEnumerator ChangeMoneyTextColorForAWhile(float changeTime)
        {
            Color saveColor  = mMoneyText.color;
            mMoneyText.color = Color.red;
            yield return new WaitForSeconds(changeTime);
            mMoneyText.color = saveColor;
        }

        private void UpdatePowerDisplay(object sender, EventArgs e)
        {
            mPowerLeftText.text = DataManager.power.ToString();
        }
    }
}
