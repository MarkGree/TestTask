﻿// Cristian Pop - https://boxophobic.com/

using Boxophobic.Constants;
using UnityEditor;
using UnityEngine;

namespace Boxophobic.StyledGUI
{
    public partial class StyledGUI 
    {
        public static void DrawInspectorCategory(string bannerText)
        {
            var fullRect = GUILayoutUtility.GetRect(0, 0, 18, 0);
            var fillRect = new Rect(0, fullRect.y, fullRect.xMax + 10, 18);
            var lineRect = new Rect(0, fullRect.y, fullRect.xMax + 10, 1);
            var titleRect = new Rect(fullRect.position.x - 1, fullRect.position.y, fullRect.width, 18);

            EditorGUI.DrawRect(fillRect, CONSTANT.CategoryColor);
            EditorGUI.DrawRect(lineRect, CONSTANT.LineColor);

            GUI.Label(titleRect, bannerText, CONSTANT.HeaderStyle);
        }

        public static bool DrawInspectorCategory(string bannerText, bool enabled, float top, float down, bool colapsable)
        {
            if (colapsable)
            {
                if (enabled)
                {
                    GUILayout.Space(top);
                }
                else
                {
                    GUILayout.Space(0);
                }
            }
            else
            {
                GUILayout.Space(top);
            }

            var fullRect = GUILayoutUtility.GetRect(0, 0, 18, 0);
            var fillRect = new Rect(0, fullRect.y, fullRect.xMax + 10, 18);
            var lineRect = new Rect(0, fullRect.y, fullRect.xMax + 10, 1);
            var titleRect = new Rect(fullRect.position.x - 1, fullRect.position.y, fullRect.width, 18);

            if (colapsable)
            {
                if (GUI.Button(fullRect, "", GUIStyle.none))
                {
                    enabled = !enabled;
                }
            }
            else
            {
                enabled = true;
            }

            EditorGUI.DrawRect(fillRect, CONSTANT.CategoryColor);
            EditorGUI.DrawRect(lineRect, CONSTANT.LineColor);

            GUI.Label(titleRect, bannerText, CONSTANT.HeaderStyle);

            if (colapsable)
            {
                if (enabled)
                {
                    GUILayout.Space(down);
                }
                else
                {
                    GUILayout.Space(0);
                }
            }
            else
            {
                GUILayout.Space(down);
            }

            return enabled;
        }
    }
}

