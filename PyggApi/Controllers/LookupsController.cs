using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PyggApi.Business.Groups;
using PyggApi.Business.Lookups;
using PyggApi.Data;
using PyggApi.Interfaces.Groups;
using PyggApi.Interfaces.Lookups;
using PyggApi.Models;

namespace PyggApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LookupsController : ControllerBase
    {
        private readonly ILookups _lookupBusiness;
        public LookupsController(ILookups lookupBusiness)
        {
            _lookupBusiness = lookupBusiness;
        }

        [HttpGet("GetAllDays")]
        [Authorize]
        public async Task<ActionResult<List<Day>>> GetAllDays()
        {
            return await _lookupBusiness.GetAllDays();
        }

        [HttpGet("GetAllFrequencies")]
        [Authorize]
        public async Task<ActionResult<List<Frequency>>> GetAllFrequencies()
        {
            return await _lookupBusiness.GetAllFrequencies();
        }

        [HttpGet("GetAllMemberRoles")]
        [Authorize]
        public async Task<ActionResult<List<MemberRole>>> GetAllMemberRoles()
        {
            return await _lookupBusiness.GetAllMemberRoles();
        }

        [HttpGet("GetTransactionTypes")]
        [Authorize]
        public async Task<ActionResult<List<TransactionType>>> GetAllTransactionTypes()
        {
            return await _lookupBusiness.GetAllTransactionTypes();
        }

        [HttpGet("GetPaymentTypes")]
        [Authorize]
        public async Task<ActionResult<List<PaymentType>>> GetPaymentTypes()
        {
            return await _lookupBusiness.GetPaymentTypes();
        }

        [HttpGet("GetFinePaymentTypes")]
        [Authorize]
        public async Task<ActionResult<List<PaymentType>>> GetFinePaymentTypes()
        {
            return await _lookupBusiness.GetFinePaymentTypes();
        }
    }
}
