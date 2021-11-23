using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(ButtonOverride))]
public class ButtonOverrideEditor : UnityEditor.UI.ButtonEditor
{
    private SerializedProperty testProperty1;
    private SerializedProperty testProperty2;

    protected override void OnEnable()
    {
        base.OnEnable();

        testProperty1 = serializedObject.FindProperty("test_GameObject");
        testProperty2 = serializedObject.FindProperty("test_string");
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();//Draw inspector UI of ImageEditor

        EditorGUILayout.PropertyField(testProperty1);//显示test_GameObject字段
        EditorGUILayout.PropertyField(testProperty2);//显示test_string字段

        ButtonOverride btnOverride = (ButtonOverride)target;
        btnOverride.test_bool = EditorGUILayout.Toggle("bool", btnOverride.test_bool);//显示test_bool字段
        btnOverride.test_int = EditorGUILayout.IntField("int", btnOverride.test_int);//显示test_int字段

        //DrawDefaultInspector();//这会用默认的方式显示脚本的字段
        serializedObject.ApplyModifiedProperties();//这会显示上述的字段，但是不用默认的方式显示脚本的字段，因为DrawDefaultInspector没有被调用
    }
}