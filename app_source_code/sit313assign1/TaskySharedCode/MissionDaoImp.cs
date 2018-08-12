using System;
using System.Linq;
using System.Collections.Generic;

using Mono.Data.Sqlite;
using System.IO;
using System.Data;

namespace Sit313assign1.Shared
{

    /*
     * This class is the implementation of missionDao
     * The function of this class is to implemement the interface that described in missionDaoBridges
     * Operations with database were implemented in this class
     * 
     */
	public class MissionDaoImp 
	{
		static object locker = new object ();

		public SqliteConnection connection;

		public string path;
        
		public MissionDaoImp (string dbPath) 
		{
			path = dbPath;

			bool exists = File.Exists (dbPath);

            //create a database file if it is not exsist
			if (!exists) {
				connection = new SqliteConnection ("Data Source=" + dbPath);

                //create a table to store mission
				connection.Open ();
				var commands = new[] {
                    "CREATE TABLE [Items] (_id INTEGER PRIMARY KEY ASC, Name NTEXT, Description NTEXT, Deadline NTEXT, Done INTEGER);"
                };
				foreach (var command in commands) {
					using (var c = connection.CreateCommand ()) {
						c.CommandText = command;
						c.ExecuteNonQuery ();
					}
				}
			} else {
			}
		}
        
        //translate sqlite object to mission object
		Mission FromReader (SqliteDataReader r) {
			var t = new Mission ();
			t.ID = Convert.ToInt32 (r ["_id"]);
			t.Name = r ["Name"].ToString ();
			t.Description = r ["Description"].ToString ();
            t.Deadline = r["Deadline"].ToString();
			t.Done = Convert.ToInt32 (r ["Done"]) == 1 ? true : false;
			return t;
		}

        //get a list of all mission
		public IEnumerable<Mission> GetItems ()
		{
			var tl = new List<Mission> ();
            lock (locker) {
				connection = new SqliteConnection ("Data Source=" + path);
				connection.Open ();
				using (var contents = connection.CreateCommand ()) {
					contents.CommandText = "SELECT [_id], [Name], [Description], [Deadline], [Done] from [Items]";
					var r = contents.ExecuteReader ();
					while (r.Read ()) {
						tl.Add (FromReader(r));
					}
				}
				connection.Close ();
			}
			return tl;
		}


        //get mission by id
		public Mission GetItem (int id) 
		{
			var t = new Mission ();
			lock (locker) {
				connection = new SqliteConnection ("Data Source=" + path);
				connection.Open ();
				using (var command = connection.CreateCommand ()) {
					command.CommandText = "SELECT [_id], [Name], [Description], [Deadline], [Done] from [Items] WHERE [_id] = ?";
					command.Parameters.Add (new SqliteParameter (DbType.Int32) { Value = id });
					var r = command.ExecuteReader ();
					while (r.Read ()) {
						t = FromReader (r);
						break;
					}
				}
				connection.Close ();
			}
			return t;
		}

        //save mission in database;
		public int SaveItem (Mission item) 
		{
			int r;
			lock (locker) {
                //if it is a mission that already exsist, then just update it.
				if (item.ID != 0) {
					connection = new SqliteConnection ("Data Source=" + path);
					connection.Open ();
					using (var command = connection.CreateCommand ()) {
						command.CommandText = "UPDATE [Items] SET [Name] = ?, [Description] = ?, [Deadline] = ?, [Done] = ? WHERE [_id] = ?;";
						command.Parameters.Add (new SqliteParameter (DbType.String) { Value = item.Name });
						command.Parameters.Add (new SqliteParameter (DbType.String) { Value = item.Description });
                        command.Parameters.Add(new SqliteParameter(DbType.String) { Value = item.Deadline });
                        command.Parameters.Add (new SqliteParameter (DbType.Int32) { Value = item.Done });
						command.Parameters.Add (new SqliteParameter (DbType.Int32) { Value = item.ID });
						r = command.ExecuteNonQuery ();
					}
					connection.Close ();
					return r;
				} else {// if it is a new mission, then create a new record in database
					connection = new SqliteConnection ("Data Source=" + path);
					connection.Open ();
					using (var command = connection.CreateCommand ()) {
						command.CommandText = "INSERT INTO [Items] ([Name], [Description], [Description], [Done]) VALUES (? ,?, ?, ?)";
						command.Parameters.Add (new SqliteParameter (DbType.String) { Value = item.Name });
						command.Parameters.Add (new SqliteParameter (DbType.String) { Value = item.Description });
                        command.Parameters.Add(new SqliteParameter(DbType.String) { Value = item.Deadline });
                        command.Parameters.Add (new SqliteParameter (DbType.Int32) { Value = item.Done });
						r = command.ExecuteNonQuery ();
					}
					connection.Close ();
					return r;
				}

			}
		}


        //delete the mission by id
		public int DeleteItem(int id) 
		{
			lock (locker) {
				int r;
				connection = new SqliteConnection ("Data Source=" + path);
				connection.Open ();
				using (var command = connection.CreateCommand ()) {
					command.CommandText = "DELETE FROM [Items] WHERE [_id] = ?;";
					command.Parameters.Add (new SqliteParameter (DbType.Int32) { Value = id});
					r = command.ExecuteNonQuery ();
				}
				connection.Close ();
				return r;
			}
		}
	}
}