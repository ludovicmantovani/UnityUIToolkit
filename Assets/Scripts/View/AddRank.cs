using System.Collections.Generic;
using UnityEngine.UIElements;
using UnityEngine;

namespace View
{
    public class AddRank : MonoBehaviour
    {
        [Header("Controller")]
        [SerializeField] private Controller.RankManager rankManager = null;

        [Header("Visual Element")]
        [SerializeField] private string textFieldName = "TextField";
        [SerializeField] private string addButtonName = "AddButton";

        private UIDocument uiDocument = null;
        private TextField textField = null;
        private Button addButton = null;

        private bool _canAdd = true;

        private void OnEnable()
        {
            if (TryGetComponent<UIDocument>(out uiDocument))
            {
                textField = uiDocument.rootVisualElement.Q<TextField>(textFieldName);
                addButton = uiDocument.rootVisualElement.Q<Button>(addButtonName);
                SetUICallback();
            }
            Controller.RankManager.onUpdateRanking += SetAddStatus;
        }
        private void OnDisable()
        {
            Controller.RankManager.onUpdateRanking -= SetAddStatus;
        }

        /// <summary>
        /// Subscribe text field and button to specific behaviour.
        /// </summary>
        private void SetUICallback()
        {
            if (addButton != null)
                addButton.clicked += Add;
            if (textField != null)
            {
                textField.RegisterCallback<KeyUpEvent>(evt =>
                {
                    if (_canAdd && (evt.keyCode == KeyCode.Return || evt.keyCode == KeyCode.KeypadEnter))
                        Add();
                });
            }
        }
        /// <summary>
        /// Called after ranklist update.
        /// Define if it is possible to add new rank compared to slot limitation and refresh UI.
        /// </summary>
        /// <param name="rankList"></param>
        private void SetAddStatus(List<string> rankList)
        {
            if (rankManager != null)
                _canAdd = rankList.Count < rankManager.MaxSlot;
            SetButtonStatus();
        }
        /// <summary>
        /// Set if add button can be enabled or not.
        /// </summary>
        private void SetButtonStatus()
        {
            if (addButton != null)
                addButton.SetEnabled(_canAdd);
        }
        /// <summary>
        /// Add new rank in list.
        /// </summary>
        private void Add()
        {
            if (textField != null && textField.text.Length > 0)
            {
                if (rankManager != null)
                    rankManager.AddRank(textField.text);
                textField.value = "";
            }
        }
    }
}
