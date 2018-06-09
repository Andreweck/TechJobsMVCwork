using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using TechJobs.Models;

namespace TechJobs.Controllers
{
    public class SearchController : Controller
    {
        private static List<Dictionary<string, string>> PrintOutJobs(List<Dictionary<string, string>> someJobs)
        {


            StreamReader needabook = new StreamReader(@"c:\Users\usr\source\repos\TechJobsConsole\src\TechJobsConsole\job_data.csv");

            List<string> preList = new List<string>();

            List<string[]> final_list = new List<string[]>();

            List<string> listValue = new List<string> { };

            StringBuilder arrayValue = new StringBuilder();

            bool isinQuotes = false;

            List<Dictionary<string, string>> finalData = new List<Dictionary<string, string>>();

            Dictionary<string, string> dictValue = new Dictionary<string, string>();

            List<string> searchList = new List<string>();

            while (needabook.Peek() > 0)
            {
                preList.Add(needabook.ReadLine());
            }

            foreach (string i in preList)
            {
                foreach (char a in i)
                {
                    if (a.Equals('"'))
                    {
                        isinQuotes = !isinQuotes;
                        arrayValue.Append(a);
                    }
                    else if (a.Equals(','))
                    {
                        if (isinQuotes == false)
                        {
                            listValue.Add(arrayValue.ToString());
                            arrayValue.Clear();
                        }
                        else
                        {
                            arrayValue.Append(a);
                        }
                    }
                    else
                    {
                        arrayValue.Append(a);
                    }

                }
                listValue.Add(arrayValue.ToString());
                arrayValue.Clear();
                final_list.Add(listValue.ToArray());
                listValue = new List<string>();
            }

            string[] firstPostList = final_list[0];

            final_list.Remove(firstPostList);

            foreach (string[] i in final_list)

            {

                for (int a = 0; a < i.Count(); a++)

                {

                    dictValue.Add(firstPostList[a], i[a]);


                }
                finalData.Add(dictValue);
                dictValue = new Dictionary<string, string>();

            }
            return finalData;


        }
        
        //[HttpGet]
        public IActionResult Index()
        {
            ViewBag.columns = ListController.columnChoices;
            ViewBag.title = "Search";
            return View();
        }
        
        [HttpPost]
        [HttpGet]
        public IActionResult Results(string searchType, string searchTerm)
        {
            string termHandler = searchTerm;
            string typeHandler = searchType;
            ViewBag.columns = ListController.columnChoices;
            ViewBag.title = "search/results";
            if (typeHandler.Equals("all"))
            { 
                ViewBag.jobs = JobData.FindByValue(termHandler);
                return View("Results");
            }
            else
            {
                ViewBag.jobs = JobData.FindByColumnAndValue(typeHandler, termHandler);
                return View("Results");
            }
            
        }
        public static void readList(List<Dictionary<string, string>> someJobs)
        {
            StreamReader needabook = new StreamReader(@"c:\Users\usr\source\repos\TechJobsConsole\src\TechJobsConsole\job_data.csv");

            List<string> preList = new List<string>();

            List<string[]> final_list = new List<string[]>();

            List<string> listValue = new List<string> { };

            StringBuilder arrayValue = new StringBuilder();

            bool isinQuotes = false;

            List<Dictionary<string, string>> finalData = new List<Dictionary<string, string>>();

            Dictionary<string, string> dictValue = new Dictionary<string, string>();

            List<string> searchList = new List<string>();

            while (needabook.Peek() > 0)
            {
                preList.Add(needabook.ReadLine());
            }

            foreach (string i in preList)
            {
                foreach (char a in i)
                {
                    if (a.Equals('"'))
                    {
                        isinQuotes = !isinQuotes;
                        arrayValue.Append(a);
                    }
                    else if (a.Equals(','))
                    {
                        if (isinQuotes == false)
                        {
                            listValue.Add(arrayValue.ToString());
                            arrayValue.Clear();
                        }
                        else
                        {
                            arrayValue.Append(a);
                        }
                    }
                    else
                    {
                        arrayValue.Append(a);
                    }

                }
                listValue.Add(arrayValue.ToString());
                arrayValue.Clear();
                final_list.Add(listValue.ToArray());
                listValue = new List<string>();
            }

            string[] firstPostList = final_list[0];

            final_list.Remove(firstPostList);

            foreach (string[] i in final_list)

            {

                for (int a = 0; a < i.Count(); a++)

                {

                    dictValue.Add(firstPostList[a], i[a]);


                }
                finalData.Add(dictValue);
                dictValue = new Dictionary<string, string>();

            }
            List<Dictionary<string, string>> printedList = new List<Dictionary<string, string>>();
            Dictionary<string, string> mstdoths = new Dictionary<string, string>();
            for (int z = 0; z < someJobs.Count(); z++)
            {
                //if (someJobs.Contains(finalData[z]))
                //{
                List<string> lines = new List<string>(someJobs[z].Values);
                List<string> lineKeys = new List<string>(someJobs[z].Keys);
                for (int i = 0; i < lineKeys.Count(); i++)
                {
                    mstdoths.Add(lineKeys[i], lines[i]);
                    printedList.Add(mstdoths);
                    mstdoths.Clear();
                }
                
            
            }

            


        }
    }
        // TODO #1 - Create a Results action method to process 
        // search request and display results

}


