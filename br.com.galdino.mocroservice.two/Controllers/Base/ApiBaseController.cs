using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace br.com.galdino.mocroservice.two.api.Controllers.Base
{
    public abstract class ApiBaseController : ControllerBase
    {
        
        protected async Task<IActionResult> AutoResult<T>(Func<Task<T>> func)
        {
            try
            {
                return Ok(await func());
            }
            catch (ValidationException)
            {
                var erroBuilder = new StringBuilder();

                return BadRequest(erroBuilder.ToString());
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}
