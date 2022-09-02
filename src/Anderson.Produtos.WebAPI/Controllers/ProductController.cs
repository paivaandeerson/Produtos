using Anderson.Produtos.Domain;
using Anderson.Produtos.Domain.Application.ViewModels;
using Anderson.Produtos.Domain.Util;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Anderson.Produtos.WebAPI.Controllers
{
    [Route("api/produtos")]
    [ApiController]
    public class ProductController : Controller
    {
        private readonly IValidationResult _validationResult;
        private readonly IProductAppService _productCrudAppService;

        public ProductController(
            IValidationResult validationResult, 
            IProductAppService productCrudAppService)
        {
            _productCrudAppService = productCrudAppService;
            _validationResult = validationResult;
        }

        /// <summary>
        /// GetAll
        /// </summary>
        /// <returns></returns>
        [HttpGet("")]
        [SwaggerOperation(OperationId = "GetAll")]
        [ProducesResponseType(typeof(IEnumerable<ProductViewModel>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<ProductViewModel>>> Get() => Ok(await _productCrudAppService.GetAllAsync());

        
        [HttpGet("{id}")]
        [SwaggerOperation(OperationId = "GetId")]
        [ProducesResponseType(typeof(ProductViewModel), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<ProductViewModel>>> GetById(long id)
        {
            var vm = await _productCrudAppService.GetByIdAsync(id);

            if (_validationResult.Errors.Any())
                return BadRequest(_validationResult.Errors.ToResponseModel());

            return Ok(vm);
        }

        
        [HttpPost("")]
        [SwaggerOperation(OperationId = "InsertProduct")]
        [ProducesResponseType(typeof(int), (int)HttpStatusCode.Created)]
        //public async Task<IActionResult> Post([FromForm] ProductViewModel viewModel) <-workaround to SPA
        public async Task<IActionResult> Post([FromBody] ProductViewModel viewModel)
        {
            if (viewModel is null)
                return BadRequest();

            await _productCrudAppService.CreateAsync(viewModel);

            if (_validationResult.Errors.Any())
                return BadRequest(_validationResult.Errors.ToResponseModel());

            return Created(string.Empty, viewModel.Id);
        }        
        
        [HttpPut("{id}")]
        [SwaggerOperation(OperationId = "AlterProduct")]
        public async Task<IActionResult> Put(long id, [FromBody] ProductViewModel viewModel)
        {
            if (viewModel is null)
                return BadRequest();

            viewModel.Id = id;
            await _productCrudAppService.UpdateAsync(viewModel);

            if (_validationResult.Errors.Any())
                return BadRequest(_validationResult.Errors.ToResponseModel());

            return NoContent();
        }
        
        [HttpDelete("{id}")]
        [SwaggerOperation(OperationId = "RemoveProduct")]
        public async Task<IActionResult> Delete(long id)
        {
            if (id is default(uint))
                return BadRequest();

            await _productCrudAppService.DeleteAsync(id);

            if (_validationResult.Errors.Any())
                return BadRequest(_validationResult.Errors.ToResponseModel());

            return NoContent();
        }
    }
}