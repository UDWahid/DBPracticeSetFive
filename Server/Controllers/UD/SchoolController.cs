using DOOR.EF.Data;
using DOOR.EF.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.Extensions.Caching.Memory;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Options;
using System.Security.Cryptography;
using System.Text.Json;
using System.Runtime.InteropServices;
using Microsoft.Extensions.Hosting.Internal;
using System.Net.Http.Headers;
using System.Drawing;
using Microsoft.AspNetCore.Identity;
using DOOR.Server.Models;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System.Data;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using System.Numerics;
using DOOR.Shared.DTO;
using DOOR.Shared.Utils;
using DOOR.Server.Controllers.Common;

namespace DOOR.Server.Controllers.UD
{
    [ApiController]
    [Route("api/[controller]")]
    public class SchoolController : BaseController
    {
        public SchoolController(DOOROracleContext DBcontext, 
            OraTransMsgs _OraTransMsgs) : 
            base(DBcontext, _OraTransMsgs)
        {
        }

        [HttpGet]
        [Route("GetSchool")]
        public async Task<IActionResult> GetSchool()
        {
            List<SchoolDTO> lst = await _context.Schools
                .Select(sp => new SchoolDTO
                {
                    SchoolId = sp.SchoolId,
                    SchoolName = sp.SchoolName,
                    CreatedBy = sp.CreatedBy,
                    CreatedDate = sp.CreatedDate,
                    ModifiedBy = sp.ModifiedBy,
                    ModifiedDate = sp.ModifiedDate
                }).ToListAsync();
            return Ok(lst);
        }

        [HttpGet]
        [Route("GetSchool/{_SchoolId}")]
        public async Task<IActionResult> GetSchool(int _SchoolId)
        {
            SchoolDTO? lst = await _context.Schools
                .Where(x => x.SchoolId == _SchoolId)
                .Select(sp => new SchoolDTO
                {
                    SchoolId = sp.SchoolId,
                    SchoolName = sp.SchoolName,
                    CreatedBy = sp.CreatedBy,
                    CreatedDate = sp.CreatedDate,
                    ModifiedBy = sp.ModifiedBy,
                    ModifiedDate = sp.ModifiedDate
                }).FirstOrDefaultAsync();
            return Ok(lst);
        }

        [HttpPost]
        [Route("PostSchool")]
        public async Task<IActionResult> PostSchool([FromBody] SchoolDTO _SchoolDTO)
        {
            try
            {
                School? s = await _context.Schools.Where(x => x.SchoolId == _SchoolDTO.SchoolId).FirstOrDefaultAsync();

                if (s == null)
                {
                    s = new School
                    {
                        SchoolId = _SchoolDTO.SchoolId,
                        SchoolName = _SchoolDTO.SchoolName
                    };
                    _context.Schools.Add(s);
                    await _context.SaveChangesAsync();
                }
            }

            catch (DbUpdateException Dex)
            {
                List<OraError> DBErrors = ErrorHandling.TryDecodeDbUpdateException(Dex, _OraTranslateMsgs);
                return StatusCode(StatusCodes.Status417ExpectationFailed, Newtonsoft.Json.JsonConvert.SerializeObject(DBErrors));
            }
            catch (Exception ex)
            {
                _context.Database.RollbackTransaction();
                List<OraError> errors = new List<OraError>();
                errors.Add(new OraError(1, ex.Message.ToString()));
                string ex_ser = Newtonsoft.Json.JsonConvert.SerializeObject(errors);
                return StatusCode(StatusCodes.Status417ExpectationFailed, ex_ser);
            }

            return Ok();
        }

        [HttpPut]
        [Route("PutSchool")]
        public async Task<IActionResult> PutSchool([FromBody] SchoolDTO _SchoolDTO)
        {
            try
            {
                School? s = await _context.Schools.Where(x => x.SchoolId == _SchoolDTO.SchoolId).FirstOrDefaultAsync();

                if (s != null)
                {
                    s.SchoolId = _SchoolDTO.SchoolId;
                    s.SchoolName = _SchoolDTO.SchoolName;

                    _context.Schools.Update(s);
                    await _context.SaveChangesAsync();
                }
            }

            catch (DbUpdateException Dex)
            {
                List<OraError> DBErrors = ErrorHandling.TryDecodeDbUpdateException(Dex, _OraTranslateMsgs);
                return StatusCode(StatusCodes.Status417ExpectationFailed, Newtonsoft.Json.JsonConvert.SerializeObject(DBErrors));
            }
            catch (Exception ex)
            {
                _context.Database.RollbackTransaction();
                List<OraError> errors = new List<OraError>();
                errors.Add(new OraError(1, ex.Message.ToString()));
                string ex_ser = Newtonsoft.Json.JsonConvert.SerializeObject(errors);
                return StatusCode(StatusCodes.Status417ExpectationFailed, ex_ser);
            }

            return Ok();
        }

        [HttpDelete]
        [Route("DeleteSchool/{_SchoolId}")]
        public async Task<IActionResult> DeleteSchool(int _SchoolId)
        {
            try
            {
                School? s = await _context.Schools.Where(x => x.SchoolId == _SchoolId).FirstOrDefaultAsync();


                if (s != null)
                {
                    _context.Schools.Remove(s);
                    await _context.SaveChangesAsync();
                }
            }

            catch (DbUpdateException Dex)
            {
                List<OraError> DBErrors = ErrorHandling.TryDecodeDbUpdateException(Dex, _OraTranslateMsgs);
                return StatusCode(StatusCodes.Status417ExpectationFailed, Newtonsoft.Json.JsonConvert.SerializeObject(DBErrors));
            }
            catch (Exception ex)
            {
                _context.Database.RollbackTransaction();
                List<OraError> errors = new List<OraError>();
                errors.Add(new OraError(1, ex.Message.ToString()));
                string ex_ser = Newtonsoft.Json.JsonConvert.SerializeObject(errors);
                return StatusCode(StatusCodes.Status417ExpectationFailed, ex_ser);
            }

            return Ok();
        }
    }
}
