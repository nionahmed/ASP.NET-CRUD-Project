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

        /*

        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> UpdateContact([FromRoute] Guid id, UpdateContactRequest updateContactRequest)
        {
            var contact = await dbContext.Contacts.FindAsync(id);
            if (contact != null)
            {
                contact.name = updateContactRequest.name;
                contact.phone = updateContactRequest.phone;
                contact.address = updateContactRequest.address;
                contact.email = updateContactRequest.email;
                await dbContext.SaveChangesAsync();
                return Ok(contact);
            }
            return NotFound();
        }

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
