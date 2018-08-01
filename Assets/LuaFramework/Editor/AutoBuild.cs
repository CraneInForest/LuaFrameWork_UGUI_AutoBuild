//
// @brief: autobuild functions
// @version: 1.0.0
// @author helin
// @date: 08/01/2018
//

using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class AutoBuild
{

    [MenuItem("AutoBuild/Build  _%#_d", false, 100)]
    public static void build()
    {
        PlayerPrefs.SetInt("autobuild", 1);
        Packager.BuildResource();
    }

    [MenuItem("AutoBuild/BuildAndRun  _%#_x", false, 100)]
    public static void buildAndRun()
    {
        PlayerPrefs.SetInt("autobuild", 2);
        Packager.BuildResource();
    }

    [MenuItem("AutoBuild/clearAndBuild  _%#_f", false, 100)]
    public static void clearAndBuild()
    {
        PlayerPrefs.SetInt("clearAndBuild", 1);
        ToLuaMenu.ClearLuaWraps();
    }

    [UnityEditor.Callbacks.DidReloadScripts]
    public static void OnScriptsReloaded()
    {
        if (PlayerPrefs.GetInt("clearAndBuild") == 1)
        {
            PlayerPrefs.SetInt("clearAndBuild", 0);
            EditorApplication.delayCall = build;
        }
        else if (PlayerPrefs.GetInt("autobuild") == 1)
        {
            PlayerPrefs.SetInt("autobuild", 0);
            PlayerPrefs.SetInt("exportPackage", 1);
            EditorApplication.delayCall = justBuildPackage;
        }
        else if (PlayerPrefs.GetInt("autobuild") == 2)
        {
            PlayerPrefs.SetInt("autobuild", 0);
            PlayerPrefs.SetInt("exportPackage", 1);
            EditorApplication.delayCall = buildPackageAndRun;
        }
        else if (PlayerPrefs.GetInt("exportPackage") == 1)
        {
            PlayerPrefs.SetInt("exportPackage", 0);

            if (EditorUtility.DisplayDialog("Complete", "Export package successful!", "Open Folder"))
            {
                System.Diagnostics.Process.Start("D:/unity_package/luaframework");
            };
        }
    }

    static void justBuildPackage()
    {
        makePackage(false);
    }

    static void buildPackageAndRun()
    {
        makePackage(true);
    }

    static void makePackage(bool isRun)
    {
        string[] levels = { "Assets/LuaFramework/Scenes/main.unity"};
        BuildPlayerOptions option = new BuildPlayerOptions();
        option.scenes = levels;

        //caculate current datetime
        string nowTime = System.DateTime.Now.ToString("MMdd-HH_mm");

        string filePath = "D:/unity_package/luaframework/luaframework" + nowTime;

#if UNITY_ANDROID
        filePath += ".apk";
        option.target = BuildTarget.Android;
#elif UNITY_IPHONE
        option.target = BuildTarget.iOS;
#else
        filePath += ".exe";
        option.target = BuildTarget.StandaloneWindows;
#endif
        option.locationPathName = filePath;

        if (isRun)
        {
            option.options = BuildOptions.AutoRunPlayer;
        }
        else
        {
            option.options = BuildOptions.None;
        }

        BuildPipeline.BuildPlayer(option);
    }
}
