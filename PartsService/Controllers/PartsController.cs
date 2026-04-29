using Microsoft.AspNetCore.Mvc;
using PartsService.Models;
using PartsService.Services;

namespace PartsService.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PartsController : ControllerBase
{
    private readonly PartService _service;

    public PartsController(PartService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<ActionResult<List<Part>>> Get() =>
        await _service.GetAsync();

    [HttpPost]
    public async Task<ActionResult<Part>> Create(Part part) =>
        await _service.CreateAsync(part);

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(string id, Part part)
    {
        await _service.UpdateAsync(id, part);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id)
    {
        await _service.DeleteAsync(id);
        return NoContent();
    }
}