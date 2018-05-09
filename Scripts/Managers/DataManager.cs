using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Castle
{
    using Config = DataConfigure.PersonDataKey;
    using TASK_STATE = DataConfigure.CustomDataType.TASK_STATE;
    public class DataManager
    {
        private static Dictionary<string, TASK_STATE> mTaskState = new Dictionary<string, TASK_STATE>();
        public static int battleLevel { 
            get { return mBattleLevel; } 
            set { mBattleLevel = value > mBattleLevel ? value : mBattleLevel; }
        }
        private static int mBattleLevel;

        public static int power {
            get { return mPower; }
            set { mPower = value <= DataConfigure.DefaultData.POWER_TOTAL ? value : DataConfigure.DefaultData.POWER_TOTAL; }
        }
        private static int mPower;

        public static float audioVolume { get; set; }
        public static bool audioPause {
            get {  return mAudioPause > 0 ? true : false;  }
            set { mAudioPause = value == true ? 1 : 0; }
        }
        private static int mAudioPause;

        static DataManager()
        {
            DataInit();
        }


        //
        // Summary:
        //         Argument : taskIndex (should be positive numbers)
        public static TASK_STATE GetTaskState(int taskIndex)
        {
            string key = Config.KEY_TASK_PREFIX + taskIndex.ToString();

            if (!mTaskState.ContainsKey(key)) {
                if (!PlayerPrefs.HasKey(key)) {
                    mTaskState[key] = (int)TASK_STATE.DOING;
                }
                else {
                    mTaskState[key] = (TASK_STATE)PlayerPrefs.GetInt(key);
                }
            } 
            return (TASK_STATE)mTaskState[key];
        }

        public static void SaveTaskState(int taskIndex, TASK_STATE taskState)
        {
            string key = Config.KEY_TASK_PREFIX + taskIndex.ToString();
            if (!mTaskState.ContainsKey(key)) {
                PlayerPrefs.SetInt(key, (int)taskState);
            }
            mTaskState[key] = taskState;
        }

        public static void WriteDataToDisk()
        {
            foreach (KeyValuePair<string, TASK_STATE> keyValuePair in mTaskState) {
                PlayerPrefs.SetInt(keyValuePair.Key, (int)keyValuePair.Value);
            }
            PlayerPrefs.SetInt(Config.KEY_BATTLE_LEVEL, mBattleLevel);
            PlayerPrefs.SetInt(Config.KEY_AUDIO_PAUSE, mAudioPause);
            PlayerPrefs.SetFloat(Config.KEY_AUDIO_VOLUME, audioVolume);
            PlayerPrefs.SetInt(Config.KEY_POWER, mPower);
        }

        public static void ClearData()
        {
            PlayerPrefs.DeleteAll();
            ResetData();
            WriteDataToDisk();
        }

        private static void DataInit()
        {
            mBattleLevel = PlayerPrefs.HasKey(Config.KEY_BATTLE_LEVEL) 
                ? PlayerPrefs.GetInt(Config.KEY_BATTLE_LEVEL) : DataConfigure.DefaultData.BATTLE_LEVEL;
            mAudioPause  = PlayerPrefs.HasKey(Config.KEY_AUDIO_PAUSE)
                ? PlayerPrefs.GetInt(Config.KEY_AUDIO_PAUSE) : DataConfigure.DefaultData.AUDIO_PAUSE;
            audioVolume  = PlayerPrefs.HasKey(Config.KEY_AUDIO_VOLUME)
                ? PlayerPrefs.GetFloat(Config.KEY_AUDIO_VOLUME) : DataConfigure.DefaultData.AUDIO_VOLUME;
            mPower       = PlayerPrefs.HasKey(Config.KEY_POWER)
                ? PlayerPrefs.GetInt(Config.KEY_POWER) : DataConfigure.DefaultData.POWER_TOTAL;
        }

        // clear data should reset memory data
        private static void ResetData()
        {
            mBattleLevel               = DataConfigure.DefaultData.BATTLE_LEVEL;
            mAudioPause                = DataConfigure.DefaultData.AUDIO_PAUSE;
            audioVolume                = DataConfigure.DefaultData.AUDIO_VOLUME;
            mPower                     = DataConfigure.DefaultData.POWER_TOTAL;
            List<string> taskStateKeys = new List<string>(mTaskState.Keys);

            foreach (string taskStateKey in taskStateKeys) {
                mTaskState[taskStateKey] = DataConfigure.DefaultData.TASK_STATE;
            }
        }
    }
}