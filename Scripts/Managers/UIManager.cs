using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Castle
{
    public class UIManager : MonoBehaviour
    {

        public GameObject mTowerSwitchCanvas;

        private int mReactiveCount = 0;

        public IEnumerator ActiveTowerSwitch(Vector3 pos, Quaternion rotation, float activeTime)
        {
            if (mTowerSwitchCanvas != null)
            {
                mTowerSwitchCanvas.transform.position = pos;
                mTowerSwitchCanvas.transform.rotation = rotation;
                if (mTowerSwitchCanvas.activeSelf)
                {
                    ++mReactiveCount;
                }
                mTowerSwitchCanvas.SetActive(true);
                yield return new WaitForSeconds(activeTime);
                if (mReactiveCount == 0)
                {
                    mTowerSwitchCanvas.SetActive(false);
                }
                else
                {
                    --mReactiveCount;
                }

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
    }
}
