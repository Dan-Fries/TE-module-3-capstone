﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using NPGeekEF.DAL;
using NPGeekEF.Models;

namespace NPGeekEF.Controllers
{
    public class SurveyController : Controller
    {
        private IParksDAO ParksDAO;
        private ISurveyDAO SurveyDAO;

        public SurveyController(IParksDAO parksDAO, ISurveyDAO surveyDAO)
        {
            this.ParksDAO = parksDAO;
            this.SurveyDAO = surveyDAO;
        }

        [HttpGet]
        public IActionResult Index()
        {
            ViewData["parks"] = parkList;
            ViewData["states"] = stateList;
            ViewData["activities"] = activityList;
            return View();
        }

        [HttpPost]
        public IActionResult Index(SurveyViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            }
            // Build survey object
            vm.BuildSurveyObject();

            // Add Survey to DB
            SurveyDAO.AddSurveyResult(vm.Survey);

            return RedirectToAction("SurveyResults");
        }

        public IActionResult SurveyResults()
        {
            List<SurveyResultsViewModel> vm = BuildSurveyResults();
            return View(vm);
        }

        private List<SurveyResultsViewModel> BuildSurveyResults()
        {
            Dictionary<string, int> results = SurveyDAO.GetSurveyResults();
            List<SurveyResultsViewModel> vmList = new List<SurveyResultsViewModel>();

            foreach (KeyValuePair<string, int> kvp in results)
            {
                SurveyResultsViewModel vm = new SurveyResultsViewModel();
                vm.ParkCode = kvp.Key;
                vm.NumVotes = kvp.Value;

                Park park = ParksDAO.GetParkByCode(vm.ParkCode);
                vm.ParkName = park.ParkName;
                vm.ParkLocation = park.State;
                vm.ParkClimate = park.Climate;
                vmList.Add(vm);
            }

            return vmList;
        }
        private List<SelectListItem> parkList
        {
            get
            {
                List<SelectListItem> items = new List<SelectListItem>();
                foreach (Park park in ParksDAO.GetAllParks())
                {
                    items.Add(new SelectListItem() { Text = park.ParkName, Value = park.ParkCode });
                }
                return items;
            }
        }

        private List<SelectListItem> activityList = new List<SelectListItem>()
        {
            new SelectListItem() {Text="Inactive"},
            new SelectListItem() {Text="Sedentary"},
            new SelectListItem() {Text="Active"},
            new SelectListItem() {Text="Extremely Active"}
        };

        private List<SelectListItem> stateList = new List<SelectListItem>()
        {
            new SelectListItem() {Text="Alabama", Value="AL"},
            new SelectListItem() { Text="Alaska", Value="AK"},
            new SelectListItem() { Text="Arizona", Value="AZ"},
            new SelectListItem() { Text="Arkansas", Value="AR"},
            new SelectListItem() { Text="California", Value="CA"},
            new SelectListItem() { Text="Colorado", Value="CO"},
            new SelectListItem() { Text="Connecticut", Value="CT"},
            new SelectListItem() { Text="District of Columbia", Value="DC"},
            new SelectListItem() { Text="Delaware", Value="DE"},
            new SelectListItem() { Text="Florida", Value="FL"},
            new SelectListItem() { Text="Georgia", Value="GA"},
            new SelectListItem() { Text="Hawaii", Value="HI"},
            new SelectListItem() { Text="Idaho", Value="ID"},
            new SelectListItem() { Text="Illinois", Value="IL"},
            new SelectListItem() { Text="Indiana", Value="IN"},
            new SelectListItem() { Text="Iowa", Value="IA"},
            new SelectListItem() { Text="Kansas", Value="KS"},
            new SelectListItem() { Text="Kentucky", Value="KY"},
            new SelectListItem() { Text="Louisiana", Value="LA"},
            new SelectListItem() { Text="Maine", Value="ME"},
            new SelectListItem() { Text="Maryland", Value="MD"},
            new SelectListItem() { Text="Massachusetts", Value="MA"},
            new SelectListItem() { Text="Michigan", Value="MI"},
            new SelectListItem() { Text="Minnesota", Value="MN"},
            new SelectListItem() { Text="Mississippi", Value="MS"},
            new SelectListItem() { Text="Missouri", Value="MO"},
            new SelectListItem() { Text="Montana", Value="MT"},
            new SelectListItem() { Text="Nebraska", Value="NE"},
            new SelectListItem() { Text="Nevada", Value="NV"},
            new SelectListItem() { Text="New Hampshire", Value="NH"},
            new SelectListItem() { Text="New Jersey", Value="NJ"},
            new SelectListItem() { Text="New Mexico", Value="NM"},
            new SelectListItem() { Text="New York", Value="NY"},
            new SelectListItem() { Text="North Carolina", Value="NC"},
            new SelectListItem() { Text="North Dakota", Value="ND"},
            new SelectListItem() { Text="Ohio", Value="OH"},
            new SelectListItem() { Text="Oklahoma", Value="OK"},
            new SelectListItem() { Text="Oregon", Value="OR"},
            new SelectListItem() { Text="Pennsylvania", Value="PA"},
            new SelectListItem() { Text="Rhode Island", Value="RI"},
            new SelectListItem() { Text="South Carolina", Value="SC"},
            new SelectListItem() { Text="South Dakota", Value="SD"},
            new SelectListItem() { Text="Tennessee", Value="TN"},
            new SelectListItem() { Text="Texas", Value="TX"},
            new SelectListItem() { Text="Utah", Value="UT"},
            new SelectListItem() { Text="Vermont", Value="VT"},
            new SelectListItem() { Text="Virginia", Value="VA"},
            new SelectListItem() { Text="Washington", Value="WA"},
            new SelectListItem() { Text="West Virginia", Value="WV"},
            new SelectListItem() { Text="Wisconsin", Value="WI"},
            new SelectListItem() { Text="Wyoming", Value="WY"}
        };
    }
}