using System;
using System.IO;
using UnityEngine;

namespace TowerDefence
{
    [Serializable]
    public class Saver<T>
    {
        public T[] data;

        public static void TryLoad(string filename, ref T[] data)
        {
            var path = GetPath(filename);

            if (File.Exists(path))
            {
                var str = File.ReadAllText(path);
                var saver = JsonUtility.FromJson<Saver<T>>(str);
                data = saver.data;
            }
            else
            {

            }
        }

        public static void TrySave(string filename, T[] data)
        {
            var path = GetPath(filename);
            var wrapper = new Saver<T> { data = data };
            var json = JsonUtility.ToJson(wrapper);
            File.WriteAllText(path, json);
        }

        private static string GetPath(string filename)
        {
            return $"{Application.persistentDataPath}/{filename}";
        }
    }
}
