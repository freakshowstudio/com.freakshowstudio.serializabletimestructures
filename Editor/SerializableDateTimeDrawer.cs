
using System;

using UnityEngine;
using UnityEditor;

using FreakshowStudio.SerializableTimeStructures.Runtime;


namespace FreakshowStudio.SerializableTimeStructures.Editor
{
    [CustomPropertyDrawer(typeof(SerializableDateTime))]
    public class SerializableDateTimeDrawer
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

        private GUIStyle GetNowButtonStyle()
        {
            var style = new GUIStyle(EditorStyles.miniButtonMid);
            style.fontSize = 6;
            style.margin = new RectOffset(0, 0, 0, 0);
            style.padding = new RectOffset(0, 0, 0, 0);
            return style;
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
                16f;

            var prefixPosition = new Rect(
                position.x, position.y + labelHeight,
                xPos, fieldHeight);

            EditorGUI.HandlePrefixLabel(
                position, prefixPosition, new GUIContent(label));

            var indent = EditorGUI.indentLevel;
            EditorGUI.indentLevel = 0;

            var prop = property.FindPropertyRelative("_ticks");
            var ticks = prop.longValue;
            var dt = new DateTime(ticks);

            var yearLabelPosition = new Rect(
                xPos, labelY,
                w * 4f, labelHeight);
            var yearPosition = new Rect(
                xPos, fieldY,
                w * 4f, fieldHeight);

            var monthLabelPosition = new Rect(
                xPos + (4f * w), labelY,
                w * 2f, labelHeight);
            var monthPosition = new Rect(
                xPos + (4f * w), fieldY,
                w * 2f, fieldHeight);

            var dayLabelPosition = new Rect(
                xPos + (6f * w), labelY,
                w * 2f, labelHeight);
            var dayPosition = new Rect(
                xPos + (6f * w), fieldY,
                w * 2f, fieldHeight);

            var hourLabelPosition = new Rect(
                xPos + (9f * w), labelY,
                w * 2f, labelHeight);
            var hourPosition = new Rect(
                xPos + (9f * w), fieldY,
                w * 2f, fieldHeight);

            var minuteLabelPosition = new Rect(
                xPos + (11f * w), labelY,
                w * 2f, labelHeight);
            var minutePosition = new Rect(
                xPos + (11f * w), fieldY,
                w * 2f, fieldHeight);

            var secondLabelPosition = new Rect(
                xPos + (13f * w), labelY,
                w * 2f, labelHeight);
            var secondPosition = new Rect(
                xPos + (13f * w), fieldY,
                w * 2f, fieldHeight);

            var nowButtonPosition = new Rect(
                xPos + (15 * w), fieldY,
                w, fieldHeight);

            EditorGUI.LabelField(
                yearLabelPosition,
                new GUIContent("Year"),
                labelStyle);

            EditorGUI.LabelField(
                monthLabelPosition,
                new GUIContent("Month"),
                labelStyle);

            EditorGUI.LabelField(
                dayLabelPosition,
                new GUIContent("Day"),
                labelStyle);

            EditorGUI.LabelField(
                hourLabelPosition,
                new GUIContent("Hr"),
                labelStyle);

            EditorGUI.LabelField(
                minuteLabelPosition,
                new GUIContent("Min"),
                labelStyle);

            EditorGUI.LabelField(
                secondLabelPosition,
                new GUIContent("Sec"),
                labelStyle);

            var year = EditorGUI.IntField(yearPosition, dt.Year);
            var month = EditorGUI.IntField(monthPosition, dt.Month);
            var day = EditorGUI.IntField(dayPosition, dt.Day);
            var hour = EditorGUI.IntField(hourPosition, dt.Hour);
            var minute = EditorGUI.IntField(minutePosition, dt.Minute);
            var second = EditorGUI.IntField(secondPosition, dt.Second);

            try
            {
                var newTime = new DateTime(
                    year,
                    month,
                    Math.Min(day, DateTime.DaysInMonth(year, month)),
                    hour,
                    minute,
                    second);

                prop.longValue = newTime.Ticks;
            }
            catch (ArgumentOutOfRangeException) {}

            if (GUI.Button(
                nowButtonPosition,
                "Now",
                GetNowButtonStyle()))
            {
                prop.longValue = DateTime.Now.Ticks;
            }

            EditorGUI.indentLevel = indent;

            EditorGUI.EndProperty();
        }
    }
}
