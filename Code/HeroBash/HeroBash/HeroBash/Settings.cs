using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.IO;
using System.IO.IsolatedStorage;

namespace HeroBash
{
    public static class Settings
    {
        public static void Save()
        {
#if WINDOWS
            BackgroundWorker bw = new BackgroundWorker();
            bw.DoWork += bw_DoSave;
            bw.RunWorkerAsync();
#endif
        }

        public static void Load()
        {
#if WINDOWS
            BackgroundWorker bw = new BackgroundWorker();
            bw.DoWork += bw_DoLoad;
            bw.RunWorkerAsync();
#endif
        }

#if WINDOWS
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


    }
}
