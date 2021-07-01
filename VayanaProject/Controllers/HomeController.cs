using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using VayanaProject.Models;

namespace VayanaProject.Controllers
{
    public class HomeController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly string strBaseAddress;

        public HomeController(IConfiguration configuration)
        {
            _configuration = configuration;
            strBaseAddress = _configuration["BaseApiAddress"];
        }

        public IActionResult Index()
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(strBaseAddress);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            string strQueryString = string.Format("Employee/GetDepartments");
            HttpRequestMessage requestMessage = new HttpRequestMessage(HttpMethod.Get, strBaseAddress + strQueryString);
            HttpResponseMessage response = client.SendAsync(requestMessage).Result;
            if (response.IsSuccessStatusCode)
            {
                ViewBag.Departments = response.Content.ReadAsAsync<List<Department>>().Result;
            }
            return View();
        }

      
        public IActionResult AddUpdateEmployee(int EmpId,string EmpFirstName, string EmpMiddleName, string EmpLastName, int EmpSalary, int EmpDeptId)
        {
            string strOutput = string.Empty;
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(strBaseAddress);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            string strQueryString = string.Format("Employee/AddUpdateEmployee?EmpId="+EmpId+"&EmpFirstName=" + EmpFirstName + "&EmpMiddleName=" + EmpMiddleName + "&EmpLastName=" + EmpLastName + "&EmpSalary=" + EmpSalary + "&EmpDeptId=" + EmpDeptId);
            HttpRequestMessage requestMessage = new HttpRequestMessage(HttpMethod.Get, strBaseAddress + strQueryString);
            HttpResponseMessage response = client.SendAsync(requestMessage).Result;
            if (response.IsSuccessStatusCode)
            {
                if (EmpId > 0)
                {
                    strOutput = "Employee Updated successfully.";
                }
                else
                {
                    strOutput = "Employee Added Successfully.";
                }
            }
            else
            {
                strOutput = "Something went wrong";
            }
            return Json(strOutput);
        }

        public IActionResult GetEmployees(bool ForDept=false)
        {
            string strOutput = string.Empty;
            List<Employee> employees = new List<Employee>();
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(strBaseAddress);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            string strQueryString = string.Format("Employee/GetEmployees");
            HttpRequestMessage requestMessage = new HttpRequestMessage(HttpMethod.Get, strBaseAddress + strQueryString);
            HttpResponseMessage response = client.SendAsync(requestMessage).Result;
            if (response.IsSuccessStatusCode)
            {
                employees = response.Content.ReadAsAsync<List<Employee>>().Result;
            }
            if (ForDept)
            {
                return PartialView("_EmployeesList", employees);
            }
            else
            {
                return PartialView("_EmployeeTable", employees);
            }
        }


        public IActionResult LoadEditEmployee(int EmpId)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(strBaseAddress);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            string strQueryString = string.Format("Employee/GetDepartments");
            HttpRequestMessage requestMessage = new HttpRequestMessage(HttpMethod.Get, strBaseAddress + strQueryString);
            HttpResponseMessage response = client.SendAsync(requestMessage).Result;
            if (response.IsSuccessStatusCode)
            {
                ViewBag.Departments = response.Content.ReadAsAsync<List<Department>>().Result;
            }
            ViewBag.Id = EmpId;
            return PartialView("_EditEmployee");
        }

        public IActionResult DeleteEmployee(int EmpId)
        {
            string strOutput = string.Empty;
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(strBaseAddress);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            string strQueryString = string.Format("Employee/DeleteEmployee?EmpId="+ EmpId);
            HttpRequestMessage requestMessage = new HttpRequestMessage(HttpMethod.Get, strBaseAddress + strQueryString);
            HttpResponseMessage response = client.SendAsync(requestMessage).Result;
            if (response.IsSuccessStatusCode)
            {
                strOutput = "Employee Deleted successfully.";
            }
            else
            {
                strOutput = "Something went wrong.";
            }
            return Json(strOutput);
        }

        public IActionResult AvgSalaryByDepartment()
        {
            string strOutput = string.Empty;
            List<AvgSalaryModel> lstAvgSalary = new List<AvgSalaryModel>();
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(strBaseAddress);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            string strQueryString = string.Format("Employee/AvgSalaryByDepartment");
            HttpRequestMessage requestMessage = new HttpRequestMessage(HttpMethod.Get, strBaseAddress + strQueryString);
            HttpResponseMessage response = client.SendAsync(requestMessage).Result;
            if (response.IsSuccessStatusCode)
            {
                lstAvgSalary = response.Content.ReadAsAsync<List<AvgSalaryModel>>().Result;
            }
            return PartialView("_DepartmentWiseSalary",lstAvgSalary);
        }
    }
}
