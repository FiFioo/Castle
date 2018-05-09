using System;

namespace Castle
{
    using TASK_STATE = DataConfigure.CustomDataType.TASK_STATE;
    public static class TaskManager
    {
        private static int mTaskNumbers = 9;
        private static event EventHandler mTaskFinishEvent;

        public static TASK_STATE GetTaskState(int taskIndex)
        {
            return DataManager.GetTaskState(taskIndex);
        }

        public static void SaveTaskState(int taskIndex, TASK_STATE taskState)
        {
            DataManager.SaveTaskState(taskIndex, taskState);
        }

        public static bool IsTaskComplete(int taskId)
        {
            if (DataManager.GetTaskState(taskId - 1) == TASK_STATE.COMPLETE) {
                return true;
            }
            return false;
        }

        // bad thing
        public static void UpdateTasksState()
        {
            for (int i = 0; i < mTaskNumbers; ++i) {
                if (i < (DataManager.battleLevel)) {
                    if (GetTaskState(i) == TASK_STATE.DOING) {
                        SaveTaskState(i, TASK_STATE.COMPLETE);
                    }
                }
            }
        }

        public static void RegisterTaskFinishEvent(EventHandler e)
        {
            mTaskFinishEvent = null;
            mTaskFinishEvent += e;
        }

        public static void FinishTask(int taskId)
        {
            ++DataManager.power;
            SaveTaskState(taskId - 1, TASK_STATE.FINISH);

            if (null != mTaskFinishEvent) {
                mTaskFinishEvent(null, null);
            }
        }
    }
}
