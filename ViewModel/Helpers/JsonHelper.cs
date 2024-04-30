using Newtonsoft.Json;
using System;
using System.IO;
using System.Windows;


namespace Calendar.Model
{
    public static class JsonHelper
    {
        public static void Serialize<T>(T obj, string filePath)
        {
            try
            {
                string json = JsonConvert.SerializeObject(obj, Formatting.Indented);
                File.WriteAllText(filePath, json);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при сериализации: " + ex.Message);
            }
        }

        public static T Deserialize<T>(string filePath)
        {
            try
            {
                string json = File.ReadAllText(filePath);
                return JsonConvert.DeserializeObject<T>(json);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при десериализации: " + ex.Message);
                return default;
            }
        }
    }
}
