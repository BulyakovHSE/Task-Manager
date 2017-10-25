using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Storage;
using Newtonsoft.Json;

namespace Task_Manager_UWP.Model
{
    public static class DataProvider<T>
    {
        public static async void Serialize(IEnumerable<T> data)
        {
            string json = JsonConvert.SerializeObject(data, Formatting.Indented, new JsonSerializerSettings());
            StorageFolder folder = ApplicationData.Current.LocalFolder;
            StorageFile file = await folder.CreateFileAsync("Tasks.json", CreationCollisionOption.ReplaceExisting);
            await FileIO.WriteTextAsync(file, json);
        }

        public static void Serialize(T data)
        {
            Serialize(new []{data});
        }

        public static async Task<IEnumerable<T>> Deserialize()
        {
            StorageFolder folder = ApplicationData.Current.LocalFolder;
            StorageFile file = await folder.GetFileAsync("Tasks.json");
            string json = await FileIO.ReadTextAsync(file);
            return JsonConvert.DeserializeObject<IEnumerable<T>>(json);
        }
    }
}
