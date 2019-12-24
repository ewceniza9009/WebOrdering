using IC.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace AccountMateWebOrder.Controllers.api
{
    public class ICController : ApiController
    {
        public IHttpActionResult Get()
        {
            string sqlServerName = "DESKTOP-VQRO7RO\\SQL2K17_CSAS";
            string dbName = "AM.Sample.Co";

            var inventory = Inventory.GetItem("AEROCHAIR-E1", sqlServerName, dbName);

            if (inventory == null)
                return NotFound();

            return Ok(inventory);
        }
    }
}
