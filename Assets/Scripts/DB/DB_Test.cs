using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Data;
using Mono.Data.Sqlite;

public class DB_Test : MonoBehaviour {

	// Use this for initialization
	void Start () {
        string connectionString = "URI=file:" + Application.dataPath + "/Database/DevTest.sqlite";
        print(connectionString);
        IDbConnection dbcon = new SqliteConnection(connectionString);
        dbcon.Open();
        IDbCommand dbcmd = dbcon.CreateCommand();
        const string sql =
          "SELECT *" +
          "FROM GameChars";
        dbcmd.CommandText = sql;
        IDataReader reader = dbcmd.ExecuteReader();
        while (reader.Read()) {
            string ID = reader.GetInt32(0).ToString();
            string CharName = reader.GetString(1);
            print("Name: "+ ID + " "+ CharName);
        }
        // clean up
        reader.Dispose();
        dbcmd.Dispose();
        dbcon.Close();
    }
}
