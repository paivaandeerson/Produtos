using Anderson.Produtos.Domain;
using Anderson.Produtos.Domain.Application.ViewModels;
using Anderson.Produtos.Domain.Util;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http.Description;
using HttpPostAttribute = System.Web.Http.HttpPostAttribute;

namespace Anderson.Produtos.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProdutosController : Controller
    {
        private readonly IValidationResult _validationResult;
        private readonly IProdutosAppService _produtosCrudAppService;

        public ProdutosController(
            IValidationResult validationResult, 
            IProdutosAppService produtosCrudAppService)
        {
            _produtosCrudAppService = produtosCrudAppService;
            _validationResult = validationResult;
        }

        // GET: api/Produtos
        [HttpGet]
        [ResponseType(typeof(IEnumerable<ProdutoViewModel>))]
        public async Task<ActionResult<IEnumerable<ProdutoViewModel>>> Get() => Ok(await _produtosCrudAppService.GetAllAsync());

        // GET: api/Produtos/1        
        [HttpGet("{id}")]
        [ResponseType(typeof(ProdutoViewModel))]
        public async Task<ActionResult<IEnumerable<ProdutoViewModel>>> GetById(long id)
        {
            var vm = await _produtosCrudAppService.GetByIdAsync(id);
            return CheckValidation(vm);
        }

        // POST: api/Produtos        
        [HttpPost]
        public async Task<IActionResult> Post([FromForm] ProdutoViewModel viewModel)
        {
            if (viewModel is null)
                return BadRequest();

            await _produtosCrudAppService.CreateAsync(viewModel);
            return CheckValidation();
        }

        // PUT: api/Produtos
        
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(long id, [FromBody] ProdutoViewModel viewModel)
        {
            if (viewModel is null)
                return BadRequest();

            viewModel.Id = id;
            await _produtosCrudAppService.UpdateAsync(viewModel);
            return CheckValidation();
        }

        // DELETE: api/Produtos/id        
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            if (id is default(uint))
                return BadRequest();

            await _produtosCrudAppService.DeleteAsync(id);
            return CheckValidation();
        }

        private ActionResult CheckValidation(ProdutoViewModel vm = null)
        {
            if (_validationResult.Erros.Any())
                return BadRequest(_validationResult.Erros.ToFormattedString());

            return Ok(vm);
        }

    }
}
