using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Castle
{
    public struct DataConfigure
    {
        

        // Animator condition id
        readonly public static int IS_RUN_ID = Animator.StringToHash("IsRun");

        // time configure
        readonly public static float TIME_COLOR_CHANGE = 0.5f;
        readonly public static float TIME_TOWER_SWITCH_ACTIVE = 2f;

        // pos configure
        readonly public static float TOWER_SWITCH_OFFSET_Y = 2f;
        readonly public static float TOWER_INSTANCE_OFFSET_Y = 0.5f;

        // GameObject name
        readonly public static string IMG_SKILL_COLD = "ImgCold";

        // tags configure
        readonly public static string TAG_TOWERBASE = "TowerBase";
        readonly public static string TAG_ENEMY = "Enemy";
        readonly public static string TAG_CASTLE = "Castle";
        readonly public static string TAG_PLAYER = "Player";

        // prefab path relative model 
        readonly public static string PATH_TURRET = "Base/Turret";
        readonly public static string PATH_HP_SLIDER = "Canvas/HPSlider";
        readonly public static string PATH_PLAYER_JOYSTICK = "../PlayerCanvas/Joystick";

        #region Tower Data
        // Acid tower data
        public struct Acid
        {
            readonly public static int ATTACK_RATE = 1;
            readonly public static int BULLET_SPEED = 2;
            readonly public static int BULLET_DAMAGE = 5;
            readonly public static Vector3 BULLET_OFFSET_POS = new Vector3(0, 0.7f, 1);
        }

        // Gatling Tower data
        public struct Gatling
        {
            readonly public static int ATTACK_RATE = 2;
            readonly public static int BULLET_SPEED = 4;
            readonly public static int BULLET_DAMAGE = 4;
            readonly public static Vector3[] BULLET_OFFSET_POSS =
            {
            new Vector3(0, 0.9f, 1),
            new Vector3(0.1f, 0.7f, 1),
            new Vector3(-0.1f, 0.7f, 1)
        };
        }

        #endregion

        // Enemy1 data
        public struct Enemy1
        {
            readonly public static int HP       = 100;
            readonly public static float SPEED  = 1;
        }

        // Hero data
        public struct Hero
        {
            readonly public static int HP                   = 100;
            readonly public static float SPEED              = 2;
            readonly public static float SPEED_FORCE        = 100;
            readonly public static int ATTACK_RATE          = 1;
            readonly public static Vector3 OFFSET_ATTACK_HIT = Vector3.forward;
            readonly public static Vector3 OFFSET_SKILL1_HIT = new Vector3(-0.03f, 0, 1.25f);
            readonly public static Vector3 OFFSET_SKILL2_HIT = new Vector3(-0.03f, 0, 1.25f);
            readonly public static Vector3 OFFSET_SKILL3_HIT = new Vector3(-0.05f, 0, 1.55f);
        }
    }
}
