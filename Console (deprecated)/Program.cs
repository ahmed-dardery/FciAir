﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;


class DBMS
{
    public static List<List<string>> data = new List<List<string>>();
    List<string> info = new List<string>();
    private SqlConnection co ;
    public DBMS(string name)
    {
        var builder = new SqlConnectionStringBuilder();
        builder.IntegratedSecurity = true;

        builder.DataSource = "localhost";
        builder.InitialCatalog = name;

        co = new SqlConnection();
        co.ConnectionString = builder.ConnectionString;
        co.Open();
    }
    
    List<List<String>> SearchAdminBy()
    {
        return ReadData("");
    }

    private List<List<String>> ReadData(string query)
    {
        var ret = new List<List<String>>();
        using (var cmd = new SqlCommand(query, co))
        {
            SqlDataReader reader = cmd.ExecuteReader();
        
            while (reader.Read())
            {
                var row = new List<String>();
                for (int i = 0; i < reader.FieldCount; i++)
                    row.Add(reader.GetString(i));

                ret.Add(row);
            }
            reader.Close();

        }
        return ret;

    }

    public void InsertAdmin(string fName,string lName,string username,string pass)
    {
        string query = $"INSERT INTO Admins VALUES(@fName, @lName, @username, @pass)";
        using (var cmd = new SqlCommand(query, co))
        {
            cmd.Parameters.Add(new SqlParameter("@fName", fName));
            cmd.Parameters.Add(new SqlParameter("@lName", lName));
            cmd.Parameters.Add(new SqlParameter("@username", username));
            cmd.Parameters.Add(new SqlParameter("@pass", pass));
            cmd.ExecuteNonQuery();
        }
    }
    public void InsertAircraft(int adminID,int maxSeat,string model)
    {
        string query = $"INSERT INTO Aircrafts VALUES(@adminID, @maxSeat, @model)";
        using (var cmd = new SqlCommand(query, co))
        {
            cmd.Parameters.Add(new SqlParameter("@adminID", adminID));
            cmd.Parameters.Add(new SqlParameter("@maxSeat", maxSeat));
            cmd.Parameters.Add(new SqlParameter("@model", model));
            cmd.ExecuteNonQuery();
        }
    }
    public void InsertCustomer(string fName,string lName,int passPort,string nationality,DateTime birthdate ,string username,string pass)
    {
        string query = $"INSERT INTO Customers VALUES(@fName, @lName, @passPort, @nationality, @birthdate, @username, @pass)";
        using (var cmd = new SqlCommand(query, co))
        {
            cmd.Parameters.Add(new SqlParameter("@fName", fName));
            cmd.Parameters.Add(new SqlParameter("@lName", lName));
            cmd.Parameters.Add(new SqlParameter("@passPort", passPort));
            cmd.Parameters.Add(new SqlParameter("@nationality", nationality));
            cmd.Parameters.Add(new SqlParameter("@birthdate", birthdate));
            cmd.Parameters.Add(new SqlParameter("@username", username));
            cmd.Parameters.Add(new SqlParameter("@pass", pass));
            cmd.ExecuteNonQuery();
        }
    }
    public void InsertFlight(int airID,DateTime depart,DateTime arrive,int seat,string source,string destination)
    {
        string query = $"INSERT INTO Flights VALUES(@airID, @depart, @arrive, @seat, @source, @destination)";
        using (var cmd = new SqlCommand(query, co))
        {
            cmd.Parameters.Add(new SqlParameter("@airID", airID));
            cmd.Parameters.Add(new SqlParameter("@depart", depart));
            cmd.Parameters.Add(new SqlParameter("@arrive", arrive));
            cmd.Parameters.Add(new SqlParameter("@seat", seat));
            cmd.Parameters.Add(new SqlParameter("@source", source));
            cmd.Parameters.Add(new SqlParameter("@destination", destination));
            cmd.ExecuteNonQuery();
        }
    }
    public void InsertMonitor(int admin, int flight)
    {
        string query = $"INSERT INTO Monitor VALUES(@admin, @flight)";
        using (var cmd = new SqlCommand(query, co))
        {
            cmd.Parameters.Add(new SqlParameter("@admin", admin));
            cmd.Parameters.Add(new SqlParameter("@flight", flight));
            cmd.ExecuteNonQuery();
        }
    }
    public void InsertPilot(int ID, string name, DateTime date, double salary)
    {
        string query = $"INSERT INTO Pilots VALUES (@ID, @name, @date, @salary)";
        using (var cmd = new SqlCommand(query, co))
        {
            cmd.Parameters.Add(new SqlParameter("@ID", ID));
            cmd.Parameters.Add(new SqlParameter("@name", name));
            cmd.Parameters.Add(new SqlParameter("@date", date));
            cmd.Parameters.Add(new SqlParameter("@salary", salary));
            cmd.ExecuteNonQuery();
        }
    }
    public void InsertTicket(int flightID, int customerID,string Class,DateTime book)
    {
        string query = $"INSERT INTO Tickets VALUES (@flightID, @customerID, @Class, @book)";
        using (var cmd = new SqlCommand(query, co))
        {
            cmd.Parameters.Add(new SqlParameter("@flightID", flightID));
            cmd.Parameters.Add(new SqlParameter("@customerID", customerID));
            cmd.Parameters.Add(new SqlParameter("@Class", Class));
            cmd.Parameters.Add(new SqlParameter("@book", book));
            cmd.ExecuteNonQuery();
        }
    }

    public void DeleteAircrafts(int[] idx)
    {
        string query = $"DELETE FROM Aircrafts WHERE AircraftID IN ({String.Join(",", idx)})";
        using (var cmd = new SqlCommand(query, co))
        {
            cmd.ExecuteNonQuery();
        }
    }
    public void DeleteCustomer(int[] idx)
    {
        string query = $"DELETE FROM Customers WHERE CustomerID IN ({String.Join(",", idx)})";
        using (var cmd = new SqlCommand(query, co))
        {
            cmd.ExecuteNonQuery();
        }
    }
    public void DeleteFlight(int[] idx)
    {
        string query = $"DELETE FROM Flights WHERE FlightID IN ({String.Join(",", idx)})";
        using (var cmd = new SqlCommand(query, co))
        {
            cmd.ExecuteNonQuery();
        }
    }

    
}



namespace ConsoleApp2
{
    class Program
    {
        static void Main(string[] args)
        {
            DBMS query = new DBMS("FciAir");
            
            //query.tst();
            Console.WriteLine("done ");
            Console.ReadKey();
        }
    }
}