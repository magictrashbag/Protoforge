// Copyright (c) 2018 Maxim Tiourin
// Please direct any bug reports/feedback to maximtiourin@gmail.com

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace Fizzik {
    [CustomEditor(typeof(HealthBarComponent))]
    [CanEditMultipleObjects]
    public class HealthBarComponentEditor : Editor {
        SerializedProperty propImage;
        SerializedProperty propCurrentHealth;
        SerializedProperty propTotalHealth;
        SerializedProperty propHealthColor;
        SerializedProperty propDamageColor;

        SerializedProperty propTickEnabled;
        SerializedProperty propHealthPerTick;
        SerializedProperty propTickWidth;
        SerializedProperty propTickHeightPercent;
        SerializedProperty propTickColor;

        SerializedProperty propBigTickEnabled;
        SerializedProperty propHealthPerBigTick;
        SerializedProperty propBigTickWidth;
        SerializedProperty propBigTickHeightPercent;
        SerializedProperty propBigTickColor;

        SerializedProperty propBorderEnabled;
        SerializedProperty propBorderWidth;
        SerializedProperty propBorderColor;

        void OnEnable() {
            propImage = serializedObject.FindProperty("image");
            propCurrentHealth = serializedObject.FindProperty("currentHealth");
            propTotalHealth = serializedObject.FindProperty("totalHealth");
            propHealthColor = serializedObject.FindProperty("healthColor");
            propDamageColor = serializedObject.FindProperty("damageColor");

            propTickEnabled = serializedObject.FindProperty("tickEnabled");
            propHealthPerTick = serializedObject.FindProperty("healthPerTick");
            propTickWidth = serializedObject.FindProperty("tickWidth");
            propTickHeightPercent = serializedObject.FindProperty("tickHeightPercent");
            propTickColor = serializedObject.FindProperty("tickColor");

            propBigTickEnabled = serializedObject.FindProperty("bigTickEnabled");
            propHealthPerBigTick = serializedObject.FindProperty("healthPerBigTick");
            propBigTickWidth = serializedObject.FindProperty("bigTickWidth");
            propBigTickHeightPercent = serializedObject.FindProperty("bigTickHeightPercent");
            propBigTickColor = serializedObject.FindProperty("bigTickColor");

            propBorderEnabled = serializedObject.FindProperty("borderEnabled");
            propBorderWidth = serializedObject.FindProperty("borderWidth");
            propBorderColor = serializedObject.FindProperty("borderColor");
        }

        public override void OnInspectorGUI() {
            serializedObject.Update();

            HealthBarComponent ct = (HealthBarComponent) target;

            //General
            EditorGUILayout.PropertyField(propImage, new GUIContent("Image", "The UnityEngine.UI.Image component used for drawing the health bar. Assigned automatically if one is found."));
            EditorGUILayout.PropertyField(propCurrentHealth, new GUIContent("Current Health", "The current Health to display."));
            EditorGUILayout.PropertyField(propTotalHealth, new GUIContent("Total Health", "The total Health of the Health Bar, used for determining health and tick distribution."));
            EditorGUILayout.PropertyField(propHealthColor, new GUIContent("Health Color", "The Color of the Current Health portion of the Health Bar."));
            EditorGUILayout.PropertyField(propDamageColor, new GUIContent("Damage Color", "The Color of the Damaged Health portion of the Health Bar."));

            //Tick
            EditorGUILayout.PropertyField(propTickEnabled, new GUIContent("Health Tick Enabled", "Whether or not to draw small health ticks"));
            if (propTickEnabled.boolValue) {
                EditorGUI.indentLevel++;

                EditorGUILayout.PropertyField(propHealthPerTick, new GUIContent("Health Per Tick", "How much Health should be contained within a \"tick\"."));
                EditorGUILayout.PropertyField(propTickWidth, new GUIContent("Tick Width", "How wide a Tick should be drawn in units, will be consistent regardless of Health Bar size."));
                EditorGUILayout.PropertyField(propTickHeightPercent, new GUIContent("Tick Height %", "How much of the height of the health bar the tick should cover from the top to the bottom."));
                EditorGUILayout.PropertyField(propTickColor, new GUIContent("Tick Color", "The Color of the Tick lines in the Health Bar."));

                EditorGUI.indentLevel--;
            }

            //Big Tick
            EditorGUILayout.PropertyField(propBigTickEnabled, new GUIContent("Health Big Tick Enabled", "Whether or not to draw big health ticks"));
            if (propBigTickEnabled.boolValue) {
                EditorGUI.indentLevel++;

                EditorGUILayout.PropertyField(propHealthPerBigTick, new GUIContent("Health Per Tick", "How much Health should be contained within a \"tick\"."));
                EditorGUILayout.PropertyField(propBigTickWidth, new GUIContent("Tick Width", "How wide a Tick should be drawn in units, will be consistent regardless of Health Bar size."));
                EditorGUILayout.PropertyField(propBigTickHeightPercent, new GUIContent("Tick Height %", "How much of the height of the health bar the tick should cover from the top to the bottom."));
                EditorGUILayout.PropertyField(propBigTickColor, new GUIContent("Tick Color", "The Color of the Tick lines in the Health Bar."));

                EditorGUI.indentLevel--;
            }

            //Border
            EditorGUILayout.PropertyField(propBorderEnabled, new GUIContent("Border Enabled", "Whether or not to draw an inset border on the health bar."));
            if (propBorderEnabled.boolValue) {
                EditorGUI.indentLevel++;

                EditorGUILayout.PropertyField(propBorderWidth, new GUIContent("Border Width", "How wide an edge of the border should be in units, will be consistent regardless of Health Bar size."));
                EditorGUILayout.PropertyField(propBorderColor, new GUIContent("Border Color", "The Color of the Border in the Health Bar."));

                EditorGUI.indentLevel--;
            }

            //EditorGUILayout.PropertyField(prop, new GUIContent("", ""));

            serializedObject.ApplyModifiedProperties();
        }
    }
}
