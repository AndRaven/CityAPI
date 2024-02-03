

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.VisualBasic;

[ApiController]
[Authorize]
[Route("api/files")]
public class FilesController : ControllerBase
{

 private readonly FileExtensionContentTypeProvider _fileExtCntProvider;

 public FilesController(FileExtensionContentTypeProvider fileExtCntProvider)
 {
    _fileExtCntProvider = fileExtCntProvider ?? throw new System.ArgumentNullException(nameof(fileExtCntProvider));
 }
 
 [HttpGet("{fileId}")]
  public ActionResult GetFile(string fileId)
  {

    var filePath = "/Users/elzacirciu/Documents/Development/net_workspace/visual_studio_mac/CityInfoAPI/CityInfoAPI/asp_payment_summary_30k.pdf";

    if (!System.IO.File.Exists(filePath))
    {
        return NotFound();
    }
    
    var bytes = System.IO.File.ReadAllBytes(filePath);

    if (!_fileExtCntProvider.TryGetContentType(filePath, out var contentType))
    {
       contentType = "application/octet-stream";
    }


     return File(bytes, contentType, Path.GetFileName(filePath));
  }
}