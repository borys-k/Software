using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Software.Application.Services;
using Software.Domain.Entities;

namespace Software.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DocumentsController : ControllerBase
    {
        private readonly IDocumentService _documentService;
        private readonly long _fileSizeLimit;

        public DocumentsController(IDocumentService documentService, IConfiguration config)
        {
            _documentService = documentService;
            _fileSizeLimit = config.GetValue<long>("FileSizeLimit");
        }

        // GET: api/Documents
        [HttpGet]
        public ActionResult<IEnumerable<DocumentDto>> GetDocuments()
        {
            return Ok(_documentService.GetDocuments());
        }

        // GET: api/Documents/5
        [HttpGet("{id}")]
        public ActionResult GetDocument(int id)
        {
            var file = _documentService.GetById(id);

            if (file == null)
            {
                return NotFound("File was not found");
            }

            return File(file.Content, "application/pdf", file.Name);
        }

        // POST: api/Documents
        [HttpPost]
        public async Task<IActionResult> PostDocument(IFormFile file)
        {
            if (!_documentService.FileWithinLimit(file.Length))
            {
                return BadRequest("File is to large");
            }

            if (!_documentService.CheckExtension(Path.GetExtension(file.FileName).Substring(1).ToLower()))
            {
                return BadRequest("Wrong file extension");
            }

            var documentId = await _documentService.Create(file, CancellationToken.None);

            return Created(nameof(GetDocument), new { documentId });
        }

        // DELETE: api/Documents/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Document>> DeleteDocument(int id)
        {
            var file = _documentService.GetById(id);

            if (file == null)
            {
                return NotFound("File was not found");
            }

            await _documentService.Delete(file, CancellationToken.None);

            return NoContent();
        }
    }
}
