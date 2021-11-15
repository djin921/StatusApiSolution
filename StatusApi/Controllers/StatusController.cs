using Microsoft.AspNetCore.Mvc;

namespace StatusApi.Controllers;

public class StatusController : ControllerBase
{
    // GET /status
    [HttpGet("/status")]
    public ActionResult GetTheStatus()
    {
        var response = new StatusResponse
        {
            Message = "The Server is Great.. Thanks",
            LastChecked = DateTime.Now
        };
        return Ok(response);
    }
}


public class StatusResponse
{
    public string Message { get; set; }
    public DateTime LastChecked { get; set; }
}