using Microsoft.AspNetCore.Mvc;
using StatusApi.Services;

namespace StatusApi.Controllers;

public class StatusController : ControllerBase
{
    private readonly ISystemTime _systemTime;

    public StatusController(ISystemTime systemTime)
    {
        _systemTime = systemTime;
    }

    // GET /status
    [HttpGet("/status")]
    public ActionResult GetTheStatus()
    {
        var response = new StatusResponse
        {
            Message = "The Service is Great.. Thanks",
            LastChecked = _systemTime.GetCurrent()
        };
        return Ok(response);
    }
}


public class StatusResponse
{
    public string Message { get; set; }
    public DateTime LastChecked { get; set; }
}