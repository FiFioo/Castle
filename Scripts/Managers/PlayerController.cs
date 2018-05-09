using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


namespace Castle
{
    public class PlayerController : MonoBehaviour
    {
        private float mChangeColorTime = DataConfigure.TIME_COLOR_CHANGE;
        private float mActiveTowerSwitchTime = DataConfigure.TIME_TOWER_SWITCH_ACTIVE;
        private string towerBaseTag = DataConfigure.TAG_TOWERBASE;

        public UIManager mUIManager;
        public EventSystem mEventSystem;
        public Canvas[] mBlockRayCanvas;

        private Hero mHero;
        public ScrollCircle mJoystick;

        private void Start()
        {
            mHero = GetHero();
        }

        private void Update()
        {
            if (false == CheckGUIRayCastObjects()) {
                if (Input.GetMouseButtonDown(0)) {
                    CheckSelect();
                }
            } 
        }

        private void FixedUpdate()
        {
            if (null != mHero) {
                MoveHero();
            }
        }

        private void CheckSelect()
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
                    }
                    
                    StartCoroutine(mUIManager.ActiveTowerSwitch(raycastHit.transform, Quaternion.LookRotation(selectRay.direction), mActiveTowerSwitchTime));
                }
            }
        }

        // Solve ray penetrate ui objects
        private bool CheckGUIRayCastObjects()
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
    
        private Hero GetHero()
        {
            if (null != GameObject.FindWithTag(DataConfigure.TAG_HERO))
            {
                return GameObject.FindWithTag(DataConfigure.TAG_HERO).GetComponent<Hero>();
            }
            return null;
        }

        private void MoveHero()
        {
            if (mJoystick.GetJoystickDirection() != Vector2.zero) {
                mHero.OnMove(mJoystick.GetJoystickDirection());
            }
            else {
                mHero.NoMove();
            }
        }

        private void OnSkill(SKILL skillIndex)
        {
            mHero.OnSkill(skillIndex);
        }

        public void OnAttack()
        {
            mHero.OnAttack();
        }

        public void OnSkillOne()
        {
            OnSkill(SKILL.ONE);
        }

        public void OnSkillTwo()
        {
            OnSkill(SKILL.TWO);
        }

        public void OnSkillThree()
        {
            OnSkill(SKILL.THREE);
        }
    }
}
