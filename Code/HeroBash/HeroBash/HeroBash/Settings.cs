using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.IO;


#if WINRT
using Windows.System.Threading;
using System.Threading.Tasks;
using Windows.Storage;
#endif

#if !WINRT
using System.IO.IsolatedStorage;
#endif

namespace HeroBash
{
    public static class Settings
    {
        public static void Save()
        {
#if WINDOWS || LINUX
            BackgroundWorker bw = new BackgroundWorker();
            bw.DoWork += bw_DoSave;
            bw.RunWorkerAsync();
#endif

#if WINRT
            ThreadPool.RunAsync(async delegate { DoSave(); });
#endif
        }

        public static void Load()
        {
#if WINDOWS || LINUX
            BackgroundWorker bw = new BackgroundWorker();
            bw.DoWork += bw_DoLoad;
            bw.RunWorkerAsync();
#endif
#if WINRT
            ThreadPool.RunAsync(async delegate { DoLoad(); });
#endif
        }

#if WINDOWS || LINUX
        static void bw_DoSave(object sender, DoWorkEventArgs e)
        {
            try
            {
                IsolatedStorageFile iso = IsolatedStorageFile.GetUserStoreForDomain();
                IsolatedStorageFileStream isoStream = iso.OpenFile("settings", FileMode.Create);
                StreamWriter sw = new StreamWriter(isoStream);
                sw.WriteLine(GameManager.PlayerName);
                sw.WriteLine(GameManager.PlayerID.ToString());
                sw.Flush();
                sw.Close();
                iso.Close();
            }
            catch (Exception ex) {  }
        }
        static void bw_DoLoad(object sender, DoWorkEventArgs e)
        {
            try
            {
                IsolatedStorageFile iso = IsolatedStorageFile.GetUserStoreForDomain();
                IsolatedStorageFileStream isoStream = iso.OpenFile("settings", FileMode.Open);
                StreamReader sw = new StreamReader(isoStream);
                GameManager.PlayerName = sw.ReadLine();
                GameManager.PlayerID = Guid.Parse(sw.ReadLine());
                sw.Close();
                iso.Close();
            }
            catch (Exception ex) {
                GameManager.PlayerName = "Player";
                GameManager.PlayerID = Guid.NewGuid();
                Settings.Save();
            }
        }
#endif
#if WINRT
        static async Task DoSave()
        {
            try
            {
                StorageFolder folder = Windows.Storage.ApplicationData.Current.LocalFolder;

                StorageFile file = await folder.CreateFileAsync("settings", CreationCollisionOption.ReplaceExisting);
                using (Stream stream = await file.OpenStreamForWriteAsync())
                {
                    StreamWriter sw = new StreamWriter(stream);
                
                    sw.WriteLine(GameManager.PlayerName);
                    sw.WriteLine(GameManager.PlayerID.ToString());
                    sw.Flush();
                }


            }
            catch (Exception ex) {  }
        }
        static async Task DoLoad()
        {
            try
            {

                StorageFolder folder = Windows.Storage.ApplicationData.Current.LocalFolder;

                StorageFile file = await folder.GetFileAsync("settings");
                using (Stream stream = await file.OpenStreamForWriteAsync())
                {
                    StreamReader sw = new StreamReader(stream);

                    GameManager.PlayerName = sw.ReadLine();
                    GameManager.PlayerID = Guid.Parse(sw.ReadLine());
                }
            }
            catch (Exception ex)
            {
                GameManager.PlayerName = "Player";
                GameManager.PlayerID = Guid.NewGuid();
                Settings.Save();
            }
        }
#endif

    }
}
