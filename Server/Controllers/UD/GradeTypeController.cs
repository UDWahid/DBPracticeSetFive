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
    public class GradeTypeController : BaseController
    {
        public GradeTypeController(DOOROracleContext DBcontext,
            OraTransMsgs _OraTransMsgs) :
            base(DBcontext, _OraTransMsgs)
        {
        }

        [HttpGet]
        [Route("GetGradeType")]
        public async Task<IActionResult> GetGradeType()
        {
            List<GradeTypeDTO> lst = await _context.GradeTypes
                .Select(sp => new GradeTypeDTO
                {
                    SchoolId = sp.SchoolId,
                    GradeTypeCode = sp.GradeTypeCode,
                    Description = sp.Description,
                    CreatedBy = sp.CreatedBy,
                    CreatedDate = sp.CreatedDate,
                    ModifiedBy = sp.ModifiedBy,
                    ModifiedDate = sp.ModifiedDate
                }).ToListAsync();
            return Ok(lst);
        }

        [HttpGet]
        [Route("GetGradeType/{_SchoolId}/{_GradeTypeCode}")]
        public async Task<IActionResult> GetGradeType(int _SchoolId, string _GradeTypeCode)
        {
            GradeTypeDTO? lst = await _context.GradeTypes
              .Where(x => x.SchoolId == _SchoolId)
              .Where(x => x.GradeTypeCode == _GradeTypeCode)
              .Select(sp => new GradeTypeDTO
              {
                  SchoolId = sp.SchoolId,
                  GradeTypeCode = sp.GradeTypeCode,
                  Description = sp.Description,
                  CreatedBy = sp.CreatedBy,
                  CreatedDate = sp.CreatedDate,
                  ModifiedBy = sp.ModifiedBy,
                  ModifiedDate = sp.ModifiedDate
              }).FirstOrDefaultAsync();
            return Ok(lst);

        }

        [HttpPost]
        [Route("PostGradeType")]
        public async Task<IActionResult> PostGradeType([FromBody] GradeTypeDTO _GradeTypeDTO)
        {
            try
            {
                GradeType? gt = await _context.GradeTypes
                     .Where(x => x.SchoolId == _GradeTypeDTO.SchoolId)
                     .Where(x => x.GradeTypeCode == _GradeTypeDTO.GradeTypeCode).FirstOrDefaultAsync();

                if (gt == null)
                {
                    gt = new GradeType
                    {
                        SchoolId = _GradeTypeDTO.SchoolId,
                        GradeTypeCode = _GradeTypeDTO.GradeTypeCode,
                        Description = _GradeTypeDTO.Description
                    };
                    _context.GradeTypes.Add(gt);
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
        [Route("PutGradeType")]
        public async Task<IActionResult> PutGradeType([FromBody] GradeTypeDTO _GradeTypeDTO)
        {
            try
            {
                GradeType? gt = await _context.GradeTypes
                    .Where(x => x.SchoolId == _GradeTypeDTO.SchoolId)
                    .Where(x => x.GradeTypeCode == _GradeTypeDTO.GradeTypeCode).FirstOrDefaultAsync();

                if (gt != null)
                {
                    gt.SchoolId = _GradeTypeDTO.SchoolId;
                    gt.GradeTypeCode = _GradeTypeDTO.GradeTypeCode;
                    gt.Description = _GradeTypeDTO.Description;

                    _context.GradeTypes.Update(gt);
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
        [Route("DeleteGradeType/{_SchoolId}/{_GradeTypeCode}")]
        public async Task<IActionResult> DeleteGradeType(int _SchoolId, string _GradeTypeCode)
        {
            try
            {
                GradeType? gt = await _context.GradeTypes
                    .Where(x => x.SchoolId == _SchoolId)
                    .Where(x => x.GradeTypeCode == _GradeTypeCode).FirstOrDefaultAsync();


                if (gt != null)
                {
                    _context.GradeTypes.Remove(gt);
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
