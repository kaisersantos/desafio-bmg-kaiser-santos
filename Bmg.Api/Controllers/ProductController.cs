using Microsoft.AspNetCore.Mvc;
using Bmg.Application.Services.Products;
using Bmg.Application.Services.Products.Models;
using Bmg.Api.Attributes;
using Bmg.Domain;

namespace Bmg.Api.Controllers;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
[AuthorizeRole(UserRole.Admin)]
public class ProductController(IProductService productService) : ControllerBase
{
    private readonly IProductService _productService = productService ?? throw new ArgumentNullException(nameof(productService));

    [HttpPost]
    [ProducesResponseType(typeof(CreatedProductResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status403Forbidden)]
    public async Task<ActionResult<CreatedProductResponse>> Post(CreateProductRequest createProductRequest)
    {
        var product = await _productService.CreateAsync(createProductRequest);
        return CreatedAtAction(nameof(Post), product);
    }

    [HttpPatch("{id:guid}/add-stock")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status403Forbidden)]
    public async Task<ActionResult> AddStock(Guid id, AddStockProductRequest addStockProductRequest)
    {
        await _productService.AddStockAsync(id, addStockProductRequest);
        return NoContent();
    }

    [HttpPut("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status422UnprocessableEntity)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status403Forbidden)]
    public async Task<ActionResult> Put(Guid id, EditProductRequest editProductRequest)
    {
        await _productService.EditAsync(id, editProductRequest);
        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status422UnprocessableEntity)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status403Forbidden)]
    public async Task<ActionResult> Delete(Guid id)
    {
        await _productService.DeleteAsync(id);
        return NoContent();
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<GetProductResponse>> Get(Guid id)
    {
        var product = await _productService.GetByIdAsync(id);
        return Ok(product);
    }

    [HttpGet("/api/v{version:apiVersion}/products")]
    [AuthorizeRole(UserRole.Admin, UserRole.Customer)]
    [ProducesResponseType(typeof(List<GetProductResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(List<GetProductResponse>), StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status403Forbidden)]
    public async Task<ActionResult<List<GetProductResponse>>> GetAll()
    {
        var products = await _productService.GetAllAsync();
        if (!products.Any())
            return NoContent();

        return Ok(products);
    }
}

