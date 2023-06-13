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
            var path = FileHandler.GetPath(filename);

            if (File.Exists(path))
            {
                var str = File.ReadAllText(path);
                var saver = JsonUtility.FromJson<Saver<T>>(str);
                data = saver.data;
            }
        }

        public static void TrySave(string filename, T[] data)
        {
            var path = FileHandler.GetPath(filename);
            var wrapper = new Saver<T> { data = data };
            var json = JsonUtility.ToJson(wrapper, true);
            File.WriteAllText(path, json);
        }
    }

    public static class FileHandler
    {
        public static string GetPath(string filename)
        {
            return $"{Application.persistentDataPath}/{filename}";
        }

        public static void Reset(string filename)
        {
            var path = FileHandler.GetPath(filename);

            if (File.Exists(path))
            {
                File.Delete(path);
            }
        }

        public static bool HasFile(string filename)
        {
            var path = FileHandler.GetPath(filename);

            return File.Exists(path);
        }
    }
}
