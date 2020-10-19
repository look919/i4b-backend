using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace recr_task.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class StockController : ControllerBase
  {
    // GET api/stock
    [HttpGet]
    public ActionResult<IEnumerable<string>> Get()
    {
      return new string[] { "data", "data2", "data3" };
    }

    //PUT api/stock
    [HttpPut]
    public void Put()
    {

    }

  }
}
