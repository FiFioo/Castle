using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace Castle
{
    public class ScrollCircle : ScrollRect
    {

        private float mJoystickAreaRaidus;
        private Vector2 mJoystickPos;

        protected override void Start()
        {
            base.Start();

            mJoystickAreaRaidus = (transform as RectTransform).sizeDelta.x * 0.5f;
        }

        public override void OnDrag(PointerEventData eventData)
        {
            base.OnDrag(eventData);

            Vector2 joystickPos = this.content.anchoredPosition;
            if (joystickPos.magnitude > mJoystickAreaRaidus) {
                this.SetContentAnchoredPosition(joystickPos.normalized * mJoystickAreaRaidus);
            }

            mJoystickPos = this.content.anchoredPosition;
        }

        public override void OnEndDrag(PointerEventData eventData)
        {
            base.OnEndDrag(eventData);

            mJoystickPos = Vector2.zero;
        }

        public Vector2 GetJoystickDirection()
        {
            return mJoystickPos.normalized;
        }
    }
}
