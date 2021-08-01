using CP380_PubsLab.Models;
using System;
using System.Linq;

namespace CP380_PubsLab
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var dbcontext = new Models.PubsDbContext())
            {
                if (dbcontext.Database.CanConnect())
                {
                    Console.WriteLine("Yes, I can connect");
                }

                // 1:Many practice
                //
                // TODO: - Loop through each employee
                //       - For each employee, list their job description (job_desc, in the jobs table)
                var empJobs = dbcontext.Employees
                    .Select(
                        e => new { 
                        EmpId = e.emp_id,
                        FName = e.fname,
                        LName = e.lname,
                        Job_Desc = e.Jobs.job_desc 
                    })
                    .ToList();
                Console.WriteLine("1.\nEmployee ID, Employee Name, Job Description");
                foreach (var empjob in empJobs)
                {
                    Console.WriteLine($"{empjob.EmpId}, {empjob.FName} {empjob.LName}, {empjob.Job_Desc}");
                }

                // TODO: - Loop through all of the jobs
                //       - For each job, list the employees (first name, last name) that have that job
                var jobEmps = dbcontext.Jobs
                    .Select(
                        j => new { 
                            JobId = j.job_id, 
                            Job_Desc = j.job_desc, 
                            Employees = j.Employee.Select(e => new { FirstName = e.fname, LastName = e.lname } ) 
                     })
                    .ToList();
                Console.WriteLine("\n2.");
                foreach (var job in jobEmps)
                {
                    Console.WriteLine($"--- {job.JobId} | {job.Job_Desc} ---");
                    foreach (var employee in job.Employees)
                    {
                        Console.WriteLine($"  {employee.FirstName},{employee.LastName}");
                    }
                }

                // Many:many practice
                //
                // TODO: - Loop through each Store
                //       - For each store, list all the titles sold at that store
                //
                // e.g.
                //  Bookbeat -> The Gourmet Microwave, The Busy Executive's Database Guide, Cooking with Computers: Surreptitious Balance Sheets, But Is It User Friendly?
                var storeTitles = dbcontext.Stores
                    .Select(
                        s => new { 
                            Store = s.stor_name, 
                            Titles = s.Sales.Select(sl => sl.Titles.title) 
                     })
                    .ToList();
                Console.WriteLine("\n3.");
                foreach (var store in storeTitles)
                {
                    var strJoin = String.Join(",", store.Titles);
                    Console.WriteLine($"\n{store.Store} -> {strJoin}");
                }

                // TODO: - Loop through each Title
                //       - For each title, list all the stores it was sold at
                //
                // e.g.
                //  The Gourmet Microwave -> Doc-U-Mat: Quality Laundry and Books, Bookbeat
                var titleStores = dbcontext.Titles
                    .Select(
                        t => new { 
                            TitleName = t.title, 
                            StoreList = t.Sales.Select(sl => sl.Stores.stor_name) 
                     })
                    .ToList();
                Console.WriteLine("\n4.");
                foreach (var title in titleStores)
                {
                    var strJoin = String.Join(",", title.StoreList);
                    Console.WriteLine($"\n{title.TitleName} -> {strJoin}");
                }
                Console.ReadLine();
            }
        }
    }
}
