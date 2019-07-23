
using System;

using UnityEngine;
using UnityEditor;

using FreakshowStudio.SerializableTimeStructures.Runtime;


namespace FreakshowStudio.SerializableTimeStructures.Editor
{
    [CustomPropertyDrawer(typeof(SerializableTimeSpan))]
    public class SerializableTimeSpanDrawer
        : PropertyDrawer
    {
        private GUIStyle GetLabelStyle()
        {
            var labelStyle = new GUIStyle(EditorStyles.miniLabel);
            labelStyle.fontSize = 7;
            labelStyle.margin = new RectOffset(0, 0, 0, 0);
            labelStyle.padding = new RectOffset(0, 0, 0, 0);
            labelStyle.alignment = TextAnchor.LowerCenter;
            return labelStyle;
        }

        public override float GetPropertyHeight(
            SerializedProperty property,
            GUIContent label)
        {
            var s = GetLabelStyle();
            return
                s.lineHeight +
                EditorGUIUtility.singleLineHeight;
        }

        public override void OnGUI(
            Rect position,
            SerializedProperty property,
            GUIContent label)
        {
            EditorGUI.BeginProperty(position, label, property);

            var labelStyle = GetLabelStyle();

            var labelHeight = labelStyle.lineHeight;
            var fieldHeight = EditorGUIUtility.singleLineHeight;

            float labelY = position.y;
            float fieldY = position.y + labelHeight;

            float xPos = EditorGUIUtility.labelWidth +
                EditorStyles.inspectorDefaultMargins.padding.left;

            float w =
                (position.width - xPos +
                    EditorStyles.inspectorDefaultMargins.padding.left) /
                4f;

            var prefixPosition = new Rect(
                position.x, position.y + labelHeight,
                xPos, fieldHeight);

            EditorGUI.HandlePrefixLabel(
                position, prefixPosition, new GUIContent(label));

            var indent = EditorGUI.indentLevel;
            EditorGUI.indentLevel = 0;

            var prop = property.FindPropertyRelative("_ticks");
            var ticks = prop.longValue;
            var ts = new TimeSpan(ticks);

            var dayLabelPosition = new Rect(
                xPos + (0f * w), labelY,
                w, labelHeight);
            var dayPosition = new Rect(
                xPos + (0f * w), fieldY,
                w, fieldHeight);

            var hourLabelPosition = new Rect(
                xPos + (1f * w), labelY,
                w, labelHeight);
            var hourPosition = new Rect(
                xPos + (1f * w), fieldY,
                w, fieldHeight);

            var minuteLabelPosition = new Rect(
                xPos + (2f * w), labelY,
                w, labelHeight);
            var minutePosition = new Rect(
                xPos + (2f * w), fieldY,
                w, fieldHeight);

            var secondLabelPosition = new Rect(
                xPos + (3f * w), labelY,
                w, labelHeight);
            var secondPosition = new Rect(
                xPos + (3f * w), fieldY,
                w, fieldHeight);

            EditorGUI.LabelField(
                dayLabelPosition,
                new GUIContent("Days"),
                labelStyle);

            EditorGUI.LabelField(
                hourLabelPosition,
                new GUIContent("Hours"),
                labelStyle);

            EditorGUI.LabelField(
                minuteLabelPosition,
                new GUIContent("Minutes"),
                labelStyle);

            EditorGUI.LabelField(
                secondLabelPosition,
                new GUIContent("Seconds"),
                labelStyle);

            var days = EditorGUI.IntField(dayPosition, ts.Days);
            var hours = EditorGUI.IntField(hourPosition, ts.Hours);
            var minutes = EditorGUI.IntField(minutePosition, ts.Minutes);
            var seconds = EditorGUI.IntField(secondPosition, ts.Seconds);

            try
            {
                var newSpan = new TimeSpan(days, hours, minutes, seconds);
                prop.longValue = newSpan.Ticks;
            }
            catch (ArgumentOutOfRangeException) {}

            EditorGUI.indentLevel = indent;

            EditorGUI.EndProperty();
        }
    }
}
