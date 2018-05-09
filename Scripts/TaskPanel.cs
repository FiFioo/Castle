using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Xml;

namespace Castle
{
    using TASK_STATE = DataConfigure.CustomDataType.TASK_STATE;
    public class TaskPanel : MonoBehaviour
    {
        private List<string>     mTaskDescriptionList     = new List<string>();
        private List<Transform>  mTaskItemList            = new List<Transform>();
        private List<Text>       mTaskItemTextList        = new List<Text>();
        private List<Button>     mTaskItemButtonList      = new List<Button>();

        private void OnEnable()
        {
            TaskManager.UpdateTasksState();
            Init();
        }

        private void Init()
        {
            GetXmlTaskDescription(DataConfigure.Xml.PATH_TASK_DESCRIPTION);
            GetTaskItems();
            GetTaskItemsText();
            GetTaskItemsButton();
            SetDescriptionToTaskItemsText();
            InitTaskList();
        }

        private void GetXmlTaskDescription(string path)
        {
            string xmlContent = Resources.Load(path).ToString();
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(xmlContent);

            mTaskDescriptionList.Clear();
            XmlNodeList xmlNodeList = xmlDoc.SelectSingleNode(DataConfigure.Xml.PATH_TASKLIST_ROOT).ChildNodes;
            foreach (XmlElement xmlElement in xmlNodeList) {
                mTaskDescriptionList.Add(xmlElement.InnerText);
            }
        }

        private void GetTaskItems()
        {
            Transform taskListLayout = transform.Find(DataConfigure.PATH_TASK_LAYOUT);
            mTaskItemList.Clear();
            for (int i = 0; i < taskListLayout.childCount; ++i) {
                mTaskItemList.Add(taskListLayout.GetChild(i));
            }
        }

        private void GetTaskItemsText()
        {
            mTaskItemTextList.Clear();
            foreach (Transform taskItem in mTaskItemList) {
                mTaskItemTextList.Add(taskItem.Find(DataConfigure.PATH_TEXT_IN_OBJECT).GetComponent<Text>());
            }
        }

        private void GetTaskItemsButton()
        {
            mTaskItemButtonList.Clear();
            foreach (Transform taskItem in mTaskItemList) {
                mTaskItemButtonList.Add(taskItem.Find(DataConfigure.PATH_BUTTON_IN_OBJECT).GetComponent<Button>());
            }
        }

        private void SetDescriptionToTaskItemsText()
        {
            int setNumbers = mTaskItemTextList.Count < mTaskDescriptionList.Count 
                ? mTaskItemTextList.Count : mTaskDescriptionList.Count;

            for (int i = 0; i < setNumbers; ++i) {
                mTaskItemTextList[i].text = mTaskDescriptionList[i];
            }

            for (int i = setNumbers; i < mTaskItemTextList.Count; ++i) {
                mTaskItemList[i].gameObject.SetActive(false);
            }
        }

        private void InitTaskList()
        {
            for (int i = 0; i < mTaskItemList.Count; ++i){
                switch (TaskManager.GetTaskState(i)) {
                    case TASK_STATE.DOING:
                        mTaskItemList[i].gameObject.SetActive(true);
                        mTaskItemButtonList[i].gameObject.SetActive(false);
                        break;
                    case TASK_STATE.COMPLETE:
                        mTaskItemButtonList[i].gameObject.SetActive(true);
                        break;
                    case TASK_STATE.FINISH:
                        mTaskItemList[i].gameObject.SetActive(false);
                        break;
                }
            }

            for (int i = mTaskDescriptionList.Count; i < mTaskItemList.Count; ++i) {
                mTaskItemList[i].gameObject.SetActive(false);
            }
        }

        public void OnTaskBtnClick(int taskId)
        {
            if (TaskManager.IsTaskComplete(taskId)) {
                TaskManager.FinishTask(taskId);
                mTaskItemList[taskId - 1].gameObject.SetActive(false);
            }
        }
    }
}