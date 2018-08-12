using System;
using System.Collections.Generic;
using System.IO;

namespace Sit313assign1.Shared 
{
    /*
     *  This class is the bridge of Dao and DaoImp, 
     *  the function of this class is to creat different database path for different op paltform. 
     */
	public class MissionDaoBridges 
	{
		MissionDaoImp db = null;
		protected static string dbLocation;		
		protected static MissionDaoBridges me;		

		static MissionDaoBridges ()
		{
			me = new MissionDaoBridges();
		}

		protected MissionDaoBridges ()
		{
			// set the database path 
			dbLocation = DatabaseFilePath;

			// open a database connection	
			db = new MissionDaoImp(dbLocation);
		}

		public static string DatabaseFilePath 
		{
			get 
			{ 
				var sqliteFilename = "TaskDatabase.db3";
				#if NETFX_CORE
				var path = Path.Combine(Windows.Storage.ApplicationData.Current.LocalFolder.Path, sqliteFilename);
				#else

				#if SILVERLIGHT
				// Windows Phone expects a local path, not absolute
				var path = sqliteFilename;
				#else

				#if __ANDROID__
				string libraryPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal); ;
				#else
				// For path in IOS
				string documentsPath = Environment.GetFolderPath (Environment.SpecialFolder.Personal); // Documents folder
				string libraryPath = Path.Combine (documentsPath, "..", "Library"); 
				#endif
				var path = Path.Combine (libraryPath, sqliteFilename);
				#endif

				#endif
				return path;	
			}
		}


        //provide interface for DAO
		public static Mission GetTask(int id)
		{
			return me.db.GetItem(id);
		}

        //provide interface for DAO
        public static IEnumerable<Mission> GetTasks ()
		{
			return me.db.GetItems();
		}

        //provide interface for DAO
        public static int SaveTask (Mission item)
		{
			return me.db.SaveItem(item);
		}

        //provide interface for DAO
        public static int DeleteTask(int id)
		{
			return me.db.DeleteItem(id);
		}
	}
}

