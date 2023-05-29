
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("[controller]")]
public class ExceptionController:ControllerBase
{
    [HttpGet(Name="GetError")]
    public IEnumerable<IActionResult> Get()
    {
        throw new Exception("Got error please check ");
    }
}