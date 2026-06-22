using System;using System.Collections;
using System.Collections.Generic;
using System.IO;
using LitJson;
using UnityEngine;

public enum JsonType
{
    JsonUtility,
    LitJson
}
/// <summary>
/// 对玩家数据进行序列化和反序列化的工具，数据存储的方式是Json
/// </summary>
public class JsonManager
{
    private static JsonManager instance;

    public static JsonManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new JsonManager();
            }

            return instance;
        }
    }
    private JsonManager(){}

    /// <summary>
    /// 存储Json 序列化
    /// </summary>
    /// <param name="data"></param>
    /// <param name="fileName"></param>
    /// <param name="jsonType"></param>
    public void SaveData(object data, string fileName, JsonType jsonType = JsonType.LitJson)
    {
        //确定存储路径
        string path = Application.persistentDataPath + "/" + fileName + ".json";

        //将数据序列化 得到Json字符串
        string jsonStr = "";
        switch (jsonType)
        {
            case JsonType.JsonUtility:
                jsonStr = JsonUtility.ToJson(data);
                break;
            case JsonType.LitJson:
                jsonStr = JsonMapper.ToJson(data);
                break;
        }
        
        //将序列化后得到的Json字符串存入到指定路径的文件当中
        File.WriteAllText(path, jsonStr);
    }

    /// <summary>
    /// 用于读取指定文件当中的Json数据 用于反序列化
    /// </summary>
    /// <param name="fileName"></param>
    /// <param name="jsonType"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public T LoadData<T>(string fileName, JsonType jsonType = JsonType.LitJson) where T : new()
    {
        //先找玩家的本地存储路径里有没有
        string path = Application.persistentDataPath + "/" + fileName + ".json";
        //如果本地路径里没有，去默认存储的路径里去找
        if (!File.Exists(path))
        {
            path = Application.streamingAssetsPath + "/" + fileName + ".json";
        }

        //如果都没有 就返回一个默认对象
        if (!File.Exists(path))
        {
            return new T();
        }

        //拿到文件中的Json字符串
        string jsonStr = File.ReadAllText(path);
        T data = default(T);

        //得到数据
        switch (jsonType)
        {
            case JsonType.LitJson:
                data = JsonMapper.ToObject<T>(jsonStr);
                break;
            case JsonType.JsonUtility:
                data = JsonUtility.FromJson<T>(jsonStr);
                break;
        }

        //将反序列化出来的对象返回出去
        return data;
    }
}
