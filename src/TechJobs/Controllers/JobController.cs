using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using TechJobs.Data;
using TechJobs.Models;
using TechJobs.ViewModels;

namespace TechJobs.Controllers
{
    public class JobController : Controller
    {

        // Our reference to the data store
        private static JobData jobData;

        static JobController()
        {
            jobData = JobData.GetInstance();
        }

        // The detail display for a given Job at URLs like /Job?id=17
        public IActionResult Index(int id)
        {
            NewJobViewModel myJob = jobData.Find(id);

            // TODO #1 - get the Job with the given ID and pass it into the view
            

            Job oneSingleJob = jobData.Find(id);
           
            return View(oneSingleJob);

        }

        public IActionResult New()
        {
            NewJobViewModel newJobViewModel = new NewJobViewModel();
            return View(newJobViewModel);
        }

        [HttpPost]
        public IActionResult New(NewJobViewModel newJobViewModel)
        {
            // TODO #6 - Validate the ViewModel and if valid, create a 
            // new Job and add it to the JobData data store. Then
            // redirect to the Job detail (Index) action/view for the new Job.
            
            if (ModelState.IsValid)
            {
                Job newJob = new Job
                {
                    Name = newJobViewModel.Name,
                    Employer = new Employer
                    {
                        ID = newJobViewModel.EmployerID,
                        Value = newJobViewModel.Employers.Where(x => x.Value == newJobViewModel.EmployerID.ToString())
                        .Select(x => x.Text).FirstOrDefault()
                    },
                    CoreCompetency = new CoreCompetency
                    {
                        ID = newJobViewModel.CoreCompetenciesID,
                        Value = newJobViewModel.CoreCompetencies.Where(x => x.Value == newJobViewModel.CoreCompetenciesID.ToString())
                        .Select(x => x.Text).FirstOrDefault()
                    },
                    PositionType = new PositionType
                    {
                        ID = newJobViewModel.PositionTypesID,
                        Value = newJobViewModel.PositionTypes.Where(x => x.Value == newJobViewModel.PositionTypesID.ToString())
                        .Select(x => x.Text).FirstOrDefault()
                    }   
                 };
                jobData.Jobs.Add(newJob);
            };
           int lastJob = jobData.Jobs.Count();
           



            return RedirectToAction("Index", lastJob);
        }
    }
}
