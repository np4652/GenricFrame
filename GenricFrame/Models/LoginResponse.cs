using System.Collections.Generic;

namespace GenricFrame.Models
{
    public class LoginResponse
    {
        public bool IsAuthenticate { get; set; }
        public string Token { get; set; }
        public string Role { get; set; }
        public string RedirectUrl { get; set; }
        public List<ClaimDto> Claims { get; set; }
    }

    public class ClaimDto
    {
        public string Type { get; set; }
        public string Value { get; set; }
    }

    public class LoginRequest
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
