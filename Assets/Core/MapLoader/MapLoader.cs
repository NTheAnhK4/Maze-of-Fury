using UnityEditor;
using UnityEngine;
using System.IO;
using System.Collections.Generic;

[System.Serializable]
public class BlockData {
    public string type;
    public float[] position;  // Dữ liệu position dưới dạng mảng float
    public float[] scale;     // Dữ liệu scale dưới dạng mảng float
}

[System.Serializable]
public class BlockDataList {
    public List<BlockData> blocks;
}

public class MapLoader : ParentBehavior
{
    public int level;
    public GameObject wallPrefab;
    public GameObject keyPrefab;
    public GameObject monsterPrefab;

    public void LoadMap()
    {
        // Đường dẫn tới file JSON
        string fileName = $"Level{level}.json";
        string filePath = Path.Combine(Application.dataPath, "Game/Data/Maps", fileName);

        if (File.Exists(filePath)) {
            // Đọc nội dung file JSON
            string json = File.ReadAllText(filePath);

            // Chuyển đổi JSON thành danh sách các block
            BlockDataList blockDataList = JsonUtility.FromJson<BlockDataList>("{\"blocks\":" + json + "}");


            // Xóa GameObject cũ (nếu có) và tạo mới (chỉ trong Editor)
            #if UNITY_EDITOR
            GameObject levelParent = GameObject.Find($"Level{level}");
            if (levelParent != null) {
                DestroyImmediate(levelParent); // Dùng DestroyImmediate trong editor để xóa ngay lập tức
            }
            levelParent = new GameObject($"Level{level}");
            #else
            GameObject levelParent = new GameObject($"Level{level}");
            #endif

            // Duyệt qua tất cả block và thu thập các type cần tạo
            HashSet<string> typesToCreate = new HashSet<string>();
            foreach (BlockData block in blockDataList.blocks) {
                typesToCreate.Add(block.type);
            }

            // Tạo các GameObject con cho từng loại block có trong JSON
            foreach (string type in typesToCreate) {
                GameObject typeParent = new GameObject(type);
                typeParent.transform.SetParent(levelParent.transform);

                // Duyệt qua các block và tạo GameObject tương ứng với type
                foreach (BlockData block in blockDataList.blocks) {
                    if (block.type == type) {
                        GameObject prefab = GetPrefab(block.type);
                        if (prefab != null) {
                            // Lấy vị trí và scale từ block
                            Vector3 position = new Vector3(block.position[0], block.position[1], block.position[2]);
                            Vector3 scale = new Vector3(block.scale[0], block.scale[1], block.scale[2]);

                            // Tạo GameObject và áp dụng scale
                            GameObject blockObject = Instantiate(prefab, position, Quaternion.identity);
                            blockObject.transform.SetParent(typeParent.transform);
                            blockObject.transform.localScale = scale;  // Áp dụng scale
                        }
                    }
                }
            }
        }
        else {
            Debug.LogError($"File {filePath} không tồn tại!");
        }
    }

    // Hàm lấy prefab dựa trên loại block
    public GameObject GetPrefab(string type) {
        switch (type) {
            case "Wall": return wallPrefab;
            case "Key": return keyPrefab;
            case "Monster": return monsterPrefab;
            default: return null;
        }
    }
}
