using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Castle {
    public class CastleUtility {
        public static void LookAtTarget(Transform self, Transform target)
        {
            Vector3 targetPos = target.position;
            targetPos.y = self.position.y;
            self.LookAt(targetPos);
        }

        public static bool IsTargetInAttackRange(Transform self, Transform target, float attackDistance)
        {
            float distance = Vector3.Distance(self.position, target.position);
            return distance < attackDistance ? true : false;
        }

        public static float CalculateIntervalBetweenAttack(int attackRate)
        {
            float attackInterval = 1f;
            try {
                attackInterval = (float)1 / attackRate;
            }
            catch (System.DivideByZeroException e) {
                attackInterval = 1f;
                Debug.Log(e.ToString());
            }
            return attackInterval;
        }
    } 
}
