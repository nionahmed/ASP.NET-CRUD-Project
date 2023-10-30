using CRUDFunctionality.api.DbContexts;
using CRUDFunctionality.api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using System.Net;
using System.Numerics;

namespace CRUDFunctionality.api.Controllers
{

    [ApiController]
    [Route("api/[Controller]")]


    public class EmpDataController : Controller
    {
        private EmployeeAPIDbContext dbContext;
        public EmpDataController(EmployeeAPIDbContext dbContext)
        {
            this.dbContext = dbContext;
        }



        [HttpGet]
        [Route("emplist")]
        public async Task<IActionResult> ShowEmployeeList()
        {
            var employees = await dbContext.AddPersonalData
                .Select(e => new { e.Id, e.Name, e.Email }) // Select only the desired fields
                .ToListAsync();

            return Ok(employees);
        }

        [HttpGet]
        [Route("eachemp/{id:int}")]
        public async Task<IActionResult> GetEachEmployeeDetails(int id)
        {
            var employee = await dbContext.AddPersonalData.Join(
                dbContext.AddOfficialData,
                personaldata => personaldata.Id,
                officialdata => officialdata.Id,
                (personaldata, officialdata) => new AddEmployeeDataViewModel
                {
                    Id = personaldata.Id,
                    Name = personaldata.Name,
                    FatherName = personaldata.FatherName,
                    MotherName = personaldata.MotherName,
                    Email = personaldata.Email,
                    Phone = personaldata.Phone,
                    Address = personaldata.Address,
                    Dob = personaldata.Dob,
                    Post = officialdata.Post,
                    Salary = officialdata.Salary,
                    Incperiod = officialdata.Incperiod
                }).FirstOrDefaultAsync(e=>e.Id==id);

            return Ok(employee);
        }


        [HttpPost]
        [Route("addemp")]
        public async Task<IActionResult> AddEmployee(AddEmployeeDataViewModel empdata) 
        {

            var newempPersonalInfo = new AddPersonalDataDbModel
            {
                Name = empdata.Name,
                FatherName = empdata.FatherName,
                MotherName = empdata.MotherName,
                Email = empdata.Email,
                Phone = empdata.Phone,
                Address = empdata.Address,
                Dob = empdata.Dob
            };

            var newempOfficialInfo = new AddOfficialDataDbModel 
            {
                Post= empdata.Post,
                Salary= empdata.Salary,
                Incperiod= empdata.Incperiod,
            };
            
            await dbContext.AddPersonalData.AddAsync(newempPersonalInfo);
            await dbContext.AddOfficialData.AddAsync(newempOfficialInfo);
            await dbContext.SaveChangesAsync();

            return Ok("Data added to Db");
        }

        [HttpPut]
        [Route("updateeachemp/{id:int}")]
        public async Task<IActionResult> UpdateEachEmployeeDetails(int id, AddEmployeeDataViewModel empdata)
        {
            var employeePersonal = await dbContext.AddPersonalData.FindAsync(id);
            var employeeOfficial = await dbContext.AddOfficialData.FindAsync(id);
            if (employeePersonal != null)
            {
                employeePersonal.Name = empdata.Name;
                employeePersonal.FatherName = empdata.FatherName;
                employeePersonal.MotherName = empdata.MotherName;
                employeePersonal.Email = empdata.Email;
                employeePersonal.Phone = empdata.Phone;
                employeePersonal.Address = empdata.Address;
                employeePersonal.Dob = empdata.Dob;
                await dbContext.SaveChangesAsync();
                
            }

            if (employeeOfficial != null)
            {
                employeeOfficial.Post = empdata.Post;
                employeeOfficial.Salary = empdata.Salary;
                employeeOfficial.Incperiod = empdata.Incperiod;
                
                await dbContext.SaveChangesAsync();

            }
            return Ok("Successfull");
            
        }

        /*

        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> DeleteContact(Guid id)
        {
            var contact = await dbContext.Contacts.FindAsync(id);
            if (contact != null)
            {
                dbContext.Remove(contact);
                dbContext.SaveChanges(); 
                return Ok(contact);
            }
            return NotFound();
        }

        [HttpPost]
        [Route("api/dueDate2")]
        public IActionResult Post([FromBody] TeamCardModel num)
        {

            return Ok(num.DueDate);
        }*/

    }
    


    
}
