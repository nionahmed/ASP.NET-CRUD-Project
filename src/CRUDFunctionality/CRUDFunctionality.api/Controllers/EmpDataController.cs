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

       

        /*[HttpGet]
        public async Task<IActionResult> GetContacts()
        {
            return Ok(await dbContext.Contacts.ToListAsync());
        }*/


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

        /*[HttpGet]
        [Route("{id:guid}")]
        public async Task<IActionResult> GetContact([FromRoute] Guid id)
        {
            var contact = await dbContext.Contacts.FindAsync(id);
            if (contact == null)
            {
                return NotFound();
            }
            return Ok(contact);
        }

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
