using System.Linq;
using UnityEditor;
using UnityEngine;

namespace WhiteArrowEditor
{
    public static class EditorGuiUtility
    {
        public static void DrawWithReadOnlyFields(SerializedObject serializedObject, params string[] fieldNamesForReadOnly)
        {
            serializedObject.Update();

            var iterator = serializedObject.GetIterator();
            var enterChildren = true;
            while (iterator.NextVisible(enterChildren))
            {
                var isReadOnly = fieldNamesForReadOnly.Contains(iterator.name);

                if (isReadOnly)
                    GUI.enabled = false;

                EditorGUILayout.PropertyField(iterator, includeChildren: true);
                enterChildren = false;

                if (isReadOnly)
                    GUI.enabled = true;
            }

            serializedObject.ApplyModifiedProperties();
        }
    }
}