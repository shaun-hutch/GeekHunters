using GeekHunters.Web.Models;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using System.Web;

namespace GeekHunters.Web.Code
{
    public class DB
    {
        private static DB instance;
        private static SQLiteConnection con;

        private DB() { }

        public static DB Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new DB();
                }

                if (con == null)
                {
                    Load();
                }
                return instance;
            }
        }

        private static void Load()
        {
            //Path relative to file
            string dbPath = HttpContext.Current.Server.MapPath("~");
            


            con = new SQLiteConnection($"Data Source={Path.Combine(dbPath, "GeekHunter.sqlite")}", true);
        }

        public List<Candidate> GetCandidates()
        {
            con.Open();
            SQLiteCommand cmd = new SQLiteCommand("SELECT Id, FirstName, LastName FROM Candidate", con);
            SQLiteDataReader reader = cmd.ExecuteReader();
            List<Candidate> candidates = new List<Candidate>();

            while (reader.Read())
            {
                candidates.Add(new Candidate()
                {
                    Id = int.Parse(reader["Id"].ToString()),
                    FirstName = reader["FirstName"].ToString(),
                    LastName = reader["LastName"].ToString()
                });
            }

            con.Close();
            return candidates;
        }

        public List<Skill> GetSkills()
        {
            con.Open();
            SQLiteCommand cmd = new SQLiteCommand("SELECT Id, Name FROM Skill", con);
            SQLiteDataReader reader = cmd.ExecuteReader();
            List<Skill> skills = new List<Skill>();

            while (reader.Read())
            {
                skills.Add(new Skill()
                {
                    Id = int.Parse(reader["Id"].ToString()),
                    Name = reader["Name"].ToString()
                });
            }

            con.Close();
            return skills;
        }


    }
}