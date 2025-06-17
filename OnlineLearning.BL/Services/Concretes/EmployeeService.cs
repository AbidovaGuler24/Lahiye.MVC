using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OnlineLearning.BL.Services.Abstracts;
using OnlineLearning.Core.Entities;
using OnlineLearning.Core.Helpers.Exictance;
using OnlineLearning.Core.ViewModels;
using OnlineLearning.DAL.Migrations;
using OnlineLearning.DAL.Repositories.Abstracts;

namespace OnlineLearning.BL.Services.Concretes
{
    public class EmployeeService : IEmployeeService
    {

        private readonly IEmployeeRepository _employeeRepository;

        public EmployeeService(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;

        }

        public async Task AddAsync(EmployeeAddVm vm, string wwwroot)
        {
            string photoFileName = null;

            if (vm.PhotoFile != null)
            {
                photoFileName = vm.PhotoFile.CreateFile( wwwroot, "\\imagess\\");
            }

            var employee = new Employee
            {
                Name = vm.Name,
                Position = vm.Position,
                PhotoPath = photoFileName
            };
            await _employeeRepository.AddAsync(employee);
        }

        public async Task DeleteAsync(int id)
        {
            var employee = await _employeeRepository.GetByIdAsync(id);
            if (employee != null)
            {
                await _employeeRepository.DeleteAsync(employee);
            }
        }

        public async Task<List<Employee>> GetAllAsync()
        {
            return await _employeeRepository.GetAllAsync();

        }

        public async Task<Employee> GetByIdAsync(int id)
        {
            return await _employeeRepository.GetByIdAsync(id);
        }

        public async Task UpdateAsync(EmployeeUpdateVm vm, string wwwroot)
        {
            var employee = await _employeeRepository.GetByIdAsync(vm.Id);
            if (employee == null) return;

            employee.Name = vm.Name;
            employee.Position = vm.Position;
            if (vm.Photo != null)
            {
                
                employee.PhotoPath?.RemoveFile(wwwroot, "Images");

               
                employee.PhotoPath = vm.Photo.CreateFile(wwwroot, "\\imagess\\");
            }

            await _employeeRepository.UpdateAsync(employee);
            await _employeeRepository.SaveAllChangesAsync();
        }
    }
    }
