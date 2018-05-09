using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Castle
{
    public struct DataConfigure
    {
        public static int BATTLING_LEVEL = 0;
        public static BattleLevelWaveInfo BATTLING_LEVEL_WAVEINFO = null;

        // simple configure  because no time
        readonly public static float SecondsBetweenWave = 3;
        readonly public static int   PerEnemyScoreValue = 100;
        readonly public static int   InitialMoney       = 100;
        readonly public static int   EnemyMoneyValue    = 10;

        public struct CustomDataType
        {
            public enum TASK_STATE { DOING, COMPLETE, FINISH }
            public enum ENEMY_STATE { IDLE, MOVE_TO_CASTLE, DISCOVER_HERO, ATTACK }
            public enum ENEMY_TYPE { WHITE_BALL, YELLOW_BALL, BULE_BALL, CACTUS, FLYING_GOLEM }

            public enum TOWER_TYPE { ACID, GATLING }
        }

        // person data key
        public struct PersonDataKey
        {
            readonly public static string KEY_TASK_PREFIX  = "TaskState";
            readonly public static string KEY_BATTLE_LEVEL = "BattleLevel";
            readonly public static string KEY_AUDIO_PAUSE  = "AudioPause";
            readonly public static string KEY_AUDIO_VOLUME = "AudioVolume";
            readonly public static string KEY_POWER        = "Power";
        }

        public struct DefaultData
        {
            readonly public static int   POWER_TOTAL  = 5;
            readonly public static int   BATTLE_LEVEL = 2;
            readonly public static int   AUDIO_PAUSE  = 0;
            readonly public static float AUDIO_VOLUME = 0.5f;
            readonly public static CustomDataType.TASK_STATE TASK_STATE = CustomDataType.TASK_STATE.DOING;
        }

        // level scene id-offset-base
        readonly public static int SCENE_ID_BATTLE_LEVEL_OFFSET_BASE = 2;

        // Animator condition id
        readonly public static int IS_RUN_ID         = Animator.StringToHash("IsRun");
        readonly public static int TRIGGER_ATTACK_ID = Animator.StringToHash("AttackTrigger");
        readonly public static int TRIGGER_GOTHIT_ID = Animator.StringToHash("GotHitTrigger");
        readonly public static int TRIGGER_DEAD_ID   = Animator.StringToHash("DeadTrigger");

        // time configure
        readonly public static float TIME_COLOR_CHANGE = 0.3f;
        readonly public static float TIME_TOWER_SWITCH_ACTIVE = 2f;
        readonly public static float TIME_RECOVER_POWER = 5f;

        // pos configure
        readonly public static float TOWER_SWITCH_OFFSET_Y = 2.5f;
        readonly public static float TOWER_INSTANCE_OFFSET_Y = 0.5f;

        // tags configure
        readonly public static string TAG_TOWERBASE = "TowerBase";
        readonly public static string TAG_ENEMY     = "Enemy";
        readonly public static string TAG_CASTLE    = "Castle";
        readonly public static string TAG_HERO      = "Player";

        // prefab path relative model 
        readonly public static string PATH_TOWER_TURRET                     = "Base/Turret";
        readonly public static string PATH_HP_SLIDER                        = "../Canvas/HPSlider";
        readonly public static string PATH_PLAYER_JOYSTICK                  = "../PlayerCanvas/Joystick";
        readonly public static string PATH_AUDIO_UI_TOGGLE                  = "Toggle";
        readonly public static string PATH_AUDIO_UI_SLIDER                  = "Slider";
        readonly public static string PATH_IMG_COLD                         = "ImgCold";
        readonly public static string PATH_TEXT_COLD                        = "ImgCold/Text";
        readonly public static string PATH_RESOURCE_SPRITE_LEVEL_BATTLE     = "Images/LevelBattle";
        readonly public static string PATH_RESOURCE_SPRITE_LEVEL_COMPLETE   = "Images/LevelComplete";
        readonly public static string PATH_RESOURCE_SPRITE_LEVEL_LOCK       = "Images/LevelLock";
        readonly public static string PATH_LEVEL_LAYOUT                     = "LevelLayout";
        readonly public static string PATH_TASK_LAYOUT                      = "ScrollArea/TaskListLayout";
        readonly public static string PATH_TEXT_IN_OBJECT                   = "Text";
        readonly public static string PATH_BUTTON_IN_OBJECT                 = "Button";

        #region Tower Data
        // Acid tower data
        public struct Acid
        {
            readonly public static int PRICE                        = 30;
            readonly public static int ATTACK_RATE                  = 1;
            readonly public static int BULLET_SPEED                 = 2;
            readonly public static int BULLET_DAMAGE                = 5;
            readonly public static Vector3 BULLET_OFFSET_TURRET_POS = new Vector3(0, 0.33f, 1);
        }

        // Gatling Tower data
        public struct Gatling
        {
            readonly public static int PRICE                          = 50;
            readonly public static int ATTACK_RATE                    = 2;
            readonly public static int BULLET_SPEED                   = 4;
            readonly public static int BULLET_DAMAGE                  = 4;
            readonly public static Vector3[] BULLET_OFFSET_TURRET_POS =
            {
            new Vector3(-0.05f, 0.44f, 1.1f),
            new Vector3(0.045f, 0.12f, 1.1f),
            new Vector3(-0.17f, 0.12f, 1.1f)
            };
        }

        #endregion

        #region Enemies Data
        // EnemyWhilteBall data
        public struct EnemyWhiteBall
        {
            readonly public static int   HP              = 100;
            readonly public static float SPEED           = 1;
            readonly public static float ATTACK_DISTANCE = 1.5f;
            readonly public static int   ATTACK_RATE     = 1;
            readonly public static int   ATTACK_DAMAGE   = 1;
        }

        // EnemyYellowBall data
        public struct EnemyYellowBall
        {
            readonly public static int   HP              = 80;
            readonly public static float SPEED           = 2;
            readonly public static float ATTACK_DISTANCE = 1.5f;
            readonly public static int   ATTACK_RATE     = 1;
            readonly public static int   ATTACK_DAMAGE   = 1;
        }

        // EnemyBlueBall data
        public struct EnemyBlueBall
        {
            readonly public static int   HP              = 50;
            readonly public static float SPEED           = 3;
            readonly public static float ATTACK_DISTANCE = 1.5f;
            readonly public static int   ATTACK_RATE     = 1;
            readonly public static int   ATTACK_DAMAGE   = 1;
        }

        // Enemy flying golem data
        public struct EnemyFlyingGolem
        {
            readonly public static int   HP              = 150;
            readonly public static float SPEED           = 1.5f;
            readonly public static float ATTACK_DISTANCE = 1f;
            readonly public static int   ATTACK_RATE     = 2;
            readonly public static int   ATTACK_DAMAGE   = 1;
        }

        // Enemy cactus data
        public struct EnemyCactus
        {
            readonly public static int HP = 300;
            readonly public static float SPEED = 0.8f;
            readonly public static float ATTACK_DISTANCE = 1f;
            readonly public static int ATTACK_RATE = 1;
            readonly public static int ATTACK_DAMAGE = 2;
        }

        #endregion

        // Hero data
        public struct Hero
        {
            readonly public static int   HP                 = 100;
            readonly public static float SPEED              = 2.5f;
            readonly public static float SPEED_FORCE        = 100;

            readonly public static Vector3 OFFSET_ATTACK_HIT = new Vector3(-0.02f, 0, 1.1f);
            readonly public static Vector3 OFFSET_SKILL1_HIT = new Vector3(-0.04f, 0, 1.27f);
            readonly public static Vector3 OFFSET_SKILL2_HIT = new Vector3(-0.04f, 0, 1.27f);
            readonly public static Vector3 OFFSET_SKILL3_HIT = new Vector3(0, 0, 1.55f);

            readonly public static int TRIGGER_ATTACK_ID      = Animator.StringToHash("AttackTrigger");
            readonly public static int TRIGGER_SKILL_ONE_ID   = Animator.StringToHash("Skill1Trigger");
            readonly public static int TRIGGER_SKILL_TWO_ID   = Animator.StringToHash("Skill2Trigger");
            readonly public static int TRIGGER_SKILL_THREE_ID = Animator.StringToHash("Skill3Trigger");

            readonly public static float COLDTIME_ATTACK      = 1.5f;
            readonly public static float COLDTIME_SKILL_ONE   = 4f;
            readonly public static float COLDTIME_SKILL_TWO   = 8f;
            readonly public static float COLDTIME_SKILL_THREE = 15f;

            readonly public static int DAMAGE_ATTACK      = 50;
            readonly public static int DAMAGE_SKILL_ONE   = 60;
            readonly public static int DAMAGE_SKILL_TWO   = 80;
            readonly public static int DAMAGE_SKILL_THREE = 120;

            readonly public static float DISTANCE_ATTACK      = 1.5f;
            readonly public static float DISTANCE_SKILL_ONE   = 1.5f;
            readonly public static float DISTANCE_SKILL_TWO   = 3f;
            readonly public static float DISTANCE_SKILL_THREE = 2f;
        }

        // Castle Data
        public struct Castle
        {
            readonly public static int HP = 5;
        }

        // xml
        public struct Xml
        {
            readonly public static string PATH_TASK_DESCRIPTION = "Xml/taskDescription";
            readonly public static string PATH_TASKLIST_ROOT    = "taskList";
        }
    }
}
