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
        private static readonly object LockObj = new object();

        private DB() { }

        public static DB Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new DB();
                }

                return instance;
            }
        }

        private static SQLiteConnection Load()
        {
            //Path relative to file
            string dbPath = HttpContext.Current.Server.MapPath("~");
            return new SQLiteConnection($"Data Source={Path.Combine(dbPath, "GeekHunter.sqlite")}", true);
        }

        public List<Candidate> GetCandidates()
        {
            List<Candidate> candidates = new List<Candidate>();
            try
            {
                lock(LockObj)
                {
                    using (SQLiteConnection con = Load())
                    {
                        con.Open();
                        using (SQLiteCommand cmd = new SQLiteCommand(con))
                        {
                            cmd.CommandText = "SELECT Id, FirstName, LastName, Skills FROM Candidate";
                            SQLiteDataReader reader = cmd.ExecuteReader();
                            while (reader.Read())
                            {
                                candidates.Add(new Candidate()
                                {
                                    Id = int.Parse(reader["Id"].ToString()),
                                    FirstName = reader["FirstName"].ToString(),
                                    LastName = reader["LastName"].ToString(),
                                    Skills = reader["Skills"].ToString()
                                });
                            }
                        }
                        con.Close();
                    }
                }
            }
            catch (SQLiteException ex)
            {
                return new List<Candidate> { new Candidate() { Error = ex.Message } };
            }
            return candidates;
        }

        public List<Skill> GetSkills()
        {
            List<Skill> skills = new List<Skill>();
            try
            {
                lock (LockObj)
                {
                    using (SQLiteConnection con = Load())
                    {
                        con.Open();
                        using (SQLiteCommand cmd = new SQLiteCommand(con))
                        {
                            cmd.CommandText = "SELECT Id, Name FROM Skill";
                            SQLiteDataReader reader = cmd.ExecuteReader();
                            while (reader.Read())
                            {
                                skills.Add(new Skill()
                                {
                                    Id = int.Parse(reader["Id"].ToString()),
                                    Name = reader["Name"].ToString()
                                });
                            }
                        }
                        con.Close();
                    }
                }
            }

            catch (SQLiteException ex)
            {
                return new List<Skill> { new Skill() { Error = ex.Message } };
            }
            return skills;
        }

        public int AddCandidate(string firstName, string lastName, string skillList)
        {
            int CandidateId = 0;
            try
            {
                lock (LockObj)
                {
                    using (SQLiteConnection con = Load())
                    {
                        con.Open();
                        using (SQLiteCommand cmd = new SQLiteCommand(con))
                        {
                            cmd.CommandText = $"INSERT INTO Candidate(FirstName, LastName, Skills) VALUES ('{firstName}','{lastName}', '{skillList}')";
                            cmd.ExecuteNonQuery();

                            //NEED TO GET THE NEWLY INSERTED CANDIDATE ID TO RETURN BACK
                            cmd.CommandText = "SELECT last_insert_rowid()";
                            object lastId = cmd.ExecuteScalar();
                            CandidateId = int.Parse(lastId.ToString());
                        }
                        con.Close();
                    }
                }
            }

            catch (SQLiteException ex)
            {
                Console.WriteLine(ex.Message);
                return 0;
            }

            return CandidateId;
        }


    }
}