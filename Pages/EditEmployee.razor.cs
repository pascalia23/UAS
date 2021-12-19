using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlazorFront.Models;
using BlazorFront.Services;
using Microsoft.AspNetCore.Components;

namespace BlazorFront.Pages
{
    public partial class EditEmployee
    {
        public Employee Employee { get; set; } = new Employee();

        [Inject]
        public IEmployeeService EmployeeService { get; set; }

        [Inject]
        public IDepartmentService DepartmentService { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        public List<Department> Departments { get; set; } = new List<Department>();

        [Parameter]
        public string Id { get; set; }

        protected async override Task OnInitializedAsync()
        {
            Employee = await EmployeeService.GetById(int.Parse(Id));
            Departments = (await DepartmentService.GetAll()).ToList();
        }

        protected async Task Delete_Click(){
            await EmployeeService.Delete(Employee.EmployeeId);
            NavigationManager.NavigateTo("/employeepage");
        }

        protected async Task HandleValidSubmit(){
            Employee.PhotoPath = "images/nophoto.jpg";
            var result = await EmployeeService.Update(int.Parse(Id),Employee);
            NavigationManager.NavigateTo("employeepage");
        }
    }
}