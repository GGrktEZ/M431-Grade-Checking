using Microsoft.AspNetCore.Mvc;
using Services.Service;

namespace WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DebugController : ControllerBase
{
    [HttpPost("test-hash")]
    public ActionResult<object> TestHash([FromBody] string password)
    {
     var hashed = PasswordHasher.HashPassword(password);
        var verified = PasswordHasher.VerifyPassword(password, hashed);
        
        return Ok(new
        {
        OriginalPassword = password,
         HashedPassword = hashed,
 HashLength = hashed.Length,
     VerificationResult = verified,
         Message = verified ? "Hash and verify working correctly" : "VERIFICATION FAILED!"
        });
    }

    [HttpPost("test-verify")]
    public ActionResult<object> TestVerify([FromBody] VerifyRequest request)
    {
        var result = PasswordHasher.VerifyPassword(request.Password, request.Hash);
        
        return Ok(new
        {
  Password = request.Password,
            Hash = request.Hash,
            Result = result,
        Message = result ? "Password matches hash" : "Password does NOT match hash"
        });
    }

    public class VerifyRequest
    {
        public string Password { get; set; } = "";
        public string Hash { get; set; } = "";
    }
}
