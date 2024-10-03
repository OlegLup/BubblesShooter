using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEditor.UIElements;
using Levels;
using System.Collections.Generic;
using GameField;
using Items;

public class LevelSettingsEditor : EditorWindow
{
    [MenuItem("Window/UI Toolkit/LevelSettingsEditor")]
    public static void ShowExample()
    {
        LevelSettingsEditor wnd = GetWindow<LevelSettingsEditor>();
        wnd.titleContent = new GUIContent("LevelSettingsEditor");
    }

    private GameFieldSettings gameFieldSettings;
    private LevelSettings levelSettings;
    private VisualElement content;
    private ObjectField levelSettingsOjectField;
    private IntegerField shots;
    private List<VisualElement> itemRows;

    public void CreateGUI()
    {
        VisualElement root = rootVisualElement;

        gameFieldSettings = AssetDatabase.LoadAssetAtPath<GameFieldSettings>("Assets/Settings/GameFieldSettings.asset");

        Debug.Assert(gameFieldSettings != null);

        var visualTree = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>("Assets/Scripts/Editor/LevelSettingsEditor.uxml");
        VisualElement labelFromUXML = visualTree.Instantiate();
        root.Add(labelFromUXML);

        var objectFields = root.Query<ObjectField>();
        levelSettingsOjectField = objectFields.Name("Level");
        levelSettingsOjectField.RegisterValueChangedCallback(OnLevelValueChanges);

        var intFields = root.Query<IntegerField>();
        shots = intFields.Name("Shots");
        shots.RegisterCallback<FocusOutEvent>(OnShotsValueChanges);

        var elements = root.Query<VisualElement>();
        content = elements.Name("Content");
        VisualElement itemsRoot = elements.Name("DefaltItems");
        var columns = itemsRoot.Query<VisualElement>();
        itemRows = columns.Children<VisualElement>(name = "ItemsRow").ToList();
        var items = itemsRoot.Query<Button>();
        items.ForEach((i) => i.RegisterCallback<ClickEvent>(OnItemClick));

        content.style.visibility = Visibility.Hidden;
    }

    private void OnLevelValueChanges(ChangeEvent<Object> evt)
    {
        if (!IsLevelChoosen())
        {
            content.style.visibility = Visibility.Hidden;
            return;
        }

        shots.value = levelSettings.shots;

        for (int h = 0; h < itemRows.Count; h++)
        {
            var items = itemRows[h].Query<Button>().ToList();

            for (int w = 0; w < items.Count; w++)
            {
                items[w].style.unityBackgroundImageTintColor = gameFieldSettings.GetItemColor(levelSettings.rows[h].items[w]);
            }
        }

        content.style.visibility = Visibility.Visible;
    }

    private void OnShotsValueChanges(FocusOutEvent evt)
    {
        if (!IsLevelChoosen()) return;

        levelSettings.shots = shots.value;

        EditorUtility.SetDirty(levelSettings);
    }

    private void OnItemClick(ClickEvent evt)
    {
        Button button = evt.currentTarget as Button;
        int h = button.parent.parent.IndexOf(button.parent);
        int w = button.parent.IndexOf(button);

        levelSettings.rows[h].items[w] = GetNextItemType(levelSettings.rows[h].items[w]);
        button.style.unityBackgroundImageTintColor = gameFieldSettings.GetItemColor(levelSettings.rows[h].items[w]);

        EditorUtility.SetDirty(levelSettings);
    }

    private bool IsLevelChoosen()
    {
        levelSettings = null;

        if (levelSettingsOjectField.value != null && levelSettingsOjectField.value is LevelSettings)
        {
            levelSettings = levelSettingsOjectField.value as LevelSettings;
            return true;
        }

        return false;
    }

    private ItemType GetNextItemType(ItemType currentItemType)
    {
        int result = (int)currentItemType + 1;

        if (result == (int)ItemType.Last) result = 0;

        return (ItemType)result;
    }
}
