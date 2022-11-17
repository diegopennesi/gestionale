using System.Data.SQLite;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class Engine
{
	public Engine()
	{
	}
	private SQLiteConnection openconnection()
    {
        String path = "C:\\Users\\Diego Pennesi\\source\\repos\\Gestionale\\bin\\src\\inf\\db";
        string tentative_path=Environment.CurrentDirectory;

        var connection = new SQLiteConnection("Data Source="+path+"\\database.db");
		connection.Open();
		return connection;
    }


	public Boolean login(string[] a)
    {

        var connection=openconnection();
        string sql =
   @"
        SELECT *
        FROM users
       WHERE username='" + a[0] + @"' and password = '" + a[1] +"'";
        var cmd = new SQLiteCommand(sql, connection);
        using (var reader = cmd.ExecuteReader())
        {
            while (reader.Read())
            {
                return true;
            }
        }
        return false;

    }
    public Dictionary<String, String> searchWorkerbyParams(Dictionary<String,String> Params, Boolean isnullable)
    {
        int lenght=Params.Count();
        var connection = openconnection();
        string sql =@"
        SELECT * FROM lavoratori where ";
        foreach (var param in Params)
        {
            sql += param.Key.ToString();
            sql +="='" + param.Value.ToString()+"'";
            if (!(--lenght == 0)) sql += " and ";

        }
        Dictionary<String, String> response = new Dictionary<String,String>();
        var cmd = new SQLiteCommand(sql, connection);
        using (var reader = cmd.ExecuteReader())
        {
            while (reader.Read())
            {
                response.Add("id", reader.GetValue(0).ToString());
                response.Add("Nome", reader.GetValue(0).ToString());
                response.Add("Cognome", reader.GetValue(0).ToString());
                response.Add("Lingue", reader.GetValue(0).ToString());
                response.Add("Extra", reader.GetValue(0).ToString());
                response.Add("Email", reader.GetValue(0).ToString());
            }
            if (response.Count > 0 && !isnullable)
            {
                return response;
            } else if (response.Count == 0 && isnullable)
            {
                return response;
            } else
            {
                throw new Exception();
            }
            
        }

        return response;
    }
    public Boolean saveWorker(Dictionary<String, String> Params)
    {
        try
        {
            int lenght = Params.Count();
            var connection = openconnection();
            string sql = @"Insert into lavoratori(Nome,Cognome,email,lingue,extra) values";
            sql += "(";
            sql += "'" + Params["Nome"]+"',";
            sql += "'" + Params["Cognome"] + "',";
            sql += "'" + Params["Email"] + "',";
            sql += "'" + Params["Lingue"] + "',";
            sql += "'" + Params["Extra"] + "'";
            sql += ")";
            var cmd = new SQLiteCommand(sql, connection);
            using (var reader = cmd.ExecuteReader()) ;
        } catch (Exception ex)
        {
            throw new Exception("Errore nell'inserimento");
        }
        return true;
    }

    public Boolean insertNewWorker(String name,String Surname,String Email,ArrayList language,String Extrafilds)
    {
        if (!(name.Length>0 && Surname.Length>0 && language.Count > 0))
        {
            return false;
        }
        Dictionary<String, String> Params = new Dictionary<String,String>();
        Params.Add("Nome", name);
        Params.Add("Cognome", Surname);
        try
        {
            searchWorkerbyParams(Params, true);
        } catch (Exception ex)
        {
            return false;
        }
        Params.Add("Email", Email);
        Params.Add("Lingue", String.Join(",",language.Cast<String>().ToArray()));// un pò una merda...
        Params.Add("Extra", Extrafilds);
        return saveWorker(Params);
            
    }

}
