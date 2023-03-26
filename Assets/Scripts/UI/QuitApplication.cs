using UnityEngine;
using UnityEngine.UIElements;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace UI
{
    public class QuitApplication : MonoBehaviour
    {
        [SerializeField] private string VisualElementName = "Quit";

        private UIDocument uiDocument = null;
        private Button quitButton = null;

        void Start()
        {
            // Connect with UI
            if (TryGetComponent<UIDocument>(out uiDocument))
            {
                // Get Specific button
                quitButton = uiDocument.rootVisualElement.Q<Button>(VisualElementName);
                // Set call function
                if (quitButton != null) quitButton.clicked += Exit;
            }
        }

        /// <summary>
        /// Quit the current execution, whether it's a build or in the editor
        /// </summary>
        private void Exit()
        {
            #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
            #else 
            Application.Quit();
            #endif
        }
    }
}
