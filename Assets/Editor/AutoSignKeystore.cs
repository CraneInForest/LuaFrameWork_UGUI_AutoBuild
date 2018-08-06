using UnityEngine;
using UnityEditor;
using System.IO;

[InitializeOnLoad]
public class AutoSignKeystore
{

    static AutoSignKeystore()
    {
        PlayerSettings.Android.keystoreName = "D:/keystore/luaframework.keystore";
        PlayerSettings.Android.keystorePass = "luaframework";
        PlayerSettings.Android.keyaliasName = "luafw";
        PlayerSettings.Android.keyaliasPass = "luaframework";
    }

}