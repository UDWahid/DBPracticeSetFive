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
using Telerik.SvgIcons;

namespace CSBA6.Server.Controllers.app
{
    [ApiController]
    [Route("api/[controller]")]
    public class CourseController : BaseController
    {
        public CourseController(DOOROracleContext _DBcontext,
            OraTransMsgs _OraTransMsgs)
            : base(_DBcontext, _OraTransMsgs)

        {
        }


        [HttpGet]
        [Route("GetCourse")]
        public async Task<IActionResult> GetCourse()
        {
            List<CourseDTO> lst = await _context.Courses
                .Select(sp => new CourseDTO
                {
                    Cost = sp.Cost,
                    CourseNo = sp.CourseNo,
                    CreatedBy = sp.CreatedBy,
                    CreatedDate = sp.CreatedDate,
                    Description = sp.Description,
                    ModifiedBy = sp.ModifiedBy,
                    ModifiedDate = sp.ModifiedDate,
                    Prerequisite = sp.Prerequisite,
                    SchoolId = sp.SchoolId,
                    PrerequisiteSchoolId = sp.PrerequisiteSchoolId
                }).ToListAsync();
            return Ok(lst);
        }

        [HttpGet]
        [Route("GetCourse/{_SchoolId}/{_CourseNo}")]
        public async Task<IActionResult> GetCourse(int _SchoolId, int _CourseNo)
        {
            CourseDTO? lst = await _context.Courses
                .Where(x => x.SchoolId == _SchoolId)
                .Where(x => x.CourseNo == _CourseNo)
                .Select(sp => new CourseDTO
                {
                    Cost = sp.Cost,
                    CourseNo = sp.CourseNo,
                    CreatedBy = sp.CreatedBy,
                    CreatedDate = sp.CreatedDate,
                    Description = sp.Description,
                    ModifiedBy = sp.ModifiedBy,
                    ModifiedDate = sp.ModifiedDate,
                    Prerequisite = sp.Prerequisite,
                    SchoolId = sp.SchoolId,
                    PrerequisiteSchoolId = sp.PrerequisiteSchoolId
                }).FirstOrDefaultAsync();
            return Ok(lst);
        }

        [HttpPost]
        [Route("PostCourse")]
        public async Task<IActionResult> PostCourse([FromBody] CourseDTO _CourseDTO)
        {
            try
            {
                Course? c = await _context.Courses
                .Where(x => x.SchoolId == _CourseDTO.SchoolId)
                .Where(x => x.CourseNo == _CourseDTO.CourseNo).FirstOrDefaultAsync();

                if (c == null)
                {
                    c = new Course
                    {
                        Cost = _CourseDTO.Cost,
                        Description = _CourseDTO.Description,
                        Prerequisite = _CourseDTO.Prerequisite,
                        SchoolId = _CourseDTO.SchoolId,
                        PrerequisiteSchoolId = _CourseDTO.PrerequisiteSchoolId
                    };
                    _context.Courses.Add(c);
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
        [Route("PutCourse")]
        public async Task<IActionResult> PutCourse([FromBody] CourseDTO _CourseDTO)
        {
            try
            {
                Course? c = await _context.Courses
                    .Where(x => x.SchoolId == _CourseDTO.SchoolId)
                    .Where(x => x.CourseNo == _CourseDTO.CourseNo).FirstOrDefaultAsync();

                if (c != null)
                {
                    c.Description = _CourseDTO.Description;
                    c.Cost = _CourseDTO.Cost;
                    c.Prerequisite = _CourseDTO.Prerequisite;
                    c.SchoolId = _CourseDTO.SchoolId;
                    c.PrerequisiteSchoolId = _CourseDTO.PrerequisiteSchoolId;

                    _context.Courses.Update(c);
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
        [Route("DeleteCourse/{_SchoolId}/{_CourseNo}")]
        public async Task<IActionResult> DeleteCourse(int _SchoolId, int _CourseNo)
        {
            try
            {
                Course? c = await _context.Courses
                .Where(x => x.SchoolId == _SchoolId)
                .Where(x => x.CourseNo == _CourseNo).FirstOrDefaultAsync();
   

                if (c != null)
                {
                    _context.Courses.Remove(c);
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