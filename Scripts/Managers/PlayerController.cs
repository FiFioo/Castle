using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


namespace Castle
{
    public class PlayerController : MonoBehaviour
    {

        private float mTowerSwitchOffsetY = DataConfigure.TOWER_SWITCH_OFFSET_Y;
        private float mChangeColorTime = DataConfigure.TIME_COLOR_CHANGE;
        private float mActiveTowerSwitchTime = DataConfigure.TIME_TOWER_SWITCH_ACTIVE;
        private string towerBaseTag = DataConfigure.TAG_TOWERBASE;

        public UIManager mUIManager;
        public EventSystem mEventSystem;
        public Canvas[] mBlockRayCanvas;

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

            if (false == CheckGUIRayCastObjects())
            {
                if (Input.GetMouseButtonDown(0))
                {
                    CheckSelect();
                }
            }

        }

        void CheckSelect()
        {
            RaycastHit raycastHit;
            Ray selectRay = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(selectRay, out raycastHit))
            {
                if (raycastHit.transform.tag.Equals(towerBaseTag))
                {
                    Material towerBaseMaterial = raycastHit.transform.GetComponent<Renderer>().material;
                    if (towerBaseMaterial != null)
                    {
                        StartCoroutine(UIManager.ChangeColorForAWhile(towerBaseMaterial, Color.gray, mChangeColorTime));
                        //StartCoroutine(UIManager.ActiveDeadThingForAWhile(towerBaseCanvas.gameObject, 1));
                    }

                    //Transform towerBaseCanvas = raycastHit.transform.Find("TowerSwitchCanvas")

                    Vector3 towerSwitchPos = raycastHit.transform.position;
                    towerSwitchPos.y += mTowerSwitchOffsetY;
                    StartCoroutine(mUIManager.ActiveTowerSwitch(towerSwitchPos, Quaternion.LookRotation(selectRay.direction), mActiveTowerSwitchTime));
                }
            }
        }


        // Solve ray penetrate ui objects
        bool CheckGUIRayCastObjects()
        {
            PointerEventData eventData = new PointerEventData(mEventSystem);
            eventData.pressPosition = Input.mousePosition;
            eventData.position = Input.mousePosition;

            List<RaycastResult> raycastResults = new List<RaycastResult>();
            for (int i = 0; mBlockRayCanvas.Length > i; ++i)
            {
                mBlockRayCanvas[i].GetComponent<GraphicRaycaster>().Raycast(eventData, raycastResults);
            }

            return raycastResults.Count > 0;
        }
    }
}
