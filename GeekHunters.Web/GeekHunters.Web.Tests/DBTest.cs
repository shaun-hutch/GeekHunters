using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Threading;
using GeekHunters.Web.Code;
using GeekHunters.Web.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GeekHunters.Web.Tests
{
    [TestClass]
    public class DBTest
    {
        DB db;
        Random random;

        [TestMethod]
        public void StressTestSelect()
        {
            db = DB.Instance(@"C:\Users\Shaun\Desktop\GeekHunters\GeekHunters\GeekHunters.Web\GeekHunters.Web\");
            Thread[] threads = new Thread[500];
            try
            {
                for (int i = 0; i < threads.Length; i++)
                {
                    threads[i] = new Thread(ThreadDBSelect);
                }

                foreach(Thread t in threads)
                {
                    t.Start();
                }

                foreach (Thread t in threads)
                {
                    t.Join();
                }
            }

            catch (AggregateException ex)
            {
                foreach (Exception _ex in ex.InnerExceptions)
                {
                    Console.WriteLine(_ex.Message);
                }
                throw;
            }
        }

        private void ThreadDBSelect()
        {
            List<Candidate> candidates = db.GetCandidates();
            List<Skill> skills = db.GetSkills();

            if (candidates.Any(x => x.Error != null) || skills.Any(x => x.Error != null))
            {
                throw new ApplicationException("Errors returned from the DB calls");
            }
        }

        [TestMethod]
        public void StressTestInsert()
        {
            db = DB.Instance(@"C:\Users\Shaun\Desktop\GeekHunters\GeekHunters\GeekHunters.Web\GeekHunters.Web\");
            random = new Random();
            Thread[] threads = new Thread[100];
            try
            {
                for (int i = 0; i < threads.Length; i++)
                {
                    threads[i] = new Thread(ThreadDBInsert);
                }

                foreach (Thread t in threads)
                {
                    t.Start();
                }

                foreach (Thread t in threads)
                {
                    t.Join();
                }
            }

            catch (AggregateException ex)
            {
                foreach (Exception _ex in ex.InnerExceptions)
                {
                    Console.WriteLine(_ex.Message);
                }
                throw;
            }
        }

        private void ThreadDBInsert()
        {
            int id = db.AddCandidate("Shaun", "Hutchy", random.Next(1, 6).ToString());

            if (id == 0)
            {
                throw new ApplicationException("Unable to insert candidate.");
            }
        }

        [TestMethod]
        [ExpectedException(exceptionType: typeof(SQLiteException), "Attempted to insert nulls into table columns, expected values.")]
        public void InsertBadCandidateData()
        {
            db = DB.Instance(@"C:\Users\Shaun\Desktop\GeekHunters\GeekHunters\GeekHunters.Web\GeekHunters.Web\");
            int result = db.AddCandidate(null, null, null);

        }

    }
}
