using UnityEditor;


[CustomEditor(typeof(MapLoader))]
public class MapLoaderEditor : Editor
{
    // Thêm mục vào menu ba chấm (context menu) trên Inspector
    [MenuItem("CONTEXT/MapLoader/Load Map")]
    private static void LoadMapFromContextMenu(MenuCommand command)
    {
        MapLoader mapLoader = (MapLoader)command.context;
        if (mapLoader != null)
        {
            mapLoader.LoadMap();
        }
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        // Các tùy chỉnh khác cho Inspector có thể thêm vào đây
    }
}