using GMGInventory.Common.Constants;
using System.ComponentModel.DataAnnotations.Schema;

namespace GMGInventory.Common.Models
{
    public class APIHMSettings
    {
        public string Host { get; set; }
        public string User { get; set; }
        public string Password { get; set; }
    }

    public class APIHM_LoginResponse
    {
        public int code { get; set; }
        public string message { get; set; }
        public APIHM_Token token { get; set; }
    }

    public class APIHM_Token
    {
        public string oauth_token { get; set; }
        public string oauth_token_secret { get; set; }
        public string consumer_key { get; set; }
        public string consumer_secret { get; set; }
    }

    public class APIHM_VerifierResponse
    {
        public string oauth_verifier { get; set; }
        public string oauth_token { get; set; }
    }

    public class APIHM_AccessTokenResponse
    {
        public string oauth_token { get; set; }
        public string oauth_token_secret { get; set; }
    }

    // Success model
    public class APIHM_SuccessResponse
    {
        public int code { get; set; }
        public string message { get; set; }
        public int move_id { get; set; }
        public string move_name { get; set; }
    }

    // Error model
    public class APIHM_ErrorResponse
    {
        public string jsonrpc { get; set; }
        public string? id { get; set; }
        public APIHM_ErrorDetail error { get; set; }
    }

    public class APIHM_ErrorDetail
    {
        public int code { get; set; }
        public string message { get; set; }
        public APIHM_ErrorData data { get; set; }
    }

    public class APIHM_ErrorData
    {
        public string name { get; set; }
        public string debug { get; set; }
        public string message { get; set; }
        public List<string> arguments { get; set; }
        public Dictionary<string, object> context { get; set; }
    }

    public class APIHM_VM
    {
        public int? HashMicroMoveId { get; set; }
        public string? HashMicroMoveName { get; set; }
        public string? HashMicroMoveStatus { get; set; }
        public string? HashMicroMoveBy { get; set; }
        public string? HashMicroMoveDate => HashMicroMoveDateD == null ? null : HashMicroMoveDateD.Value.ToString(SystemFormat.StringDateTime);
        public DateTime? HashMicroMoveDateD { get; set; }
    }
}