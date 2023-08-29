using UnityEngine;
using UnityEditor;
using System.IO;
using UnityEngine.UIElements;
using UnityEditor.UIElements;

public class TestWindow : EditorWindow
{
    [SerializeField] VisualTreeAsset _tree;

    //GameSpeed
    private FloatField gameSpeed;

    //골드량
    private IntegerField goldAmount;

    VisualElement root;

    [MenuItem("Tools/Test")]
    private static void ShowWindow()
    {
        var window = GetWindow<TestWindow>();
        window.titleContent = new GUIContent("Test");
        window.ShowPopup();
    }
    void CreateGUI()
    {
        root = rootVisualElement;
        _tree.CloneTree(root);

        //게임 스피드 
        gameSpeed = root.Q<FloatField>("GameSpeed");
        gameSpeed.RegisterValueChangedCallback(x => Time.timeScale = x.newValue);


        goldAmount = root.Q<IntegerField>("GoldAmount");
        goldAmount.RegisterValueChangedCallback(x => GoldManager.Instance.SetGold(x.newValue));


    }

}
