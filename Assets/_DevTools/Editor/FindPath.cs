using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

/// <summary> 快速获取物体路径 </summary>
public class FindPath
{
    public static List<string> filelist = new List<string>();
    public static List<string> deallist = new List<string>();
    public static string filepath;

    [MenuItem("Tools/Copy Path %`", priority = 0)]
    public static void GetPath()
    {
        clearmemory();
        recursiveFind(Selection.activeGameObject.gameObject);
        CopyPath();
        clearmemory();
    }

    public static void recursiveFind(GameObject go)
    {
        if (go != null)
        {
            filelist.Add(go.name);
            if (go.transform.parent != null)
                recursiveFind(go.transform.parent.gameObject);
        }
    }

    public static void clearmemory()
    {
        filelist.Clear();
        deallist.Clear();
    }

    public static void CopyPath()
    {
        for (int i = filelist.Count - 1; i >= 0; i--)
        {
            string str = filelist[i];
            if (i != 0)
                str = str + "/";
            deallist.Add(str);
        }
        string path = "";
        foreach (var list in deallist)
            path += list;
        GUIUtility.systemCopyBuffer = path;
    }
}
