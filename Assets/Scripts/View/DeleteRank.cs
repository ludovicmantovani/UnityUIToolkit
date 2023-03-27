using System.Collections.Generic;
using UnityEngine.UIElements;
using UnityEngine;

namespace View
{
    public class DeleteRank : MonoBehaviour
    {
        [Header("Controller")]
        [SerializeField] private Controller.RankManager rankManager = null;

        [Header("Visual Element")]
        [SerializeField] private string listViewName = "List";
        [SerializeField] private string deleteButtonName = "DeleteButton";

        private UIDocument uiDocument = null;
        private ListView listView = null;
        private Button deleteButton = null;

        private bool _canDelete = false;

        private void OnEnable()
        {
            if (TryGetComponent<UIDocument>(out uiDocument))
            {
                listView = uiDocument.rootVisualElement.Q<ListView>(listViewName);
                deleteButton = uiDocument.rootVisualElement.Q<Button>(deleteButtonName);
                SetUICallback();
            }
            Controller.RankManager.onUpdateRanking += SetDeleteStatus;
        }
        private void OnDisable()
        {
            Controller.RankManager.onUpdateRanking -= SetDeleteStatus;
        }

        /// <summary>
        /// Subscribe button to specific behaviour.
        /// </summary>
        private void SetUICallback()
        {
            if (deleteButton != null)
                deleteButton.clicked += Delete;
        }

        /// <summary>
        /// Called after ranklist update.
        /// Define if it is possible to remove rank and refresh UI.
        /// </summary>
        /// <param name="rankList"></param>
        private void SetDeleteStatus(List<string> ranklist)
        {
            _canDelete = ranklist.Count > 0;
            SetButtonStatus();
        }
        /// <summary>
        /// Set if delete button can be enabled or not.
        /// </summary>
        private void SetButtonStatus()
        {
            if (deleteButton != null)
                deleteButton.SetEnabled(_canDelete);
        }
        /// <summary>
        /// Remove rank in list.
        /// </summary>
        private void Delete()
        {
            if (listView != null && rankManager != null)
                rankManager.DeleteRank(listView.selectedIndex);
        }
    }
}
